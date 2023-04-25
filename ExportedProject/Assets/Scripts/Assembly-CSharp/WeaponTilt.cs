using UnityEngine;

public class WeaponTilt : MonoBehaviour
{
	[Header("Player Data")]
	public PlayerData playerData;

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

	private void Update()
	{
		targetTiltRotation.z = tiltHorizontal * (0f - playerData.movementInput.x);
		targetTiltRotation.x = tiltVertical * playerData.movementInput.y;
		targetTiltRotation = Vector3.SmoothDamp(targetTiltRotation, Vector3.zero, ref targetTiltVelocity, tiltSmoothing);
		tiltRotation = Vector3.SmoothDamp(tiltRotation, targetTiltRotation, ref tiltVelocity, tiltSmoothing);
		target.localRotation = Quaternion.Euler(tiltRotation);
		if (playerData.isAiming)
		{
			targetPushPosition.z = aimPush * (0f - playerData.movementInput.y);
		}
		else
		{
			targetPushPosition.z = hipPush * (0f - playerData.movementInput.y);
		}
		targetPushPosition = Vector3.SmoothDamp(targetPushPosition, Vector3.zero, ref targetPushVelocity, pushSmoothing);
		pushPosition = Vector3.SmoothDamp(pushPosition, targetPushPosition, ref pushVelocity, pushSmoothing);
		target.localPosition = pushPosition;
	}
}
