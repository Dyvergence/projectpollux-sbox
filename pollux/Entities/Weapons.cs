using Sandbox;
using SWB_Base;

namespace ProjectPollux.Entities.Weapons
{
	[Library( "weapon_crowbar", Title = "Crowbar", Description = "Gordon's crowbar." )]
	[Hammer.EditorModel( "models/weapons/crowbar/w_crowbar.vmdl" )]
	[Hammer.EntityTool( "Crowbar", "Weapons", "Gordon's crowbar." )]
	public class HL2Crowbar : WeaponBaseMelee
	{
		public override int Bucket => 0;
		public override HoldType HoldType => HoldType.Fists;
		public override string ViewModelPath => "models/weapons/crowbar/v_crowbar.vmdl";
		public override string WorldModelPath => "models/weapons/crowbar/w_crowbar.vmdl";

		public override string SwingAnimationHit => "swing";
		public override string SwingAnimationMiss => "swing_miss";

		public override string SwingSound => "Weapon_Crowbar.Single";
		public override string MissSound => "Weapon_Crowbar.Single";
		public override string HitWorldSound => "Weapon_Crowbar.Melee_HitWorld";

		public override float SwingSpeed => 0.4f;
		public override float SwingDamage => 25f;
		public override float SwingForce => 25f;
		public override float DamageDistance => 35f;
		public override float ImpactSize => 10f;

		public HL2Crowbar()
		{
			UISettings = new UISettings
			{
				ShowAmmoCount = false
			};
		}
	}

	[Library( "weapon_pistol", Title = "Pistol", Description = "A pistol." )]
	[Hammer.EditorModel( "models/weapons/pistol/w_pistol.vmdl" )]
	[Hammer.EntityTool( "Pistol", "Weapons", "A pistol." )]
	public class HL2Pistol : WeaponBase
	{
		public override int Bucket => 1;
		public override HoldType HoldType => HoldType.Pistol;
		public override string ViewModelPath => "models/weapons/pistol/v_pistol.vmdl";
		public override string WorldModelPath => "models/weapons/pistol/w_pistol.vmdl";

		public HL2Pistol()
		{
			General = new WeaponInfo
			{
				DrawTime = 0.5f,
				ReloadTime = 1.5f,
				// ReloadEmptyTime = 1.5f
			};

			Primary = new ClipInfo
			{
				Ammo = 18,
				AmmoType = AmmoType.Pistol,
				ClipSize = 18,

				BulletSize = 6f,
				Damage = 10f,
				Force = 5f,
				Spread = 0.05f,
				Recoil = 0.5f,
				RPM = 600,
				FiringType = FiringType.semi,

				DryFireSound = "Weapon_Pistol.Empty",
				ShootSound = "Weapon_Pistol.Single",

				BulletEjectParticle = "particles/pistol_ejectbrass.vpcf",
				BulletTracerParticle = null,
				MuzzleFlashParticle = "particles/pistol_muzzleflash.vpcf",
				BarrelSmokeParticle = null
			};
		}
	}

	[Library( "weapon_smg1", Title = "SMG", Description = "A submachine gun." )]
	[Hammer.EditorModel( "models/weapons/smg/w_smg1.vmdl" )]
	[Hammer.EntityTool( "SMG", "Weapons", "A submachine gun." )]
	public class HL2SMG : WeaponBase
	{
		public override int Bucket => 2;
		public override HoldType HoldType => HoldType.Rifle;
		public override string ViewModelPath => "models/weapons/smg/v_smg1.vmdl";
		public override string WorldModelPath => "models/weapons/smg/w_smg1.vmdl";

		public HL2SMG()
		{
			General = new WeaponInfo
			{
				DrawTime = 1f,
				ReloadTime = 1.5f,
				//ReloadEmptyTime = 1.5f
			};

			Primary = new ClipInfo
			{
				Ammo = 45,
				AmmoType = AmmoType.SMG1,
				ClipSize = 45,

				BulletSize = 6f,
				Damage = 10f,
				Force = 5f,
				Spread = 0.06f,
				Recoil = 1f,
				RPM = 800,
				FiringType = FiringType.auto,

				DryFireSound = "Weapon_SMG1.Empty",
				ShootSound = "Weapon_SMG1.Single",

				BulletEjectParticle = "particles/pistol_ejectbrass.vpcf",
				BulletTracerParticle = null,
				MuzzleFlashParticle = "particles/pistol_muzzleflash.vpcf",
				BarrelSmokeParticle = null
			};
		}
	}

