using Sandbox;
using SWB_Base;

namespace ProjectPollux.Entities.Items.Consumables
{
	[Library( "item_ammo_pistol", Description = "Box of Pistol ammo" )]
	[Hammer.Model( Model = "models/items/boxsrounds.vmdl" )]
	[Hammer.EntityTool( "Box of Pistol ammo", "Ammo", "Provides 20 pistol bullets." )]
	[Hammer.VisGroup( Hammer.VisGroup.Physics )]
	[Hammer.SkipProperty( "armortoreplenish" )]
	[Hammer.SkipProperty( "healthtoreplenish" )]
	partial class AmmoPistolSmall : Consumable
	{
		public override ConsumableType TypeConsumable => ConsumableType.Ammo;
		public override AmmoType TypeOfAmmo => AmmoType.Pistol;
		public override int AmmoToReplenish => 20;
		public override string OnConsumeSound => "ConsumableSounds.EquipWeapon";
	}

	[Library( "item_ammo_pistol_large", Description = "Large Box of Pistol ammo" )]
	[Hammer.Model( Model = "models/items/boxsrounds.vmdl" )]
	[Hammer.EntityTool( "Large Box of Pistol ammo", "Ammo", "Provides 100 pistol bullets." )]
	[Hammer.VisGroup( Hammer.VisGroup.Physics )]
	[Hammer.SkipProperty( "armortoreplenish" )]
	[Hammer.SkipProperty( "healthtoreplenish" )]
	partial class AmmoPistolLarge : Consumable
	{
		public override ConsumableType TypeConsumable => ConsumableType.Ammo;
		public override AmmoType TypeOfAmmo => AmmoType.Pistol;
		public override int AmmoToReplenish => 100;
		public override string OnConsumeSound => "ConsumableSounds.EquipWeapon";
	}

	[Library( "item_ammo_357", Description = "Box of 357 ammo" )]
	[Hammer.Model( Model = "models/items/357ammo.vmdl" )]
	[Hammer.EntityTool( "Box of 357 ammo", "Ammo", "Provides 6 revolver bullets." )]
	[Hammer.VisGroup( Hammer.VisGroup.Physics )]
	[Hammer.SkipProperty( "armortoreplenish" )]
	[Hammer.SkipProperty( "healthtoreplenish" )]
	partial class AmmoRevolverSmall : Consumable
	{
		public override ConsumableType TypeConsumable => ConsumableType.Ammo;
		public override AmmoType TypeOfAmmo => AmmoType.ThreeFiveSeven;
		public override int AmmoToReplenish => 6;
		public override string OnConsumeSound => "ConsumableSounds.EquipWeapon";
	}

	[Library( "item_ammo_357_large", Description = "Large Box of 357 ammo" )]
	[Hammer.Model( Model = "models/items/357ammobox.vmdl" )]
	[Hammer.EntityTool( "Large Box of 357 ammo", "Ammo", "Provides 12 revolver bullets." )]
	[Hammer.VisGroup( Hammer.VisGroup.Physics )]
	[Hammer.SkipProperty( "armortoreplenish" )]
	[Hammer.SkipProperty( "healthtoreplenish" )]
	partial class AmmoRevolverLarge : Consumable
	{
		public override ConsumableType TypeConsumable => ConsumableType.Ammo;
		public override AmmoType TypeOfAmmo => AmmoType.ThreeFiveSeven;
		public override int AmmoToReplenish => 12;
		public override string OnConsumeSound => "ConsumableSounds.EquipWeapon";
	}

	[Library( "item_ammo_ar2", Description = "Box of AR2 ammo" )]
	[Hammer.Model( Model = "models/items/combine_rifle_cartridge01.vmdl" )]
	[Hammer.EntityTool( "Box of AR2 ammo", "Ammo", "Provides 20 AR2 pulses." )]
	[Hammer.VisGroup( Hammer.VisGroup.Physics )]
	[Hammer.SkipProperty( "armortoreplenish" )]
	[Hammer.SkipProperty( "healthtoreplenish" )]
	partial class AmmoAR2Small : Consumable
	{
		public override ConsumableType TypeConsumable => ConsumableType.Ammo;
		public override AmmoType TypeOfAmmo => AmmoType.PulseRifle;
		public override int AmmoToReplenish => 20;
		public override string OnConsumeSound => "ConsumableSounds.EquipWeapon";
	}

	[Library( "item_ammo_ar2_large", Description = "Large Box of AR2 ammo" )]
	[Hammer.Model( Model = "models/items/combine_rifle_cartridge01.vmdl" )]
	[Hammer.EntityTool( "Large Box of AR2 ammo", "Ammo", "Provides 60 AR2 pulses." )]
	[Hammer.VisGroup( Hammer.VisGroup.Physics )]
	[Hammer.SkipProperty( "armortoreplenish" )]
	[Hammer.SkipProperty( "healthtoreplenish" )]
	partial class AmmoAR2Large : Consumable
	{
		public override ConsumableType TypeConsumable => ConsumableType.Ammo;
		public override AmmoType TypeOfAmmo => AmmoType.PulseRifle;
		public override int AmmoToReplenish => 60;
		public override string OnConsumeSound => "ConsumableSounds.EquipWeapon";
	}

