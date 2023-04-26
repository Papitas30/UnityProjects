using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[ExecuteAlways]
public class CableProceduralMultipoint : MonoBehaviour
{
	[Serializable]
	private class CableSection
	{
		public Transform start;

		public Transform end;

		public float sag;
	}

	private LineRenderer line;

	[SerializeField]
	[Tooltip("Series of points in cable.  If this transform is the desired start, add it to this array.")]
	private CableSection[] cableSections;

	[SerializeField]
	[Tooltip("Number of points per unit length, using the straight line from the start to the end transform.")]
	private float pointDensity = 3f;

	private Vector3 sagDirection;

	private void Start()
	{
		line = GetComponent<LineRenderer>();
		line.positionCount = 0;
		if (cableSections.Length == 0)
		{
			Debug.LogError("No sections assigned to Cable_Procedural component attached to " + base.gameObject.name);
			return;
		}
		sagDirection = Physics.gravity.normalized;
		CableSection[] array = cableSections;
		foreach (CableSection section in array)
		{
			Draw(section);
		}
	}

	private void Draw(CableSection section)
	{
		Vector3 vector = section.end.position - section.start.position;
		section.start.forward = vector.normalized;
		int num = Mathf.FloorToInt(pointDensity * vector.magnitude);
		for (int i = 0; i < num; i++)
		{
			float num2 = (float)i / (float)((num - 1 <= 0) ? 1 : (num - 1));
			float num3 = Mathf.Sin(num2 * MathF.PI);
			Vector3 vector2 = vector * num2;
			Vector3 vector3 = sagDirection * section.sag;
			Vector3 position = section.start.position + vector2 + vector3 * num3;
			line.positionCount++;
			line.SetPosition(line.positionCount - 1, position);
		}
	}
}
