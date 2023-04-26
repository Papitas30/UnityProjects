using UnityEngine;

public class BoatController : MonoBehaviour
{
	public float speed = 1f;

	public float steerSpeed = 1f;

	public float movementThresold = 10f;

	public float verticalInput;

	public float movementFactor;

	private float horizontalInput;

	private float steerFactor;

	[Header("Transform")]
	public Transform target;

	[Header("Tilt Settings")]
	public float tiltHorizontal;

	public float tiltVertical;

	public float tiltSmoothing;

	[Header("Push Settings")]
	public float aimPush;

	public float hipPush;

	public float pushSmoothing;

	private Vector3 tiltRotation;

	private Vector3 tiltVelocity;

	private Vector3 pushPosition;

	private Vector3 pushVelocity;

	private Vector3 targetPushPosition;

	private Vector3 targetPushVelocity;

	private Vector3 targetTiltRotation;

	private Vector3 targetTiltVelocity;

	private Vector2 movementInput;

	public bool isActive;

	private void Update()
	{
		if (isActive)
		{
			targetTiltRotation.z = tiltHorizontal * (0f - Input.GetAxis("Horizontal"));
			targetTiltRotation.x = tiltVertical * (0f - Input.GetAxis("Vertical"));
			targetTiltRotation = Vector3.SmoothDamp(targetTiltRotation, Vector3.zero, ref targetTiltVelocity, tiltSmoothing);
			tiltRotation = Vector3.SmoothDamp(tiltRotation, targetTiltRotation, ref tiltVelocity, tiltSmoothing);
			target.localRotation = Quaternion.Euler(tiltRotation);
			targetPushPosition = Vector3.SmoothDamp(targetPushPosition, Vector3.zero, ref targetPushVelocity, pushSmoothing);
			pushPosition = Vector3.SmoothDamp(pushPosition, targetPushPosition, ref pushVelocity, pushSmoothing);
			target.localPosition = pushPosition;
			Movement();
			Steer();
		}
	}

	private void Movement()
	{
		verticalInput = Input.GetAxis("Vertical");
		movementFactor = Mathf.Lerp(movementFactor, verticalInput, Time.deltaTime / movementThresold);
		base.transform.Translate(0f, 0f, movementFactor * speed);
	}

	private void Steer()
	{
		horizontalInput = Input.GetAxis("Horizontal");
		steerFactor = Mathf.Lerp(steerFactor, horizontalInput * verticalInput, Time.deltaTime / movementThresold);
		base.transform.Rotate(0f, steerFactor * steerSpeed, 0f);
	}
}
