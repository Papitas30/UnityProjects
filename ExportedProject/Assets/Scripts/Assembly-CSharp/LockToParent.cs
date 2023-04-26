using UnityEngine;

public class LockToParent : MonoBehaviour
{
	public Transform target;

	private void Update()
	{
		base.transform.position = target.position;
		base.transform.rotation = target.rotation;
	}
}
