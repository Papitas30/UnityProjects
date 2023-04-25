using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LootSimulation : MonoBehaviour
{
	private struct SimulatedBody
	{
		public readonly Rigidbody rigidbody;

		public readonly bool isChild;

		private readonly Vector3 originalPosition;

		private readonly Quaternion originalRotation;

		private readonly Transform transform;

		public SimulatedBody(Rigidbody rigidbody, bool isChild)
		{
			this.rigidbody = rigidbody;
			this.isChild = isChild;
			rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
			transform = rigidbody.transform;
			originalPosition = rigidbody.position;
			originalRotation = rigidbody.rotation;
		}

		public void Reset()
		{
			transform.position = originalPosition;
			transform.rotation = originalRotation;
			if (rigidbody != null)
			{
				rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
				rigidbody.velocity = Vector3.zero;
				rigidbody.angularVelocity = Vector3.zero;
			}
		}
	}

	[Serializable]
	[CompilerGenerated]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

		public static Func<SimulatedBody, bool> _003C_003E9__28_1;

		internal bool _003CRunSimulation_003Eb__28_1(SimulatedBody body)
		{
			if (!body.rigidbody.IsSleeping())
			{
				return !body.isChild;
			}
			return true;
		}
	}

	[HideInInspector]
	public int maxIterations = 1000;

	[HideInInspector]
	public float simulationSize = 10f;

	[HideInInspector]
	public float radius = 5f;

	[HideInInspector]
	public float power = 10f;

	[HideInInspector]
	public float modifier;

	[HideInInspector]
	public int maxAmount;

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

	private SimulatedBody[] simulatedBodies;

	private List<Rigidbody> generatedRigidbodies;

	private List<Collider> generatedColliders;

	public void Generate()
	{
		amount = 0;
		Clear();
		GenerateBuckets();
		Roll();
		GenerateLoot();
		Spawn();
	}

	public void Auto()
	{
		Generate();
		RunSimulation();
	}

	public void Clear()
	{
		for (int num = base.transform.childCount; num > 0; num--)
		{
			UnityEngine.Object.DestroyImmediate(base.transform.GetChild(0).gameObject);
		}
		items.Clear();
		commonBucket.Clear();
		uncommonBucket.Clear();
		rareBucket.Clear();
		extremelyRareBucket.Clear();
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
				if (amount >= maxAmount)
				{
					break;
				}
				int index = UnityEngine.Random.Range(0, extremelyRareBucket.Count);
				items.Add(extremelyRareBucket[index]);
				amount++;
			}
		}
		if (rarityRoll <= 5 && rarityRoll > 1)
		{
			rarePicks = UnityEngine.Random.Range(1, 3);
			for (int j = 0; j < rarePicks; j++)
			{
				if (amount >= maxAmount)
				{
					break;
				}
				int index2 = UnityEngine.Random.Range(0, rareBucket.Count);
				items.Add(rareBucket[index2]);
				amount++;
			}
		}
		if (rarityRoll <= 10)
		{
			uncommonPicks = UnityEngine.Random.Range(1, 4);
			for (int k = 0; k < uncommonPicks; k++)
			{
				if (amount >= maxAmount)
				{
					break;
				}
				int index3 = UnityEngine.Random.Range(0, uncommonBucket.Count);
				items.Add(uncommonBucket[index3]);
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
			if (amount >= maxAmount)
			{
				break;
			}
			int index4 = UnityEngine.Random.Range(0, commonBucket.Count);
			items.Add(commonBucket[index4]);
			amount++;
		}
	}

	public void Spawn()
	{
		for (int i = 0; i < items.Count; i++)
		{
			GameObject obj = UnityEngine.Object.Instantiate(items[i].pickup);
			Vector3 position = RandomPointInBox(base.transform.position, new Vector3(radius, 0f, radius));
			Vector3 eulerAngles = new Vector3(UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(0f, 360f));
			obj.transform.SetParent(base.transform);
			obj.transform.position = position;
			obj.transform.eulerAngles = eulerAngles;
		}
	}

	public void RunSimulation()
	{
		AutoGenerateComponents();
		simulatedBodies = UnityEngine.Object.FindObjectsOfType<Rigidbody>().Select(_003CRunSimulation_003Eb__28_0).ToArray();
		Physics.autoSimulation = false;
		for (int i = 0; i < maxIterations; i++)
		{
			Vector3 position = base.transform.position;
			Collider[] array = Physics.OverlapSphere(position, radius);
			for (int j = 0; j < array.Length; j++)
			{
				Rigidbody component = array[j].GetComponent<Rigidbody>();
				if (component != null)
				{
					component.AddExplosionForce(power, position, radius, modifier);
				}
			}
			Physics.Simulate(Time.fixedDeltaTime);
			if (simulatedBodies.All(_003C_003Ec._003C_003E9__28_1 ?? (_003C_003Ec._003C_003E9__28_1 = _003C_003Ec._003C_003E9._003CRunSimulation_003Eb__28_1)))
			{
				break;
			}
		}
		Physics.autoSimulation = true;
		SimulatedBody[] array2 = simulatedBodies;
		for (int j = 0; j < array2.Length; j++)
		{
			SimulatedBody simulatedBody = array2[j];
			if (!simulatedBody.isChild)
			{
				simulatedBody.Reset();
			}
		}
		RemoveAutoGeneratedComponents();
	}

	private void AutoGenerateComponents()
	{
		generatedRigidbodies = new List<Rigidbody>();
		generatedColliders = new List<Collider>();
		foreach (Transform item in base.transform)
		{
			if (!item.GetComponent<Rigidbody>())
			{
				generatedRigidbodies.Add(item.gameObject.AddComponent<Rigidbody>());
			}
			if (!item.GetComponent<Collider>())
			{
				generatedColliders.Add(item.gameObject.AddComponent<BoxCollider>());
			}
		}
	}

	private void RemoveAutoGeneratedComponents()
	{
		foreach (Rigidbody generatedRigidbody in generatedRigidbodies)
		{
			UnityEngine.Object.DestroyImmediate(generatedRigidbody);
		}
		foreach (Collider generatedCollider in generatedColliders)
		{
			UnityEngine.Object.DestroyImmediate(generatedCollider);
		}
	}

	public void ResetAllBodies()
	{
		if (simulatedBodies != null)
		{
			SimulatedBody[] array = simulatedBodies;
			foreach (SimulatedBody simulatedBody in array)
			{
				simulatedBody.Reset();
			}
		}
	}

	private static Vector3 RandomPointInBox(Vector3 center, Vector3 size)
	{
		return center + new Vector3((UnityEngine.Random.value - 0.5f) * size.x, (UnityEngine.Random.value - 0.5f) * size.y, (UnityEngine.Random.value - 0.5f) * size.z);
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(base.transform.position, radius);
	}

	[CompilerGenerated]
	private SimulatedBody _003CRunSimulation_003Eb__28_0(Rigidbody rb)
	{
		return new SimulatedBody(rb, rb.transform.IsChildOf(base.transform));
	}
}
