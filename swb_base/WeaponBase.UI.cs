using Sandbox;
using Sandbox.UI;
using ProjectPollux.UI.HUD;

/* 
 * Weapon base UI
*/

namespace SWB_Base
{

    public partial class WeaponBase
    {
        private Panel ammoDisplay;
        public override void CreateHudElements()
        {
            var showHUDCL = GetSetting<bool>("swb_cl_showhud", true);
            var showHUDSV = GetSetting<bool>("swb_sv_showhud", true);

            if (Local.Hud == null || !showHUDCL || !showHUDSV) return;

    //        if (UISettings.ShowCrosshair)
    //        {
    //            CrosshairPanel = new ProjectPollux.UI.HUD.Crosshair
				//{
    //                Parent = Local.Hud
    //            };
    //        }

            if (UISettings.ShowAmmoCount)
            {
                ammoDisplay = new AmmoDisplay()
                {
                    Parent = Local.Hud
                };
            }
        }

        public override void DestroyHudElements()
        {
            base.DestroyHudElements();

            if (ammoDisplay != null) ammoDisplay.Delete(true);
        }

        public void UISimulate(Client player)
        {
            // Cutomization menu
            // if (EnableCustomizationSV > 0 && Input.Pressed(InputButton.Menu) && AttachmentCategories != null)
            // {
            //     if (customizationMenu == null)
            //     {
            //         customizationMenu = new CustomizationMenu();
            //         customizationMenu.Parent = Local.Hud;
            //     }
            //     else
            //     {
            //         customizationMenu.Delete();
            //         customizationMenu = null;
            //     }
            // }

            // IsCustomizing = customizationMenu != null;
        }
    }
}
