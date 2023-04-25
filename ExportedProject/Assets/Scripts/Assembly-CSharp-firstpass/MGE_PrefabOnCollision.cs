using UnityEngine;

public class MGE_PrefabOnCollision : MonoBehaviour
{
	public GameObject createThis;

	private ParticleSystem[] p;

	private void OnCollisionEnter(Collision coll)
	{
		Object.Instantiate(createThis, base.transform.position, base.transform.rotation);
		Object.Destroy(GetComponent<Rigidbody>());
		Object.Destroy(GetComponent<Renderer>());
		p = GetComponentsInChildren<ParticleSystem>();
		ParticleSystem[] array = p;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Stop();
		}
	}
}
