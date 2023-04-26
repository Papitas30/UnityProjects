using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Focus : MonoBehaviour
{
	[Header("Player Data")]
	public PlayerData playerData;

	[Header("Settings")]
	public float targetIntensity;

	public float lerpSpeed;

	public Volume volume;

	private Vignette targetVignette;

	private float previousIntensity;

	private void Start()
	{
		Vignette component;
		if (volume.profile.TryGet<Vignette>(out component))
		{
			targetVignette = component;
			previousIntensity = targetVignette.intensity.value;
		}
	}

	private void Update()
	{
		if (playerData.isHoldingBreath)
		{
			targetVignette.intensity.value = Mathf.Lerp(targetVignette.intensity.value, targetIntensity, lerpSpeed * Time.deltaTime);
		}
		else
		{
			targetVignette.intensity.value = Mathf.Lerp(targetVignette.intensity.value, previousIntensity, lerpSpeed * Time.deltaTime);
		}
	}
}