	[Library( "weapon_shotgun", Title = "Shotgun", Description = "A shotgun." )]
	[Hammer.EditorModel( "models/weapons/shotgun/w_shotgun.vmdl" )]
	[Hammer.EntityTool( "Shotgun", "Weapons", "A shotgun." )]
	public class HL2Shotgun : WeaponBase
	{
		public override int Bucket => 3;
		public override HoldType HoldType => HoldType.Shotgun;
		public override string ViewModelPath => "models/weapons/shotgun/v_shotgun.vmdl";
		public override string WorldModelPath => "models/weapons/shotgun/w_shotgun.vmdl";

		public HL2Shotgun()
		{
			General = new WeaponInfo
			{
				DrawTime = 1f,
				ReloadTime = 2.5f,
				// ReloadEmptyTime = 2.5f
			};

			Primary = new ClipInfo
			{
				Ammo = 6,
				AmmoType = AmmoType.Buckshot,
				ClipSize = 6,

				BulletSize = 6f,
				Damage = 10f,
				Force = 5f,
				Spread = 0.06f,
				Recoil = 1f,
				RPM = 300,
				FiringType = FiringType.semi,

				DryFireSound = "Weapon_Shotgun.Empty",
				ShootSound = "Weapon_Shotgun.Single",

				BulletEjectParticle = "particles/shotgun_ejectbrass.vpcf",
				BulletTracerParticle = null,
				MuzzleFlashParticle = "particles/shotgun_muzzleflash.vpcf",
				BarrelSmokeParticle = null
			};
		}
	}

	[Library( "weapon_ar2", Title = "AR2", Description = "An assault rifle." )]
	[Hammer.EditorModel( "models/weapons/ar2/w_ar2.vmdl" )]
	[Hammer.EntityTool( "AR2", "Weapons", "An assault rifle." )]
	public class HL2AR2 : WeaponBase
	{
		public override int Bucket => 2;
		public override HoldType HoldType => HoldType.Rifle;
		public override string ViewModelPath => "models/weapons/ar2/v_ar2.vmdl";
		public override string WorldModelPath => "models/weapons/ar2/w_ar2.vmdl";

		public HL2AR2()
		{
			General = new WeaponInfo
			{
				DrawTime = 1f,
				ReloadTime = 2.5f,
				// ReloadEmptyTime = 2.5f
			};

			Primary = new ClipInfo
			{
				Ammo = 30,
				AmmoType = AmmoType.PulseRifle,
				ClipSize = 30,

				BulletSize = 6f,
				Damage = 10f,
				Force = 5f,
				Spread = 0.06f,
				Recoil = 1f,
				RPM = 300,
				FiringType = FiringType.semi,

				DryFireSound = "Weapon_AR2.Empty",
				ShootSound = "Weapon_AR2.Single",

				BulletEjectParticle = "particles/ar2_ejectbrass.vpcf",
				BulletTracerParticle = null,
				MuzzleFlashParticle = "particles/ar2_muzzleflash.vpcf",
				BarrelSmokeParticle = null
			};
		}
	}

	[Library( "weapon_357", Title = "357", Description = "A 357." )]
	[Hammer.EditorModel( "models/weapons/357/w_357.vmdl" )]
	[Hammer.EntityTool( "357", "Weapons", "A 357." )]
	public class HL2357 : WeaponBase
	{
		public override int Bucket => 4;
		public override HoldType HoldType => HoldType.Pistol;
		public override string ViewModelPath => "models/weapons/357/v_357.vmdl";
		public override string WorldModelPath => "models/weapons/357/w_357.vmdl";

		public HL2357()
		{
			General = new WeaponInfo
			{
				DrawTime = 1f,
				ReloadTime = 1.8f,
				// ReloadEmptyTime = 1.8f
			};

			Primary = new ClipInfo
			{
				Ammo = 6,
				AmmoType = AmmoType.ThreeFiveSeven,
				ClipSize = 6,

				BulletSize = 6f,
				Damage = 50f,
				Force = 5f,
				Spread = 0.06f,
				Recoil = 1f,
				RPM = 300,
				FiringType = FiringType.semi,

				DryFireSound = "Weapon_357.Empty",
				ShootSound = "Weapon_357.Single",

				BulletEjectParticle = "particles/357_ejectbrass.vpcf",
				BulletTracerParticle = null,
				MuzzleFlashParticle = "particles/357_muzzleflash.vpcf",
				BarrelSmokeParticle = null
			};
		}
	}
}
