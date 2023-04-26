using UnityEngine;

public class UICursor : MonoBehaviour
{
	public enum CursomMode
	{
		Use = 0,
		Inspect = 1,
		Transfer = 2,
		Drop = 3,
		Destroy = 4
	}

	public enum SortMode
	{
		Name = 0,
		Type = 1,
		Volume = 2,
		Weight = 3,
		Value = 4,
		Rarity = 5
	}

	[Header("Cursor Mode")]
	public CursomMode cursorMode;

	[Header("Sort Mode")]
	public SortMode sortMode;

	public void ShowCursor()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void HideCursor()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public void Use()
	{
		cursorMode = CursomMode.Use;
	}

	public void Inspect()
	{
		cursorMode = CursomMode.Inspect;
	}

	public void Transfer()
	{
		cursorMode = CursomMode.Transfer;
	}

	public void Drop()
	{
		cursorMode = CursomMode.Drop;
	}

	public void Destroy()
	{
		cursorMode = CursomMode.Destroy;
	}
}
