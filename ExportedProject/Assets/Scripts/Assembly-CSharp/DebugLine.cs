using UnityEngine;

public class DebugLine : MonoBehaviour
{
	private void OnDrawGizmos()
	{
		Debug.DrawLine(base.transform.position, base.transform.position + base.transform.forward * 50f, Color.green);
	}
}
