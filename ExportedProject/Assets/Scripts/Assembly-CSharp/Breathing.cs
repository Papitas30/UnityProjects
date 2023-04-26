using UnityEngine;

public class Breathing : MonoBehaviour
{
	[Header("Transforms")]
	public Transform eyes;

	[Header("Data")]
	public PlayerData playerData;

	[Space]
	[Header("Axis")]
	[SerializeField]
	private bool x = true;

	[SerializeField]
	private bool y = true;

	[SerializeField]
	private bool z = true;

	private Vector3 noiseOffset;

	private Vector3 noise;

	private Vector3 rotation;

	private float targetFrequency;

	private float targetAmplitude;

	private float targetLerpSpeed;

	public float multiplier;

	private void Update()
	{
		if (!playerData.isFreezed)
		{
			if (playerData.isIdle)
			{
				targetFrequency = 0f;
				targetAmplitude = 0f;
				targetLerpSpeed = 10f;
			}
			if (playerData.isWalking)
			{
				targetFrequency = 1f;
				targetAmplitude = 1.25f;
				targetLerpSpeed = 2f;
			}
			if (playerData.isRunning)
			{
				targetFrequency = 2.5f;
				targetAmplitude = 2f;
				targetLerpSpeed = 2f;
			}
			if (playerData.isSprinting)
			{
				targetFrequency = 4f;
				targetAmplitude = 4f;
				targetLerpSpeed = 2f;
			}
			if (playerData.isJumping)
			{
				targetFrequency = 0f;
				targetAmplitude = 0f;
				targetLerpSpeed = 5f;
			}
			if (playerData.isCrouching && playerData.isMoving)
			{
				targetFrequency = 1f;
				targetAmplitude = 2f;
				targetLerpSpeed = 2f;
			}
			if (playerData.isCrouchingDown)
			{
				targetFrequency = 0.25f;
				targetAmplitude = 50f;
				targetLerpSpeed = 4f;
			}
			playerData.cameraNoiseFrequency = Mathf.Lerp(playerData.cameraNoiseFrequency, targetFrequency * multiplier, targetLerpSpeed * Time.deltaTime);
			playerData.cameraNoiseAmplitude = Mathf.Lerp(playerData.cameraNoiseAmplitude, targetAmplitude * multiplier, targetLerpSpeed * Time.deltaTime);
		}
	}

	private void LateUpdate()
	{
		if (!playerData.isFreezed)
		{
			float num = Time.deltaTime * playerData.cameraNoiseFrequency;
			noiseOffset.x += num;
			noiseOffset.y += num;
			noiseOffset.z += num;
			noise.x = Mathf.PerlinNoise(noiseOffset.x, 0f);
			noise.y = Mathf.PerlinNoise(noiseOffset.x, 1f);
			noise.z = Mathf.PerlinNoise(noiseOffset.x, 2f);
			noise -= Vector3.one * 0.5f;
			noise *= playerData.cameraNoiseAmplitude;
			Vector3 zero = Vector3.zero;
			if (x)
			{
				zero.x += noise.x;
			}
			if (y)
			{
				zero.y += noise.y;
			}
			if (z)
			{
				zero.z += noise.z;
			}
			rotation.x = (x ? zero.x : eyes.localEulerAngles.x);
			rotation.y = (y ? zero.y : eyes.localEulerAngles.y);
			rotation.z = (z ? zero.z : eyes.localEulerAngles.z);
			eyes.localEulerAngles = rotation;
		}
	}
}
