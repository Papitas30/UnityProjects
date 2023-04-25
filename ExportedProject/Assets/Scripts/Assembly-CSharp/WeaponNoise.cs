using UnityEngine;

public class WeaponNoise : MonoBehaviour
{
	[Header("Data")]
	public PlayerData playerData;

	[Header("Data")]
	public Transform target;

	private Vector3 weaponNoiseOffset;

	private Vector3 weaponNoise;

	private Vector3 weaponRotation;

	private bool x = true;

	private bool y = true;

	private bool z = true;

	private float targetFrequency;

	private float targetAmplitude;

	private float targetLerpSpeed;

	public float staminaMultiplier;

	public ArmStamina armStamina;

	public Vitals vitals;

	private void Start()
	{
		vitals = GetComponent<Vitals>();
	}

	private void Update()
	{
		if (playerData.isIdle)
		{
			targetFrequency = 0.5f;
			targetAmplitude = 4f;
			targetLerpSpeed = 2f;
		}
		if (playerData.isWalking && (!playerData.isCrouching & !playerData.isProning))
		{
			targetFrequency = 1f;
			targetAmplitude = 8f;
			targetLerpSpeed = 2f;
		}
		if (playerData.isCrouching && playerData.isMoving)
		{
			targetFrequency = 1f;
			targetAmplitude = 8f;
			targetLerpSpeed = 2f;
		}
		if (playerData.isProning && playerData.isMoving)
		{
			targetFrequency = 1f;
			targetAmplitude = 25f;
			targetLerpSpeed = 2f;
		}
		if (playerData.isRunning)
		{
			targetFrequency = 1.5f;
			targetAmplitude = 8f;
			targetLerpSpeed = 2f;
		}
		if (playerData.isSprinting)
		{
			targetFrequency = 2.5f;
			targetAmplitude = 10f;
			targetLerpSpeed = 2f;
		}
		if (playerData.isAiming && !playerData.isHoldingBreath)
		{
			targetFrequency = 0.5f;
			targetAmplitude = 0.5f;
			targetLerpSpeed = 4f;
		}
		if (playerData.isAiming && playerData.isHoldingBreath)
		{
			targetFrequency = 0.2f;
			targetAmplitude = 0.2f;
			targetLerpSpeed = 2f;
		}
		if (playerData.isReloading)
		{
			targetFrequency = 2f;
			targetAmplitude = 10f;
			targetLerpSpeed = 1f;
		}
		if (!playerData.isAiming && !playerData.isReloading)
		{
			playerData.weaponNoiseFrequency = Mathf.Lerp(playerData.weaponNoiseFrequency, targetFrequency * armStamina.multiplier / 2f, targetLerpSpeed * Time.deltaTime);
			playerData.weaponNoiseAmplitude = Mathf.Lerp(playerData.weaponNoiseAmplitude, targetAmplitude * armStamina.multiplier / 2f, targetLerpSpeed * Time.deltaTime);
		}
		if (playerData.isAiming && !playerData.isReloading)
		{
			playerData.weaponNoiseFrequency = Mathf.Lerp(playerData.weaponNoiseFrequency, targetFrequency * armStamina.multiplier * 1.2f, targetLerpSpeed * Time.deltaTime);
			playerData.weaponNoiseAmplitude = Mathf.Lerp(playerData.weaponNoiseAmplitude, targetAmplitude * armStamina.multiplier * 2f, targetLerpSpeed * Time.deltaTime);
		}
		if (playerData.isReloading)
		{
			playerData.weaponNoiseAmplitude = Mathf.Lerp(playerData.weaponNoiseAmplitude, targetAmplitude, targetLerpSpeed * Time.deltaTime);
			playerData.weaponNoiseFrequency = Mathf.Lerp(playerData.weaponNoiseFrequency, targetFrequency, targetLerpSpeed * Time.deltaTime);
		}
	}

	private void LateUpdate()
	{
		float num = Time.deltaTime * playerData.weaponNoiseFrequency;
		weaponNoiseOffset.x += num;
		weaponNoiseOffset.y += num;
		weaponNoiseOffset.z += num;
		weaponNoise.x = Mathf.PerlinNoise(weaponNoiseOffset.x, 0f);
		weaponNoise.y = Mathf.PerlinNoise(weaponNoiseOffset.x, 1f);
		weaponNoise.z = Mathf.PerlinNoise(weaponNoiseOffset.x, 2f);
		weaponNoise -= Vector3.one * 0.5f;
		weaponNoise *= playerData.weaponNoiseAmplitude;
		Vector3 zero = Vector3.zero;
		if (x)
		{
			zero.x += weaponNoise.x;
		}
		if (y)
		{
			zero.y += weaponNoise.y;
		}
		if (z)
		{
			zero.z += weaponNoise.z;
		}
		weaponRotation.x = (x ? zero.x : base.transform.localEulerAngles.x);
		weaponRotation.y = (y ? zero.y : base.transform.localEulerAngles.y);
		weaponRotation.z = (z ? zero.z : base.transform.localEulerAngles.z);
		target.localEulerAngles = weaponRotation;
	}
}
