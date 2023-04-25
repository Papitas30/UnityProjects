using System.Collections.Generic;
using UnityEngine;

public class EnemyIK : MonoBehaviour
{
	[Header("Aim IK Settings")]
	public Transform aimTransform;

	public Transform aimBone;

	public int aimIterations;

	[Range(0f, 1f)]
	public float aimWeight;

	public float aimAngleLimit;

	public float aimDistanceLimit;

	public Vector3 aimTargetOffset;

	[Header("Hit IK Settings")]
	public Transform hitTransform;

	public Transform hitBone;

	public int hitIterations;

	[Range(0f, 1f)]
	public float hitWeight;

	public float hitAngleLimit;

	public float hitDistanceLimit;

	public Vector3 hitTargetOffset;

	private int nextHitPoint;

	public Transform demoHit;

	public Vector3 aimTarget;

	public Vector3 IKHitTarget;

	private AI AI;

	public List<Transform> hitpoints;

	private void Start()
	{
		AI = GetComponent<AI>();
	}

	private void Awake()
	{
		aimWeight = 0f;
	}

	private Vector3 GetTargetPosition()
	{
		Vector3 vector = aimTarget + aimTargetOffset - aimTransform.position;
		Vector3 forward = aimTransform.forward;
		float num = 0f;
		float num2 = Vector3.Angle(vector, forward);
		if (num2 > aimAngleLimit)
		{
			num += (num2 - aimAngleLimit) / 50f;
		}
		float magnitude = vector.magnitude;
		if (magnitude < aimDistanceLimit)
		{
			num += aimDistanceLimit - magnitude;
		}
		Vector3 vector2 = Vector3.Slerp(vector, forward, num);
		return aimTransform.position + vector2;
	}

	private Vector3 GetHitPosition()
	{
		Vector3 vector = IKHitTarget + hitTargetOffset - hitTransform.position;
		Vector3 forward = hitTransform.forward;
		float num = 0f;
		float num2 = Vector3.Angle(vector, forward);
		if (num2 > hitAngleLimit)
		{
			num += (num2 - hitAngleLimit) / 50f;
		}
		float magnitude = vector.magnitude;
		if (magnitude < hitDistanceLimit)
		{
			num += hitDistanceLimit - magnitude;
		}
		Vector3 vector2 = Vector3.Slerp(vector, forward, num);
		return hitTransform.position + vector2;
	}

	private void LateUpdate()
	{
		if (!(aimTransform == null))
		{
			Vector3 aimTarget2 = aimTarget;
			Vector3 targetPosition = GetTargetPosition();
			Vector3 hitPosition = GetHitPosition();
			for (int i = 0; i < aimIterations; i++)
			{
				AimAtTarget(aimBone, targetPosition, aimWeight);
				HitAtTarget(hitBone, hitPosition, hitWeight);
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

	private void HitAtTarget(Transform bone, Vector3 targetPosition, float weight)
	{
		Vector3 forward = hitTransform.forward;
		Vector3 toDirection = targetPosition - hitTransform.position;
		Quaternion b = Quaternion.FromToRotation(forward, toDirection);
		Quaternion quaternion = Quaternion.Slerp(Quaternion.identity, b, weight);
		bone.rotation = quaternion * bone.rotation;
	}

	public void ChangeHitPoint(Transform hitTarget)
	{
		float x = Random.Range(0f, 0.25f);
		IKHitTarget = hitTarget.position + new Vector3(x, 0f, 0f);
	}
}
