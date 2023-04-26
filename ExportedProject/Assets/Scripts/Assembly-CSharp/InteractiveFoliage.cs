using UnityEngine;

public class InteractiveFoliage : MonoBehaviour
{
	[Header("Shader References")]
	public Material foliageMaterial;

	public Transform player;

	private Vector3 position;

	[Header("Grass Bend Settings")]
	public float bendArea;

	public float bendAmount;

	private void LateUpdate()
	{
		position = player.transform.position;
		foliageMaterial.SetVector("_PlayerPosition", position);
		foliageMaterial.SetFloat("_BendArea", bendArea);
		foliageMaterial.SetFloat("_BendAmount", bendAmount);
	}
}
