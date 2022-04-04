using Sandbox;

public class ConsumableSounds
{
	static SoundEvent EquipWeapon = new( "sounds/items/ammo_pickup.vsnd", 0.8f );
	static SoundEvent EquipHealthkit = new( "sounds/items/smallmedkit1.vsnd", 1f );
	static SoundEvent EquipBattery = new( "sounds/items/battery_pickup.vsnd", 1f );
}

public class ArmorChargerSounds
{
	static SoundEvent Start = new( "sounds/items/suitchargeok1.vsnd", 0.75f );
	static SoundEvent Loop = new( "sounds/items/suitcharge1.vsnd", 0.75f );
	static SoundEvent Deny = new( "sounds/items/suitchargeno1.vsnd", 0.75f );
}
public class HealthChargerSounds
{
	static SoundEvent Start = new( "sounds/items/medshot4.vsnd", 0.7f );
	static SoundEvent Loop = new( "sounds/items/medcharge4.vsnd", 0.7f );
	static SoundEvent Deny = new( "sounds/items/medshotno1.vsnd", 0.7f );
	static SoundEvent Recharge = new( "sounds/items/medshot4.vsnd", 0.7f );
}

public class SuitSounds
{
	static SoundEvent EquipSuit = new( "sounds/hl1/fvox/bell.vsnd" );
	static SoundEvent SprintStart = new( "sounds/player/suit_sprint.vsnd" );
}

public class Weapon_Crowbar
{
	static SoundEvent Single = new( "sounds/weapons/iceaxe/iceaxe_swing1.vsnd" );
	static SoundEvent Melee_HitWorld = new( "sounds/weapons/crowbar/crowbar_impact1.vsnd" );
}

public class Weapon_Pistol
{
	static SoundEvent Empty = new( "sounds/weapons/pistol/pistol_empty.vsnd" );
	static SoundEvent Single = new( "sounds/weapons/pistol/pistol_fire2.vsnd" );
	static SoundEvent Reload = new( "sounds/weapons/pistol/pistol_reload1.vsnd" );
}

public class Weapon_SMG1
{
	static SoundEvent Single = new( "sounds/weapons/smg1/smg1_fire1.vsnd" );
	static SoundEvent Melee_HitWorld = new( "sounds/weapons/ar2/ar2_altfire.vsnd" );
	static SoundEvent Reload = new( "sounds/weapons/smg1/smg1_reload.vsnd" );
}
