using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[ExecuteAlways]
public class CableProceduralCurve : MonoBehaviour
{
	private LineRenderer line;

	[SerializeField]
	private Transform endPointTransform;

	[SerializeField]
	[Tooltip("Number of points per unit length, using the straight line from the start to the end transform.")]
	private float pointDensity = 3f;

	[SerializeField]
	[Tooltip("Positive keys are applied downward on cable.  Recommended use is to have keys at 0 at the start and end of the curve.")]
	private AnimationCurve curve;

	private int pointsInLineRenderer;

	private Vector3 vectorFromStartToEnd;

	private Vector3 sagDirection;

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
		Draw();
	}

	private void Draw()
	{
		for (int i = 0; i < pointsInLineRenderer; i++)
		{
			float num = (float)i / (float)(pointsInLineRenderer - 1);
			Vector3 vector = vectorFromStartToEnd * num;
			Vector3 vector2 = sagDirection * (0f - curve.Evaluate(num));
			Vector3 position = base.transform.position + vector + vector2;
			line.SetPosition(i, position);
		}
	}
}
