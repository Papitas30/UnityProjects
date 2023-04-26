using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item", order = 2)]
public class ItemData : ScriptableObject
{
	public enum ItemType
	{
		Consumables = 0,
		Medical = 1,
		Melee = 2,
		Weapon = 3,
		Primary = 4,
		Secondary = 5,
		Equipment = 6,
		Valuables = 7,
		Misc = 8
	}

	public enum ItemRarity
	{
		Common = 0,
		Uncommon = 1,
		Rare = 2,
		ExtremelyRare = 3
	}

	public enum FireType
	{
		Single = 0,
		Semi = 1,
		SemiAuto = 2,
		Auto = 3
	}

	[Header("Naming")]
	public string fileName;

	public string itemName;

	public string abbreviation;

	[Header("Type")]
	public ItemType type;

	[Header("Rarity")]
	public ItemRarity rarity;

	[Header("Description")]
	[TextArea(4, 4)]
	public string description;

	[Header("Stats")]
	public float volume;

	public float weight;

	public float value;

	public bool hasUsage;

	[Header("Icon")]
	public Sprite icon;

	[Header("Prefabs")]
	public GameObject equippable;

	public GameObject pickup;

	public GameObject slot;

	[Header("Audio Events")]
	public AudioEvent pickupSound;

	public AudioEvent useSound;

	[Space(15f)]
	public List<GameEvent> gameEvents;

	[Space(20f)]
	[Header("Weapon Stats")]
	public float fireRate;

	public float magazineSize;

	public string ammoType;

	public float damage;

	public float impactForce;

	[Header("Fire Type")]
	public FireType fireType;

	public bool hasScope;

	public float aimFOV;

	public bool pumpAction;

	public bool boltAction;

	public bool hasSlideKick;

	public bool hasSuppressor;

	[Header("Weapon Audio")]
	public AudioEvent fire;

	public AudioEvent unload;

	public AudioEvent load;

	public AudioEvent cock;

	public AudioEvent empty;

	[Header("Weapon Reload")]
	public float reloadSpeed;

	[Header("Weapon Cartridge")]
	public GameObject cartridge;

	[Header("Weapon Kick")]
	public float kickPower;

	public float kickRecovery;

	[Header("Weapon Rotation")]
	public float rotationPower;

	public float rotationRecovery;

	[Header("Recoil Behavior")]
	public float weaponKick;

	public float verticalRecoil;

	public float horizontalRecoil;

	[Header("Weapon Handling")]
	public Vector3 inspectPosition;

	public Vector3 inspectRotation;

	public Vector3 aimPosition;

	public Vector3 aimRotation;

	public Vector3 upPosition;

	public Vector3 upRotation;

	public Vector3 highPosition;

	public Vector3 highRotation;

	public Vector3 lowPosition;

	public Vector3 lowRotation;

	public Vector3 hidePosition;

	public Vector3 hideRotation;

	public Vector3 reloadPosition;

	public Vector3 reloadRotation;

	public Vector3 collisionPosition;

	public Vector3 collisionRotation;

	[Header("Weapon UI Details")]
	public string muzzle;

	public string sight;

	[Header("Weapon UI Offset")]
	public Vector2 muzzlePosition;

	public Vector2 caliberPosition;

	public Vector2 conditionPosition;

	public Vector2 firemodePosition;

	public Vector2 magazinePosition;

	public Vector2 sightPosition;

	[Space(20f)]
	[HideInInspector]
	public float healthEffect;

	[HideInInspector]
	public float staminaEffect;

	[HideInInspector]
	public float hydrationEffect;

	[HideInInspector]
	public float energyEffect;

	[HideInInspector]
	public bool fatique;

	[HideInInspector]
	public bool pain;

	[HideInInspector]
	public bool bleeding;

	[HideInInspector]
	public bool fracture;

	[HideInInspector]
	public bool delirium;

	[HideInInspector]
	public bool poisoning;

	[HideInInspector]
	public bool rupture;

	[HideInInspector]
	public bool hypovolemia;
}
