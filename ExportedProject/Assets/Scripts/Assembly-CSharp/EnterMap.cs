using UnityEngine;

public class EnterMap : MonoBehaviour
{
	public PlayerData playerData;

	private EventManager eventManager;

	private void Awake()
	{
		ResetPlayerData();
	}

	private void Start()
	{
		eventManager = GameObject.FindWithTag("Event Manager").GetComponent<EventManager>();
		eventManager.enterMap.Raise();
	}

	private void ResetPlayerData()
	{
		playerData.isIdle = false;
		playerData.isMoving = false;
		playerData.isWalking = false;
		playerData.isRunning = false;
		playerData.isSprinting = false;
		playerData.isProning = false;
		playerData.isCrouchingDown = false;
		playerData.isCrouching = false;
		playerData.isJumping = false;
		playerData.isGrounded = false;
		playerData.isSlope = false;
		playerData.cameraCollision = false;
		playerData.isDawn = false;
		playerData.isDay = false;
		playerData.isDusk = false;
		playerData.isNight = false;
		playerData.armStamina = 100f;
		playerData.bodyStamina = 100f;
		playerData.health = 100f;
		playerData.isArmed = false;
		playerData.primaryEquipped = false;
		playerData.primaryActive = false;
		playerData.secondaryEquipped = false;
		playerData.secondaryActive = false;
		playerData.melee = false;
		playerData.isAiming = false;
		playerData.isHoldingBreath = false;
		playerData.isReloading = false;
		playerData.isFiring = false;
		playerData.isDead = false;
		playerData.isInspecting = false;
		playerData.magazineFull = false;
		playerData.magazineEmpty = false;
		playerData.laserActive = false;
		playerData.flashlightActive = false;
		playerData.isColliding = false;
		playerData.isHolstering = false;
		playerData.isUsing = false;
		playerData.isFreezed = false;
		playerData.isPlacing = false;
		playerData.dualRender = false;
		playerData.spawnAI = false;
		playerData.coreSpawned = false;
		playerData.canStand = false;
		playerData.isOutOfStamina = false;
		playerData.hardsurface = false;
		playerData.woodSurface = false;
		playerData.fireMode = PlayerData.FireMode.Single;
		playerData.velocity = Vector3.zero;
		playerData.movementInput = Vector2.zero;
		playerData.mouseInput = Vector2.zero;
		playerData.currentSpeed = 0f;
		playerData.speedLimit = 0f;
		playerData.cameraNoiseFrequency = 0f;
		playerData.cameraNoiseAmplitude = 0f;
		playerData.weaponNoiseFrequency = 0f;
		playerData.weaponNoiseAmplitude = 0f;
	}

	private void OnDisable()
	{
		playerData.primaryActive = false;
		playerData.primaryEquipped = false;
		playerData.secondaryActive = false;
		playerData.secondaryEquipped = false;
		playerData.coreSpawned = false;
		playerData.canStand = false;
		playerData.isOutOfStamina = false;
	}
}
