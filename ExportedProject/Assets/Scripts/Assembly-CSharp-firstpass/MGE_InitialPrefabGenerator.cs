using UnityEngine;

public class MGE_InitialPrefabGenerator : MonoBehaviour
{
	public GameObject[] createThese;

	private GameObject justCreated;

	public int createThisMany;

	public bool parentUnderCreator;

	public float x_rnd;

	public float y_rnd;

	public float z_rnd;

	public float x_rot_rnd;

	public float y_rot_rnd;

	public float z_rot_rnd;

	private void Start()
	{
		for (int i = 0; i < createThisMany; i++)
		{
			justCreated = Object.Instantiate(createThese[Random.Range(0, createThese.Length)], base.transform.position, base.transform.rotation);
			justCreated.transform.Translate(new Vector3(Random.Range(0f - x_rnd, x_rnd), Random.Range(0f - y_rnd, y_rnd), Random.Range(0f - z_rnd, z_rnd)));
			justCreated.transform.Rotate(new Vector3(Random.Range(0f - x_rot_rnd, x_rot_rnd), Random.Range(0f - y_rot_rnd, y_rot_rnd), Random.Range(0f - z_rot_rnd, z_rot_rnd)));
			if (parentUnderCreator)
			{
				justCreated.transform.parent = base.transform;
			}
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.matrix = base.transform.localToWorldMatrix;
		float x = 0.1f + x_rnd * 2f;
		float y = 0.1f + y_rnd * 2f;
		float z = 0.1f + z_rnd * 2f;
		Gizmos.DrawWireCube(Vector3.zero, new Vector3(x, y, z));
		Gizmos.DrawWireCube(new Vector3(0f, 0.25f + y_rnd, 0f), new Vector3(0.1f, 0.5f, 0.1f));
	}
}
