using System.Collections;
using UnityEngine;

public class Placeable : MonoBehaviour
{
	public float waitOnPickup = 0.2f;

	public float breakForce = 35f;

	[HideInInspector]
	public bool pickedUp;

	[HideInInspector]
	public Placer placer;

	private void OnCollisionEnter(Collision collision)
	{
		if (pickedUp)
		{
			if (collision.gameObject.tag == "Weapon")
			{
				GetComponent<Rigidbody>().isKinematic = true;
			}
			placer.BreakConnection();
		}
	}

	public IEnumerator PickUp()
	{
		yield return new WaitForSecondsRealtime(waitOnPickup);
		GetComponent<Rigidbody>().isKinematic = false;
		pickedUp = true;
	}
}
