using UnityEngine;

public class WeaponWall : MonoBehaviour
{
	private Placer placer;

	private void Start()
	{
		placer = GameObject.FindWithTag("Player").GetComponent<Placer>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Weapon")
		{
			other.GetComponent<Rigidbody>().isKinematic = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Weapon")
		{
			other.GetComponent<Rigidbody>().isKinematic = false;
		}
	}
}
