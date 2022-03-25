// based on: https://github.com/timmybo5/simple-weapon-base/blob/master/code/ExampleInventory.cs
using System;
using System.Linq;
using Sandbox;
using SWB_Base;

namespace ProjectPollux.Player.Inventory
{
	partial class PolluxInventoryBase : InventoryBase
	{
		public PolluxInventoryBase( PolluxPlayer player ) : base( player )
		{
		}

		public override Entity DropActive()
		{
			if ( !Host.IsServer || Owner is not PolluxPlayer player ) return null;

			var ent = player.ActiveChild;
			if ( ent == null ) return null;

			if ( ent is WeaponBase weapon && weapon.CanDrop && Drop( ent ) )
			{
				player.ActiveChild = null;
				return ent;
			}

			return null;
		}

		public override bool Add( Entity ent, bool makeActive = false )
		{
			var player = Owner as PolluxPlayer;
			var weapon = ent as WeaponBase;

			if ( weapon != null && IsCarryingType( ent.GetType() ) )
			{
				if ( weapon.TimeSinceActiveStart == 0 ) return false;

				var ammo = weapon.Primary.Ammo;
				var ammoType = weapon.Primary.AmmoType;

				if ( ammo > 0 )
				{
					player.GiveAmmo( ammoType, ammo );

					Sound.FromWorld( "ConsumableSounds.EquipWeapon", ent.Position );
					// PickupFeed.OnPickup( To.Single( player ), $"+{ammo} {ammoType}" );
				}
				weapon.Delete();
				return false;
			}

			if ( weapon != null )
			{
				Sound.FromWorld( "ConsumableSounds.EquipWeapon", ent.Position );
			}

			return base.Add( ent, makeActive );
		}

		public bool IsCarryingType( Type t )
		{
			return List.Any( x => x.GetType() == t );
		}
	}
}
