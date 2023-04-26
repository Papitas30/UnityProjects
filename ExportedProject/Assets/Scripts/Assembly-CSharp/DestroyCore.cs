using UnityEngine;

public class DestroyCore : MonoBehaviour
{
	public GameObject core;

	private void Awake()
	{
		core = GameObject.Find("Core");
		if (core != null)
		{
			Object.Destroy(core);
		}
	}
}
