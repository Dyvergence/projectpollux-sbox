using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPollux.UI.HUD
{
	public class Crosshair : Panel
	{
		public Label CrosshairLabel;

		public Crosshair()
		{
			CrosshairLabel = Add.Label( "Q" );
		}

		public override void Tick()
		{
			if ( Local.Pawn is not PolluxPlayer player )
				return;

			SetClass( "hidden", player.LifeState != LifeState.Alive );
		}
	}
}
