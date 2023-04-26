using UnityEngine;

public class HeadIK : MonoBehaviour
{
	[Header("Tranforms")]
	public Transform targetTransform;

	public Transform aimTransform;

	public Transform bone;

	public Vector3 targetOffset;

	[Header("Settings")]
	public int iterations;

	[Range(0f, 1f)]
	public float weight;

	public float angleLimit;

	public float distanceLimit;

	private Vector3 GetTargetPosition()
	{
		Vector3 vector = targetTransform.position + targetOffset - aimTransform.position;
		Vector3 forward = aimTransform.forward;
		float num = 0f;
		float num2 = Vector3.Angle(vector, forward);
		if (num2 > angleLimit)
		{
			num += (num2 - angleLimit) / 50f;
		}
		float magnitude = vector.magnitude;
		if (magnitude < distanceLimit)
		{
			num += distanceLimit - magnitude;
		}
		Vector3 vector2 = Vector3.Slerp(vector, forward, num);
		return aimTransform.position + vector2;
	}

	private void LateUpdate()
	{
		if (!(aimTransform == null) && !(targetTransform == null))
		{
			Vector3 targetPosition = GetTargetPosition();
			for (int i = 0; i < iterations; i++)
			{
				AimAtTarget(bone, targetPosition, weight);
			}
		}
	}

	private void AimAtTarget(Transform bone, Vector3 targetPosition, float weight)
	{
		Vector3 forward = aimTransform.forward;
		Vector3 toDirection = targetPosition - aimTransform.position;
		Quaternion b = Quaternion.FromToRotation(forward, toDirection);
		Quaternion quaternion = Quaternion.Slerp(Quaternion.identity, b, weight);
		bone.rotation = quaternion * bone.rotation;
	}
}
