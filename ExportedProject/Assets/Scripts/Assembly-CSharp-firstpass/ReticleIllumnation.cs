using System;

[Serializable]
public class ReticleIllumnation
{
	public bool dynamicReticleIllumnation;

	public float illumnationSteps = 0.5f;

	public float currentIllumnation = 10f;

	public float MaxIllumnation = 35f;

	public float MinIllumnation = 10f;
}
