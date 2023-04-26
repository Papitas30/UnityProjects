using UnityEngine;

public class DualRender : MonoBehaviour
{
	public PlayerData playerData;

	public GameObject scopeRegural;

	public GameObject scopeDualRender;

	public ItemData weaponData;

	private Camera dualRenderCamera;

	private void Start()
	{
		dualRenderCamera = GetComponentInChildren<Camera>();
		if (playerData.dualRender)
		{
			dualRenderCamera.fieldOfView = weaponData.aimFOV;
			dualRenderCamera.enabled = true;
			scopeRegural.SetActive(false);
			scopeDualRender.SetActive(true);
		}
		else
		{
			dualRenderCamera.enabled = false;
			scopeRegural.SetActive(true);
			scopeDualRender.SetActive(false);
		}
	}

	private void Update()
	{
		if (playerData.dualRender)
		{
			dualRenderCamera.fieldOfView = weaponData.aimFOV - 10f;
			dualRenderCamera.enabled = true;
			scopeRegural.SetActive(false);
			scopeDualRender.SetActive(true);
		}
		else
		{
			dualRenderCamera.enabled = false;
			scopeRegural.SetActive(true);
			scopeDualRender.SetActive(false);
		}
	}
}
