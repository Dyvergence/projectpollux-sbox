using Pollux.Player.Camera;
using ProjectPollux.Player.GameMovement;
using ProjectPollux.Player.Inventory;
using Source1;

namespace ProjectPollux
{
	public partial class PolluxPlayer : Source1Player
	{
		public PolluxPlayer()
		{
			Inventory = new PolluxInventoryBase( this );
			Controller = new PolluxGameMovement();
			CameraMode = new PolluxCamera();
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
