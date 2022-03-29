// loosely based on the code from here for CNewRecharge: https://github.com/ValveSoftware/source-sdk-2013/blob/master/mp/src/game/server/hl2/func_recharge.cpp
using Sandbox;
using System.ComponentModel;

namespace ProjectPollux.Entities
{
	[Library( "ent_charger", Description = "An entity that gradually replenishes a player's armor and/or health when used." )]
	[Hammer.EntityTool( "Player Charging Station", "Player", "An entity that gradually replenishes a player's armor and/or health when used." )]
	[Hammer.Model]
	[Hammer.VisGroup( Hammer.VisGroup.Dynamic )]
	partial class ItemPlayerCharger : AnimEntity, IUse
	{
		/// <summary>
		/// Remaining Charge.
		/// </summary>
		protected Output<float> OutRemainingCharge { get; set; }

		/// <summary>
		/// Half-Empty
		/// </summary>
		protected Output OnHalfEmpty { get; set; }

		/// <summary>
		/// Empty
		/// </summary>
		protected Output OnEmpty { get; set; }

		/// <summary>
		/// Fired when this charger is recharged to full.
		/// </summary>
		protected Output OnFull { get; set; }

		/// <summary>
		/// Fired when the player +IV_USEs the charger.
		/// </summary>
		protected Output OnPlayerUse { get; set; }

		public override void Spawn()
		{
			base.Spawn();

			MoveType = MoveType.None;
			CollisionGroup = CollisionGroup.Always;
			EnableSolidCollisions = true;

			SetupPhysicsFromModel( PhysicsMotionType.Static );

			UpdateAnim();
		}

		// Charger settings

		// sounds

		/// <summary>
		/// Name of the sound to play when this charger begins outputting juice.
		/// </summary>
		[Property( "startchargeSound", Title = "Start Charge Sound" ), FGDType( "sound" ), Category( "Sound Settings" )]
		public string StartChargeSound { get; set; } = "HealthChargerSounds.Start";

		/// <summary>
		/// Name of the sound to play when this charger is charging the player's health/armor.
		/// </summary>
		[Property( "chargeloopSound", Title = "Charge In Progress Sound" ), FGDType( "sound" ), Category( "Sound Settings" )]
		public string ChargeLoopSound { get; set; } = "HealthChargerSounds.Loop";

		/// <summary>
		/// Name of the sound to play when this charger denies the player a recharge for any reason.
		/// </summary>
		[Property( "chargeDenySound", Title = "Charge Denied Sound" ), FGDType( "sound" ), Category( "Sound Settings" )]
		public string ChargeDenySound { get; set; } = "HealthChargerSounds.Deny";

		/// <summary>
		/// Name of the sound to play if this charger's juice gets refilled for any reason.
		/// </summary>
		[Property( "chargerrefilledSound", Title = "Juice Refilled Sound" ), FGDType( "sound" ), Category( "Sound Settings" )]
		public string ChargerRefilledSound { get; set; } = "HealthChargerSounds.Recharge";

		// what to replenish

		/// <summary>
		/// Determines whether this charger replenishes health on use.
		/// </summary>
		[Property( "replenishhealth", Title = "Replenish Health?" ), Category( "Charger Settings" )]
		public bool ReplenishesHealth { get; set; } = true;

		/// <summary>
		/// Determines whether this charger replenishes armor on use.
		/// </summary>
		[Property( "replenisharmor", Title = "Replenish Armor?" ), Category( "Charger Settings" )]
		public bool ReplenishesArmor { get; set; } = false;

		// output options

		/// <summary>
		/// The amount of juice (how much health/armor can be replenished) this charger has.
		/// </summary>
		[Property( "juice", Title = "Juice" ), Category( "Charger Settings" )]
		public int Juice { get; set; } = 50;

		/// <summary>
		/// The maximum amount of juice this charger can have.
		/// </summary>
		[Property( "maxjuice", Title = "Max Juice" ), Category( "Charger Settings" )]
		public int MaxJuice { get; set; } = 50;

		// Armor settings

		/// <summary>
		/// The maximum amount of armor the player can have before being denied.
		/// </summary>
		[Property( "maxplayerjuice", Title = "Max Player Armor" ), Category( "Armor Settings" )]
		public int MaxPlayerArmor { get; set; } = 100;

		/// <summary>
		/// Amount to gradually replenish armor by.
		/// </summary>
		[Property( "amountToIncrementArmorBy", Title = "Increment Armor By" ), Category( "Armor Settings" )]
		public int AmountToIncrementArmorBy { get; set; } = 1;

		// Health charger settings

