using UnityEngine;

public class Look : MonoBehaviour
{
	public Transform head;

	public PlayerData playerData;

	public InputManager inputManager;

	[SerializeField]
	public float smoothing = 0.03f;

	private float cameraPitch;

	[Header("Sensitivity")]
	public float currentSensitivity;

	public float mouseSensitivity;

	public float scopeSensitivity;

	public float multiplier;

	private Vector2 currentMouseDelta = Vector2.zero;

	private Vector2 currentMouseDeltaVelocity = Vector2.zero;

	private Weapon weaponSystem;

	private void Start()
	{
		weaponSystem = GetComponent<Weapon>();
	}

	private void Update()
	{
		if (playerData.isAiming && weaponSystem.itemData.hasScope)
		{
			currentSensitivity = scopeSensitivity;
		}
		else
		{
			currentSensitivity = mouseSensitivity;
		}
		float num = Remap(currentSensitivity, -10f, 10f, 0.1f, 2f);
		Vector2 target = playerData.mouseInput * num;
		currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, target, ref currentMouseDeltaVelocity, smoothing);
		cameraPitch -= currentMouseDelta.y;
		cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);
		head.localEulerAngles = Vector3.right * cameraPitch;
		base.transform.Rotate(Vector3.up * currentMouseDelta.x);
	}

	public static float Remap(float from, float fromMin, float fromMax, float toMin, float toMax)
	{
		float num = from - fromMin;
		float num2 = fromMax - fromMin;
		float num3 = num / num2;
		return (toMax - toMin) * num3 + toMin;
	}
}
