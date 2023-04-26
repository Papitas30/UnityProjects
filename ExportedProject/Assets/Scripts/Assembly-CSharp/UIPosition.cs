using UnityEngine;
using UnityEngine.UI;

public class UIPosition : MonoBehaviour
{
	public enum UIType
	{
		Magazine = 0,
		Condition = 1,
		Firemode = 2,
		Sight = 3,
		Muzzle = 4,
		Caliber = 5
	}

	public PlayerData playerData;

	public ItemData weaponData;

	private UIManager UIManager;

	private Vector2 targetPosition;

	private Camera mainCamera;

	[Header("Weapon UI Type")]
	public UIType weaponUIType;

	private Image image;

	private void Start()
	{
		mainCamera = Camera.main;
		UIManager = GameObject.FindWithTag("UI Manager").GetComponent<UIManager>();
		if (weaponUIType == UIType.Magazine)
		{
			image = UIManager.magazineUI;
			targetPosition = weaponData.magazinePosition;
		}
		if (weaponUIType == UIType.Condition)
		{
			image = UIManager.conditionUI;
			targetPosition = weaponData.conditionPosition;
		}
		if (weaponUIType == UIType.Firemode)
		{
			image = UIManager.firemodeUI;
			targetPosition = weaponData.firemodePosition;
		}
		if (weaponUIType == UIType.Sight)
		{
			image = UIManager.sightUI;
			targetPosition = weaponData.sightPosition;
		}
		if (weaponUIType == UIType.Muzzle)
		{
			image = UIManager.muzzleUI;
			targetPosition = weaponData.muzzlePosition;
		}
		if (weaponUIType == UIType.Caliber)
		{
			image = UIManager.ammoTypeUI;
			targetPosition = weaponData.caliberPosition;
		}
	}

	private void Update()
	{
		if (playerData.isInspecting)
		{
			if (weaponUIType == UIType.Magazine)
			{
				targetPosition = weaponData.magazinePosition;
			}
			if (weaponUIType == UIType.Condition)
			{
				targetPosition = weaponData.conditionPosition;
			}
			if (weaponUIType == UIType.Firemode)
			{
				targetPosition = weaponData.firemodePosition;
			}
			if (weaponUIType == UIType.Sight)
			{
				targetPosition = weaponData.sightPosition;
			}
			if (weaponUIType == UIType.Muzzle)
			{
				targetPosition = weaponData.muzzlePosition;
			}
			if (weaponUIType == UIType.Caliber)
			{
				targetPosition = weaponData.caliberPosition;
			}
			Vector3 position = mainCamera.WorldToScreenPoint(base.transform.position);
			position.z = 0f;
			image.rectTransform.position = position;
			image.rectTransform.pivot = Vector2.Lerp(image.rectTransform.pivot, targetPosition, 5f * Time.deltaTime);
		}
		else
		{
			image.rectTransform.pivot = Vector2.Lerp(image.rectTransform.pivot, new Vector2(0.5f, 0.5f), 5f * Time.deltaTime);
		}
	}
}
