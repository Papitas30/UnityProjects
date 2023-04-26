using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
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

	public Transform placer;

	public Vector3 parentOffset;

	public GameObject currentlyPickedUpObject;

	private Rigidbody pickupRB;

	[Header("ObjectFollow")]
	[SerializeField]
	private float minSpeed;

	[SerializeField]
	private float maxSpeed = 300f;

	[SerializeField]
	private float maxDistance = 10f;

	private float currentSpeed;

	private float currentDist;

	[Header("Rotation")]
	public float rotationSpeed = 100f;

	private Quaternion lookRot;

	private void Start()
	{
		mainCamera = Camera.main;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(pickupParent.position, 0.1f);
	}

	private void Update()
	{
		raycastPos = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
		RaycastHit hitInfo;
		if (Physics.SphereCast(raycastPos, sphereCastRadius, mainCamera.transform.forward, out hitInfo, maxDistance, 1 << interactableLayerIndex))
		{
			lookObject = hitInfo.collider.transform.root.gameObject;
		}
		else
		{
			lookObject = null;
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
			currentlyPickedUpObject.transform.rotation = Quaternion.Lerp(currentlyPickedUpObject.transform.localRotation, pickupParent.transform.rotation, 10f * Time.deltaTime);
		}
	}

	public void BreakConnection()
	{
		pickupRB.constraints = RigidbodyConstraints.None;
		pickupRB.useGravity = true;
		currentlyPickedUpObject = null;
		physicsObject.pickedUp = false;
		currentDist = 0f;
	}

	public void PickUpObject()
	{
		physicsObject = lookObject.GetComponentInChildren<Placeable>();
		currentlyPickedUpObject = lookObject;
		pickupRB = currentlyPickedUpObject.GetComponent<Rigidbody>();
		pickupRB.useGravity = false;
		pickupRB.constraints = RigidbodyConstraints.FreezeAll;
		StartCoroutine(physicsObject.PickUp());
	}
}
