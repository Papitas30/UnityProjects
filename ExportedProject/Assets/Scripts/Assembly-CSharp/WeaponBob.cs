using UnityEngine;

public class WeaponBob : MonoBehaviour
{
	public Transform weaponBob;

	[Header("Data")]
	public PlayerData playerData;

	[Header("Bob Settings")]
	public float velocityThreshold;

	public float weaponRecoverySpeed;

	public float bobSmoothing;

	private Transform mainCamera;

	private float horizontalOffset;

	private float verticalOffset;

	private float headbobTime;

	private Vector3 targetPosition;

	private Vector3 initialWeaponPosition;

	private Vitals vitals;

	private void Start()
	{
		vitals = GetComponent<Vitals>();
		mainCamera = Camera.main.GetComponent<Camera>().transform;
		initialWeaponPosition = weaponBob.localPosition;
	}

	private void Update()
	{
		if (playerData.velocity.sqrMagnitude > velocityThreshold)
		{
			headbobTime += Time.deltaTime;
		}
		else
		{
			weaponBob.localPosition = Vector3.Lerp(weaponBob.localPosition, initialWeaponPosition, weaponRecoverySpeed * Time.deltaTime);
			headbobTime = 0f;
		}
		targetPosition = mainCamera.position + CalculateHeadbobOffset(headbobTime);
		weaponBob.position = Vector3.Lerp(weaponBob.position, targetPosition, bobSmoothing * Time.deltaTime);
	}

	private Vector3 CalculateHeadbobOffset(float t)
	{
		Vector3 result = Vector3.zero;
		if (t > 0f)
		{
			horizontalOffset = Mathf.Cos(t * playerData.bobFrequency) * playerData.bobHorizontalAmplitude / 4f;
			verticalOffset = Mathf.Sin(t * playerData.bobFrequency) * playerData.bobVerticalAmplitude / 6f;
			result = mainCamera.right * horizontalOffset + mainCamera.up * verticalOffset;
		}
		return result;
	}
}
