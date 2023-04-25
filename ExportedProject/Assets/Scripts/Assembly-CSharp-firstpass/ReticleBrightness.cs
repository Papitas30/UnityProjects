using System;
using UnityEngine;

[Serializable]
public class ReticleBrightness
{
	public bool can_Illuminate;

	[Space(5f)]
	public KeyCode brightness_increase = KeyCode.UpArrow;

	public KeyCode brightness_decrease = KeyCode.DownArrow;

	[Space(5f)]
	public float v_brightness_increase = 1f;

	public float v_brightness_max = 25f;

	public float v_brightness_min;

	public float v_brightness = 1f;
}
