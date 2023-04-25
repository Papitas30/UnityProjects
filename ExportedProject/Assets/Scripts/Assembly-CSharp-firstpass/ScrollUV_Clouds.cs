using UnityEngine;

public class ScrollUV_Clouds : MonoBehaviour
{
	public float horizontalScrollSpeed = 0.25f;

	public float verticalScrollSpeed = 0.25f;

	private Renderer _myRenderer;

	private bool scroll = true;

	private void Start()
	{
		_myRenderer = GetComponent<Renderer>();
		if (_myRenderer == null)
		{
			base.enabled = false;
		}
	}

	public void FixedUpdate()
	{
		if (scroll)
		{
			float y = Time.time * verticalScrollSpeed;
			float x = Time.time * horizontalScrollSpeed;
			_myRenderer.material.mainTextureOffset = new Vector2(x, y);
		}
	}

	public void DoActivateTrigger()
	{
		scroll = !scroll;
	}
}
