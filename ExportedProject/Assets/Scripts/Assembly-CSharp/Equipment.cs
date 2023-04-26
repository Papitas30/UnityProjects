using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
	[Header("Inventory Stats")]
	public float amount;

	public float volume;

	public float weight;

	public float value;

	[Header("Player Data")]
	public PlayerData playerData;

	[Header("Weapon Holder")]
	public Transform weaponHolder;

	[Header("Equipment")]
	public ItemData[] equipments = new ItemData[16];

	private Weapon weaponSystem;

	private WeaponHandling weaponHandling;

	private UIManager UIManager;

	private InputManager inputManager;

	public List<Slot> equipmentSlots;

	private void Start()
	{
		UIManager = GameObject.FindWithTag("UI Manager").GetComponent<UIManager>();
		inputManager = GameObject.FindWithTag("Input Manager").GetComponent<InputManager>();
		weaponSystem = GetComponent<Weapon>();
		weaponHandling = GetComponent<WeaponHandling>();
	}

	public void ActivatePrimary()
	{
		playerData.fireMode = PlayerData.FireMode.Single;
		inputManager.fireModeToggle = true;
		GameObject obj = Object.Instantiate(equipments[0].equippable);
		weaponHolder.localPosition = new Vector3(0f, -1f, -0.25f);
		obj.transform.SetParent(weaponHolder);
		obj.transform.SetSiblingIndex(0);
		obj.transform.localPosition = new Vector3(0f, 0f, 0f);
		obj.transform.localRotation = Quaternion.Euler(45f, 0f, 0f);
		obj.transform.localScale = Vector3.one;
		obj.name = equipments[0].name;
		weaponSystem.itemData = equipments[0];
		weaponSystem.SetReferences();
		weaponHandling.DrawWeapon();
		UIManager.weapon = equipments[0];
	}

	public void ActivateSecondary()
	{
		playerData.fireMode = PlayerData.FireMode.Single;
		inputManager.fireModeToggle = true;
		GameObject obj = Object.Instantiate(equipments[1].equippable);
		weaponHolder.localPosition = new Vector3(0f, -1f, 0f);
		obj.transform.SetParent(weaponHolder);
		obj.transform.SetSiblingIndex(0);
		obj.transform.localPosition = new Vector3(0f, 0f, 0f);
		obj.transform.localRotation = Quaternion.Euler(45f, 0f, 0f);
		obj.transform.localScale = Vector3.one;
		obj.name = equipments[1].name;
		weaponSystem.itemData = equipments[1];
		weaponSystem.SetReferences();
		weaponHandling.DrawWeapon();
		UIManager.weapon = equipments[1];
	}

	public void AddEquipment(ItemData item, int index)
	{
		equipments[index] = item;
		if (index == 0)
		{
			playerData.primaryEquipped = true;
		}
		if (index == 1)
		{
			playerData.secondaryEquipped = true;
		}
	}

	public void DeactivatePrimary()
	{
		if (playerData.primaryActive)
		{
			playerData.primaryActive = false;
		}
		playerData.isHolstering = true;
		Invoke("DestroyPrimary", 0.25f);
	}

	private void DestroyPrimary()
	{
		if (inputManager.fastSecondarySwap)
		{
			Object.Destroy(weaponHolder.GetChild(1).gameObject);
			inputManager.fastSecondarySwap = false;
		}
		else
		{
			UIManager.weapon = null;
			Object.Destroy(weaponHolder.GetChild(0).gameObject);
		}
		playerData.isReloading = false;
		playerData.isHolstering = false;
	}

	public void DeactivateSecondary()
	{
		if (playerData.secondaryActive)
		{
			playerData.secondaryActive = false;
		}
		playerData.isHolstering = true;
		Invoke("DestroySecondary", 0.25f);
	}

	private void DestroySecondary()
	{
		if (inputManager.fastPrimarySwap)
		{
			Object.Destroy(weaponHolder.GetChild(1).gameObject);
			inputManager.fastPrimarySwap = false;
		}
		else
		{
			UIManager.weapon = null;
			Object.Destroy(weaponHolder.GetChild(0).gameObject);
		}
		playerData.isReloading = false;
		playerData.isHolstering = false;
	}

	public void RemoveEquipment(int index)
	{
		equipments[index] = null;
		if (index == 0)
		{
			playerData.primaryEquipped = false;
			if (playerData.primaryActive)
			{
				playerData.primaryActive = false;
				Object.Destroy(weaponHolder.GetChild(0).gameObject);
			}
		}
		if (index == 1)
		{
			playerData.secondaryEquipped = false;
			if (playerData.secondaryActive)
			{
				playerData.secondaryActive = false;
				Object.Destroy(weaponHolder.GetChild(0).gameObject);
			}
		}
	}
}
