using UnityEngine;

public class LootPhysics : MonoBehaviour
{
	private Vector3 initPosition;

	private Rigidbody myRigidBody;

	private void Start()
	{
		myRigidBody = GetComponent<Rigidbody>();
	}

	public void GetInitialPositon()
	{
		initPosition = base.transform.position;
	}

	public void ResetPositon()
	{
		base.transform.position = initPosition;
		if (myRigidBody != null)
		{
			myRigidBody.velocity = Vector3.zero;
			myRigidBody.angularVelocity = Vector3.zero;
		}
	}

	public bool CheckIfSleeping()
	{
		myRigidBody = GetComponent<Rigidbody>();
		if (myRigidBody.IsSleeping())
		{
			return true;
		}
		return false;
	}
}
