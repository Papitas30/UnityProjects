using UnityEngine;

public class CursorHandler : MonoBehaviour
{
	public bool isMenu;

	private void Start()
	{
		UnlockCursor();
	}

	public void LockCursor()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public void UnlockCursor()
	{
		Cursor.lockState = CursorLockMode.Confined;
		if (isMenu)
		{
			Cursor.visible = true;
		}
		else
		{
			Cursor.visible = false;
		}
	}
}
