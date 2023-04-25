using System;

[Serializable]
public class CameraDisable
{
	public enum cameraDisable
	{
		None = 0,
		Distance = 1
	}

	public cameraDisable o_cameraDisable;

	public float distance;

	public bool canZoomCamera;
}
