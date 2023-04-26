using UnityEngine;

public class Prone : MonoBehaviour
{
	private enum CrouchState
	{
		Down = 0,
		Up = 1
	}

	[Header("References")]
	public PlayerData playerData;

	public InputManager inputManager;

	public CharacterController controller;

	public Transform pelvis;

	public Transform head;

	public Transform weapon;

	private float crouchColliderHeight = 0.75f;

	private float crouchPelvisHeight = -0.25f;

	[SerializeField]
	private float crouchSpeed = 2f;

	[Header("Crouch Bob")]
	public float crouchBob = 2f;

	public float crouchRecovery = 10f;

	public Vector3 crouchBobBehavior = new Vector3(-10f, 4f, 4f);

	public Vector3 crouchLandBobBehavior = new Vector3(-10f, 4f, 4f);

	private Vector3 currentRotation;

	private Vector3 finalRotation;

	private float originalPelvisHeight;

	private float originalControllerHeight;

	private float originalControllerCenter;

	private CrouchState crouchState;

	private Vector3 zeroRotation = new Vector3(-0.0001f, -0.0001f, -0.0001f);

	private AudioManager audioManager;

	private void Start()
	{
		audioManager = GameObject.FindWithTag("Audio Manager").GetComponent<AudioManager>();
		originalPelvisHeight = pelvis.localPosition.y;
		originalControllerHeight = controller.height;
		originalControllerCenter = controller.center.y;
		CheckCrouch();
	}

	private void Update()
	{
		CheckCrouch();
		currentRotation = Vector3.Lerp(currentRotation, zeroRotation, crouchRecovery * Time.deltaTime);
		finalRotation = Vector3.Slerp(finalRotation, currentRotation, crouchBob * Time.fixedDeltaTime);
		head.localRotation = Quaternion.Euler(finalRotation);
		weapon.localRotation = Quaternion.Euler(finalRotation);
	}

	private void CheckCrouch()
	{
		if (playerData.isProning)
		{
			crouchState = CrouchState.Down;
		}
		else
		{
			crouchState = CrouchState.Up;
		}
		SetCrouch();
	}

	private void SetCrouch()
	{
		switch (crouchState)
		{
		case CrouchState.Down:
			CrouchDown();
			break;
		case CrouchState.Up:
			StandUp();
			break;
		}
	}

	private void CrouchDown()
	{
		if (controller.height != crouchColliderHeight)
		{
			JumpKick();
			controller.height = Mathf.MoveTowards(controller.height, crouchColliderHeight, Time.deltaTime * crouchSpeed);
		}
		if (controller.center.y != originalControllerCenter - crouchColliderHeight * 0.25f)
		{
			controller.center = new Vector3(controller.center.x, originalControllerCenter - (originalControllerHeight - controller.height) * 0.5f, controller.center.z);
		}
		if (pelvis.position.y != crouchColliderHeight)
		{
			pelvis.localPosition = new Vector3(pelvis.localPosition.x, Mathf.MoveTowards(pelvis.localPosition.y, crouchPelvisHeight, Time.deltaTime * crouchSpeed), pelvis.localPosition.z);
		}
	}

	private void StandUp()
	{
		if (controller.height != originalControllerHeight)
		{
			LandKick();
			controller.height = Mathf.MoveTowards(controller.height, originalControllerHeight, Time.deltaTime * crouchSpeed);
		}
		if (controller.center.y != originalControllerCenter)
		{
			controller.center = new Vector3(controller.center.x, originalControllerCenter - (originalControllerHeight - controller.height) * 0.5f, controller.center.z);
		}
		if (pelvis.transform.localPosition.y != originalPelvisHeight)
		{
			LandKick();
			pelvis.transform.localPosition = new Vector3(pelvis.transform.localPosition.x, Mathf.MoveTowards(pelvis.transform.localPosition.y, originalPelvisHeight, Time.deltaTime * crouchSpeed / 2f), pelvis.transform.localPosition.z);
		}
	}

	public void JumpKick()
	{
		currentRotation += new Vector3(crouchBobBehavior.x, crouchBobBehavior.y, crouchBobBehavior.z);
	}

	public void LandKick()
	{
		currentRotation += new Vector3(crouchLandBobBehavior.x, Random.Range(0f - crouchLandBobBehavior.y, crouchLandBobBehavior.y), Random.Range(0f - crouchLandBobBehavior.z, crouchLandBobBehavior.z));
	}
}
