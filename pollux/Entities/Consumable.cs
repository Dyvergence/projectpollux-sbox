using Sandbox;
using System.ComponentModel;

namespace ProjectPollux.Entities.Items.Consumables
{
	[Library( "ent_consumable", Description = "An item that is consumed when a player touches it." )]
	[Hammer.Model]
	[Hammer.VisGroup( Hammer.VisGroup.Dynamic )]
	partial class Consumable : BasePhysics
	{
		/// <summary>
		/// Fires when the player consumes this object
		/// </summary>
		protected Output OnPlayerTouch { get; set; }
		public PickupTrigger PickupTrigger { get; protected set; }

		public enum ConsumableType
		{
			Ammo,
			Armor,
			Health
		}

		/// <summary>
		/// What this item will give the player upon successful consumption.
		/// </summary>
		[Property( "consumabletype", Title = "Type of consumable" )]
		public virtual ConsumableType TypeConsumable { get; set; } = ConsumableType.Health;

		/// <summary>
		/// Name of the sound to play when this item is consumed.
		/// </summary>
		[Property( "consumesound", Title = "Sound To Play When Consumed" ), FGDType( "sound" ), Category( "Sound Settings" )]
		public virtual string OnConsumeSound { get; set; } = "ConsumableSounds.EquipHealthkit";

		/// <summary>
		/// The amount of armor this item will replenish when consumed.
		/// </summary>
		[Property( "armortoreplenish", Title = "Amount to replenish" ), Category( "Armor Settings" )]
		public virtual int ArmorToReplenish { get; set; } = 15;

		/// <summary>
		/// The amount of health this item will replenish when consumed.
		/// </summary>
		[Property( "healthtoreplenish", Title = "Amount to replenish" ), Category( "Health Settings" )]
		public virtual int HealthToReplenish { get; set; } = 25;

		/// <summary>
		/// The amount of ammo this item will replenish when consumed.
		/// </summary>
		[Property( "ammotoreplenish", Title = "Amount to replenish" ), Category( "Ammo Settings" )]
		public virtual int AmmoToReplenish { get; set; } = 20;

		/// <summary>
		/// The type of ammo this item will replenish when consumed.
		/// </summary>
		[Property( "typeofammo", Title = "Type Of Ammo" ), Category( "Ammo Settings" )]
		public virtual SWB_Base.AmmoType TypeOfAmmo { get; set; } = SWB_Base.AmmoType.Pistol;

		public override void Spawn()
		{
			base.Spawn();

			PickupTrigger = new PickupTrigger
			{
				Parent = this,
				Position = Position,
				EnableTouch = true,
				EnableSelfCollisions = false
			};

			PickupTrigger.PhysicsBody.AutoSleep = false;
		}

		public override void StartTouch( Entity activator ) // This function is called when something touches the item.
		{
			if ( IsClient ) return;
			if ( activator.IsWorld ) return;
			if ( Parent != null ) return;

			if ( activator is PolluxPlayer player )
			{
				if ( TryConsume( player ) )
				{
					PlaySound( OnConsumeSound );
					OnPlayerTouch.Fire( player );
					Delete();
				}
			}
		}

		public bool TryConsume( PolluxPlayer player )
		{
			if ( TypeConsumable is ConsumableType.Armor )
			{
				if ( player.ApplyBattery( ArmorToReplenish, 100 ) )
					return true;
				else
					return false;
			}
			else if (TypeConsumable is ConsumableType.Health )
			{
				if ( player.ApplyMedkit( HealthToReplenish, 100 ) )
					return true;
				else
					return false;
			}
			else if ( TypeConsumable is ConsumableType.Ammo )
			{
				if ( player.GiveAmmo( TypeOfAmmo, AmmoToReplenish ) )
					return true;
				else
					return false;
			}

			return false;
		}
	}

	[Library( "item_healthkit", Description = "Health Kit" )]
	[Hammer.Model( Model = "models/items/healthkit.vmdl" )]
	[Hammer.EntityTool( "Health Kit", "Consumables", "An item that provides health when stepped on." )]
	[Hammer.VisGroup( Hammer.VisGroup.Physics )]
	[Hammer.SkipProperty( "armortoreplenish" )]
	[Hammer.SkipProperty( "ammotoreplenish" )]
	[Hammer.SkipProperty( "typeofammo" )]
	partial class ItemMedkit : Consumable
	{
		public override ConsumableType TypeConsumable => ConsumableType.Health;
		public override int HealthToReplenish => 25;
		public override string OnConsumeSound => "ConsumableSounds.EquipHealthkit";
		public override void Spawn()
		{
			base.Spawn();
		}
	}

	[Library( "item_healthvial", Description = "Health Kit" )]
	[Hammer.Model( Model = "models/healthvial.vmdl" )]
	[Hammer.EntityTool( "Health Vial", "Consumables", "An item that provides health when stepped on." )]
	[Hammer.VisGroup( Hammer.VisGroup.Physics )]
	[Hammer.SkipProperty( "armortoreplenish" )]
	[Hammer.SkipProperty( "ammotoreplenish" )]
	[Hammer.SkipProperty( "typeofammo" )]
	partial class ItemHealthVial : Consumable
	{
		public override ConsumableType TypeConsumable => ConsumableType.Health;
		public override int HealthToReplenish => 10;
		public override string OnConsumeSound => "ConsumableSounds.EquipHealthkit";
		public override void Spawn()
		{
			base.Spawn();
		}
	}

	[Library( "item_battery", Description = "HEV battery" )]
	[Hammer.Model( Model = "models/items/battery.vmdl" )]
	[Hammer.EntityTool( "HEV Battery", "Consumables", "An item that recharges the HEV suit when stepped on." )]
	[Hammer.VisGroup( Hammer.VisGroup.Physics )]
	[Hammer.SkipProperty( "healthtoreplenish" )]
	[Hammer.SkipProperty( "ammotoreplenish" )]
	[Hammer.SkipProperty( "typeofammo" )]
	partial class ItemBattery : Consumable
	{
		public override ConsumableType TypeConsumable => ConsumableType.Armor;
		public override string OnConsumeSound => "ConsumableSounds.EquipBattery";
		public override void Spawn()
		{
			base.Spawn();
		}
	}
}
