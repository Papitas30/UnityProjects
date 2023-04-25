using UnityEngine;

public class MaskObject : MonoBehaviour
{
	public GameObject[] ObjMasked;

	private void Start()
	{
		for (int i = 0; i < ObjMasked.Length; i++)
		{
			ObjMasked[i].GetComponent<MeshRenderer>().material.renderQueue = 3002;
		}
	}
}
