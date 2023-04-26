using UnityEngine;

public class Placer : MonoBehaviour
{
	public PlayerData playerData;

	public Transform placer;

	public float rotationScroll;

	public float scroll;

	public float minDistance;

	public float maxDistance;

	public float multiplier;

	public bool rotateMode;

	public Vector3 rotation;

	public float targetValue;

	public float rotationMultiplier;

	public float rotationSpeed;

	[Header("InteractableInfo")]
	public float sphereCastRadius = 0.5f;

	public int interactableLayerIndex;

	private Vector3 raycastPos;

	public GameObject lookObject;

	private Placeable physicsObject;

	private Camera mainCamera;

	[Header("Pickup")]
	[SerializeField]
	private Transform pickupParent;

	public GameObject currentlyPickedUpObject;

	private Rigidbody pickupRB;

	private bool initialPick;

	private void Start()
	{
		mainCamera = Camera.main;
		rotationScroll = 180f;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(1))
		{
			rotateMode = !rotateMode;
		}
		if (rotateMode)
		{
			rotationScroll += Input.GetAxis("Mouse ScrollWheel") * rotationMultiplier;
			targetValue = Mathf.Lerp(targetValue, rotationScroll, rotationSpeed * Time.deltaTime);
			placer.localEulerAngles = new Vector3(0f, targetValue, 0f);
		}
		else
		{
			scroll += Input.GetAxis("Mouse ScrollWheel") * multiplier;
			scroll = Mathf.Clamp(scroll, minDistance, maxDistance);
			placer.localPosition = new Vector3(0f, 0f, scroll);
		}
		if (Input.GetKeyDown(KeyCode.G))
		{
			if (currentlyPickedUpObject == null)
			{
				if (lookObject != null)
				{
					PickUpObject();
				}
			}
			else
			{
				BreakConnection();
			}
		}
		CalculatePhysics();
	}

	private void CalculatePhysics()
	{
		if (currentlyPickedUpObject != null)
		{
			currentlyPickedUpObject.transform.position = Vector3.Lerp(currentlyPickedUpObject.transform.position, pickupParent.position, 10f * Time.deltaTime);
			currentlyPickedUpObject.transform.eulerAngles = new Vector3(0f, pickupParent.transform.eulerAngles.y, 0f);
		}
	}

	public void BreakConnection()
	{
		currentlyPickedUpObject.gameObject.layer = 7;
		rotateMode = false;
		pickupRB.constraints = RigidbodyConstraints.None;
		pickupRB.useGravity = true;
		currentlyPickedUpObject = null;
		physicsObject.pickedUp = false;
		playerData.isPlacing = false;
	}

	public void PickUpObject()
	{
		playerData.isPlacing = true;
		rotateMode = false;
		scroll = minDistance;
		physicsObject = lookObject.GetComponentInChildren<Placeable>();
		physicsObject.gameObject.layer = 14;
		currentlyPickedUpObject = lookObject;
		pickupRB = currentlyPickedUpObject.GetComponent<Rigidbody>();
		pickupRB.useGravity = false;
		pickupRB.constraints = RigidbodyConstraints.FreezeAll;
		physicsObject.placer = this;
		StartCoroutine(physicsObject.PickUp());
	}

	public void PlaceObject(Placeable placeable)
	{
		playerData.isPlacing = true;
		scroll = minDistance;
		physicsObject = placeable;
		physicsObject.gameObject.layer = 14;
		currentlyPickedUpObject = physicsObject.transform.gameObject;
		pickupRB = currentlyPickedUpObject.GetComponent<Rigidbody>();
		pickupRB.useGravity = false;
		pickupRB.constraints = RigidbodyConstraints.FreezeAll;
		physicsObject.placer = this;
		StartCoroutine(physicsObject.PickUp());
	}

	public void ResetSlotDropCollision()
	{
		Invoke("ResetCollision", 0.2f);
	}

	private void ResetCollision()
	{
		Physics.IgnoreLayerCollision(7, 9, false);
	}
}
