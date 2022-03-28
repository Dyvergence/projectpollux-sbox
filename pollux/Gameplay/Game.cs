using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox;
using Sandbox.UI;

namespace ProjectPollux.Gameplay
{
	public partial class Game : Source1.GameRules
	{
		public HudEntity<RootPanel> Hud { get; private set; }
		public override void ClientJoined( Client client )
		{
			base.ClientJoined( client );

			var pawn = new PolluxPlayer();
			pawn.Respawn();

			client.Pawn = pawn;

			if ( IsServer )
				Hud = new UI.HUD.HL2Hud();
		}

		public override void DeclareGameTeams()
		{
			Source1.TeamManager.DeclareTeam( 0, "unassigned", "UNASSIGNED", Color.White, true, true );
		}

		public override void Tick() { }
	}
}
