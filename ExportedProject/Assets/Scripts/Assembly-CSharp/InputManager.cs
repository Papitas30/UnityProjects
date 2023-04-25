using UnityEngine;

public class InputManager : MonoBehaviour
{
	[Header("Key Mapping")]
	public KeyCode interaction = KeyCode.F;

	public KeyCode UI = KeyCode.Tab;

	public KeyCode pauseMenu = KeyCode.Escape;

	public KeyCode crouch = KeyCode.C;

	public KeyCode prone = KeyCode.Z;

	public KeyCode run = KeyCode.LeftShift;

	public KeyCode sprint = KeyCode.LeftControl;

	public KeyCode jump = KeyCode.Space;

	public KeyCode aim = KeyCode.Mouse1;

	public KeyCode inspect = KeyCode.Mouse2;

	public KeyCode reload = KeyCode.R;

	public KeyCode fire = KeyCode.Mouse0;

	public KeyCode firemode = KeyCode.X;

	public KeyCode primary = KeyCode.Alpha1;

	public KeyCode secondary = KeyCode.Alpha2;

	public KeyCode useMode = KeyCode.Alpha1;

	public KeyCode flashlight = KeyCode.Mouse4;

	public KeyCode laser = KeyCode.Mouse3;

	public KeyCode equipMode = KeyCode.Alpha2;

	public KeyCode inspectMode = KeyCode.Alpha3;

	public KeyCode transferMode = KeyCode.Alpha4;

	public KeyCode dropMode = KeyCode.Alpha5;

	public KeyCode destroyMode = KeyCode.Alpha6;

	public KeyCode sortMode = KeyCode.LeftShift;

	public KeyCode weaponUI = KeyCode.F2;

	[Header("Mouse Sensitivity")]
	public float mouseSensitivity = 1f;

	[Header("Toggles")]
	public bool UIToggle;

	public bool pauseMenuToggle;

	public bool holdBreathToggle;

	public bool fireModeToggle;

	public bool laserToggle;

	public bool flashlightToggle;

	public bool sortSelection;

	public bool primaryEquipped;

	public bool jumpToggle;

	public bool fastSecondarySwap;

	public bool fastPrimarySwap;

	public bool weaponUIToggle;

	[Header("Player Data")]
	public PlayerData playerData;

	[Header("Core References")]
	public EventManager eventManager;

	public AudioManager audioManager;

	public UIManager UIManager;

	public Interactor interactor;

	public Equipment equipment;

	public Container container;

	[Header("Debug")]
	public bool lockAim;

	private void Start()
	{
		weaponUIToggle = true;
		fireModeToggle = true;
		laserToggle = true;
		flashlightToggle = true;
		audioManager = GameObject.FindWithTag("Audio Manager").GetComponent<AudioManager>();
		playerData.movementInput = Vector2.zero;
	}

