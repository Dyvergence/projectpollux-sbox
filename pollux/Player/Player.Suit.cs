using Sandbox;

namespace ProjectPollux
{
	partial class PolluxPlayer
	{
		[Net] public float ArmorValue { get; set; }

		public void EquipSuit()
		{
			Tags.Add( "suitequipped" );
		}

		public bool IsSuitEquipped => Tags.Has( PlayerTags.SuitEquipped );

		public void IncrementArmorValue( int nCount, int nMaxValue )
		{
			ArmorValue += nCount;
			if ( nMaxValue > 0 )
			{
				if ( ArmorValue > nMaxValue )
					ArmorValue = nMaxValue;
			}
		}

		public void IncrementHealthValue( int Count, int MaxValue )
		{
			float NewHealth = Health + Count;

			if (NewHealth >= MaxValue )
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
				PunchView( -punchAngle, 0, 0 );
			}

			LastAttacker = info.Attacker;
			LastAttackerWeapon = info.Weapon;

			if ( IsServer && Health > 0f && LifeState == LifeState.Alive )
			{
				if ( ArmorValue > 0 )
				{
					float flNew = (info.Damage * 0.2f);

					float flArmor = ((info.Damage - flNew) * 1.0f);

					if ( (flArmor < 1.0) ) { flArmor = 1.0f; }

					if ( flArmor > ArmorValue )
					{
						flArmor = ArmorValue;
						flArmor *= (1 / 1.0f);
						flNew = info.Damage - flArmor;
						ArmorValue = 0;
					}
					else
					{
						ArmorValue -= flArmor;
					}

					info.Damage = flNew;
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
	}
}
