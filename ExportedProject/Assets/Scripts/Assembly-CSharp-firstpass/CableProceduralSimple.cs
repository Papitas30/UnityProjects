using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[ExecuteAlways]
public class CableProceduralSimple : MonoBehaviour
{
	private LineRenderer line;

	[SerializeField]
	private Transform endPointTransform;

	[SerializeField]
	[Tooltip("Number of points per unit length, using the straight line from the start to the end transform.")]
	private float pointDensity = 3f;

	[SerializeField]
	private float sagAmplitude = 1f;

	[SerializeField]
	private float swayMultiplier = 1f;

	[SerializeField]
	private float swayXMultiplier = 1f;

	[SerializeField]
	private float swayYMultiplier = 0.5f;

	[SerializeField]
	private float swayFrequency = 1f;

	private int pointsInLineRenderer;

	private Vector3 vectorFromStartToEnd;

	private Vector3 sagDirection;

	private float swayValue;

	private void Start()
	{
		line = GetComponent<LineRenderer>();
		if (!endPointTransform)
		{
			Debug.LogError("No Endpoint Transform assigned to Cable_Procedural component attached to " + base.gameObject.name);
			return;
		}
		vectorFromStartToEnd = endPointTransform.position - base.transform.position;
		base.transform.forward = vectorFromStartToEnd.normalized;
		pointsInLineRenderer = Mathf.FloorToInt(pointDensity * vectorFromStartToEnd.magnitude);
		line.positionCount = pointsInLineRenderer;
		sagDirection = Physics.gravity.normalized;
	}

	private void Update()
	{
		Draw();
	}

	private void Draw()
	{
		if ((bool)endPointTransform)
		{
			int i = 0;
			swayValue += swayFrequency * Time.deltaTime;
			if (swayValue > MathF.PI * 2f)
			{
				swayValue = 0f;
			}
			if (swayValue < 0f)
			{
				swayValue = MathF.PI * 2f;
			}
			for (; i < pointsInLineRenderer; i++)
			{
				float num = (float)i / (float)(pointsInLineRenderer - 1);
				float num2 = Mathf.Sin(num * MathF.PI);
				Vector3 vector = vectorFromStartToEnd * num;
				Vector3 vector2 = sagDirection * sagAmplitude;
				Vector3 vector3 = swayMultiplier * base.transform.TransformDirection(new Vector3(Mathf.Sin(swayValue) * swayXMultiplier, Mathf.Cos(2f * swayValue + MathF.PI) * 0.5f * swayYMultiplier, 0f));
				Vector3 position = base.transform.position + vector + (vector3 + Vector3.ClampMagnitude(vector2, sagAmplitude)) * num2;
				line.SetPosition(i, position);
			}
		}
	}
}
