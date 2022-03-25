using Sandbox;
using SWB_Base;
using System;
using System.Collections.Generic;

// Borrowed from https://github.com/timmybo5/simple-weapon-base/blob/master/code/swb_base/PlayerBase.Ammo.cs
namespace ProjectPollux
{
	partial class PolluxPlayer
	{
		[Net]
		public new List<int> Ammo { get; set; }

		public virtual void ClearAmmo()
		{
			Ammo.Clear();
		}

		public virtual int AmmoCount( AmmoType type )
		{
			var iType = (int)type;
			if ( Ammo == null ) return 0;
			if ( Ammo.Count <= iType ) return 0;

			return Ammo[(int)type];
		}

		public virtual bool SetAmmo( AmmoType type, int amount )
		{
			var iType = (int)type;
			if ( !Host.IsServer ) return false;
			if ( Ammo == null ) return false;

			while ( Ammo.Count <= iType )
			{
				Ammo.Add( 0 );
			}

			Ammo[(int)type] = amount;
			return true;
		}

		public virtual bool GiveAmmo( AmmoType type, int amount )
		{
			if ( !Host.IsServer ) return false;
			if ( Ammo == null ) return false;

			SetAmmo( type, AmmoCount( type ) + amount );
			return true;
		}

		public virtual int TakeAmmo( AmmoType type, int amount )
		{
			if ( Ammo == null ) return 0;

			var available = AmmoCount( type );
			amount = Math.Min( available, amount );

			SetAmmo( type, available - amount );

			return amount;
		}

		public virtual bool HasAmmo( AmmoType type )
		{
			return AmmoCount( type ) > 0;
		}
	}
}
