using UnityEngine;

public class WeaponHandling : MonoBehaviour
{
	[Header("Player Data")]
	public PlayerData playerData;

	[Header("Weapon Target")]
	public Transform weaponTarget;

	public Transform weaponHolder;

	[Header("Weapon FOV")]
	public float aimVelocity;

	public float mainTarget;

	public float fpsTarget;

	[Header("Weapon Collision")]
	public Transform collisionTarget;

	public Transform barrelTarget;

	public float distanceThreshold;

	private float handlingSpeed;

	private ItemData itemData;

	public Camera mainCamera;

	public Camera fpsCamera;

	private Weapon weaponSystem;

	public Transform cameraLook;

	public bool collision;

	public float distance;

	public LayerMask collisionLayer;

	public SettingsManager settingsManager;

	public float worldFOV;

	public float weaponFOV;

	public float horizontalOffset;

	public float verticalOffset;

	private void Start()
	{
		settingsManager = GameObject.FindWithTag("Settings Manager").GetComponent<SettingsManager>();
	}

	public void DrawWeapon()
	{
		weaponSystem = GetComponent<Weapon>();
		weaponHolder.LerpTransform(weaponTarget, Time.deltaTime * handlingSpeed);
		itemData = weaponSystem.itemData;
		playerData.isInspecting = false;
		playerData.weaponPosition = 1;
		handlingSpeed = 5f;
	}

	private void Update()
	{
		CollisionTarget();
		playerData.weaponFOV = fpsCamera.fieldOfView;
		if (!playerData.primaryActive && !playerData.secondaryActive)
		{
			mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, worldFOV, aimVelocity * Time.deltaTime);
			fpsCamera.fieldOfView = Mathf.Lerp(fpsCamera.fieldOfView, weaponFOV, aimVelocity * Time.deltaTime);
		}
		if (playerData.primaryActive || playerData.secondaryActive)
		{
			distance = Vector3.Distance(collisionTarget.position, barrelTarget.position);
			if (distance <= distanceThreshold && !playerData.isReloading)
			{
				collision = true;
				playerData.isColliding = true;
				if (playerData.weaponPosition == 2 && cameraLook.localRotation.x <= 0f)
				{
					weaponTarget.localPosition = itemData.upPosition;
					weaponTarget.localRotation = Quaternion.Euler(itemData.upRotation.x, itemData.upRotation.y, itemData.upRotation.z);
				}
				else if (playerData.weaponPosition == 2)
				{
					weaponTarget.localPosition = itemData.collisionPosition;
					weaponTarget.localRotation = Quaternion.Euler(itemData.collisionRotation.x, itemData.collisionRotation.y, itemData.collisionRotation.z);
				}
			}
			else
			{
				playerData.isColliding = false;
				collision = false;
			}
			if (!collision)
			{
				if (playerData.isAiming && !playerData.isReloading)
				{
					if (weaponSystem.itemData.hasScope && settingsManager.dualRender == 1)
					{
						mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, itemData.aimFOV, aimVelocity * Time.deltaTime);
						fpsCamera.fieldOfView = Mathf.Lerp(fpsCamera.fieldOfView, weaponFOV - 10f, aimVelocity * Time.deltaTime);
						weaponTarget.localPosition = itemData.aimPosition;
						weaponTarget.localRotation = Quaternion.Euler(itemData.aimRotation.x, itemData.aimRotation.y, itemData.aimRotation.z);
						handlingSpeed = 8f;
					}
					else
					{
						mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, worldFOV - 20f, aimVelocity * Time.deltaTime);
						fpsCamera.fieldOfView = Mathf.Lerp(fpsCamera.fieldOfView, weaponFOV - 10f, aimVelocity * Time.deltaTime);
						weaponTarget.localPosition = itemData.aimPosition;
						weaponTarget.localRotation = Quaternion.Euler(itemData.aimRotation.x, itemData.aimRotation.y, itemData.aimRotation.z);
						handlingSpeed = 8f;
					}
				}
				else
				{
					mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, worldFOV, aimVelocity * Time.deltaTime);
					fpsCamera.fieldOfView = Mathf.Lerp(fpsCamera.fieldOfView, weaponFOV, aimVelocity * Time.deltaTime);
					if (playerData.weaponPosition == 1 || playerData.isSprinting)
					{
						weaponTarget.localPosition = itemData.lowPosition;
						weaponTarget.localRotation = Quaternion.Euler(itemData.lowRotation.x, itemData.lowRotation.y, itemData.lowRotation.z);
						handlingSpeed = 5f;
					}
					if (playerData.weaponPosition == 2 && !playerData.isSprinting)
					{
						weaponTarget.localPosition = itemData.highPosition + new Vector3(horizontalOffset, verticalOffset, 0f);
						weaponTarget.localRotation = Quaternion.Euler(itemData.highRotation.x, itemData.highRotation.y, itemData.highRotation.z);
						handlingSpeed = 5f;
					}
					if (playerData.isInspecting && !playerData.isReloading && !playerData.isSprinting)
					{
						weaponTarget.localPosition = itemData.inspectPosition;
						weaponTarget.localRotation = Quaternion.Euler(itemData.inspectRotation.x, itemData.inspectRotation.y, itemData.inspectRotation.z);
						handlingSpeed = 5f;
					}
					if (playerData.isReloading && !playerData.isSprinting)
					{
						weaponTarget.localPosition = itemData.reloadPosition;
						weaponTarget.localRotation = Quaternion.Euler(itemData.reloadRotation.x, itemData.reloadRotation.y, itemData.reloadRotation.z);
						handlingSpeed = 5f;
					}
				}
			}
		}
		if (playerData.isHolstering)
		{
			weaponTarget.localPosition = new Vector3(0f, -1f, 0f);
			weaponTarget.localRotation = Quaternion.Euler(100f, -50f, 0f);
			handlingSpeed = 5f;
		}
		weaponHolder.LerpTransform(weaponTarget, Time.deltaTime * handlingSpeed);
	}

	private void CollisionTarget()
	{
		Vector3 origin = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
		Vector3 forward = mainCamera.transform.forward;
		RaycastHit hitInfo;
		if (Physics.SphereCast(origin, 0.05f, forward, out hitInfo, 2f, collisionLayer))
		{
			if (hitInfo.collider != null)
			{
				collisionTarget.transform.position = hitInfo.point;
			}
		}
		else
		{
			collisionTarget.transform.position = new Vector3(0f, 0f, 0f);
		}
	}
}