		/// <summary>
		/// The maximum amount of health the player can have before being denied.
		/// </summary>
		[Property( "maxplayerhealth", Title = "Max Player Health" ), Category( "Health Settings" )]
		public int MaxPlayerHealth { get; set; } = 100;

		/// <summary>
		/// Amount to gradually replenish health by.
		/// </summary>
		[Property( "amountToIncrementHealthBy", Title = "Increment Health By" ), Category( "Health Settings" )]
		public int AmountToIncrementHealthBy { get; set; } = 1;

		/// <summary>
		/// Recharge to full
		/// </summary>
		[Input( "Recharge" )]
		public void Recharge()
		{
			if ( IsClient )
				return;

			PlaySound( ChargerRefilledSound );

			UpdateJuice( MaxJuice );
		}

		/// <summary>
		/// This sets the remaining charge in the charger to whatever value you specify
		/// </summary>
		[Input]
		public void SetCharge( int inputdata )
		{
			if ( IsClient )
				return;

			UpdateJuice( inputdata );
		}

		public void UpdateJuice( int newJuice )
		{
			bool reduced = newJuice < Juice;
			if ( reduced )
			{
				// Fire 1/2 way output and/or empty output
				int oneHalfJuice = (int)(MaxJuice * 0.5f);
				if ( newJuice <= oneHalfJuice && Juice > oneHalfJuice )
				{
					OnHalfEmpty.Fire( this );
				}

				if ( newJuice <= 0 )
				{
					OnEmpty.Fire( this );
				}
			}
			else if ( newJuice != Juice &&
				newJuice == MaxJuice )
			{
				UpdateAnim();
				OnFull.Fire( this );
			}
			Juice = newJuice;
		}

		private float WdTimer { get; set; }

		private Sound ChargingLoopSound;

		private float NextCharge { get; set; }
		private int On { get; set; } // 0 = off, 1 = startup, 2 = going
		private float SoundTime { get; set; }

		public void Off()
		{
			// Stop looping sound. 
			if ( On > 1 )
			{
				ChargingLoopSound.Stop();
			}

			On = 0;
		}

		private float Progress = 0.0f;

		public void UpdateAnim()
		{
			Progress = 1.0f - (float)Juice / (float)MaxJuice;
			SetAnimParameter( "progress", Progress );
		}

