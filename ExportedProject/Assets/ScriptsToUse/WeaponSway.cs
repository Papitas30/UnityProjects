using UnityEngine;

public class WeaponSway : MonoBehaviour
{
	[Header("Player Data")]
	public PlayerData playerData;

	public Transform target;

	[Header("Aim Settings")]
	public float aimAmount;

	public float maxAimAmount;

	public float aimSmoothing;

	[Header("Other Settings")]
	public float amount;

	public float maxAmount;

	public float smoothAmount;

	private Vector3 initialPosition;

	private float inputX;

	private float inputY;

	private void Start()
	{
		initialPosition = target.localPosition;
	}

	private void Update()
	{
		inputX = 0f - Input.GetAxisRaw("Mouse X");
		inputY = 0f - Input.GetAxisRaw("Mouse Y");
		if (playerData.isAiming)
		{
			float x = Mathf.Clamp(inputX * aimAmount, 0f - maxAimAmount, maxAimAmount);
			float y = Mathf.Clamp(inputY * aimAmount, 0f - maxAimAmount, maxAimAmount);
			Vector3 vector = new Vector3(x, y, 0f);
			target.localPosition = Vector3.Lerp(target.localPosition, vector + initialPosition, aimSmoothing * Time.deltaTime);
		}
		else
		{
			float x2 = Mathf.Clamp(inputX * amount, 0f - maxAmount, maxAmount);
			float y2 = Mathf.Clamp(inputY * amount, 0f - maxAmount, maxAmount);
			Vector3 vector2 = new Vector3(x2, y2, 0f);
			target.localPosition = Vector3.Lerp(target.localPosition, vector2 + initialPosition, smoothAmount * Time.deltaTime);
		}
	}
}
