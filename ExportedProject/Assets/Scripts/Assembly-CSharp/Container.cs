using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Container : MonoBehaviour
{
	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static Func<ItemData, string> _003C_003E9__41_0;

		public static Func<ItemData, float> _003C_003E9__42_0;

		public static Func<ItemData, float> _003C_003E9__43_0;

		public static Func<ItemData, float> _003C_003E9__44_0;

		public static Func<ItemData, ItemData.ItemType> _003C_003E9__45_0;

		public static Func<ItemData, ItemData.ItemRarity> _003C_003E9__46_0;

		internal string _003CNameSort_003Eb__41_0(ItemData go)
		{
			return go.itemName;
		}

		internal float _003CVolumeSort_003Eb__42_0(ItemData go)
		{
			return go.volume;
		}

		internal float _003CWeightSort_003Eb__43_0(ItemData go)
		{
			return go.weight;
		}

		internal float _003CValueSort_003Eb__44_0(ItemData go)
		{
			return go.value;
		}

		internal ItemData.ItemType _003CTypeSort_003Eb__45_0(ItemData go)
		{
			return go.type;
		}

		internal ItemData.ItemRarity _003CRaritySort_003Eb__46_0(ItemData go)
		{
			return go.rarity;
		}
	}

	[Header("Container Name")]
	public string containerName;

	[Header("Container Limits")]
	public float maxVolume;

	public float maxWeight;

	private float maxAmount = 42f;

	[HideInInspector]
	public float volume;

	[HideInInspector]
	public float weight;

	[HideInInspector]
	public float value;

	[HideInInspector]
	public int amount;

	[Header("Loot Tables")]
	[SerializeField]
	private LootTable lootTable;

	[Header("Loot Buckets")]
	public List<ItemData> commonBucket;

	public List<ItemData> uncommonBucket;

	public List<ItemData> rareBucket;

	public List<ItemData> extremelyRareBucket;

	[Header("Rarity Roll")]
	public int rarityRoll;

	[Header("Amount Rolls")]
	[SerializeField]
	private int commonPicks;

	[SerializeField]
	private int uncommonPicks;

	[SerializeField]
	private int rarePicks;

	[SerializeField]
	private int extremelyRarePicks;

	[Header("Generated Items")]
	public List<ItemData> items;

	private UIManager UIManager;

	private EventManager eventManager;

	private AudioManager audioManager;

	private Transform containerSlots;

	public bool AIContainer;

	public bool groundCache;

	public AudioEvent containerAudio;

	private float currentAmount;

	private float currentVolume;

	private float currentWeight;

	private void Start()
	{
		eventManager = GameObject.FindWithTag("Event Manager").GetComponent<EventManager>();
		audioManager = GameObject.FindWithTag("Audio Manager").GetComponent<AudioManager>();
		UIManager = GameObject.FindWithTag("UI Manager").GetComponent<UIManager>();
		containerSlots = UIManager.containerSlots;
		CalculateStats();
		if (AIContainer)
		{
			Generate();
		}
	}

	public void Generate()
	{
		if (!groundCache)
		{
			amount = 0;
			ClearItems();
			GenerateBuckets();
			Roll();
			GenerateLoot();
			CalculateStats();
		}
	}

	public void GenerateBuckets()
	{
		foreach (ItemData item in lootTable.itemList)
		{
			if (item.rarity == ItemData.ItemRarity.Common)
			{
				commonBucket.Add(item);
			}
			if (item.rarity == ItemData.ItemRarity.Uncommon)
			{
				uncommonBucket.Add(item);
			}
			if (item.rarity == ItemData.ItemRarity.Rare)
			{
				rareBucket.Add(item);
			}
			if (item.rarity == ItemData.ItemRarity.ExtremelyRare)
			{
				extremelyRareBucket.Add(item);
			}
		}
	}

	public void Roll()
	{
		rarityRoll = UnityEngine.Random.Range(0, 100);
	}

	public void GenerateLoot()
	{
		if (rarityRoll <= 1)
		{
			extremelyRarePicks = 1;
			for (int i = 0; i < extremelyRarePicks; i++)
			{
				if (volume >= maxVolume && weight >= maxWeight && (float)amount >= maxAmount)
				{
					break;
				}
				int index = UnityEngine.Random.Range(0, extremelyRareBucket.Count);
				items.Add(extremelyRareBucket[index]);
				volume += extremelyRareBucket[index].volume;
				weight += extremelyRareBucket[index].weight;
				value += extremelyRareBucket[index].value;
				amount++;
			}
		}
		if (rarityRoll <= 5 && rarityRoll > 1)
		{
			rarePicks = UnityEngine.Random.Range(1, 3);
			for (int j = 0; j < rarePicks; j++)
			{
				if (volume >= maxVolume && weight >= maxWeight && (float)amount >= maxAmount)
				{
					break;
				}
				int index2 = UnityEngine.Random.Range(0, rareBucket.Count);
				items.Add(rareBucket[index2]);
				volume += rareBucket[index2].volume;
				weight += rareBucket[index2].weight;
				value += rareBucket[index2].value;
				amount++;
			}
		}
		if (rarityRoll <= 10)
		{
			uncommonPicks = UnityEngine.Random.Range(1, 4);
			for (int k = 0; k < uncommonPicks; k++)
			{
				if (volume >= maxVolume && weight >= maxWeight && (float)amount >= maxAmount)
				{
					break;
				}
				int index3 = UnityEngine.Random.Range(0, uncommonBucket.Count);
				items.Add(uncommonBucket[index3]);
				volume += uncommonBucket[index3].volume;
				weight += uncommonBucket[index3].weight;
				value += uncommonBucket[index3].value;
				amount++;
			}
		}
		if (rarityRoll > 25)
		{
			return;
		}
		commonPicks = UnityEngine.Random.Range(1, 4);
		for (int l = 0; l < commonPicks; l++)
		{
			if (volume >= maxVolume && weight >= maxWeight && (float)amount >= maxAmount)
			{
				break;
			}
			int index4 = UnityEngine.Random.Range(0, commonBucket.Count);
			items.Add(commonBucket[index4]);
			volume += commonBucket[index4].volume;
			weight += commonBucket[index4].weight;
			value += commonBucket[index4].value;
			amount++;
		}
	}

	public void Add(ItemData item, int usage)
	{
		items.Add(item);
		volume += item.volume;
		weight += item.weight;
		value += item.value;
		amount++;
		GameObject obj = UnityEngine.Object.Instantiate(item.slot);
		obj.GetComponent<Slot>().itemData = item;
		obj.GetComponent<Slot>().slotType = Slot.SlotType.ContainerSlot;
		obj.transform.localScale = new Vector3(0.9f, 0.9f);
		obj.transform.SetParent(containerSlots, false);
		obj.name = item.name;
		obj.GetComponent<Slot>().UpdateUsage(usage);
		UIManager.UpdateContainerStats();
	}

	public void Remove(ItemData item)
	{
		items.Remove(item);
		amount--;
		volume -= item.volume;
		weight -= item.weight;
		value -= item.value;
		UIManager.UpdateContainerStats();
	}

	public void ClearItems()
	{
		items.Clear();
		commonBucket.Clear();
		uncommonBucket.Clear();
		rareBucket.Clear();
		extremelyRareBucket.Clear();
	}

	private void GenerateUI()
	{
		foreach (Transform containerSlot in containerSlots)
		{
			UnityEngine.Object.Destroy(containerSlot.gameObject);
		}
		for (int i = 0; i < items.Count; i++)
		{
			GameObject obj = UnityEngine.Object.Instantiate(items[i].slot);
			obj.GetComponent<Slot>().itemData = items[i];
			obj.GetComponent<Slot>().slotType = Slot.SlotType.ContainerSlot;
			obj.transform.localScale = new Vector3(0.9f, 0.9f);
			obj.transform.SetParent(containerSlots, false);
			obj.name = items[i].itemName;
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

	public void OpenContainer()
	{
		UIManager.container = this;
		GenerateUI();
		eventManager.openContainerUI.Raise();
		audioManager.PlayOpenContainer(containerAudio);
	}

	public void CalculateStats()
	{
		volume = 0f;
		foreach (ItemData item in items)
		{
			volume += item.volume;
		}
		weight = 0f;
		foreach (ItemData item2 in items)
		{
			weight += item2.weight;
		}
		value = 0f;
		foreach (ItemData item3 in items)
		{
			value += item3.value;
		}
	}

	public void NameSort()
	{
		items = items.OrderBy(_003C_003Ec._003C_003E9__41_0 ?? (_003C_003Ec._003C_003E9__41_0 = _003C_003Ec._003C_003E9._003CNameSort_003Eb__41_0)).ToList();
		GenerateUI();
	}

	public void VolumeSort()
	{
		items = items.OrderBy(_003C_003Ec._003C_003E9__42_0 ?? (_003C_003Ec._003C_003E9__42_0 = _003C_003Ec._003C_003E9._003CVolumeSort_003Eb__42_0)).ToList();
		GenerateUI();
	}

	public void WeightSort()
	{
		items = items.OrderBy(_003C_003Ec._003C_003E9__43_0 ?? (_003C_003Ec._003C_003E9__43_0 = _003C_003Ec._003C_003E9._003CWeightSort_003Eb__43_0)).ToList();
		GenerateUI();
	}

	public void ValueSort()
	{
		items = items.OrderBy(_003C_003Ec._003C_003E9__44_0 ?? (_003C_003Ec._003C_003E9__44_0 = _003C_003Ec._003C_003E9._003CValueSort_003Eb__44_0)).ToList();
		GenerateUI();
	}

	public void TypeSort()
	{
		items = items.OrderBy(_003C_003Ec._003C_003E9__45_0 ?? (_003C_003Ec._003C_003E9__45_0 = _003C_003Ec._003C_003E9._003CTypeSort_003Eb__45_0)).ToList();
		GenerateUI();
	}

	public void RaritySort()
	{
		items = items.OrderBy(_003C_003Ec._003C_003E9__46_0 ?? (_003C_003Ec._003C_003E9__46_0 = _003C_003Ec._003C_003E9._003CRaritySort_003Eb__46_0)).ToList();
		GenerateUI();
	}
}
