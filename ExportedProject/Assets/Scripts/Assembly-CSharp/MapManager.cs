using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MapManager : MonoBehaviour
{
	public PlayerData playerData;

	public Transform tp1;

	public GameObject placeholder;

	private Transform player;

	private TimeManager timeManager;

	public Transform[] AISpawns;

	public List<Transform> potentialSpawns;

	public List<Transform> flankPositions;

	public List<Transform> activeAI;

	private ReflectionProbe probe;

	private Headbob headbob;

	public int AICount;

	public int maxAICount;

	public float spawnTimer;

	public bool spawn;

	private void Awake()
	{
		placeholder.SetActive(false);
		if (!playerData.coreSpawned)
		{
			UnityEngine.Object.Instantiate(Resources.Load("Core") as GameObject, Vector3.zero, Quaternion.identity).name = "Core";
			playerData.coreSpawned = true;
		}
	}

	private void Start()
	{
		playerData.cameraNoiseAmplitude = 0f;
		playerData.cameraNoiseFrequency = 0f;
		playerData.health = 100f;
		timeManager = GameObject.FindWithTag("Time Manager").GetComponent<TimeManager>();
		headbob = GameObject.FindWithTag("Player").GetComponent<Headbob>();
		probe = GameObject.FindWithTag("Probe").GetComponent<ReflectionProbe>();
		probe.transform.position = new Vector3(0f, 2f, 0f);
		headbob.ResetHeadbob();
		if (playerData.isDawn)
		{
			timeManager.Dawn();
		}
		if (playerData.isDay)
		{
			timeManager.Day();
		}
		if (playerData.isDusk)
		{
			timeManager.Dusk();
		}
		if (playerData.isNight)
		{
			timeManager.Night();
		}
		player = GameObject.FindWithTag("Player").GetComponent<Transform>();
		player.position = tp1.localPosition;
		player.rotation = tp1.localRotation;
		spawnTimer = UnityEngine.Random.Range(10, 20);
	}

	private void Update()
	{
		if (playerData.spawnAI && maxAICount != 0)
		{
			if (AICount < maxAICount)
			{
				spawnTimer -= Time.deltaTime;
			}
			if (spawnTimer <= 0f)
			{
				spawnTimer = UnityEngine.Random.Range(10, 20);
				PotentialSpawns();
				SpawnAI();
			}
		}
	}

	private void PotentialSpawns()
	{
		potentialSpawns.Clear();
		AISpawns = AISpawns.OrderBy(_003CPotentialSpawns_003Eb__18_0).ToArray();
		Array.Reverse(AISpawns);
		for (int i = 0; i < 3; i++)
		{
			potentialSpawns.Add(AISpawns[i]);
		}
	}

	public void UpdateFlankPositions()
	{
		AISpawns = AISpawns.OrderBy(_003CUpdateFlankPositions_003Eb__19_0).ToArray();
		Array.Reverse(AISpawns);
		for (int i = 0; i < 3; i++)
		{
			flankPositions.Add(AISpawns[i]);
		}
	}

	private void SpawnAI()
	{
		int index = UnityEngine.Random.Range(0, potentialSpawns.Count);
		Transform transform = potentialSpawns[index];
		GameObject gameObject = Resources.Load("AI") as GameObject;
		UnityEngine.Object.Instantiate(gameObject, transform.position, transform.rotation);
		activeAI.Add(gameObject.transform);
		AICount++;
	}

	[CompilerGenerated]
	private float _003CPotentialSpawns_003Eb__18_0(Transform AISpawn)
	{
		return Vector3.Distance(player.position, AISpawn.transform.position);
	}

	[CompilerGenerated]
	private float _003CUpdateFlankPositions_003Eb__19_0(Transform AISpawn)
	{
		return Vector3.Distance(player.position, AISpawn.transform.position);
	}
}
