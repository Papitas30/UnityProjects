using System.Collections.Generic;
using OccaSoftware.Buto;
using UnityEngine;
using UnityEngine.Rendering;

public class TimeManager : MonoBehaviour
{
	[Header("Lighting References")]
	public PlayerData playerData;

	public Material sky;

	public Light sun;

	public ReflectionProbe probe;

	public Volume volume;

	public Light thunder;

	[Header("Weather VFX")]
	public GameObject rainVFX;

	private AudioManager audioManager;

	public bool sandboxDay;

	public bool rain;

	public bool storm;

	[Header("Wind Materials")]
	public List<Material> treeMaterials;

	public List<Material> foliageMaterials;

	[Header("Volumetrics")]
	public VolumetricFog volumetrics;

	private float thunderTimer;

	private void Awake()
	{
		audioManager = GameObject.FindWithTag("Audio Manager").GetComponent<AudioManager>();
		thunderTimer = Random.Range(1f, 10f);
	}

	private void Update()
	{
		if (storm)
		{
			thunderTimer -= Time.deltaTime;
			if (thunderTimer <= 0f)
			{
				thunder.intensity = 2000f;
				sky.SetFloat("_Exposure", 1.25f);
				Invoke("ThunderReset", 0.1f);
				audioManager.PlayThunder();
				thunderTimer = Random.Range(5f, 20f);
			}
		}
	}

	public void ApplyWind()
	{
		foreach (Material treeMaterial in treeMaterials)
		{
			treeMaterial.SetFloat("_BendAmount", 0.015f);
		}
		foreach (Material foliageMaterial in foliageMaterials)
		{
			foliageMaterial.SetFloat("_Speed", 2f);
			foliageMaterial.SetFloat("_Frequency", 0.5f);
			foliageMaterial.SetFloat("_Amplitude", 0.075f);
		}
	}

	public void ResetWind()
	{
		foreach (Material treeMaterial in treeMaterials)
		{
			treeMaterial.SetFloat("_BendAmount", 0.0075f);
		}
		foreach (Material foliageMaterial in foliageMaterials)
		{
			foliageMaterial.SetFloat("_Speed", 1f);
			foliageMaterial.SetFloat("_Frequency", 0.5f);
			foliageMaterial.SetFloat("_Amplitude", 0.02f);
		}
	}

	public void ThunderReset()
	{
		thunder.intensity = 0f;
		sky.SetFloat("_Exposure", 1f);
	}

