using UnityEngine;

public class ReflexSightV2 : MonoBehaviour
{
	[Header("------ KEYCODES ------")]
	public KeyCode activatorKey = KeyCode.LeftControl;

	[Space(10f)]
	public KeyCode switchReticleKey = KeyCode.Mouse1;

	[Space(10f)]
	public KeyCode brightnessIncrease = KeyCode.UpArrow;

	public KeyCode brightnessDecrease = KeyCode.DownArrow;

	[Space(10f)]
	public Material ReticleMaterial;

	[Space(10f)]
	public ReticleCycling reticles;

	public ReticleIllumnation reticleIllumnation;

	[Space(10f)]
	public AudioSource transitionSound;

	private void Start()
	{
		if (ReticleMaterial == null)
		{
			ReticleMaterial = GetComponent<MeshRenderer>().material;
			if (ReticleMaterial == null)
			{
				Debug.LogError("NO MATERIAL ASSIGNED! " + base.gameObject.name);
			}
		}
		if (reticles.reticleCycling)
		{
			cycleReticle();
		}
		if (reticleIllumnation.dynamicReticleIllumnation)
		{
			reticleIllumnation.currentIllumnation = ReticleMaterial.GetFloat("Reticle_Brightness");
			if (reticleIllumnation.currentIllumnation > reticleIllumnation.MaxIllumnation)
			{
				reticleIllumnation.currentIllumnation = reticleIllumnation.MaxIllumnation;
			}
			if (reticleIllumnation.currentIllumnation < reticleIllumnation.MinIllumnation)
			{
				reticleIllumnation.currentIllumnation = reticleIllumnation.MinIllumnation;
			}
			ReticleMaterial.SetFloat("Reticle_Brightness", reticleIllumnation.currentIllumnation);
		}
	}

	public void Update()
	{
		if (reticles.reticleCycling && Input.GetKey(activatorKey) && Input.GetKeyDown(switchReticleKey))
		{
			cycleReticle();
		}
		if (!reticleIllumnation.dynamicReticleIllumnation)
		{
			return;
		}
		if (Input.GetKey(brightnessIncrease))
		{
			reticleIllumnation.currentIllumnation += reticleIllumnation.illumnationSteps;
			if (reticleIllumnation.currentIllumnation > reticleIllumnation.MaxIllumnation)
			{
				reticleIllumnation.currentIllumnation = reticleIllumnation.MaxIllumnation;
			}
		}
		if (Input.GetKey(brightnessDecrease))
		{
			reticleIllumnation.currentIllumnation -= reticleIllumnation.illumnationSteps;
			if (reticleIllumnation.currentIllumnation < reticleIllumnation.MinIllumnation)
			{
				reticleIllumnation.currentIllumnation = reticleIllumnation.MinIllumnation;
			}
		}
		ReticleMaterial.SetFloat("Reticle_Brightness", reticleIllumnation.currentIllumnation);
	}

	public void cycleReticle()
	{
		reticles.reticleIndex++;
		if (reticles.reticleIndex > reticles.allReticles.Count - 1)
		{
			reticles.reticleIndex = 0;
		}
		ReticleMaterial.SetTexture("Reticle", reticles.allReticles[reticles.reticleIndex]);
		if (transitionSound.clip != null)
		{
			transitionSound.Play();
		}
	}
}