	private void Update()
	{
		if (playerData.isDead)
		{
			return;
		}
		if (Input.GetKeyDown(interaction))
		{
			if (!UIToggle)
			{
				interactor.Interact();
			}
			else
			{
				UIToggle = false;
				eventManager.closeUI.Raise();
			}
		}
		playerData.movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		playerData.movementInput.Normalize();
		if (Input.GetKeyDown(crouch) && playerData.canStand)
		{
			playerData.isCrouching = !playerData.isCrouching;
		}
		if (Input.GetKeyDown(jump) && !playerData.isCrouching)
		{
			jumpToggle = true;
		}
		else
		{
			jumpToggle = false;
		}
		playerData.mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
		if (playerData.primaryEquipped && !playerData.secondaryActive && Input.GetKeyDown(primary) && !playerData.isReloading && !UIToggle && !playerData.isHolstering)
		{
			playerData.primaryActive = !playerData.primaryActive;
			if (playerData.primaryActive)
			{
				equipment.ActivatePrimary();
				audioManager.PlayRifleEquip();
			}
			else
			{
				equipment.DeactivatePrimary();
				audioManager.PlayRifleUnequip();
			}
		}
		if (playerData.primaryEquipped && playerData.secondaryActive && Input.GetKeyDown(primary) && !playerData.isReloading && !UIToggle && !playerData.isHolstering)
		{
			playerData.primaryActive = !playerData.primaryActive;
			if (playerData.primaryActive)
			{
				fastPrimarySwap = true;
				equipment.ActivatePrimary();
				equipment.DeactivateSecondary();
				audioManager.PlayRifleEquip();
			}
		}
		if (playerData.secondaryEquipped && !playerData.primaryActive && Input.GetKeyDown(secondary) && !playerData.isReloading && !UIToggle && !playerData.isHolstering)
		{
			playerData.secondaryActive = !playerData.secondaryActive;
			if (playerData.secondaryActive)
			{
				equipment.ActivateSecondary();
				audioManager.PlayRifleEquip();
			}
			else
			{
				equipment.DeactivateSecondary();
				audioManager.PlayRifleUnequip();
			}
		}
		if (playerData.secondaryEquipped && playerData.primaryActive && Input.GetKeyDown(secondary) && !playerData.isReloading && !UIToggle && !playerData.isHolstering)
		{
			playerData.secondaryActive = !playerData.secondaryActive;
			if (playerData.secondaryActive)
			{
				fastSecondarySwap = true;
				equipment.ActivateSecondary();
				equipment.DeactivatePrimary();
				audioManager.PlayRifleEquip();
			}
		}
		if (Input.GetKey(aim) && !playerData.isRunning && !playerData.isSprinting && !playerData.isPlacing && !playerData.isInspecting && !UIToggle && !pauseMenuToggle && (playerData.primaryActive || playerData.secondaryActive))
		{
			playerData.isAiming = true;
		}
		else if (!lockAim)
		{
			playerData.isAiming = false;
		}
		if ((!UIToggle && !playerData.isReloading && !playerData.isInspecting && !playerData.isFiring && !playerData.isPlacing && playerData.primaryActive) || playerData.secondaryActive)
		{
			float axis = Input.GetAxis("Mouse ScrollWheel");
			if (axis > 0f)
			{
				if (axis < 1f && playerData.weaponPosition < 2)
				{
					audioManager.PlayHandling();
					playerData.weaponPosition++;
				}
			}
			else if (axis < 0f && playerData.weaponPosition > 0 && playerData.weaponPosition > 1)
			{
				audioManager.PlayHandling();
				playerData.weaponPosition--;
			}
		}
		if (Input.GetKeyDown(inspect) && !playerData.isReloading && (playerData.primaryActive || playerData.secondaryActive))
		{
			playerData.isInspecting = !playerData.isInspecting;
			if (playerData.isInspecting)
			{
				UIManager.weaponUI.enabled = true;
				UIManager.UpdateWeaponUI();
				UIManager.mainUI.enabled = false;
			}
			else
			{
				UIManager.weaponUI.enabled = false;
				eventManager.closeUI.Raise();
			}
		}
		if (Input.GetKeyDown(firemode))
		{
			if (playerData.primaryActive && equipment.equipments[0].fireType != ItemData.FireType.Semi && !UIManager.weapon.pumpAction && !UIManager.weapon.boltAction)
			{
				fireModeToggle = !fireModeToggle;
				if (fireModeToggle)
				{
					playerData.fireMode = PlayerData.FireMode.Single;
					UIManager.UpdateWeaponUI();
					audioManager.PlaySwitchFiremode();
				}
				else
				{
					playerData.fireMode = PlayerData.FireMode.Auto;
					UIManager.UpdateWeaponUI();
					audioManager.PlaySwitchFiremode();
				}
			}
			if (playerData.secondaryActive && equipment.equipments[1].fireType != ItemData.FireType.Semi)
			{
				fireModeToggle = !fireModeToggle;
				if (fireModeToggle)
				{
					playerData.fireMode = PlayerData.FireMode.Single;
					UIManager.UpdateWeaponUI();
					audioManager.PlaySwitchFiremode();
				}
				else
				{
					playerData.fireMode = PlayerData.FireMode.Auto;
					UIManager.UpdateWeaponUI();
					audioManager.PlaySwitchFiremode();
				}
			}
		}
		if (!playerData.magazineEmpty && !playerData.isInspecting && !playerData.isReloading && !UIToggle && !pauseMenuToggle && (playerData.weaponPosition == 2 || playerData.isAiming) && (playerData.primaryActive || playerData.secondaryActive) && !playerData.isColliding)
		{
			switch (playerData.fireMode)
			{
			case PlayerData.FireMode.Single:
				playerData.isFiring = Input.GetKeyDown(fire);
				break;
			case PlayerData.FireMode.Auto:
				playerData.isFiring = Input.GetKey(fire);
				break;
			}
		}
		else
		{
			playerData.isFiring = false;
		}
		if (Input.GetKeyDown(reload) && !playerData.isReloading && !playerData.magazineFull && !playerData.isSprinting && !playerData.isInspecting && !playerData.isHolstering)
		{
			playerData.isReloading = true;
		}
		if (Input.GetKeyDown(laser) || Input.GetKeyDown(KeyCode.B))
		{
			laserToggle = !laserToggle;
			audioManager.PlayLaserToggle();
			if (laserToggle)
			{
				playerData.laserActive = false;
			}
			else
			{
				playerData.laserActive = true;
			}
		}
		if (Input.GetKeyDown(flashlight) || Input.GetKeyDown(KeyCode.V))
		{
			flashlightToggle = !flashlightToggle;
			audioManager.PlayFlashlightToggle();
			if (flashlightToggle)
			{
				playerData.flashlightActive = false;
			}
			else
			{
				playerData.flashlightActive = true;
			}
		}
		if (Input.GetKeyDown(pauseMenu) || Input.GetKeyDown(KeyCode.Escape))
		{
			pauseMenuToggle = !pauseMenuToggle;
			if (pauseMenuToggle)
			{
				eventManager.openPauseMenu.Raise();
				audioManager.PlayUIToggle();
				UIToggle = false;
			}
			else
			{
				eventManager.closeUI.Raise();
				audioManager.PlayUIToggle();
			}
		}
		if (Input.GetKeyDown(weaponUI) && playerData.isInspecting)
		{
			weaponUIToggle = !weaponUIToggle;
			if (weaponUIToggle)
			{
				UIManager.weaponUIPositions.SetActive(true);
				audioManager.PlayUIToggle();
			}
			else
			{
				UIManager.weaponUIPositions.SetActive(false);
				audioManager.PlayUIToggle();
			}
		}
		if (!playerData.isMoving && !playerData.isAiming)
		{
			playerData.isIdle = true;
		}
		else
		{
			playerData.isIdle = false;
		}
		if (playerData.movementInput.x != 0f || playerData.movementInput.y != 0f)
		{
			playerData.isMoving = true;
			if (playerData.isProning || playerData.isCrouching)
			{
				playerData.isWalking = false;
			}
			else
			{
				playerData.isWalking = true;
			}
			if (Input.GetKey(run) && !playerData.isCrouching && !playerData.isProning)
			{
				playerData.isRunning = true;
				playerData.isWalking = false;
			}
			else
			{
				playerData.isRunning = false;
			}
			if (Input.GetKey(sprint) && !playerData.isAiming && !playerData.isCrouching && !playerData.isProning && !playerData.isPlacing)
			{
				playerData.isSprinting = true;
				playerData.isWalking = false;
			}
			else
			{
				playerData.isHoldingBreath = false;
				playerData.isSprinting = false;
			}
		}
		else
		{
			playerData.isMoving = false;
			playerData.isWalking = false;
			playerData.isRunning = false;
			playerData.isSprinting = false;
		}
		if (Input.GetKeyDown(UI) && !pauseMenuToggle)
		{
			UIToggle = !UIToggle;
			if (UIToggle)
			{
				eventManager.openUI.Raise();
				audioManager.PlayUIToggle();
			}
			else
			{
				eventManager.closeUI.Raise();
				audioManager.PlayUIToggle();
			}
		}
		if (UIToggle)
		{
			if (Input.GetKey(sortMode))
			{
				sortSelection = true;
			}
			else
			{
				sortSelection = false;
			}
		}
		bool uIToggle = UIToggle;
	}

	public void ToggleUIKey()
	{
		UIToggle = true;
	}
}
