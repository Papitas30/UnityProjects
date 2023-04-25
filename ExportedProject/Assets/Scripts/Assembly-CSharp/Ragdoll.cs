using UnityEngine;

public class Ragdoll : MonoBehaviour
{
	public EnemyIK weaponIK;

	public Rigidbody weaponRb;

	public Transform weapon;

	public Collider weaponCollider;

	public GameObject rig;

	public Collider[] ragdollColliders;

	public Rigidbody[] ragdollRigidbodys;

	public Vector3 hitPosition;

	public Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();
		GetRagdoll();
		RagdollModeOff();
	}

	private void GetRagdoll()
	{
		ragdollColliders = rig.GetComponentsInChildren<Collider>();
		ragdollRigidbodys = rig.GetComponentsInChildren<Rigidbody>();
	}

	public void RagdollModeOn(Vector3 force, Vector3 hitPoint)
	{
		Rigidbody[] array = ragdollRigidbodys;
		foreach (Rigidbody obj in array)
		{
			obj.isKinematic = false;
			obj.useGravity = true;
			obj.constraints = RigidbodyConstraints.None;
			FindHit(hitPoint).AddForceAtPosition(force * 5f, hitPoint, ForceMode.Impulse);
		}
		weapon.SetParent(null);
		weaponCollider.enabled = true;
		weaponRb.useGravity = true;
		weaponIK.enabled = false;
		animator.enabled = false;
	}

	private Rigidbody FindHit(Vector3 hitPoint)
	{
		Rigidbody rigidbody = null;
		float num = 0f;
		Rigidbody[] array = ragdollRigidbodys;
		foreach (Rigidbody rigidbody2 in array)
		{
			float num2 = Vector3.Distance(rigidbody2.position, hitPoint);
			if (rigidbody == null || num2 < num)
			{
				num = num2;
				rigidbody = rigidbody2;
			}
		}
		return rigidbody;
	}

	public void RagdollModeOff()
	{
		Rigidbody[] array = ragdollRigidbodys;
		foreach (Rigidbody obj in array)
		{
			obj.isKinematic = true;
			obj.useGravity = false;
			obj.constraints = RigidbodyConstraints.FreezeAll;
		}
		weaponCollider.enabled = false;
		weaponRb.useGravity = false;
		weaponIK.enabled = true;
		animator.enabled = true;
	}
}
