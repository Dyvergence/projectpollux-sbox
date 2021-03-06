using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

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

			if ( player.IsSuitEquipped && player.ArmorValue > 0)
			{
				SetClass( "hidden", player.LifeState != LifeState.Alive );
			}

			SuitAmountLabel.Text = $"{player.ArmorValue.CeilToInt()}";
		}
	}
}
