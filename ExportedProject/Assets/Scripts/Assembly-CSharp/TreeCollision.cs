using UnityEngine;

public class TreeCollision : MonoBehaviour
{
	public Controller controller;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Tree")
		{
			Debug.Log("ENTER");
			controller.touch = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Tree")
		{
			Debug.Log("EXIT");
			controller.touch = false;
		}
	}
}
