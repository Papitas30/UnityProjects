using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "Player Data", order = 1)]
public class PlayerData : ScriptableObject
{
	public enum FireMode
	{
		Single = 0,
		Burst = 1,
		Auto = 2
	}

	[Header("Movement States")]
	public bool isIdle;

	public bool isMoving;

	public bool isWalking;

	public bool isRunning;

	public bool isSprinting;

	public bool isProning;

	public bool isCrouchingDown;

	public bool isCrouching;

	public bool isJumping;

	public bool isGrounded;

	public bool isSlope;

	public bool cameraCollision;

	[Header("FOV")]
	public float weaponFOV;

	[Header("Time of Day")]
	public bool isDawn;

	public bool isDay;

	public bool isDusk;

	public bool isNight;

	[Header("Vitals")]
	public float armStamina;

	public float bodyStamina;

	public float health;

	[Header("Current Position")]
	public Vector3 currentPosition;

	[Header("Equipment States")]
	public bool isArmed;

	public bool primaryEquipped;

	public bool primaryActive;

	public bool secondaryEquipped;

	public bool secondaryActive;

	public bool melee;

	public bool isAiming;

	public bool isHoldingBreath;

	public bool isReloading;

	public bool isFiring;

	public bool isDead;

	public bool isInspecting;

	public bool magazineFull;

	public bool magazineEmpty;

	public bool laserActive;

	public bool flashlightActive;

	public bool isColliding;

	public bool isHolstering;

	public bool isUsing;

	public bool isFreezed;

	public bool isPlacing;

	public bool dualRender;

	public bool spawnAI;

	public bool coreSpawned;

	public bool canStand;

	public bool isOutOfStamina;

	public bool hardsurface;

	public bool woodSurface;

	[Header("Firemode")]
	public FireMode fireMode;

	[Header("Speed")]
	public Vector3 velocity;

	public float acceleration;

	public float currentSpeed;

	public float walkSpeed;

	public float runSpeed;

	public float crouchSpeed;

	public float proneSpeed;

	public float sprintSpeed;

	public float speedLimit;

	[Header("Camera Noise")]
	public float cameraNoiseFrequency;

	public float cameraNoiseAmplitude;

	[Header("Weapon Noise")]
	public float weaponNoiseFrequency;

	public float weaponNoiseAmplitude;

	public float weaponNoiseLerpSpeed;

	[Header("Headbob")]
	public float bobMultiplier;

	public float bobFrequency;

	public float bobHorizontalAmplitude;

	public float bobVerticalAmplitude;

	public float bobFrequencyLerpSpeed;

	public float bobAmplitudeLerpSpeed;

	[Header("Handling")]
	public int weaponPosition;

	[Header("Movement Input")]
	public Vector2 movementInput;

	[Header("Mouse Input")]
	public Vector2 mouseInput;

	public void Freeze()
	{
		isFreezed = true;
	}

	public void Unfreeze()
	{
		isFreezed = false;
	}
}
