// TODO: Make more like the AUX power HUD element from HL2.
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace ProjectPollux.UI.HUD
{
    public class AuxPower : Panel
	{
		public Label AuxAmountLabel;

		public Label PanelIdent;

		public AuxPower()
		{
			PanelIdent = Add.Label( "AUX POWER", "HASIdent" );
			AuxAmountLabel = Add.Label( "100", "HASNumbers" );
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

			AuxAmountLabel.Text = $"{player.AuxPower.CeilToInt()}";
		}
	}
}
