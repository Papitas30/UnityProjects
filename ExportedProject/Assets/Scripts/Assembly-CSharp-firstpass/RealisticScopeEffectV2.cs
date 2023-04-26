using UnityEngine;

public class RealisticScopeEffectV2 : MonoBehaviour
{
	public Camera ScopeCamera;

	private Material scopeMaterial;

	public ReticleBrightness reticleBrightness;

	public Zooming zooming;

	public CameraFPS cameraFPS;

	public CameraDisable cameraDisable;

	private float targetZoom = 1f;

	private float targetReticleSize = 1f;

	private float fpsTimer;

	private float fpsTime;

	private void Start()
	{
		scopeMaterial = GetComponent<MeshRenderer>().material;
		if (reticleBrightness.can_Illuminate)
		{
			scopeMaterial.SetFloat("Reticle_Brightness", 1f);
		}
		switch (zooming.zoomType)
		{
		case Zooming.ZoomType.Variable:
			ScopeCamera.fieldOfView = zooming.startFOV;
			targetReticleSize = zooming.minSize;
			zooming.currentSize = targetReticleSize;
			if (zooming.zoomReticle)
			{
				scopeMaterial.SetFloat("Reticle_Size", zooming.minSize);
			}
			break;
		case Zooming.ZoomType.None:
		case Zooming.ZoomType.Toggle:
			break;
		}
	}

	private void Update()
	{
		if (reticleBrightness.can_Illuminate)
		{
			changeReticleIllumnation();
		}
		switch (zooming.zoomType)
		{
		case Zooming.ZoomType.Variable:
			if (zooming.zoomReticle)
			{
				zooming.sizeIncrements = (zooming.maxSize - zooming.minSize) / (float)zooming.maxZoom * zooming.zoomIncrements;
			}
			if (Input.GetKey(zooming.ActivatorKey))
			{
				if (Input.GetAxis("Mouse ScrollWheel") > 0f && targetZoom < (float)zooming.maxZoom)
				{
					increaseZoom();
				}
				if (Input.GetAxis("Mouse ScrollWheel") < 0f && targetZoom > (float)zooming.minZoom)
				{
					decreaseZoom();
				}
			}
			if (zooming.zoomReticle)
			{
				targetReticleSize = zooming.minSize + zooming.sizeIncrements * targetZoom;
			}
			zooming.currentSize = Mathf.Lerp(zooming.currentSize, targetReticleSize, zooming.zoomSpeed * Time.deltaTime);
			zooming.currentZoom = Mathf.Lerp(zooming.currentZoom, targetZoom, zooming.zoomSpeed * Time.deltaTime);
			if (ScopeCamera != null)
			{
				ScopeCamera.fieldOfView = zooming.startFOV / zooming.currentZoom;
			}
			if (zooming.zoomReticle)
			{
				scopeMaterial.SetFloat("Reticle_Size", zooming.currentSize);
			}
			break;
		case Zooming.ZoomType.Toggle:
			if (Input.GetKey(zooming.ActivatorKey) && Input.GetKeyDown(zooming.ToggleKey))
			{
				zooming.isToggled = !zooming.isToggled;
			}
			if (zooming.isToggled)
			{
				if (ScopeCamera != null)
				{
					ScopeCamera.fieldOfView = zooming.ToggledZoomFOV;
				}
				scopeMaterial.SetFloat("Reticle_Size", zooming.ToggledReticleSize);
			}
			if (!zooming.isToggled)
			{
				if (ScopeCamera != null)
				{
					ScopeCamera.fieldOfView = zooming.UntoggledZoomFOV;
				}
				scopeMaterial.SetFloat("Reticle_Size", zooming.unToggledReticleSize);
			}
			break;
		}
		float num = 1f / Time.deltaTime;
		float num2 = 0f;
		switch (cameraFPS.fPSLimitType)
		{
		case CameraFPS.FPSLimitType.None:
			if (!ScopeCamera.enabled && ScopeCamera != null)
			{
				ScopeCamera.enabled = true;
			}
			break;
		case CameraFPS.FPSLimitType.Half:
			if (ScopeCamera.enabled && ScopeCamera != null)
			{
				ScopeCamera.enabled = false;
			}
			if (cameraDisable.canZoomCamera)
			{
				num2 = num / 2f;
				fpsTime = 1f / num2;
				if (fpsTimer < fpsTime)
				{
					fpsTimer += Time.deltaTime;
				}
				if (fpsTimer > fpsTime)
				{
					fpsTimer = 0f;
					ScopeCamera.Render();
				}
			}
			break;
		case CameraFPS.FPSLimitType.Quarter:
			if (ScopeCamera.enabled && ScopeCamera != null)
			{
				ScopeCamera.enabled = false;
			}
			if (cameraDisable.canZoomCamera)
			{
				num2 = num / 4f;
				fpsTime = 1f / num2;
				if (fpsTimer < fpsTime)
				{
					fpsTimer += Time.deltaTime;
				}
				if (fpsTimer > fpsTime)
				{
					ScopeCamera.Render();
					fpsTimer = 0f;
				}
			}
			break;
		case CameraFPS.FPSLimitType.Fixed:
			if (ScopeCamera.enabled)
			{
				ScopeCamera.enabled = false;
			}
			if (cameraDisable.canZoomCamera)
			{
				fpsTime = 1f / cameraFPS.fixedFps;
				if (fpsTimer < fpsTime)
				{
					fpsTimer += Time.deltaTime;
				}
				if (fpsTimer > fpsTime)
				{
					ScopeCamera.Render();
					fpsTimer = 0f;
				}
			}
			break;
		}
		switch (cameraDisable.o_cameraDisable)
		{
		case CameraDisable.cameraDisable.None:
			cameraDisable.canZoomCamera = true;
			break;
		case CameraDisable.cameraDisable.Distance:
			Debug.Log(Vector3.Distance(base.transform.position, Camera.main.transform.position));
			if (Vector3.Distance(base.transform.position, Camera.main.transform.position) < cameraDisable.distance)
			{
				cameraDisable.canZoomCamera = true;
			}
			else
			{
				cameraDisable.canZoomCamera = false;
			}
			break;
		}
	}

	public void increaseZoom()
	{
		targetZoom += zooming.zoomIncrements;
		if (targetZoom > (float)zooming.maxZoom)
		{
			targetZoom = zooming.maxZoom;
		}
	}

	public void decreaseZoom()
	{
		targetZoom -= zooming.zoomIncrements;
		if (targetZoom < (float)zooming.minZoom)
		{
			targetZoom = zooming.minZoom;
		}
	}

	public void changeReticleIllumnation()
	{
		reticleBrightness.v_brightness = scopeMaterial.GetFloat("Reticle_Brightness");
		if (Input.GetKey(reticleBrightness.brightness_increase) && reticleBrightness.v_brightness < reticleBrightness.v_brightness_max)
		{
			reticleBrightness.v_brightness += reticleBrightness.v_brightness_increase;
		}
		if (Input.GetKey(reticleBrightness.brightness_decrease) && reticleBrightness.v_brightness > reticleBrightness.v_brightness_min)
		{
			reticleBrightness.v_brightness -= reticleBrightness.v_brightness_increase;
		}
		scopeMaterial.SetFloat("Reticle_Brightness", reticleBrightness.v_brightness);
	}
}
