using UnityEngine;

public class Headbob : MonoBehaviour
{
	[Header("Data")]
	public PlayerData playerData;

	[Header("Transform")]
	public Transform mainCamera;

	[Header("Settings")]
	public float velocityThreshold = 1f;

	public float recoverySpeed = 2f;

	public float bobSmoothing = 1f;

	private float horizontalOffset;

	private float verticalOffset;

	private float headbobTime;

	private Vector3 targetPosition;

	public Vector3 initialEyePosition;

	private Controller controller;

	private Footstep footstep;

	private Vitals vitals;

	private bool step;

	private void Start()
	{
		initialEyePosition = mainCamera.localPosition;
		controller = GetComponent<Controller>();
		footstep = GetComponent<Footstep>();
		vitals = GetComponent<Vitals>();
	}

	private void Update()
	{
		if (!playerData.isFreezed)
		{
			if (playerData.velocity.sqrMagnitude > velocityThreshold)
			{
				headbobTime += Time.deltaTime;
			}
			else
			{
				mainCamera.localPosition = Vector3.Lerp(mainCamera.localPosition, initialEyePosition, recoverySpeed * Time.deltaTime);
				headbobTime = 0f;
			}
			if (playerData.isWalking && !playerData.isProning && !playerData.isAiming)
			{
				playerData.bobFrequency = 10f;
				playerData.bobHorizontalAmplitude = 0.01f;
				playerData.bobVerticalAmplitude = 0.25f;
			}
			if (playerData.isRunning)
			{
				playerData.bobFrequency = 15f;
				playerData.bobHorizontalAmplitude = 0.02f;
				playerData.bobVerticalAmplitude = 0.4f;
			}
			if (playerData.isCrouching)
			{
				playerData.bobFrequency = 8f;
				playerData.bobHorizontalAmplitude = 0.02f;
				playerData.bobVerticalAmplitude = 0.2f;
			}
			if (playerData.isSprinting)
			{
				playerData.bobFrequency = 18f;
				playerData.bobHorizontalAmplitude = 0.04f;
				playerData.bobVerticalAmplitude = 0.6f;
			}
			if (playerData.isAiming)
			{
				playerData.bobFrequency = 10f;
				playerData.bobHorizontalAmplitude = 0.005f;
				playerData.bobVerticalAmplitude = 0.1f;
			}
			if (playerData.isProning && playerData.isMoving)
			{
				playerData.bobFrequency = 8f;
				playerData.bobHorizontalAmplitude = 0.02f;
				playerData.bobVerticalAmplitude = 0.2f;
			}
			if (verticalOffset < 0f && step)
			{
				step = false;
			}
			if (verticalOffset > 0f && !step && controller.isGrounded)
			{
				footstep.OnFootDown();
				step = true;
			}
			targetPosition = mainCamera.position + CalculateHeadbobOffset(headbobTime);
			mainCamera.position = Vector3.Lerp(mainCamera.position, targetPosition, bobSmoothing * Time.deltaTime);
		}
	}

	private Vector3 CalculateHeadbobOffset(float t)
	{
		Vector3 result = Vector3.zero;
		if (t > 0f)
		{
			horizontalOffset = Mathf.Cos(t * playerData.bobFrequency) * playerData.bobHorizontalAmplitude * playerData.bobMultiplier;
			verticalOffset = Mathf.Sin(t * playerData.bobFrequency) * playerData.bobVerticalAmplitude * playerData.bobMultiplier;
			result = mainCamera.right * horizontalOffset + mainCamera.up * verticalOffset;
		}
		return result;
	}

	public void ResetHeadbob()
	{
		mainCamera.localPosition = initialEyePosition;
	}
}
