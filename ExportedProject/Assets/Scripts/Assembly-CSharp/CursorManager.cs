using UnityEngine;

public class CursorManager : MonoBehaviour
{
	public void ShowCursor()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void HideCursor()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
}
