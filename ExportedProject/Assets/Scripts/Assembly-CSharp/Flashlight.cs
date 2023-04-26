using UnityEngine;

public class Flashlight : MonoBehaviour
{
	public PlayerData playerData;

	public Light flashlight;

	public Transform target;

	public float lerpSpeed;

	private void Update()
	{
		if (playerData.flashlightActive)
		{
			flashlight.enabled = true;
		}
		else
		{
			flashlight.enabled = false;
		}
		base.transform.position = Vector3.Lerp(base.transform.position, target.position, lerpSpeed * Time.deltaTime);
		base.transform.rotation = Quaternion.Slerp(base.transform.rotation, target.rotation, lerpSpeed * Time.deltaTime);
	}
}
