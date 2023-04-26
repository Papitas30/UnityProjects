using UnityEngine;

public class Crouch : MonoBehaviour
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

	private float crouchColliderHeight = 1.5f;

	private float crouchPelvisHeight = 0.5f;

	[SerializeField]
	private float crouchSpeed = 1.25f;

	private float originalPelvisHeight;

	private float originalControllerHeight;

	private float originalControllerCenter;

	private CrouchState crouchState;

	private Vector3 currentRotation;

	private Vector3 targetRotation;

	public float recoilX;

	public float recoilY;

	public float recoilZ;

	public float snappiness;

	public float returnSpeed;

	public bool recoil;

	public LayerMask standLayers;

	private Transform mainCamera;

	private void Start()
	{
		mainCamera = Camera.main.transform;
		originalPelvisHeight = pelvis.localPosition.y;
		originalControllerHeight = controller.height;
		originalControllerCenter = controller.center.y;
		playerData.canStand = true;
	}

	private void Update()
	{
		if (playerData.isCrouching)
		{
			CanStand();
			crouchState = CrouchState.Down;
		}
		else if (playerData.canStand)
		{
			crouchState = CrouchState.Up;
		}
		switch (crouchState)
		{
		case CrouchState.Down:
			CrouchDown();
			break;
		case CrouchState.Up:
			StandUp();
			break;
		}
		if (Input.GetKeyDown(KeyCode.C))
		{
			LandKick();
		}
		targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
		currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.deltaTime);
		head.localRotation = Quaternion.Euler(currentRotation);
		weapon.localRotation = Quaternion.Euler(currentRotation);
	}

	public void CanStand()
	{
		Vector3 position = mainCamera.transform.position;
		RaycastHit hitInfo;
		if (Physics.Raycast(position, Vector3.up, out hitInfo, 0.5f, standLayers))
		{
			Debug.DrawRay(position, Vector3.up, Color.black, 2f);
			playerData.canStand = false;
		}
		else
		{
			playerData.canStand = true;
		}
	}

	private void CrouchDown()
	{
		if (controller.height != crouchColliderHeight)
		{
			controller.height = Mathf.MoveTowards(controller.height, crouchColliderHeight, Time.deltaTime * crouchSpeed);
		}
		if (controller.center.y != originalControllerCenter - crouchColliderHeight * 0.4f)
		{
			controller.center = new Vector3(controller.center.x, originalControllerCenter - (originalControllerHeight - controller.height) * 0.4f, controller.center.z);
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
			controller.height = Mathf.MoveTowards(controller.height, originalControllerHeight, Time.deltaTime * crouchSpeed);
		}
		if (controller.center.y != originalControllerCenter)
		{
			controller.center = new Vector3(controller.center.x, originalControllerCenter - (originalControllerHeight - controller.height) * 0.4f, controller.center.z);
		}
		if (pelvis.transform.localPosition.y != originalPelvisHeight)
		{
			pelvis.transform.localPosition = new Vector3(pelvis.transform.localPosition.x, Mathf.MoveTowards(pelvis.transform.localPosition.y, originalPelvisHeight, Time.deltaTime * crouchSpeed / 1.2f), pelvis.transform.localPosition.z);
		}
	}

	public void LandKick()
	{
		targetRotation += new Vector3(recoilX, Random.Range(0f - recoilY, recoilY), Random.Range(0f - recoilZ, recoilZ));
	}
}
