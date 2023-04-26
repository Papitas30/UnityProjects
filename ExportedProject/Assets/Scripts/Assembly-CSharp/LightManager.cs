using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
	public PlayerData playerData;

	public List<GameObject> lights;

	public List<Material> emissions;

	private bool hasChanged;

	private bool toggle;

	private void Update()
	{
		if (playerData.isNight)
		{
			if (hasChanged)
			{
				return;
			}
			foreach (GameObject light in lights)
			{
				light.SetActive(true);
			}
			foreach (Material emission in emissions)
			{
				emission.SetFloat("_Emission", 2f);
			}
			hasChanged = true;
			toggle = true;
		}
		else
		{
			if (!toggle)
			{
				return;
			}
			foreach (GameObject light2 in lights)
			{
				light2.SetActive(false);
			}
			foreach (Material emission2 in emissions)
			{
				emission2.SetFloat("_Emission", 0f);
			}
			toggle = false;
			hasChanged = false;
		}
	}
}
