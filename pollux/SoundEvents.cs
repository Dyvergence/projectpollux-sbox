using Sandbox;
using System.ComponentModel;

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
