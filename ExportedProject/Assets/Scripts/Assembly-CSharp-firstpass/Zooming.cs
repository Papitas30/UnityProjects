using System;
using UnityEngine;

[Serializable]
public class Zooming
{
	public enum ZoomType
	{
		None = 0,
		Variable = 1,
		Toggle = 2
	}

	public ZoomType zoomType;

	public KeyCode ActivatorKey = KeyCode.LeftAlt;

	[Header("Variable")]
	public bool zoomReticle;

	public float startFOV = 60f;

	public float zoomSpeed = 5f;

	public float currentSize = 1f;

	public float sizeIncrements = 1f;

	public float minSize = 1f;

	public float maxSize = 2f;

	public float currentZoom = 1f;

	public float zoomIncrements = 1f;

	public int minZoom = 1;

	public int maxZoom = 6;

	[Header("Toggle")]
	public bool isToggled;

	public KeyCode ToggleKey = KeyCode.Mouse1;

	public float ToggledZoomFOV = 15f;

	public float UntoggledZoomFOV = 60f;

	public bool t_zoomReticle;

	public float ToggledReticleSize = 2f;

	public float unToggledReticleSize = 1f;
}
