using UnityEngine;

public class EnemyHit : MonoBehaviour
{
	public enum HitTarget
	{
		Head = 0,
		Torso = 1,
		Arms = 2,
		Legs = 3
	}

	[Header("Target")]
	public HitTarget hitTarget;

	public Transform IKTarget;

	private AI AI;

	private void Start()
	{
		AI = base.transform.root.GetComponent<AI>();
	}

	public void Hit(float damageMultiplier, float impactForce, Vector3 hitDirection, Vector3 hitPoint)
	{
		if (hitTarget == HitTarget.Head)
		{
			AI.ApplyDamage(100f, impactForce, hitDirection, hitPoint, IKTarget);
		}
		if (hitTarget == HitTarget.Torso)
		{
			AI.ApplyDamage(damageMultiplier, impactForce, hitDirection, hitPoint, IKTarget);
		}
		if (hitTarget == HitTarget.Arms)
		{
			AI.ApplyDamage(damageMultiplier, impactForce, hitDirection, hitPoint, IKTarget);
		}
		if (hitTarget == HitTarget.Legs)
		{
			AI.ApplyDamage(damageMultiplier, impactForce, hitDirection, hitPoint, IKTarget);
		}
	}
}
