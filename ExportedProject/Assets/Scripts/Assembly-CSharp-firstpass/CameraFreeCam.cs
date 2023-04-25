using UnityEngine;

public class CameraFreeCam : MonoBehaviour
{
	public float Speed = 5f;

	private void Update()
	{
		if (!Input.GetKey(KeyCode.LeftAlt))
		{
			base.transform.Translate(Input.GetAxisRaw("Horizontal") * Speed * Time.deltaTime, Input.GetAxisRaw("Vertical") * Speed * Time.deltaTime, Input.GetAxisRaw("Mouse ScrollWheel") * Speed * 100f * Time.deltaTime);
		}
	}
}