	[Library( "item_ammo_ar2_altfire", Description = "AR2 Alt-fire Round" )]
	[Hammer.Model( Model = "models/items/boxmrounds.vmdl" )]
	[Hammer.EntityTool( "AR2 Alt-fire Round", "Ammo", "Provides 1 combine ball." )]
	[Hammer.VisGroup( Hammer.VisGroup.Physics )]
	[Hammer.SkipProperty( "armortoreplenish" )]
	[Hammer.SkipProperty( "healthtoreplenish" )]
	partial class AmmoAR2AltFire : Consumable
	{
		public override ConsumableType TypeConsumable => ConsumableType.Ammo;
		public override AmmoType TypeOfAmmo => AmmoType.PulseRifleAltFire;
		public override int AmmoToReplenish => 1;
		public override string OnConsumeSound => "ConsumableSounds.EquipWeapon";
	}

	[Library( "item_ammo_smg1", Description = "Box of SMG ammo" )]
	[Hammer.Model( Model = "models/items/boxmrounds.vmdl" )]
	[Hammer.EntityTool( "Box of SMG ammo", "Ammo", "Provides 45 SMG bullets." )]
	[Hammer.VisGroup( Hammer.VisGroup.Physics )]
	[Hammer.SkipProperty( "armortoreplenish" )]
	[Hammer.SkipProperty( "healthtoreplenish" )]
	partial class AmmoSMG1Small : Consumable
	{
		public override ConsumableType TypeConsumable => ConsumableType.Ammo;
		public override AmmoType TypeOfAmmo => AmmoType.SMG1;
		public override int AmmoToReplenish => 45;
		public override string OnConsumeSound => "ConsumableSounds.EquipWeapon";
	}

	[Library( "item_ammo_smg1_grenade", Description = "SMG grenade" )]
	[Hammer.Model( Model = "models/items/ar2_grenade.vmdl" )]
	[Hammer.EntityTool( "SMG Grenade", "Ammo", "Provides 1 SMG grenade." )]
	[Hammer.VisGroup( Hammer.VisGroup.Physics )]
	[Hammer.SkipProperty( "armortoreplenish" )]
	[Hammer.SkipProperty( "healthtoreplenish" )]
	partial class AmmoSMG1Grenade : Consumable
	{
		public override ConsumableType TypeConsumable => ConsumableType.Ammo;
		public override AmmoType TypeOfAmmo => AmmoType.SMG1_Grenade;
		public override int AmmoToReplenish => 1;
		public override string OnConsumeSound => "ConsumableSounds.EquipWeapon";
	}

	[Library( "item_ammo_smg1_large", Description = "Large Box of SMG ammo" )]
	[Hammer.Model( Model = "models/items/boxmrounds.vmdl" )]
	[Hammer.EntityTool( "Large Box of SMG ammo", "Ammo", "Provides 225 SMG bullets." )]
	[Hammer.VisGroup( Hammer.VisGroup.Physics )]
	[Hammer.SkipProperty( "armortoreplenish" )]
	[Hammer.SkipProperty( "healthtoreplenish" )]
	partial class AmmoSMGLarge : Consumable
	{
		public override ConsumableType TypeConsumable => ConsumableType.Ammo;
		public override AmmoType TypeOfAmmo => AmmoType.SMG1;
		public override int AmmoToReplenish => 225;
		public override string OnConsumeSound => "ConsumableSounds.EquipWeapon";
	}

	[Library( "item_rpg_round", Description = "RPG round" )]
	[Hammer.Model( Model = "models/weapons/w_missile_closed.vmdl" )]
	[Hammer.EntityTool( "RPG round", "Ammo", "Provides 1 RPG round." )]
	[Hammer.VisGroup( Hammer.VisGroup.Physics )]
	[Hammer.SkipProperty( "armortoreplenish" )]
	[Hammer.SkipProperty( "healthtoreplenish" )]
	partial class AmmoRPG : Consumable
	{
		public override ConsumableType TypeConsumable => ConsumableType.Ammo;
		public override AmmoType TypeOfAmmo => AmmoType.RPG_Round;
		public override int AmmoToReplenish => 1;
		public override string OnConsumeSound => "ConsumableSounds.EquipWeapon";
	}

	[Library( "item_ammo_crossbow", Description = "Box of Crossbow ammo" )]
	[Hammer.Model( Model = "models/items/crossbowrounds.vmdl" )]
	[Hammer.EntityTool( "Box of Crossbow ammo", "Ammo", "Provides 6 crossbow bolts." )]
	[Hammer.VisGroup( Hammer.VisGroup.Physics )]
	[Hammer.SkipProperty( "armortoreplenish" )]
	[Hammer.SkipProperty( "healthtoreplenish" )]
	partial class AmmoCrossbowSmall : Consumable
	{
		public override ConsumableType TypeConsumable => ConsumableType.Ammo;
		public override AmmoType TypeOfAmmo => AmmoType.XBowBolt;
		public override int AmmoToReplenish => 6;
		public override string OnConsumeSound => "ConsumableSounds.EquipWeapon";
	}

	[Library( "Item_box_buckshot", Description = "Box of Shotgun ammo" )]
	[Hammer.Model( Model = "models/items/boxbuckshot.mdl" )]
	[Hammer.EntityTool( "Box of Shotgun ammo", "Ammo", "Provides 20 shotgun bullets." )]
	[Hammer.VisGroup( Hammer.VisGroup.Physics )]
	[Hammer.SkipProperty( "armortoreplenish" )]
	[Hammer.SkipProperty( "healthtoreplenish" )]
	partial class AmmoShotgunSmall : Consumable
	{
		public override ConsumableType TypeConsumable => ConsumableType.Ammo;
		public override AmmoType TypeOfAmmo => AmmoType.Buckshot;
		public override int AmmoToReplenish => 20;
		public override string OnConsumeSound => "ConsumableSounds.EquipWeapon";
	}
}