	public void Dawn()
	{
		playerData.isDawn = true;
		playerData.isDay = false;
		playerData.isDusk = false;
		playerData.isNight = false;
		if (rain)
		{
			rainVFX.SetActive(true);
			sun.intensity = 0.5f;
			sun.shadowStrength = 0.8f;
			sun.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 1);
			sun.transform.eulerAngles = new Vector3(45f, 20f);
			RenderSettings.ambientIntensity = 0.8f;
			RenderSettings.fogStartDistance = 0f;
			RenderSettings.fogEndDistance = 250f;
			RenderSettings.fogColor = new Color32(120, 120, 130, 1);
			sky.SetFloat("_Exposure", 1f);
			sky.SetColor("_Tint", new Color32(75, 75, 80, 1));
			sky.SetColor("_FogCol", new Color32(120, 120, 130, 1));
			probe.RenderProbe();
			DynamicGI.UpdateEnvironment();
			if (storm)
			{
				audioManager.PlayStorm();
				ApplyWind();
			}
			else
			{
				audioManager.PlayRain();
				ResetWind();
			}
			sky.SetColor("_MieTint", new Color32(byte.MaxValue, 210, 150, 1));
			sky.SetFloat("_MieIntens", 0f);
			sky.SetFloat("_MieSize", 0.1f);
			sky.SetFloat("_SunDir", 250f);
		}
		else
		{
			rainVFX.SetActive(false);
			sun.intensity = 0.75f;
			sun.shadowStrength = 1f;
			sun.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 1);
			sun.transform.eulerAngles = new Vector3(45f, 20f);
			RenderSettings.ambientIntensity = 0.8f;
			RenderSettings.fogStartDistance = 0f;
			RenderSettings.fogEndDistance = 250f;
			RenderSettings.fogColor = new Color32(140, 145, 160, 1);
			sky.SetFloat("_Exposure", 1f);
			sky.SetColor("_Tint", new Color32(110, 110, 120, 1));
			sky.SetColor("_FogCol", new Color32(165, 170, 183, 1));
			probe.RenderProbe();
			DynamicGI.UpdateEnvironment();
			audioManager.PlayDawn();
			ResetWind();
			sky.SetColor("_MieTint", new Color32(byte.MaxValue, 210, 150, 1));
			sky.SetFloat("_MieIntens", 1f);
			sky.SetFloat("_MieSize", 0.1f);
			sky.SetFloat("_SunDir", 250f);
		}
		ResetExposure();
	}

	public void Day()
	{
		playerData.isDawn = false;
		playerData.isDay = true;
		playerData.isDusk = false;
		playerData.isNight = false;
		if (rain)
		{
			rainVFX.SetActive(true);
			sun.intensity = 0.5f;
			sun.shadowStrength = 0.8f;
			sun.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 1);
			sun.transform.eulerAngles = new Vector3(45f, 20f);
			RenderSettings.ambientIntensity = 0.8f;
			RenderSettings.fogStartDistance = 0f;
			RenderSettings.fogEndDistance = 250f;
			RenderSettings.fogColor = new Color32(128, 128, 128, 1);
			sky.SetFloat("_Exposure", 1f);
			sky.SetColor("_Tint", new Color32(110, 110, 110, 1));
			sky.SetColor("_FogCol", new Color32(128, 128, 128, 1));
			probe.RenderProbe();
			DynamicGI.UpdateEnvironment();
			if (storm)
			{
				audioManager.PlayStorm();
				ApplyWind();
			}
			else
			{
				audioManager.PlayRain();
				ResetWind();
			}
			sky.SetColor("_MieTint", new Color32(byte.MaxValue, 210, 150, 1));
			sky.SetFloat("_MieIntens", 0f);
			sky.SetFloat("_MieSize", 0.1f);
			sky.SetFloat("_SunDir", 250f);
		}
		else
		{
			rainVFX.SetActive(false);
			sun.shadowStrength = 1f;
			sun.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 1);
			if (sandboxDay)
			{
				sun.intensity = 1.5f;
				sun.transform.eulerAngles = new Vector3(45f, 45f);
			}
			else
			{
				sun.intensity = 1.5f;
				sun.transform.eulerAngles = new Vector3(45f, 45f);
			}
			RenderSettings.ambientIntensity = 0.8f;
			RenderSettings.fogStartDistance = 0f;
			RenderSettings.fogEndDistance = 250f;
			RenderSettings.fogColor = new Color32(155, 155, 160, 1);
			sky.SetFloat("_Exposure", 1f);
			sky.SetColor("_Tint", new Color32(165, 175, 185, 1));
			sky.SetColor("_FogCol", new Color32(169, 173, 176, 1));
			probe.RenderProbe();
			DynamicGI.UpdateEnvironment();
			audioManager.PlayDay();
			ResetWind();
			sky.SetColor("_MieTint", new Color32(0, 0, 0, 1));
			sky.SetFloat("_MieIntens", 0f);
			sky.SetFloat("_MieSize", 0f);
			sky.SetFloat("_SunDir", 0f);
		}
		ResetExposure();
	}

	public void Dusk()
	{
		playerData.isDawn = false;
		playerData.isDay = false;
		playerData.isDusk = true;
		playerData.isNight = false;
		if (rain)
		{
			rainVFX.SetActive(true);
			sun.intensity = 0.5f;
			sun.shadowStrength = 0.8f;
			sun.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 1);
			sun.transform.eulerAngles = new Vector3(45f, 20f);
			RenderSettings.ambientIntensity = 0.8f;
			RenderSettings.fogStartDistance = 0f;
			RenderSettings.fogEndDistance = 250f;
			RenderSettings.fogColor = new Color32(130, 125, 120, 1);
			sky.SetFloat("_Exposure", 1f);
			sky.SetColor("_Tint", new Color32(105, 100, 95, 1));
			sky.SetColor("_FogCol", new Color32(130, 125, 120, 1));
			probe.RenderProbe();
			DynamicGI.UpdateEnvironment();
			if (storm)
			{
				audioManager.PlayStorm();
				ApplyWind();
			}
			else
			{
				audioManager.PlayRain();
				ResetWind();
			}
			sky.SetColor("_MieTint", new Color32(byte.MaxValue, 210, 150, 1));
			sky.SetFloat("_MieIntens", 0f);
			sky.SetFloat("_MieSize", 0.1f);
			sky.SetFloat("_SunDir", 250f);
		}
		else
		{
			rainVFX.SetActive(false);
			sun.intensity = 1.5f;
			sun.shadowStrength = 1f;
			sun.color = new Color32(byte.MaxValue, 235, 220, 1);
			sun.transform.eulerAngles = new Vector3(45f, 100f);
			RenderSettings.ambientIntensity = 0.8f;
			RenderSettings.fogStartDistance = 0f;
			RenderSettings.fogEndDistance = 250f;
			RenderSettings.fogColor = new Color32(160, 150, 145, 1);
			sky.SetFloat("_Exposure", 1f);
			sky.SetColor("_Tint", new Color32(165, 160, 150, 1));
			sky.SetColor("_FogCol", new Color32(179, 168, 163, 1));
			probe.RenderProbe();
			DynamicGI.UpdateEnvironment();
			audioManager.PlayDusk();
			ResetWind();
			sky.SetColor("_MieTint", new Color32(byte.MaxValue, 144, 0, 1));
			sky.SetFloat("_MieIntens", 1f);
			sky.SetFloat("_MieSize", 0.1f);
			sky.SetFloat("_SunDir", 178f);
		}
		ResetExposure();
	}

	public void Night()
	{
		playerData.isDawn = false;
		playerData.isDay = false;
		playerData.isDusk = false;
		playerData.isNight = true;
		if (rain)
		{
			rainVFX.SetActive(true);
			sun.intensity = 0.1f;
			sun.shadowStrength = 0.5f;
			sun.color = new Color32(225, 240, byte.MaxValue, 1);
			sun.transform.eulerAngles = new Vector3(45f, 160f);
			RenderSettings.ambientIntensity = 1.2f;
			RenderSettings.fogStartDistance = -100f;
			RenderSettings.fogEndDistance = 250f;
			RenderSettings.fogColor = new Color32(35, 35, 40, 1);
			sky.SetFloat("_Exposure", 1f);
			sky.SetColor("_Tint", new Color32(25, 25, 27, 1));
			sky.SetColor("_FogCol", new Color32(35, 35, 40, 1));
			probe.RenderProbe();
			DynamicGI.UpdateEnvironment();
			if (storm)
			{
				audioManager.PlayStorm();
				ApplyWind();
			}
			else
			{
				audioManager.PlayRain();
				ResetWind();
			}
			sky.SetColor("_MieTint", new Color32(0, 0, 0, 1));
			sky.SetFloat("_MieIntens", 0f);
			sky.SetFloat("_MieSize", 0f);
			sky.SetFloat("_SunDir", 0f);
		}
		else
		{
			rainVFX.SetActive(false);
			sun.intensity = 0.1f;
			sun.shadowStrength = 0.5f;
			sun.color = new Color32(225, 240, byte.MaxValue, 1);
			sun.transform.eulerAngles = new Vector3(45f, 160f);
			RenderSettings.ambientIntensity = 1.2f;
			RenderSettings.fogStartDistance = -100f;
			RenderSettings.fogEndDistance = 250f;
			RenderSettings.fogColor = new Color32(35, 35, 40, 1);
			sky.SetFloat("_Exposure", 1f);
			sky.SetColor("_Tint", new Color32(25, 25, 27, 1));
			sky.SetColor("_FogCol", new Color32(40, 44, 46, 1));
			probe.RenderProbe();
			DynamicGI.UpdateEnvironment();
			audioManager.PlayNight();
			ResetWind();
			sky.SetColor("_MieTint", new Color32(0, 0, 0, 1));
			sky.SetFloat("_MieIntens", 0f);
			sky.SetFloat("_MieSize", 0f);
			sky.SetFloat("_SunDir", 0f);
		}
		ResetExposure();
	}

	private void ResetExposure()
	{
		sky.SetFloat("_Exposure", 0.75f);
	}
}
