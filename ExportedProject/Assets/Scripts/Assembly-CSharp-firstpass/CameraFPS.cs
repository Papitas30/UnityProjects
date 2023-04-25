using System;

[Serializable]
public class CameraFPS
{
	public enum FPSLimitType
	{
		None = 0,
		Half = 1,
		Quarter = 2,
		Fixed = 3
	}

	public FPSLimitType fPSLimitType;

	public float fixedFps = 60f;
}
