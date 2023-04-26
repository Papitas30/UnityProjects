using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LootManager : MonoBehaviour
{
	public List<Container> containers;

	public List<LootSimulation> simulations;

	public List<Barricade> barricades;

	public Material[] barricadeMaterials;

	public bool sandbox;

	[Header("Ground Cache")]
	public Transform cacheSpawner;

	public Transform cache;

	private AudioManager audioManager;

	private void Start()
	{
		if (!sandbox)
		{
			SelectCache();
			GetAllContainers();
			GetAllBarricades();
			GetAllSimulation();
		}
		GenerateLoot();
	}

	public void SelectCache()
	{
		cacheSpawner.GetComponent<Spawner>().Clear();
		cacheSpawner.GetComponent<Spawner>().Spawn();
		int index = Random.Range(0, cacheSpawner.childCount);
		cache = cacheSpawner.GetChild(index);
		cache.SetParent(base.transform);
		cacheSpawner.GetComponent<Spawner>().Clear();
		cache.SetParent(cacheSpawner);
	}

	public void GetAllContainers()
	{
		containers.Clear();
		Container[] array = Object.FindObjectsOfType<Container>();
		for (int i = 0; i < array.Length; i++)
		{
			Container component = array[i].GetComponent<Container>();
			if (component != null)
			{
				containers.Add(component);
			}
		}
	}

	public void GetAllSimulation()
	{
		simulations.Clear();
		LootSimulation[] array = Object.FindObjectsOfType<LootSimulation>();
		for (int i = 0; i < array.Length; i++)
		{
			LootSimulation component = array[i].GetComponent<LootSimulation>();
			if (component != null)
			{
				simulations.Add(component);
			}
		}
	}

	public void GetAllBarricades()
	{
		barricades.Clear();
		Barricade[] array = Object.FindObjectsOfType<Barricade>();
		for (int i = 0; i < array.Length; i++)
		{
			Barricade component = array[i].GetComponent<Barricade>();
			if (component != null)
			{
				barricades.Add(component);
			}
		}
	}

	public void GenerateLoot()
	{
		foreach (Container container in containers)
		{
			container.Generate();
		}
		foreach (LootSimulation simulation in simulations)
		{
			simulation.Auto();
		}
		foreach (Barricade barricade in barricades)
		{
			barricade.Randomize(barricadeMaterials);
		}
	}

	public void ClearLoot()
	{
		foreach (Container container in containers)
		{
			container.ClearItems();
		}
		foreach (LootSimulation simulation in simulations)
		{
			simulation.Clear();
		}
		foreach (Barricade barricade in barricades)
		{
			barricade.ShowAllBarricades();
		}
	}
}
