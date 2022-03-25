using ProjectPollux.Entities.Weapons;
using ProjectPollux.Player.Inventory;
using Sandbox;
using Source1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPollux
{
	public partial class PolluxPlayer : Source1Player
	{
		public static new PolluxPlayer LocalPlayer => Local.Pawn as PolluxPlayer;

		public PolluxPlayer()
		{
			Inventory = new PolluxInventoryBase( this );
		}

		public override void Spawn()
		{
			base.Spawn();

			CameraMode = new PolluxCamera();
		}

		public override void Respawn()
		{
			base.Respawn();

			ArmorValue = 0;
			// MaxArmorValue = MaxArmorValue;
			//Inventory.Add( new HL2Pistol(), true );
			//Inventory.Add( new HL2Crowbar(), false );

			//GiveAmmo( SWB_Base.AmmoType.Pistol, 30 );
		}

		public static class PlayerTags
		{
			public const string Ducked = "ducked";
			public const string WaterJump = "waterjump";
			public const string Noclipped = "noclipped";
			public const string SuitEquipped = "suitequipped";
		}
	}
}