		public bool OnUse( Entity user )
		{
			if ( user is not PolluxPlayer Player )
				return false;

			if ( IsClient )
				return false;

			// Only usable if you have the HEV suit on
			if ( !Player.IsSuitEquipped && ReplenishesArmor )
			{
				if ( SoundTime <= Time.Now )
				{
					SoundTime = Time.Now + 0.62f;
					PlaySound( ChargeDenySound );
				}
				return false;
			}

			// if there is no juice left, turn it off
			if ( Juice <= 0 )
			{
				// Shut off
				Off();

				// Play a deny sound
				if ( SoundTime <= Time.Now )
				{
					SoundTime = Time.Now + 0.62f;
					PlaySound( ChargeDenySound );
				}

				return false;
			}

			if ( (ReplenishesArmor && ReplenishesHealth) && (Player.ArmorValue >= MaxPlayerArmor && Player.Health >= MaxPlayerHealth) ) // if this charger is a dual recharger and both of player's values are above or equal to max, deny charging
			{
				if ( SoundTime <= Time.Now )
				{
					SoundTime = Time.Now + 0.62f;
					PlaySound( ChargeDenySound );
				}
				return false;
			}
			else if ( (ReplenishesArmor && !ReplenishesHealth) && (Player.ArmorValue >= MaxPlayerArmor) ) // this charger replenishes only armor & player's armor is above or equal to max, so deny charging
			{
				if ( SoundTime <= Time.Now )
				{
					SoundTime = Time.Now + 0.62f;
					PlaySound( ChargeDenySound );
				}
				return false;
			}
			else if ( (ReplenishesHealth && !ReplenishesArmor) && (Player.Health >= MaxPlayerHealth) ) // this charger replenishes only health & player's health is above or equal to max, so deny charging
			{
				if ( SoundTime <= Time.Now )
				{
					SoundTime = Time.Now + 0.62f;
					PlaySound( ChargeDenySound );
				}
				return false;
			}

			if ( NextCharge >= Time.Now )
				return true;

			if ( On is 0 )
			{
				On++;
				PlaySound( StartChargeSound );
				SoundTime = 0.56f + Time.Now;

				OnPlayerUse.Fire( Player );
			}

			if ( On is 1 && WdTimer < Time.Now + 0.2f )
			{
				On++;
				ChargingLoopSound = PlaySound( ChargeLoopSound );
			}

			if ( ReplenishesArmor && ReplenishesHealth ) // this charger replenishes both, armor and health
			{
				if ( Player.ArmorValue < MaxPlayerArmor && Player.Health < MaxPlayerHealth ) // Player's health and armor are below max values
				{
					UpdateJuice( Juice - AmountToIncrementArmorBy + AmountToIncrementHealthBy );
					Player.IncrementArmorValue( AmountToIncrementArmorBy, MaxPlayerArmor );
					Player.IncrementHealthValue( AmountToIncrementHealthBy, MaxPlayerHealth );
					UpdateAnim();
				}
				else if ( Player.ArmorValue < MaxPlayerArmor && Player.Health >= MaxPlayerHealth ) // player's armor value is below max, but health is either at or above max
				{
					UpdateJuice( Juice - AmountToIncrementArmorBy );
					Player.IncrementArmorValue( AmountToIncrementArmorBy, MaxPlayerArmor );
					UpdateAnim();
				}
				else if ( Player.Health < MaxPlayerHealth && Player.Health >= MaxPlayerArmor ) // player's health value is below max, but armor is either at or above max
				{
					UpdateJuice( Juice - AmountToIncrementHealthBy );
					Player.IncrementHealthValue( AmountToIncrementHealthBy, MaxPlayerHealth );
					UpdateAnim();
				}
			}
			else if ( ReplenishesArmor && !ReplenishesHealth ) // this charger replenishes only armor
			{
				if ( Player.ArmorValue < MaxPlayerArmor )
				{
					UpdateJuice( Juice - AmountToIncrementArmorBy );
					Player.IncrementArmorValue( AmountToIncrementArmorBy, MaxPlayerArmor );
					UpdateAnim();
				}
			}
			else if ( ReplenishesHealth && !ReplenishesArmor ) // this charger replenishes only health
			{
				if ( Player.Health < MaxPlayerHealth )
				{
					UpdateJuice( Juice - AmountToIncrementHealthBy );
					Player.IncrementHealthValue( AmountToIncrementHealthBy, MaxPlayerHealth );
					UpdateAnim();
				}
			}

			// Send the output.
			float flRemaining = Juice / MaxJuice;
			OutRemainingCharge.Fire( Player, flRemaining );
			WdTimer = Time.Now + 0.4f;

			// govern the rate of charge
			NextCharge = Time.Now + 0.1f;

			return true;
		}

		// This is to ensure the player is still using the charger, if not, then we've gotta set On to 0 and stop the charging loop sound.
		[Event.Tick.Server]
		protected virtual void Think()
		{
			if ( On is 2 )
			{
				if ( WdTimer < Time.Now + 0.2f )
				{
					On = 0;
					ChargingLoopSound.Stop();
				}
			}
		}

		public bool IsUsable( Entity user ) { return true; }
	}

	[Library( "item_healthcharger", Description = "A preconfigured ent_charger with appropriate settings for a health charger." )]
	[Hammer.Model( Model = "models/props_combine/health_charger001.vmdl" )]
	[Hammer.EntityTool( "Health Station", "Items", "An entity that gradually replenishes health." )]
	[Hammer.VisGroup( Hammer.VisGroup.Dynamic )]
	[Hammer.SkipProperty( "chargerlocation" )]
	[Hammer.SkipProperty( "maxplayerhealth" )]
	[Hammer.SkipProperty( "maxplayerjuice" )]
	[Hammer.SkipProperty( "chargertype" )]
	[Hammer.SkipProperty( "usesskill" )]
	[Hammer.SkipProperty( "juice" )]
	[Hammer.SkipProperty( "maxjuice" )]
	[Hammer.SkipProperty( "replenishhealth" )]
	[Hammer.SkipProperty( "replenisharmor" )]
	[Hammer.SkipProperty( "startchargesound" )]
	[Hammer.SkipProperty( "chargeloopsound" )]
	[Hammer.SkipProperty( "chargedenysound" )]
	[Hammer.SkipProperty( "chargerrefilledsound" )]
	[Hammer.SkipProperty( "amounttoincrementarmorby" )]
	[Hammer.SkipProperty( "amounttoincrementhealthby" )]
	partial class ItemHealthCharger : ItemPlayerCharger
	{
		public override void Spawn()
		{
			base.Spawn();
			ReplenishesHealth = true;
			ReplenishesArmor = false;
			StartChargeSound = "HealthChargerSounds.Start";
			ChargeLoopSound = "HealthChargerSounds.Loop";
			ChargeDenySound = "HealthChargerSounds.Deny";
			ChargerRefilledSound = "HealthChargerSounds.Refill";
			AmountToIncrementHealthBy = 1;
			Juice = 50;
			MaxJuice = 50;

			SetModel( "models/props_combine/health_charger001.vmdl" );
		}
	}

