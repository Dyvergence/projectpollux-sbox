using Sandbox;
using Sandbox.UI;

namespace ProjectPollux.UI.HUD
{
	[Library]
	public partial class HL2Hud : HudEntity<RootPanel>
	{
		/// <summary>
		/// A HUD designed to look and feel similar to the HUD from HL2.
		/// </summary>
		public HL2Hud()
		{
			if ( !IsClient )
				return;

			RootPanel.StyleSheet.Load( "/pollux/ui/HUD/HL2Hud.scss" );

			RootPanel.AddChild<Health>();

			RootPanel.AddChild<Suit>();

			RootPanel.AddChild<Crosshair>();

			RootPanel.AddChild<AuxPower>();
		}

		[ConVar.Replicated( "cl_drawhud", Help = "Enable the rendering of the hud")]
		public static bool ShouldDisplay { get; set; } = true;

		[Event.Tick]
		public void Tick()
		{
			if ( !IsClient )
				return;
			if ( Local.Pawn is not PolluxPlayer)
				return;

			RootPanel.SetClass( "hidden", !ShouldDisplay );
		}

	}
}
