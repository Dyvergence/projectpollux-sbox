using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using SWB_Base;
using System;

namespace ProjectPollux.UI.HUD
{
	public class AmmoDisplay : Panel
	{
		public Label ReserveAmountLabel;

		public Label ClipAmountLabel;

		public Label PanelIdent;

		public Label AmmoTypeIcon;

		public AmmoDisplay()
		{
			PanelIdent = Add.Label( "AMMO", "HASIdent" );
			ClipAmountLabel = Add.Label( "18", "AmmoNumber" );
			ReserveAmountLabel = Add.Label( "150", "ReserveNumber" );
			AmmoTypeIcon = Add.Label( "�", "AmmoTypeIcon" );
		}

		public override void Tick()
		{
			if ( Local.Pawn is not PolluxPlayer player )
				return;

			var weapon = player.ActiveChild as WeaponBase;
			bool isValidWeapon = weapon != null;

			if ( !isValidWeapon )
				return;

			if ( !player.IsSuitEquipped )
				SetClass( "hideAmmoDisplay", true );
			else
				SetClass( "hideAmmoDisplay", false );

			string ammoTypeIcon() => weapon.Primary.AmmoType switch
			{
				AmmoType.Pistol => "p",
				AmmoType.Buckshot => "s",
				AmmoType.PulseRifle => "u",
				AmmoType.SMG1 => "r",
				AmmoType.ThreeFiveSeven => "q",
				_ => "�",
			};

			AmmoTypeIcon.Text = ammoTypeIcon();

			var hasClipSize = weapon.Primary.ClipSize > 0;
			var reserveAmmo = Math.Min( player.AmmoCount( weapon.Primary.AmmoType ), 999 );

			if ( weapon.Primary.InfiniteAmmo != InfiniteAmmoType.clip )
			{
				var clipAmmo = hasClipSize ? weapon.Primary.Ammo : reserveAmmo;
				clipAmmo = Math.Min( clipAmmo, 999 );

				ClipAmountLabel.SetText( clipAmmo.ToString() );
			}
			else
			{
				ReserveAmountLabel.SetText( "∞" );
			}

			if ( hasClipSize )
			{
				if ( weapon.Primary.InfiniteAmmo == 0 )
				{
					ReserveAmountLabel.SetText( $"{reserveAmmo}" );
				}
				else
				{
					ReserveAmountLabel.SetText( "∞" );
				}
			}
		}
	}
}
