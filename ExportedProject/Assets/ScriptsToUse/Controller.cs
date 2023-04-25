using UnityEngine;

public class Controller : MonoBehaviour
{
	[Header("Player Data")]
	public PlayerData playerData;

	[Header("Input Manager")]
	public InputManager inputManager;

	[Header("Gravity")]
	public float gravity = -13f;

	[Header("Slope")]
	public float slopeForce;

	public float slopeForceRayLenght;

	[Header("Inertia")]
	public float moveBackwardsSpeedPercent = 0.5f;

	public float moveSideSpeedPercent = 0.75f;

	[Header("Smoothing")]
	[SerializeField]
	[Range(0f, 0.5f)]
	private float movementSmoothing = 0.3f;

	public float velocityY;

	private CharacterController controller;

	public Vector2 currentDirection = Vector2.zero;

	private Vector2 currentDirVelocity = Vector2.zero;

	private CollisionFlags m_collisionFlags;

	public bool isGrounded = true;

	[Header("Movement")]
	public Vector3 movement;

	public bool isStrafing;

	public bool isWalkingBackwards;

	public float inertia;

	public float inertiaSpeed;

	public bool rockSurface;

	public float angle;

	public float fallStartLevel;

	public float fallThreshold;

	public bool falling;

	public bool touch;

	public bool isSliding;

	public float movementMultiplier;

	public Vector3 slideDirection;

	public float slideSpeed;

	private Vitals vitals;

	private void Start()
	{
		vitals = GetComponent<Vitals>();
		playerData.isFreezed = false;
		controller = GetComponent<CharacterController>();
		controller.slopeLimit = 60f;
		controller.minMoveDistance = 0f;
		movementMultiplier = 1f;
	}

	private void Update()
	{
		playerData.currentPosition = base.transform.position;
		if (controller.isGrounded)
		{
			isGrounded = true;
		}
		else
		{
			isGrounded = false;
		}
		if (rockSurface)
		{
			controller.stepOffset = 0.5f;
			controller.slopeLimit = 80f;
		}
		else
		{
			controller.stepOffset = 0.25f;
			controller.slopeLimit = 60f;
		}
		Inertia();
		Falling();
		Speed();
		Movement();
		Slope();
	}

	private void Falling()
	{
		if (controller.isGrounded)
		{
			if (falling)
			{
				falling = false;
				if (base.transform.position.y < fallStartLevel - fallThreshold)
				{
					FallingDamageAlert(fallStartLevel - base.transform.position.y);
				}
			}
		}
		else if (!falling)
		{
			falling = true;
			fallStartLevel = base.transform.position.y;
		}
	}

	private void Speed()
	{
		if (playerData.isMoving)
		{
			playerData.currentSpeed = Mathf.Min(playerData.currentSpeed + playerData.acceleration * Time.deltaTime, playerData.speedLimit);
		}
		else
		{
			playerData.currentSpeed = Mathf.Max(playerData.currentSpeed - playerData.acceleration * Time.deltaTime, 0f);
			playerData.speedLimit = 0f;
			playerData.isIdle = true;
		}
		if (playerData.isProning)
		{
			playerData.speedLimit = Mathf.Max(playerData.currentSpeed - playerData.acceleration * Time.deltaTime, playerData.proneSpeed);
		}
		if (playerData.isCrouching)
		{
			playerData.speedLimit = Mathf.Max(playerData.currentSpeed - playerData.acceleration * Time.deltaTime, playerData.crouchSpeed);
		}
		if (playerData.isWalking)
		{
			playerData.speedLimit = Mathf.Max(playerData.currentSpeed - playerData.acceleration * Time.deltaTime, playerData.walkSpeed);
		}
		if (playerData.isRunning)
		{
			playerData.speedLimit = Mathf.Min(playerData.currentSpeed + playerData.acceleration * Time.deltaTime, playerData.runSpeed);
		}
		if (playerData.isSprinting)
		{
			playerData.speedLimit = Mathf.Min(playerData.currentSpeed + playerData.acceleration * Time.deltaTime, playerData.sprintSpeed);
		}
	}

	private void Movement()
	{
		if (playerData.isFreezed)
		{
			currentDirection = Vector2.zero;
		}
		else
		{
			currentDirection = Vector2.SmoothDamp(currentDirection, playerData.movementInput, ref currentDirVelocity, movementSmoothing);
		}
		if (controller.isGrounded)
		{
			velocityY = 0f;
		}
		velocityY += gravity * Time.deltaTime;
		playerData.velocity = (base.transform.forward * currentDirection.y + base.transform.right * currentDirection.x) * inertia * playerData.currentSpeed + Vector3.up * velocityY;
		Vector3 vector = playerData.velocity + slideDirection;
		controller.Move(vector * Time.deltaTime);
	}

	private void Slope()
	{
		if (playerData.isJumping)
		{
			return;
		}
		RaycastHit hitInfo;
		if (Physics.Raycast(base.transform.position, Vector3.down, out hitInfo, controller.height / 2f * slopeForceRayLenght))
		{
			if (hitInfo.normal != Vector3.up)
			{
				playerData.isSlope = true;
			}
			else
			{
				playerData.isSlope = false;
			}
		}
		else
		{
			playerData.isSlope = false;
		}
		if (playerData.isMoving && playerData.isSlope)
		{
			controller.Move(Vector3.down * controller.height / 2f * slopeForce * Time.deltaTime);
		}
	}

	private void Sliding()
	{
		RaycastHit hitInfo;
		if (Physics.Raycast(base.transform.position, Vector3.down, out hitInfo, controller.height / 2f * slopeForceRayLenght))
		{
			Debug.DrawRay(hitInfo.point, hitInfo.normal, Color.black, 2f);
			angle = Vector3.Angle(Vector3.up, hitInfo.normal);
			if (angle > controller.slopeLimit)
			{
				Vector3 vector = Vector3.Cross(hitInfo.normal, Vector3.Cross(hitInfo.normal, Vector3.up));
				vector.Normalize();
				slideDirection = vector * slideSpeed;
				movementMultiplier = 0f;
				Debug.DrawRay(base.transform.position, vector, Color.yellow, 3f);
				Debug.Log("Slide");
				isSliding = true;
			}
			else
			{
				slideDirection = Vector3.zero;
				isSliding = false;
				movementMultiplier = 1f;
			}
		}
	}

	private void Inertia()
	{
		movement = base.transform.right * currentDirection.x + base.transform.forward * currentDirection.y;
		float num = Vector3.Dot(base.transform.forward, movement);
		float b;
		if (playerData.isMoving && num < -0.5f)
		{
			isWalkingBackwards = true;
			isStrafing = false;
			b = 0.5f;
			inertiaSpeed = 10f;
		}
		else if (playerData.isMoving && num < 0.5f)
		{
			isWalkingBackwards = false;
			isStrafing = true;
			b = 0.75f;
			inertiaSpeed = 5f;
		}
		else
		{
			isWalkingBackwards = false;
			isStrafing = false;
			b = 1f;
			inertiaSpeed = 10f;
		}
		inertia = Mathf.Lerp(inertia, b, inertiaSpeed * Time.deltaTime);
	}

	private void FallingDamageAlert(float fallDistance)
	{
		base.transform.GetComponent<Damage>().TakeFallDamage();
		MonoBehaviour.print("Ouch! Fell " + fallDistance + " units!");
	}
}
