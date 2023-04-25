using UnityEngine;

public class Barricade : MonoBehaviour
{
	private Material[] materials;

	private MeshRenderer[] childRenderers;

	public void Randomize(Material[] barricadeMaterials)
	{
		materials = barricadeMaterials;
		childRenderers = GetComponentsInChildren<MeshRenderer>();
		int num = Random.Range(0, 2);
		MeshRenderer[] array = childRenderers;
		foreach (MeshRenderer meshRenderer in array)
		{
			meshRenderer.material = materials[Random.Range(0, materials.Length)];
			if (num == 1)
			{
				meshRenderer.enabled = false;
				meshRenderer.transform.GetComponent<BoxCollider>().enabled = false;
			}
			else
			{
				meshRenderer.enabled = true;
				meshRenderer.transform.GetComponent<BoxCollider>().enabled = true;
			}
		}
	}

	public void ShowAllBarricades()
	{
		childRenderers = GetComponentsInChildren<MeshRenderer>();
		MeshRenderer[] array = childRenderers;
		foreach (MeshRenderer obj in array)
		{
			obj.enabled = true;
			obj.transform.GetComponent<BoxCollider>().enabled = true;
		}
	}
}