	[Library( "item_suitcharger", Description = "A preconfigured ent_charger with appropriate settings for a armor charger." )]
	[Hammer.Model( Model = "models/props_combine/suit_charger001.vmdl" )]
	[Hammer.EntityTool( "Armor Charging Station", "Items", "An entity that gradually replenishes armor." )]
	[Hammer.VisGroup( Hammer.VisGroup.Dynamic )]
	[Hammer.SkipProperty( "chargerlocation" )]
	[Hammer.SkipProperty( "maxplayerhealth" )]
	[Hammer.SkipProperty( "maxplayerjuice" )]
	[Hammer.SkipProperty( "chargertype" )]
	[Hammer.SkipProperty( "usesskill" )]
	[Hammer.SkipProperty( "juice" )]
	[Hammer.SkipProperty( "maxjuice" )]
	[Hammer.SkipProperty( "replenishhealth" )]
	[Hammer.SkipProperty( "replenisharmor" )]
	[Hammer.SkipProperty( "startchargesound" )]
	[Hammer.SkipProperty( "chargeloopsound" )]
	[Hammer.SkipProperty( "chargedenysound" )]
	[Hammer.SkipProperty( "chargerrefilledsound" )]
	[Hammer.SkipProperty( "amounttoincrementarmorby" )]
	[Hammer.SkipProperty( "amounttoincrementhealthby" )]
	partial class ItemSuitCharger : ItemPlayerCharger
	{
		[Spawnflags]
		public enum Spawnflags
		{
			CitadelRecharger = 8192,
			KleinerRecharger = 16384
		}

		public new int Juice()
		{
			if ( SpawnFlags.Has( Spawnflags.CitadelRecharger ) )
				return 500;
			else if ( SpawnFlags.Has( Spawnflags.KleinerRecharger ) )
				return 25;
			else
				return 75;
		}

		public new int MaxJuice()
		{
			if ( SpawnFlags.Has( Spawnflags.CitadelRecharger ) )
				return 500;
			else
				return 100;
		}

		public new int AmountToIncrementArmorBy()
		{
			if ( SpawnFlags.Has( Spawnflags.CitadelRecharger ) )
				return 10;
			else
				return 1;
		}

		public new bool ReplenishesHealth()
		{
			if ( SpawnFlags.Has( Spawnflags.CitadelRecharger ) )
				return true;
			else
				return false;
		}

		public new int MaxPlayerArmor()
		{
			if ( SpawnFlags.Has( Spawnflags.CitadelRecharger ) )
				return 200;
			else
				return 100;
		}
		public override void Spawn()
		{
			base.Spawn();
			base.Juice = Juice();
			base.MaxJuice = MaxJuice();
			base.ReplenishesHealth = ReplenishesHealth();
			base.AmountToIncrementArmorBy = AmountToIncrementArmorBy();
			base.MaxPlayerArmor = MaxPlayerArmor();
			ReplenishesArmor = true;
			AmountToIncrementHealthBy = 5; // only increments if citadel recharger
			StartChargeSound = "ArmorChargerSounds.Start";
			ChargeLoopSound = "ArmorChargerSounds.Loop";
			ChargeDenySound = "ArmorChargerSounds.Deny";
			ChargerRefilledSound = "ArmorChargerSounds.Refill";
		}
	}

	[Library( "dbg_ent_deathcharger", Description = "An entity that gradually deals damage to its user. For debugging purposes." )]
	[Hammer.EntityTool( "Damage Station", "Debug", "An entity that gradually deals damage to its user. For debugging purposes." )]
	[Hammer.Solid]
	[Hammer.VisGroup( Hammer.VisGroup.Dynamic )]
	partial class DeathCharger : BrushEntity, IUse
	{
		public override void Spawn()
		{
			base.Spawn();
		}

		private float NextCharge { get; set; }

		public bool OnUse( Entity user )
		{
			if ( user is not PolluxPlayer Player )
				return false;

			if ( IsClient )
				return false;

			// Time to recharge yet?
			if ( NextCharge >= Time.Now )
				return true;

			var dmg = DamageInfo.Generic( 10 )
			.WithAttacker( this )
			.WithPosition( Position );

			Player.TakeDamage( dmg );

			// govern the rate of charge
			NextCharge = Time.Now + 0.8f;

			return true;
		}

		public bool IsUsable( Entity user ) { return true; }
	}
}
