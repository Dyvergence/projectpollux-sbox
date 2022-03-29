using ProjectPollux.Entities.Weapons;
using ProjectPollux.Player.GameMovement;
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
		public PolluxPlayer()
		{
			Inventory = new PolluxInventoryBase( this );
			Controller = new PolluxGameMovement();
		}

		public override void Spawn()
		{
			base.Spawn();
		}

		public override void Respawn()
		{
			base.Respawn();

			ArmorValue = 0;

			AuxPower = 100;
		}

		public static class PlayerTags
		{
			public const string Ducked = "ducked";
			public const string WaterJump = "waterjump";
			public const string Noclipped = "noclipped";
			public const string SuitEquipped = "suitequipped";
			public const string Sprinted = "sprinted";
			public const string FlashlightOn = "flashlighton";
		}
	}
}
