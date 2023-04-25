using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static Func<ItemData, string> _003C_003E9__19_0;

		public static Func<ItemData, float> _003C_003E9__20_0;

		public static Func<ItemData, float> _003C_003E9__21_0;

		public static Func<ItemData, float> _003C_003E9__22_0;

		public static Func<ItemData, ItemData.ItemType> _003C_003E9__23_0;

		public static Func<ItemData, ItemData.ItemRarity> _003C_003E9__24_0;

		internal string _003CNameSort_003Eb__19_0(ItemData go)
		{
			return go.itemName;
		}

		internal float _003CVolumeSort_003Eb__20_0(ItemData go)
		{
			return go.volume;
		}

		internal float _003CWeightSort_003Eb__21_0(ItemData go)
		{
			return go.weight;
		}

		internal float _003CValueSort_003Eb__22_0(ItemData go)
		{
			return go.value;
		}

		internal ItemData.ItemType _003CTypeSort_003Eb__23_0(ItemData go)
		{
			return go.type;
		}

		internal ItemData.ItemRarity _003CRaritySort_003Eb__24_0(ItemData go)
		{
			return go.rarity;
		}
	}

	[Header("Inventory Limits")]
	private float maxAmount = 42f;

	public float maxVolume;

	public float maxWeight;

	[Header("Inventory Stats")]
	public float amount;

	public float volume;

	public float weight;

	public float value;

	[Header("Inventory Items")]
	public List<ItemData> items;

	private Transform inventorySlots;

	private UIManager UIManager;

	private float currentAmount;

	private float currentVolume;

	private float currentWeight;

	private void Start()
	{
		inventorySlots = GameObject.FindWithTag("Inventory Slots").transform;
		UIManager = GameObject.FindWithTag("UI Manager").GetComponent<UIManager>();
	}

	public void Add(ItemData item, int usage)
	{
		items.Add(item);
		amount += 1f;
		volume += item.volume;
		weight += item.weight;
		value += item.value;
		if (!(amount > maxAmount) && !(volume > maxVolume) && !(weight > maxWeight))
		{
			UIManager.UpdateInventoryStats();
			GameObject obj = UnityEngine.Object.Instantiate(item.slot);
			obj.GetComponent<Slot>().itemData = item;
			obj.GetComponent<Slot>().slotType = Slot.SlotType.InventorySlot;
			obj.transform.localScale = new Vector3(0.95f, 0.95f);
			obj.transform.SetParent(inventorySlots, false);
			obj.name = item.name;
			obj.GetComponent<Slot>().UpdateUsage(usage);
		}
	}

	public void Remove(ItemData item)
	{
		items.Remove(item);
		amount -= 1f;
		volume -= item.volume;
		weight -= item.weight;
		value -= item.value;
		UIManager.UpdateInventoryStats();
	}

	public void UpdateTransferTarget()
	{
		Container component = GameObject.FindWithTag("Container").GetComponent<Container>();
		for (int i = 0; i < inventorySlots.childCount; i++)
		{
			inventorySlots.GetChild(i).GetComponent<Slot>().UIManager.container = component;
		}
	}

	public bool CanFit(ItemData item)
	{
		currentAmount = amount;
		currentWeight = weight;
		currentVolume = volume;
		currentAmount += 1f;
		currentWeight += item.weight;
		currentVolume += item.volume;
		if (currentAmount > maxAmount || currentWeight > maxWeight || currentVolume > maxVolume)
		{
			return false;
		}
		return true;
	}

	private void GenerateUI()
	{
		foreach (Transform inventorySlot in inventorySlots)
		{
			UnityEngine.Object.Destroy(inventorySlot.gameObject);
		}
		for (int i = 0; i < items.Count; i++)
		{
			GameObject obj = UnityEngine.Object.Instantiate(items[i].slot);
			obj.GetComponent<Slot>().itemData = items[i];
			obj.GetComponent<Slot>().slotType = Slot.SlotType.InventorySlot;
			obj.transform.localScale = new Vector3(0.95f, 0.95f);
			obj.transform.SetParent(inventorySlots, false);
			obj.name = items[i].itemName;
		}
	}

	public void NameSort()
	{
		items = items.OrderBy(_003C_003Ec._003C_003E9__19_0 ?? (_003C_003Ec._003C_003E9__19_0 = _003C_003Ec._003C_003E9._003CNameSort_003Eb__19_0)).ToList();
		GenerateUI();
	}

	public void VolumeSort()
	{
		items = items.OrderBy(_003C_003Ec._003C_003E9__20_0 ?? (_003C_003Ec._003C_003E9__20_0 = _003C_003Ec._003C_003E9._003CVolumeSort_003Eb__20_0)).ToList();
		GenerateUI();
	}

	public void WeightSort()
	{
		items = items.OrderBy(_003C_003Ec._003C_003E9__21_0 ?? (_003C_003Ec._003C_003E9__21_0 = _003C_003Ec._003C_003E9._003CWeightSort_003Eb__21_0)).ToList();
		GenerateUI();
	}

	public void ValueSort()
	{
		items = items.OrderBy(_003C_003Ec._003C_003E9__22_0 ?? (_003C_003Ec._003C_003E9__22_0 = _003C_003Ec._003C_003E9._003CValueSort_003Eb__22_0)).ToList();
		GenerateUI();
	}

	public void TypeSort()
	{
		items = items.OrderBy(_003C_003Ec._003C_003E9__23_0 ?? (_003C_003Ec._003C_003E9__23_0 = _003C_003Ec._003C_003E9._003CTypeSort_003Eb__23_0)).ToList();
		GenerateUI();
	}

	public void RaritySort()
	{
		items = items.OrderBy(_003C_003Ec._003C_003E9__24_0 ?? (_003C_003Ec._003C_003E9__24_0 = _003C_003Ec._003C_003E9._003CRaritySort_003Eb__24_0)).ToList();
		GenerateUI();
	}
}
