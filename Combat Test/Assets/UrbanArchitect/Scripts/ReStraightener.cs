// ReStraightener
using UnityEngine;

[ExecuteInEditMode]
public class ReStraightener : MonoBehaviour
{
	private void Start()
	{
	}

	private void Update()
	{
		if (base.transform.position != Vector3.zero)
		{
			base.transform.position = Vector3.zero;
		}
	}
}
