using Sandbox;
using Sandbox.UI;

namespace ProjectPollux.UI.HUD
{
	[Library]
	public partial class HL2Hud : HudEntity<RootPanel>
	{
		/// <summary>
		/// A HUD designed to look and feel similar to the HUD from HL2. Only tested at 4K 150% scaling.
		/// </summary>
		public HL2Hud()
		{
			if ( !IsClient )
				return;

			RootPanel.StyleSheet.Load( "/pollux/ui/HUD/HL2Hud.scss" );

			RootPanel.AddChild<Health>();

			RootPanel.AddChild<Suit>();

			RootPanel.AddChild<Crosshair>();
		}

	}
}
