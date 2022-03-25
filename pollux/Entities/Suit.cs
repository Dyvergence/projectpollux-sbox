using Sandbox;

namespace ProjectPollux.Entities.Items
{
	[Library( "item_suit", Description = "HEV Suit" )] 
	[Hammer.EditorModel( "models/items/hevsuit.vmdl" )]
	partial class ItemSuit : ModelEntity
	{
		/// <summary>
		/// Fires when the player touches this object
		/// </summary>
		protected Output OnPlayerTouch { get; set; }

		public PickupTrigger PickupTrigger { get; protected set; }

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

			SetModel( "models/items/hevsuit.vmdl" );
		}

		public override void StartTouch( Entity activator )
		{
			if ( IsClient ) return;
			if ( activator.IsWorld ) return;
			if ( Parent != null ) return;

			if ( activator is PolluxPlayer player )
			{
				PlaySound( "SuitSounds.EquipSuit" );
				player.EquipSuit();
				OnPlayerTouch.Fire( player );
				Delete();
			}
		}
	}
}
