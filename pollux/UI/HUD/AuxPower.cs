using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace ProjectPollux.UI.HUD
{
	public class AuxPower : Panel
	{
		public Label AuxPanelIdent;
		public Panel AuxPowerBlocksPanel;
		public Panel Block1;
		public Panel Block2;
		public Panel Block3;
		public Panel Block4;
		public Panel Block5;
		public Panel Block6;
		public Panel Block7;
		public Panel Block8;
		public Panel Block9;
		public Panel Block10;

		public AuxPower()
		{
			AuxPanelIdent = Add.Label( "AUX POWER", "AuxPanelIdent" );
			AuxPowerBlocksPanel = Add.Panel( "AuxPowerBlocksPanel" );
			Block1 = AuxPowerBlocksPanel.Add.Panel( "AuxPowerBlock" );
			Block2 = AuxPowerBlocksPanel.Add.Panel("AuxPowerBlock");
			Block3 = AuxPowerBlocksPanel.Add.Panel("AuxPowerBlock");
			Block4 = AuxPowerBlocksPanel.Add.Panel("AuxPowerBlock");
			Block5 = AuxPowerBlocksPanel.Add.Panel("AuxPowerBlock");
			Block6 = AuxPowerBlocksPanel.Add.Panel("AuxPowerBlock");
			Block7 = AuxPowerBlocksPanel.Add.Panel("AuxPowerBlock");
			Block8 = AuxPowerBlocksPanel.Add.Panel("AuxPowerBlock");
			Block9 = AuxPowerBlocksPanel.Add.Panel("AuxPowerBlock");
			Block10 = AuxPowerBlocksPanel.Add.Panel("AuxPowerBlock");
		}


		public override void Tick()
		{
			if ( Local.Pawn is not PolluxPlayer player )
				return;

			SetClass( "hidden", true );

			if ( player.IsSuitEquipped )
			{
				bool _ShouldDisplay()
				{
					if ( player.Tags.Has("sprinted") || player.Tags.Has("flashlighton") ) return true;
					else if ( player.AuxPower < 100 ) return true;
					else return false;
				}
				SetClass( "hidden", !_ShouldDisplay() );
			}

			int EnabledBlocks = (int)((float)10 * (player.AuxPower.CeilToInt() * 1.0f / 100.0f) + 0.5f);

			Block10.SetClass( "disabled" , EnabledBlocks <= 9); 
			Block9.SetClass( "disabled" , EnabledBlocks <= 8);
			Block8.SetClass( "disabled" , EnabledBlocks <= 7);
			Block7.SetClass( "disabled" , EnabledBlocks <= 6);
			Block6.SetClass( "disabled" , EnabledBlocks <= 5);
			Block5.SetClass( "disabled" , EnabledBlocks <= 4);
			Block4.SetClass( "disabled" , EnabledBlocks <= 3);
			Block3.SetClass( "disabled" , EnabledBlocks <= 2);
			Block2.SetClass( "disabled" , EnabledBlocks <= 1);
			Block1.SetClass( "disabled" , EnabledBlocks <= 0); 
		}
	}
}
