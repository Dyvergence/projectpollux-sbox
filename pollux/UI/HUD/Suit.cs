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
	public class Suit : Panel
	{
		public Label SuitAmountLabel;

		public Label PanelIdent;

		public Suit()
		{
			PanelIdent = Add.Label( "SUIT", "HASIdent" );
			SuitAmountLabel = Add.Label( "100", "HASNumbers" );
		}

		public override void Tick()
		{
			if ( Local.Pawn is not PolluxPlayer player )
				return;

			SetClass( "hidden", true );

			if ( player.IsSuitEquipped )
			{
				SetClass( "hidden", player.LifeState != LifeState.Alive );
			}

			SuitAmountLabel.Text = $"{player.ArmorValue.CeilToInt()}";
		}
	}
}
