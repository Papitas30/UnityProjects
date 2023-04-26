using System;
using UnityEngine;

[Serializable]
public class Element
{
	public string name;

	[Range(1f, 10f)]
	public int density;

	public bool surfaceRotation;

	public float minScale;

	public float maxScale;

	public GameObject[] prefabs;

	public bool CanPlace()
	{
		if (UnityEngine.Random.Range(0, 10) < density)
		{
			return true;
		}
		return false;
	}

	public GameObject GetRandom()
	{
		return prefabs[UnityEngine.Random.Range(0, prefabs.Length)];
	}
}
