using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
	[Header("PlayerData")]
	public PlayerData playerData;

	[Header("Render Quality")]
	public int renderQuality;

	[Header("Pipeline Assets")]
	public UniversalRenderPipelineAsset performance;

	public UniversalRenderPipelineAsset standard;

	public UniversalRenderPipelineAsset extreme;

	[Header("Field of View")]
	public float worldFOV;

	public float weaponFOV;

	public Slider worldFOVSlider;

	public Slider weaponFOVSlider;

	[Header("Time of Day")]
	public int timeOfDay;

	[Header("Weather")]
	public int weather;

	[Header("Sharpen Filter")]
	public int sharpenFilter;

	[Header("Texture Resolution")]
	public int textureResolution;

	[Header("Vertical Sync")]
	public int vSync;

	[Header("Profiler")]
	public int profiler;

	[Header("Camera Noise")]
	public int cameraNoise;

	[Header("Dual Render")]
	public int dualRender;

	[Header("AI Spawner")]
	public int aiSpawner;

	[Header("Look Settings")]
	public float mouseSensitivity;

	public float scopeSensitivity;

	public int cameraSmoothing;

	public Slider mouseSensitivitySlider;

	public Slider scopeSensitivitySlider;

	[Header("Weapon Position")]
	public float horizontalPosition;

	public float verticalPosition;

	public Slider weaponHorizontalSlider;

	public Slider weaponVerticalSlider;

	[Header("Audio Settings")]
	public float ambientVolume;

	public float SFXVolume;

	public AudioMixerGroup ambientMixer;

	public AudioMixerGroup SFXMixer;

	public Slider ambientVolumeSlider;

	public Slider SFXVolumeSlider;

	[Header("Post-prosessing")]
	public Volume volume;

	public int contrast;

	public int saturation;

	public int vignette;

	public int bloom;

	[Header("Decals")]
	public ScriptableRendererFeature decals;

	[Header("Ambient Occlusion")]
	public int ambientOcclusion;

	public ScriptableRendererFeature deferredSSAO;

	public ScriptableRendererFeature forwardSSAO;

	public ScriptableRendererFeature FPSSSAO;

	[Header("FPS Profiler")]
	public GameObject fpsCounter;

	private Breathing breathing;

	private UIManager UIManager;

	private InputManager inputManager;

	private TimeManager timeManager;

	private Look cameraLook;

	private MapManager mapManager;

	private WeaponHandling weaponHandling;

	public ColorAdjustments targetGrading;

	public Vignette targetVignette;

	private Bloom targetBloom;

	private void Awake()
	{
		weaponHandling = GameObject.FindWithTag("Player").GetComponent<WeaponHandling>();
		weaponHandling = GameObject.FindWithTag("Player").GetComponent<WeaponHandling>();
		cameraLook = GameObject.FindWithTag("Player").GetComponent<Look>();
		UIManager = GameObject.FindWithTag("UI Manager").GetComponent<UIManager>();
		mapManager = GameObject.FindWithTag("Map Manager").GetComponent<MapManager>();
		inputManager = GameObject.FindWithTag("Input Manager").GetComponent<InputManager>();
		timeManager = GameObject.FindWithTag("Time Manager").GetComponent<TimeManager>();
		breathing = GameObject.FindWithTag("Player").GetComponent<Breathing>();
		InitialPrefValues();
	}

	private void InitialPrefValues()
	{
		timeOfDay = PlayerPrefs.GetInt("TOD", 2);
		weather = PlayerPrefs.GetInt("Weather", 1);
		renderQuality = PlayerPrefs.GetInt("Render Quality", 2);
		textureResolution = PlayerPrefs.GetInt("Texture Resolution", 3);
		cameraSmoothing = PlayerPrefs.GetInt("Smoothing", 2);
		contrast = PlayerPrefs.GetInt("Contrast", 2);
		saturation = PlayerPrefs.GetInt("Saturation", 2);
		vignette = PlayerPrefs.GetInt("Vignette", 2);
		bloom = PlayerPrefs.GetInt("Bloom", 2);
		worldFOV = PlayerPrefs.GetFloat("World FOV", 70f);
		weaponFOV = PlayerPrefs.GetFloat("Weapon FOV", 60f);
		vSync = PlayerPrefs.GetInt("Vsync", 1);
		horizontalPosition = PlayerPrefs.GetFloat("Horizontal Position", 0f);
		verticalPosition = PlayerPrefs.GetFloat("Vertical Position", 0f);
		mouseSensitivity = PlayerPrefs.GetFloat("Mouse Sensitivity", 0f);
		scopeSensitivity = PlayerPrefs.GetFloat("Scope Sensitivity", 0f);
		ambientVolume = PlayerPrefs.GetFloat("Ambient Volume", 0f);
		SFXVolume = PlayerPrefs.GetFloat("SFX Volume", 0f);
		profiler = PlayerPrefs.GetInt("Profiler", 2);
		ambientOcclusion = PlayerPrefs.GetInt("SSAO", 2);
		sharpenFilter = PlayerPrefs.GetInt("Sharpen", 2);
		cameraNoise = PlayerPrefs.GetInt("Noise", 2);
		dualRender = PlayerPrefs.GetInt("Dual Render", 1);
		aiSpawner = PlayerPrefs.GetInt("AI Spawner", 1);
	}

	private void Start()
	{
		if (timeOfDay == 1)
		{
			UIManager.Dawn();
			timeManager.Dawn();
		}
		if (timeOfDay == 2)
		{
			UIManager.Day();
			timeManager.Day();
		}
		if (timeOfDay == 3)
		{
			UIManager.Dusk();
			timeManager.Dusk();
		}
		if (timeOfDay == 4)
		{
			UIManager.Night();
			timeManager.Night();
		}
		if (weather == 1)
		{
			UIManager.ChangeWeatherIndicator(1);
			timeManager.rain = false;
			timeManager.storm = false;
			ChangeTimeOfDay(timeOfDay);
		}
		if (weather == 2)
		{
			UIManager.ChangeWeatherIndicator(2);
			timeManager.rain = true;
			timeManager.storm = false;
			ChangeTimeOfDay(timeOfDay);
		}
		if (weather == 3)
		{
			UIManager.ChangeWeatherIndicator(3);
			timeManager.rain = true;
			timeManager.storm = true;
			ChangeTimeOfDay(timeOfDay);
		}
		QualitySettings.SetQualityLevel(renderQuality);
		UIManager.ChangeRenderQuality(renderQuality);
		if (renderQuality == 1)
		{
			decals.SetActive(false);
			QualitySettings.SetQualityLevel(0);
			if (vSync == 0)
			{
				QualitySettings.vSyncCount = 0;
			}
			else
			{
				QualitySettings.vSyncCount = 1;
			}
		}
		if (renderQuality == 2)
		{
			decals.SetActive(true);
			QualitySettings.SetQualityLevel(1);
			if (vSync == 0)
			{
				QualitySettings.vSyncCount = 0;
			}
			else
			{
				QualitySettings.vSyncCount = 1;
			}
		}
		if (renderQuality == 3)
		{
			QualitySettings.SetQualityLevel(2);
			if (vSync == 0)
			{
				QualitySettings.vSyncCount = 0;
			}
			else
			{
				QualitySettings.vSyncCount = 1;
			}
		}
		if (textureResolution == 2)
		{
			QualitySettings.masterTextureLimit = 2;
		}
		if (textureResolution == 1)
		{
			QualitySettings.masterTextureLimit = 1;
		}
		if (textureResolution == 0)
		{
			QualitySettings.masterTextureLimit = 0;
		}
		ColorAdjustments component;
		if (volume.profile.TryGet<ColorAdjustments>(out component))
		{
			targetGrading = component;
			UIManager.ChangeContrastIndicator(contrast);
			UIManager.ChangeSaturationIndicator(saturation);
			if (contrast == 1)
			{
				targetGrading.contrast.value = 0f;
			}
			if (contrast == 2)
			{
				targetGrading.contrast.value = 10f;
			}
			if (contrast == 3)
			{
				targetGrading.contrast.value = 20f;
			}
			if (saturation == 1)
			{
				targetGrading.saturation.value = -20f;
			}
			if (saturation == 2)
			{
				targetGrading.saturation.value = 0f;
			}
			if (saturation == 3)
			{
				targetGrading.saturation.value = 20f;
			}
		}
		Vignette component2;
		if (volume.profile.TryGet<Vignette>(out component2))
		{
			targetVignette = component2;
			UIManager.ChangeVignetteIndicator(vignette);
			if (vignette == 1)
			{
				targetVignette.intensity.value = 0f;
			}
			if (vignette == 2)
			{
				targetVignette.intensity.value = 0.25f;
			}
			if (vignette == 3)
			{
				targetVignette.intensity.value = 0.35f;
			}
		}
		Bloom component3;
		if (volume.profile.TryGet<Bloom>(out component3))
		{
			targetBloom = component3;
			UIManager.ChangeBloomIndicator(bloom);
			if (bloom == 1)
			{
				targetBloom.intensity.value = 1f;
				targetBloom.threshold.value = 1f;
			}
			if (bloom == 2)
			{
				targetBloom.intensity.value = 1f;
				targetBloom.threshold.value = 0.8f;
			}
			if (bloom == 3)
			{
				targetBloom.intensity.value = 1f;
				targetBloom.threshold.value = 0.6f;
			}
		}
		UIManager.ChangeSmoothingIndicator(cameraSmoothing);
		if (cameraSmoothing == 1)
		{
			cameraLook.smoothing = 0.025f;
		}
		if (cameraSmoothing == 2)
		{
			cameraLook.smoothing = 0.05f;
		}
		if (cameraSmoothing == 3)
		{
			cameraLook.smoothing = 0.1f;
		}
		QualitySettings.vSyncCount = vSync;
		if (vSync == 1)
		{
			QualitySettings.vSyncCount = 0;
			UIManager.VsyncOff();
		}
		if (vSync == 2)
		{
			QualitySettings.vSyncCount = 1;
			UIManager.VsyncOn();
		}
		cameraLook.mouseSensitivity = mouseSensitivity;
		mouseSensitivitySlider.value = mouseSensitivity;
		UIManager.ChangeMouseSensitivity((int)mouseSensitivity);
		ambientVolumeSlider.value = ambientVolume;
		UIManager.ChangeAmbientVolume((int)ambientVolume);
		SFXVolumeSlider.value = SFXVolume;
		UIManager.ChangeSFXVolume((int)SFXVolume);
		weaponHandling.worldFOV = worldFOV;
		worldFOVSlider.value = worldFOV;
		UIManager.ChangeWorldFOV((int)worldFOV);
		weaponHandling.weaponFOV = weaponFOV;
		weaponFOVSlider.value = weaponFOV;
		UIManager.ChangeWeaponFOV((int)weaponFOV);
		cameraLook.scopeSensitivity = scopeSensitivity;
		scopeSensitivitySlider.value = scopeSensitivity;
		UIManager.ChangeScopeSensitivity((int)scopeSensitivity);
		weaponHorizontalSlider.value = horizontalPosition;
		weaponHandling.horizontalOffset = horizontalPosition / 400f;
		UIManager.ChangeWeaponHorizontalPosition((int)horizontalPosition);
		weaponVerticalSlider.value = verticalPosition;
		weaponHandling.verticalOffset = verticalPosition / 400f;
		UIManager.ChangeWeaponVerticalPosition((int)verticalPosition);
		if (profiler == 1)
		{
			UIManager.ProfilerOff();
			fpsCounter.SetActive(false);
		}
		if (profiler == 2)
		{
			UIManager.ProfilerOn();
			fpsCounter.SetActive(true);
		}
		if (textureResolution == 1)
		{
			UIManager.ChangeTextureResolution(1);
			QualitySettings.masterTextureLimit = 2;
		}
		if (textureResolution == 2)
		{
			UIManager.ChangeTextureResolution(2);
			QualitySettings.masterTextureLimit = 1;
		}
		if (textureResolution == 3)
		{
			UIManager.ChangeTextureResolution(3);
			QualitySettings.masterTextureLimit = 0;
		}
		if (ambientOcclusion == 1)
		{
			UIManager.ChangeAOIndicator(1);
			deferredSSAO.SetActive(false);
			forwardSSAO.SetActive(false);
			FPSSSAO.SetActive(false);
		}
		if (ambientOcclusion == 2)
		{
			UIManager.ChangeAOIndicator(2);
			deferredSSAO.SetActive(true);
			forwardSSAO.SetActive(true);
			FPSSSAO.SetActive(true);
		}
		UIManager.ChangeSharpenFilterIndicator(sharpenFilter);
		if (sharpenFilter == 1)
		{
			performance.upscalingFilter = UpscalingFilterSelection.Auto;
			standard.upscalingFilter = UpscalingFilterSelection.Auto;
			extreme.upscalingFilter = UpscalingFilterSelection.Auto;
		}
		if (sharpenFilter == 2)
		{
			performance.upscalingFilter = UpscalingFilterSelection.FSR;
			standard.upscalingFilter = UpscalingFilterSelection.FSR;
			extreme.upscalingFilter = UpscalingFilterSelection.FSR;
		}
		UIManager.ChangeCameraNoiseIndicator(cameraNoise);
		if (cameraNoise == 1)
		{
			playerData.bobMultiplier = 0.75f;
			breathing.multiplier = 0.1f;
		}
		if (cameraNoise == 2)
		{
			playerData.bobMultiplier = 1f;
			breathing.multiplier = 0.5f;
		}
		if (cameraNoise == 3)
		{
			playerData.bobMultiplier = 1.2f;
			breathing.multiplier = 0.75f;
		}
		UIManager.ChangeDualRenderIndicator(dualRender);
		if (dualRender == 1)
		{
			playerData.dualRender = false;
		}
		if (dualRender == 2)
		{
			playerData.dualRender = true;
		}
		UIManager.ChangeAISpawnerIndicator(aiSpawner);
		if (aiSpawner == 1)
		{
			playerData.spawnAI = false;
		}
		if (aiSpawner == 2)
		{
			playerData.spawnAI = true;
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			ChangeTimeOfDay(1);
		}
		if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			ChangeTimeOfDay(2);
		}
		if (Input.GetKeyDown(KeyCode.Alpha6))
		{
			ChangeTimeOfDay(3);
		}
		if (Input.GetKeyDown(KeyCode.Alpha7))
		{
			ChangeTimeOfDay(4);
		}
	}

	public void ChangeAISpawner(int value)
	{
		aiSpawner = value;
		PlayerPrefs.SetInt("AI Spawner", aiSpawner);
		UIManager.ChangeAISpawnerIndicator(value);
		if (value == 1)
		{
			playerData.spawnAI = false;
		}
		if (value == 2)
		{
			playerData.spawnAI = true;
		}
	}

	public void ChangeDualRender(int value)
	{
		dualRender = value;
		PlayerPrefs.SetInt("Dual Render", dualRender);
		UIManager.ChangeDualRenderIndicator(value);
		if (value == 1)
		{
			playerData.dualRender = false;
		}
		if (value == 2)
		{
			playerData.dualRender = true;
		}
	}

	public void ChangeCameraNoise(int value)
	{
		cameraNoise = value;
		PlayerPrefs.SetInt("Noise", cameraNoise);
		UIManager.ChangeCameraNoiseIndicator(value);
		if (cameraNoise == 1)
		{
			playerData.bobMultiplier = 0.75f;
			breathing.multiplier = 0.1f;
		}
		if (cameraNoise == 2)
		{
			playerData.bobMultiplier = 1f;
			breathing.multiplier = 0.5f;
		}
		if (cameraNoise == 3)
		{
			playerData.bobMultiplier = 1.2f;
			breathing.multiplier = 0.75f;
		}
	}

	public void ChangeAmbientVolume()
	{
		PlayerPrefs.SetFloat("Ambient Volume", ambientVolumeSlider.value);
		ambientMixer.audioMixer.SetFloat("_AmbientVolume", ambientVolumeSlider.value);
		ambientVolume = ambientVolumeSlider.value;
		UIManager.ChangeAmbientVolume((int)ambientVolume);
	}

	public void ChangeSFXVolume()
	{
		PlayerPrefs.SetFloat("SFX Volume", SFXVolumeSlider.value);
		SFXMixer.audioMixer.SetFloat("_SFXVolume", SFXVolumeSlider.value);
		SFXVolume = SFXVolumeSlider.value;
		UIManager.ChangeSFXVolume((int)SFXVolume);
	}

	public void ChangeWeaponPositionHorizontal()
	{
		PlayerPrefs.SetFloat("Horizontal Position", weaponHorizontalSlider.value);
		horizontalPosition = weaponHorizontalSlider.value;
		weaponHandling.horizontalOffset = horizontalPosition / 400f;
		UIManager.ChangeWeaponHorizontalPosition((int)horizontalPosition);
	}

	public void ChangeWeaponPositionVertical()
	{
		PlayerPrefs.SetFloat("Vertical Position", weaponVerticalSlider.value);
		verticalPosition = weaponVerticalSlider.value;
		weaponHandling.verticalOffset = verticalPosition / 500f;
		UIManager.ChangeWeaponVerticalPosition((int)verticalPosition);
	}

	public void ChangeMouseSensitivity()
	{
		PlayerPrefs.SetFloat("Mouse Sensitivity", mouseSensitivitySlider.value);
		mouseSensitivity = mouseSensitivitySlider.value;
		cameraLook.mouseSensitivity = mouseSensitivity;
		UIManager.ChangeMouseSensitivity((int)mouseSensitivity);
	}

	public void ChangeScopeSensitivity()
	{
		PlayerPrefs.SetFloat("Scope Sensitivity", scopeSensitivitySlider.value);
		scopeSensitivity = scopeSensitivitySlider.value;
		cameraLook.scopeSensitivity = scopeSensitivity;
		UIManager.ChangeScopeSensitivity((int)scopeSensitivity);
	}

	public void ChangeSharpenFilter(int value)
	{
		sharpenFilter = value;
		PlayerPrefs.SetInt("Sharpen", sharpenFilter);
		UIManager.ChangeSharpenFilterIndicator(value);
		if (value == 1)
		{
			performance.upscalingFilter = UpscalingFilterSelection.Auto;
			standard.upscalingFilter = UpscalingFilterSelection.Auto;
			extreme.upscalingFilter = UpscalingFilterSelection.Auto;
		}
		if (value == 2)
		{
			performance.upscalingFilter = UpscalingFilterSelection.FSR;
			standard.upscalingFilter = UpscalingFilterSelection.FSR;
			extreme.upscalingFilter = UpscalingFilterSelection.FSR;
		}
	}

	public void ChangeCameraSmoothing(int value)
	{
		cameraSmoothing = value;
		PlayerPrefs.SetInt("Smoothing", cameraSmoothing);
		UIManager.ChangeSmoothingIndicator(value);
		if (value == 1)
		{
			cameraLook.smoothing = 0.025f;
		}
		if (value == 2)
		{
			cameraLook.smoothing = 0.05f;
		}
		if (value == 3)
		{
			cameraLook.smoothing = 0.1f;
		}
	}

	public void ChangeWeather(int value)
	{
		weather = value;
		PlayerPrefs.SetInt("Weather", weather);
		if (weather == 1)
		{
			UIManager.ChangeWeatherIndicator(1);
			timeManager.rain = false;
			timeManager.storm = false;
			ChangeTimeOfDay(timeOfDay);
		}
		if (weather == 2)
		{
			UIManager.ChangeWeatherIndicator(2);
			timeManager.rain = true;
			timeManager.storm = false;
			ChangeTimeOfDay(timeOfDay);
		}
		if (weather == 3)
		{
			UIManager.ChangeWeatherIndicator(3);
			timeManager.rain = true;
			timeManager.storm = true;
			ChangeTimeOfDay(timeOfDay);
		}
	}

	public void ChangeWorldFOV()
	{
		PlayerPrefs.SetFloat("World FOV", worldFOVSlider.value);
		worldFOV = worldFOVSlider.value;
		weaponHandling.worldFOV = worldFOV;
		UIManager.ChangeWorldFOV((int)worldFOV);
	}

	public void ChangeWeaponFOV()
	{
		PlayerPrefs.SetFloat("Weapon FOV", weaponFOVSlider.value);
		weaponFOV = weaponFOVSlider.value;
		weaponHandling.weaponFOV = weaponFOV;
		UIManager.ChangeWeaponFOV((int)weaponFOV);
	}

	public void ChangeTimeOfDay(int value)
	{
		timeOfDay = value;
		PlayerPrefs.SetInt("TOD", timeOfDay);
		if (timeOfDay == 1)
		{
			UIManager.Dawn();
			timeManager.Dawn();
		}
		if (timeOfDay == 2)
		{
			UIManager.Day();
			timeManager.Day();
		}
		if (timeOfDay == 3)
		{
			UIManager.Dusk();
			timeManager.Dusk();
		}
		if (timeOfDay == 4)
		{
			UIManager.Night();
			timeManager.Night();
		}
	}

	public void ChangeSSAO(int value)
	{
		ambientOcclusion = value;
		PlayerPrefs.SetInt("SSAO", ambientOcclusion);
		if (ambientOcclusion == 1)
		{
			UIManager.ChangeAOIndicator(1);
			deferredSSAO.SetActive(false);
			forwardSSAO.SetActive(false);
			FPSSSAO.SetActive(false);
		}
		if (ambientOcclusion == 2)
		{
			UIManager.ChangeAOIndicator(2);
			deferredSSAO.SetActive(true);
			forwardSSAO.SetActive(true);
			FPSSSAO.SetActive(true);
		}
	}

	public void ChangeTextureResolution(int value)
	{
		textureResolution = value;
		PlayerPrefs.SetInt("Texture Resolution", textureResolution);
		if (textureResolution == 1)
		{
			UIManager.ChangeTextureResolution(1);
			QualitySettings.masterTextureLimit = 2;
		}
		if (textureResolution == 2)
		{
			UIManager.ChangeTextureResolution(2);
			QualitySettings.masterTextureLimit = 1;
		}
		if (textureResolution == 3)
		{
			UIManager.ChangeTextureResolution(3);
			QualitySettings.masterTextureLimit = 0;
		}
	}

	public void ProfilerOff()
	{
		profiler = 1;
		PlayerPrefs.SetInt("Profiler", profiler);
		fpsCounter.gameObject.SetActive(false);
	}

	public void ProfilerOn()
	{
		fpsCounter.gameObject.SetActive(true);
		profiler = 2;
		PlayerPrefs.SetInt("Profiler", profiler);
	}

	public void VsyncOff()
	{
		vSync = 1;
		PlayerPrefs.SetInt("Vsync", vSync);
		QualitySettings.vSyncCount = 0;
	}

	public void VsyncOn()
	{
		vSync = 2;
		PlayerPrefs.SetInt("Vsync", vSync);
		QualitySettings.vSyncCount = 1;
	}

	public void ChangeRenderQuality(int value)
	{
		renderQuality = value;
		PlayerPrefs.SetInt("Render Quality", renderQuality);
		UIManager.ChangeRenderQuality(value);
		if (value == 1)
		{
			decals.SetActive(false);
			QualitySettings.SetQualityLevel(0);
			if (vSync == 1)
			{
				QualitySettings.vSyncCount = 0;
			}
			else
			{
				QualitySettings.vSyncCount = 1;
			}
		}
		if (value == 2)
		{
			decals.SetActive(true);
			QualitySettings.SetQualityLevel(1);
			if (vSync == 1)
			{
				QualitySettings.vSyncCount = 0;
			}
			else
			{
				QualitySettings.vSyncCount = 1;
			}
		}
		if (value == 3)
		{
			QualitySettings.SetQualityLevel(2);
			if (vSync == 1)
			{
				QualitySettings.vSyncCount = 0;
			}
			else
			{
				QualitySettings.vSyncCount = 1;
			}
		}
		if (textureResolution == 2)
		{
			QualitySettings.masterTextureLimit = 2;
		}
		if (textureResolution == 1)
		{
			QualitySettings.masterTextureLimit = 1;
		}
		if (textureResolution == 0)
		{
			QualitySettings.masterTextureLimit = 0;
		}
	}

	public void ChangeBloom(int value)
	{
		bloom = value;
		PlayerPrefs.SetInt("Bloom", value);
		UIManager.ChangeBloomIndicator(value);
		if (value == 1)
		{
			targetBloom.intensity.value = 1f;
			targetBloom.threshold.value = 1f;
		}
		if (value == 2)
		{
			targetBloom.intensity.value = 1f;
			targetBloom.threshold.value = 0.8f;
		}
		if (value == 3)
		{
			targetBloom.intensity.value = 1f;
			targetBloom.threshold.value = 0.6f;
		}
	}

	public void ChangeVignette(int value)
	{
		vignette = value;
		PlayerPrefs.SetInt("Vignette", value);
		UIManager.ChangeVignetteIndicator(value);
		if (value == 1)
		{
			targetVignette.intensity.value = 0f;
		}
		if (value == 2)
		{
			targetVignette.intensity.value = 0.25f;
		}
		if (value == 3)
		{
			targetVignette.intensity.value = 0.35f;
		}
	}

	public void ChangeContrast(int value)
	{
		contrast = value;
		PlayerPrefs.SetInt("Contrast", value);
		UIManager.ChangeContrastIndicator(value);
		ColorAdjustments component;
		if (value == 1 && volume.profile.TryGet<ColorAdjustments>(out component))
		{
			component.contrast.value = 0f;
		}
		ColorAdjustments component2;
		if (value == 2 && volume.profile.TryGet<ColorAdjustments>(out component2))
		{
			component2.contrast.value = 10f;
		}
		ColorAdjustments component3;
		if (value == 3 && volume.profile.TryGet<ColorAdjustments>(out component3))
		{
			component3.contrast.value = 20f;
		}
	}

	public void ChangeSaturation(int value)
	{
		saturation = value;
		PlayerPrefs.SetInt("Saturation", saturation);
		UIManager.ChangeSaturationIndicator(value);
		ColorAdjustments component;
		if (value == 1 && volume.profile.TryGet<ColorAdjustments>(out component))
		{
			component.saturation.value = -20f;
		}
		ColorAdjustments component2;
		if (value == 2 && volume.profile.TryGet<ColorAdjustments>(out component2))
		{
			component2.saturation.value = 0f;
		}
		ColorAdjustments component3;
		if (value == 3 && volume.profile.TryGet<ColorAdjustments>(out component3))
		{
			component3.saturation.value = 20f;
		}
	}
}
