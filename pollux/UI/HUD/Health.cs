using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace ProjectPollux.UI.HUD
{
	public class Health : Panel
	{
		public Label HealthAmountLabel;

		public Label PanelIdent;

		public Health()
		{
			PanelIdent = Add.Label( "HEALTH", "HASIdent" );
			HealthAmountLabel = Add.Label( "100", "HASNumbers" );
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

			HealthAmountLabel.Text = $"{player.Health.CeilToInt()}";
		}
	}
}
