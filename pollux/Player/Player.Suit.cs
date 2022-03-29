using Sandbox;

namespace ProjectPollux;

partial class PolluxPlayer
{
	[Net] public float ArmorValue { get; set; }
	[Net] public float AuxPower { get; set; }

	[Net] public float m_flTimeAllSuitDevicesOff { get; set; }

	[Net] public float AuxPowerLoad { get; set; }

	public void EquipSuit()
	{
		Tags.Add( "suitequipped" );
	}

	public bool IsSuitEquipped => Tags.Has( PlayerTags.SuitEquipped );

	public void IncrementArmorValue( int Count, int MaxValue )
	{
		ArmorValue += Count;
		if ( MaxValue > 0 )
		{
			if ( ArmorValue > MaxValue )
				ArmorValue = MaxValue;
		}
	}

	public void IncrementHealthValue( int Count, int MaxValue )
	{
		float NewHealth = Health + Count;

		if ( NewHealth >= MaxValue )
		{
			Health = MaxValue;
		}
		else
			Health = NewHealth;
	}

	public bool ApplyBattery( int Count, int MaxValue )
	{
		if ( IsSuitEquipped )
		{
			if ( ArmorValue >= MaxValue )
			{
				return false;
			}
			else
			{
				float NewArmor = ArmorValue + Count;

				if ( NewArmor >= MaxValue )
				{
					ArmorValue = MaxValue;
					return true;
				}
				else
				{
					ArmorValue = NewArmor;
					return true;
				}
			}
		}
		else
			return false;
	}

	public bool ApplyMedkit( int Count, int MaxValue )
	{
		if ( Health >= MaxValue )
		{
			return false;
		}
		else
		{
			float NewHealth = Health + Count;

			if ( NewHealth >= MaxValue )
			{
				Health = MaxValue;
				return true;
			}
			else
			{
				Health = NewHealth;
				return true;
			}
		}
	}

	public override void TakeDamage( DamageInfo info )
	{
		TimeSinceTakeDamage = 0;

		LastDamageInfo = info;

		if ( LifeState == LifeState.Alive )
		{
			var maxPunch = 5;
			var maxDamage = 100;
			var punchAngle = info.Damage.Remap( 0, maxDamage, 0, maxPunch );
			PunchViewOffset( -punchAngle, 0, 0 );
		}

		LastAttacker = info.Attacker;
		LastAttackerWeapon = info.Weapon;

		if ( IsServer && Health > 0f && LifeState == LifeState.Alive )
		{
			if ( ArmorValue > 0 )
			{
				float New = (info.Damage * 0.2f);

				float Armor = ((info.Damage - New) * 1.0f);

				if ( (Armor < 1.0) ) { Armor = 1.0f; }

				if ( Armor > ArmorValue )
				{
					Armor = ArmorValue;
					Armor *= (1 / 1.0f);
					New = info.Damage - Armor;
					ArmorValue = 0;
				}
				else
				{
					ArmorValue -= Armor;
				}

				info.Damage = New;
			}
			Health -= info.Damage;
			if ( Health <= 0f )
			{
				Health = 0f;
				OnKilled();
			}
		}

		Source1.GameRules.Current.PlayerHurt( this, info );
	}

	public void SuitPower_Charge( float Power )
	{
		AuxPower += Power;

		if ( AuxPower > 100.0 )
		{
			AuxPower = 100.0f;
		}
	}

	public bool SuitPower_Drain( float Power )
	{
		AuxPower -= Power;

		if ( AuxPower < 0.0 )
		{
			AuxPower = 0.0f;
			return false;
		}

		return true;
	}

	public const float SUITPOWER_BEGIN_RECHARGE_DELAY = 0.5f;

	public bool SuitPower_ShouldRecharge()
	{
		if ( AuxPower is 100.0f )
			return false;

		if ( Time.Now < m_flTimeAllSuitDevicesOff + SUITPOWER_BEGIN_RECHARGE_DELAY )
			return false;

		return true;
	}

	// AUX Power handling
	[Event.Tick]
	public void Tick()
	{
		if ( IsSuitEquipped )
		{
			if ( Tags.Has( PlayerTags.Sprinted ) && (!Tags.Has(PlayerTags.FlashlightOn)) ) // Sprint is active but flashlight is off
			{
				// TODO: Add sprint tag to AUX HUD
				if ( Velocity.x is not 0 && Velocity.y is not 0 ) // Player is requesting sprint and actively on the run
				{
					SuitPower_Drain( AuxPowerLoad * Time.Delta );
				}
			}
			else if ( (Tags.Has( PlayerTags.FlashlightOn ) && (!Tags.Has(PlayerTags.Sprinted))) || (Tags.Has( PlayerTags.Sprinted ) && Tags.Has( PlayerTags.FlashlightOn ))) // flashlight is on, player is or is not sprinting
			{
				SuitPower_Drain( AuxPowerLoad * Time.Delta );
			}
			else // Player is not sprinting and flashlight is off
			{
				// recharge aux power
				if ( SuitPower_ShouldRecharge() )
				{
					SuitPower_Charge( 12.5f * Time.Delta );
				}
			}
		}
	}
}
