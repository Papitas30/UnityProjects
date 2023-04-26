using UnityEngine;

public class Lean : MonoBehaviour
{
	[Header("Transform")]
	public Transform pelvis;

	public PlayerData playerData;

	[Header("Settings")]
	public float leanSpeed = 4f;

	public float leanAngle = 20f;

	public float leanPosition = 0.25f;

	private Quaternion targetRotation;

	private Quaternion originalRotation;

	private Vector3 originalPosition;

	private Vector3 targetPosition;

	private void Start()
	{
		originalPosition = pelvis.transform.localPosition;
		originalRotation = pelvis.transform.localRotation;
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.Q))
		{
			targetPosition = new Vector3(0f - leanPosition, 1f, 0f);
			targetRotation.eulerAngles = new Vector3(0f, 0f, leanAngle);
		}
		else if (Input.GetKey(KeyCode.E))
		{
			targetPosition = new Vector3(leanPosition, 1f, 0f);
			targetRotation.eulerAngles = new Vector3(0f, 0f, 0f - leanAngle);
		}
		else
		{
			targetPosition = originalPosition;
			targetRotation = originalRotation;
		}
		pelvis.localRotation = Quaternion.Lerp(pelvis.localRotation, targetRotation, leanSpeed * Time.deltaTime);
	}
}
