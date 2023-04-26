using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public enum InventoryCursorMode
	{
		Use = 0,
		Inspect = 1,
		Place = 2,
		Transfer = 3,
		Drop = 4,
		Destroy = 5
	}

	public enum InventorySortMode
	{
		Name = 0,
		Type = 1,
		Volume = 2,
		Weight = 3,
		Value = 4,
		Rarity = 5
	}

	public enum ContainerCursorMode
	{
		Use = 0,
		Inspect = 1,
		Place = 2,
		Transfer = 3,
		Drop = 4,
		Destroy = 5
	}

	public enum ContainerSortMode
	{
		Name = 0,
		Type = 1,
		Volume = 2,
		Weight = 3,
		Value = 4,
		Rarity = 5
	}

	public PlayerData playerData;

	public Canvas mainUI;

	public Canvas weaponUI;

	[Header("Container Modes")]
	public InventoryCursorMode inventoryCursorMode;

	public InventorySortMode inventorySortMode;

	[Header("Container Modes")]
	public ContainerCursorMode containerCursorMode;

	public ContainerSortMode containerSortMode;

	[Header("Inventory")]
	public Inventory inventory;

	public FloatData inventoryVolumeData;

	public FloatData inventoryWeightData;

	public Text inventoryVolume;

	public Text inventoryWeight;

	[Header("Container")]
	public Container container;

	public FloatData containerVolumeData;

	public FloatData containerWeightData;

	public Text containerName;

	public Text containerVolume;

	public Text containerWeight;

	public bool containerIsOpen;

	[Header("Loadout Slots")]
	public Transform primarySlot;

	public Transform secondarySlot;

	public Transform knifeSlot;

	public Transform flashlightSlot;

	public Transform binocularSlot;

	public Transform mapSlot;

	public Transform compassSlot;

	public Transform toolSlot;

	[Header("Equipment Slots")]
	public Transform headSlot;

	public Transform faceSlot;

	public Transform torsoSlot;

	public Transform rigSlot;

	public Transform backpackSlot;

	public Transform legSlot;

	public Transform handSlot;

	public Transform feetSlot;

	[Header("Weapon UI")]
	public ItemData weapon;

	public Image magazineUI;

	public Image conditionUI;

	public Image firemodeUI;

	public Image sightUI;

	public Image muzzleUI;

	public Image ammoTypeUI;

	public Text firemode;

	public Text ammoType;

	public Text magazine;

	public Text condition;

	public Text sight;

	public Text muzzle;

	[Header("Inventory Indicators")]
	public Image inventoryUseIndicator;

	public Image inventoryEquipIndicator;

	public Image inventoryPlaceIndicator;

	public Image inventoryTransferIndicator;

	public Image inventoryDropIndicator;

	public Image inventoryDestroyIndicator;

	public Image inventoryNameIndicator;

	public Image inventoryTypeIndicator;

	public Image inventoryVolumeIndicator;

	public Image inventoryWeightIndicator;

	public Image inventoryValueIndicator;

	public Image inventoryRarityIndicator;

	[Header("Container Indicators")]
	public Image containerUseIndicator;

	public Image containerEquipIndicator;

	public Image containerPlaceIndicator;

	public Image containerTransferIndicator;

	public Image containerDropIndicator;

	public Image containerDestroyIndicator;

	public Image containerNameIndicator;

	public Image containerTypeIndicator;

	public Image containerVolumeIndicator;

	public Image containerWeightIndicator;

	public Image containerValueIndicator;

	public Image containerRarityIndicator;

	[Header("Quality")]
	public Image performance;

	public Image standard;

	public Image extreme;

	[Header("Color Adjustments")]
	public Image lowContrast;

	public Image midContrast;

	public Image highContrast;

	public Image lowSaturation;

	public Image midSaturation;

	public Image highSaturation;

	[Header("Vignette")]
	public Image lowVignette;

	public Image midVignette;

	public Image highVignette;

	[Header("Bloom")]
	public Image lowBloom;

	public Image midBloom;

	public Image highBloom;

	[Header("Vsync")]
	public Image vsyncOn;

	public Image vsyncOff;

	[Header("Profiler")]
	public Image profilerOn;

	public Image profilerOff;

	[Header("Time of Day")]
	public Image dawn;

	public Image day;

	public Image dusk;

	public Image night;

	[Header("Textures")]
	public Image quarter;

	public Image half;

	public Image full;

	[Header("Ambient Occlusion")]
	public Image AOOff;

	public Image AOOn;

	[Header("Field of View")]
	public Image fovNarrow;

	public Image fovStandard;

	public Image fovWide;

	[Header("Camera Smoothing")]
	public Image smoothingOff;

	public Image smoothingLow;

	public Image smoothingHigh;

	[Header("Weather")]
	public Image neutral;

	public Image rain;

	public Image storm;

	[Header("Volumetrics")]
	public Image volumetricsOff;

	public Image volumetricsOn;

	[Header("Sharpen Filter")]
	public Image sharpenOff;

	public Image sharpenOn;

	[Header("Camera Noise")]
	public Image cameraNoiseLow;

	public Image cameraNoiseMid;

	public Image cameraNoiseHigh;

	[Header("Dual Render")]
	public Image dualRenderOff;

	public Image dualRenderOn;

	[Header("Slider Values")]
	public Text mouseSensitivity;

	public Text scopeSensitivity;

	public Text horizontalPosition;

	public Text verticalPosition;

	public Text ambientVolume;

	public Text SFXVolume;

	public Text worldFOV;

	public Text weaponFOV;

	[Header("AI Spawner")]
	public Image spawnerOff;

	public Image spawnerOn;

	public GameObject weaponUIPositions;

	public Transform containerSlots;

	public void ChangePerformanceIndicator()
	{
		performance.enabled = true;
		standard.enabled = false;
		extreme.enabled = false;
	}

	public void ChangeStandardIndicator()
	{
		performance.enabled = false;
		standard.enabled = true;
		extreme.enabled = false;
	}

	public void ChangeExtremeIndicator()
	{
		performance.enabled = false;
		standard.enabled = false;
		extreme.enabled = true;
	}

	public void ChangeSaturationIndicator(int value)
	{
		if (value == 1)
		{
			lowSaturation.enabled = true;
			midSaturation.enabled = false;
			highSaturation.enabled = false;
		}
		if (value == 2)
		{
			lowSaturation.enabled = false;
			midSaturation.enabled = true;
			highSaturation.enabled = false;
		}
		if (value == 3)
		{
			lowSaturation.enabled = false;
			midSaturation.enabled = false;
			highSaturation.enabled = true;
		}
	}

	public void ChangeContrastIndicator(int value)
	{
		if (value == 1)
		{
			lowContrast.enabled = true;
			midContrast.enabled = false;
			highContrast.enabled = false;
		}
		if (value == 2)
		{
			lowContrast.enabled = false;
			midContrast.enabled = true;
			highContrast.enabled = false;
		}
		if (value == 3)
		{
			lowContrast.enabled = false;
			midContrast.enabled = false;
			highContrast.enabled = true;
		}
	}

	public void ChangeVignetteIndicator(int value)
	{
		if (value == 1)
		{
			lowVignette.enabled = true;
			midVignette.enabled = false;
			highVignette.enabled = false;
		}
		if (value == 2)
		{
			lowVignette.enabled = false;
			midVignette.enabled = true;
			highVignette.enabled = false;
		}
		if (value == 3)
		{
			lowVignette.enabled = false;
			midVignette.enabled = false;
			highVignette.enabled = true;
		}
	}

	public void ChangeBloomIndicator(int value)
	{
		if (value == 1)
		{
			lowBloom.enabled = true;
			midBloom.enabled = false;
			highBloom.enabled = false;
		}
		if (value == 2)
		{
			lowBloom.enabled = false;
			midBloom.enabled = true;
			highBloom.enabled = false;
		}
		if (value == 3)
		{
			lowBloom.enabled = false;
			midBloom.enabled = false;
			highBloom.enabled = true;
		}
	}

	public void VsyncOn()
	{
		vsyncOn.enabled = true;
		vsyncOff.enabled = false;
	}

	public void VsyncOff()
	{
		vsyncOn.enabled = false;
		vsyncOff.enabled = true;
	}

	public void ProfilerOff()
	{
		profilerOff.enabled = true;
		profilerOn.enabled = false;
	}

	public void ProfilerOn()
	{
		profilerOff.enabled = false;
		profilerOn.enabled = true;
	}

	public void Dawn()
	{
		dawn.enabled = true;
		day.enabled = false;
		dusk.enabled = false;
		night.enabled = false;
	}

	public void Day()
	{
		dawn.enabled = false;
		day.enabled = true;
		dusk.enabled = false;
		night.enabled = false;
	}

	public void Dusk()
	{
		dawn.enabled = false;
		day.enabled = false;
		dusk.enabled = true;
		night.enabled = false;
	}

	public void Night()
	{
		dawn.enabled = false;
		day.enabled = false;
		dusk.enabled = false;
		night.enabled = true;
	}

	public void ChangeAISpawnerIndicator(int value)
	{
		if (value == 1)
		{
			spawnerOff.enabled = true;
			spawnerOn.enabled = false;
		}
		if (value == 2)
		{
			spawnerOff.enabled = false;
			spawnerOn.enabled = true;
		}
	}

	public void ChangeDualRenderIndicator(int value)
	{
		if (value == 1)
		{
			dualRenderOff.enabled = true;
			dualRenderOn.enabled = false;
		}
		if (value == 2)
		{
			dualRenderOff.enabled = false;
			dualRenderOn.enabled = true;
		}
	}

	public void ChangeCameraNoiseIndicator(int value)
	{
		if (value == 1)
		{
			cameraNoiseLow.enabled = true;
			cameraNoiseMid.enabled = false;
			cameraNoiseHigh.enabled = false;
		}
		if (value == 2)
		{
			cameraNoiseLow.enabled = false;
			cameraNoiseMid.enabled = true;
			cameraNoiseHigh.enabled = false;
		}
		if (value == 3)
		{
			cameraNoiseLow.enabled = false;
			cameraNoiseMid.enabled = false;
			cameraNoiseHigh.enabled = true;
		}
	}

	public void ChangeWorldFOV(int value)
	{
		worldFOV.text = value.ToString();
	}

	public void ChangeWeaponFOV(int value)
	{
		weaponFOV.text = value.ToString();
	}

	public void ChangeAmbientVolume(int value)
	{
		ambientVolume.text = value.ToString();
	}

	public void ChangeSFXVolume(int value)
	{
		SFXVolume.text = value.ToString();
	}

	public void ChangeWeaponHorizontalPosition(int value)
	{
		horizontalPosition.text = value.ToString();
	}

	public void ChangeWeaponVerticalPosition(int value)
	{
		verticalPosition.text = value.ToString();
	}

	public void ChangeMouseSensitivity(int value)
	{
		mouseSensitivity.text = value.ToString();
	}

	public void ChangeScopeSensitivity(int value)
	{
		scopeSensitivity.text = value.ToString();
	}

	public void ChangeRenderQuality(int value)
	{
		if (value == 1)
		{
			performance.enabled = true;
			standard.enabled = false;
			extreme.enabled = false;
		}
		if (value == 2)
		{
			performance.enabled = false;
			standard.enabled = true;
			extreme.enabled = false;
		}
		if (value == 3)
		{
			performance.enabled = false;
			standard.enabled = false;
			extreme.enabled = true;
		}
	}

	public void ChangeSharpenFilterIndicator(int value)
	{
		if (value == 1)
		{
			sharpenOff.enabled = true;
			sharpenOn.enabled = false;
		}
		if (value == 2)
		{
			sharpenOff.enabled = false;
			sharpenOn.enabled = true;
		}
	}

	public void ChangeSmoothingIndicator(int value)
	{
		if (value == 1)
		{
			smoothingOff.enabled = true;
			smoothingLow.enabled = false;
			smoothingHigh.enabled = false;
		}
		if (value == 2)
		{
			smoothingOff.enabled = false;
			smoothingLow.enabled = true;
			smoothingHigh.enabled = false;
		}
		if (value == 3)
		{
			smoothingOff.enabled = false;
			smoothingLow.enabled = false;
			smoothingHigh.enabled = true;
		}
	}

	public void ChangeVolumetricsIndicator(int value)
	{
		if (value == 1)
		{
			volumetricsOff.enabled = true;
			volumetricsOn.enabled = false;
		}
		else
		{
			volumetricsOff.enabled = false;
			volumetricsOn.enabled = true;
		}
	}

	public void ChangeWeatherIndicator(int value)
	{
		if (value == 1)
		{
			neutral.enabled = true;
			rain.enabled = false;
			storm.enabled = false;
		}
		if (value == 2)
		{
			neutral.enabled = false;
			rain.enabled = true;
			storm.enabled = false;
		}
		if (value == 3)
		{
			neutral.enabled = false;
			rain.enabled = false;
			storm.enabled = true;
		}
	}

	public void ChangeAOIndicator(int value)
	{
		if (value == 1)
		{
			AOOff.enabled = true;
			AOOn.enabled = false;
		}
		if (value == 2)
		{
			AOOff.enabled = false;
			AOOn.enabled = true;
		}
	}

	public void ChangeTextureResolution(int value)
	{
		if (value == 1)
		{
			quarter.enabled = true;
			half.enabled = false;
			full.enabled = false;
		}
		if (value == 2)
		{
			quarter.enabled = false;
			half.enabled = true;
			full.enabled = false;
		}
		if (value == 3)
		{
			quarter.enabled = false;
			half.enabled = false;
			full.enabled = true;
		}
	}

	public void ContainerIsOpen()
	{
		containerIsOpen = true;
	}

	public void ContainerIsClosed()
	{
		containerIsOpen = false;
	}

	public void UpdateInventoryStats()
	{
		inventoryVolumeData.SetValue(inventory.volume);
		inventoryWeightData.SetValue(inventory.weight);
		inventoryVolume.text = inventory.volume.ToString("F2").Replace(",", ".") + " / " + inventory.maxVolume.ToString("F0");
		inventoryWeight.text = inventory.weight.ToString("F2").Replace(",", ".") + " / " + inventory.maxWeight.ToString("F0");
	}

	public void UpdateContainerStats()
	{
		containerName.text = container.containerName.ToUpper();
		containerVolumeData.SetValue(container.volume);
		containerWeightData.SetValue(container.weight);
		containerVolume.text = container.volume.ToString("F2").Replace(",", ".") + " / " + container.maxVolume.ToString("F0");
		containerWeight.text = container.weight.ToString("F2").Replace(",", ".") + " / " + container.maxWeight.ToString("F0");
	}

	public void AssignContainerUI()
	{
		container = GameObject.FindWithTag("Container").GetComponent<Container>();
		containerName.text = container.containerName.ToUpper();
		containerVolumeData.SetValue(container.volume);
		containerWeightData.SetValue(container.weight);
		containerVolume.text = container.volume.ToString("F2").Replace(",", ".") + " / " + container.maxVolume.ToString("F0");
		containerWeight.text = container.weight.ToString("F2").Replace(",", ".") + " / " + container.maxWeight.ToString("F0");
	}

	public void ShowCursor()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	public void HideCursor()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	public void UpdateWeaponUI()
	{
		if (playerData.fireMode == PlayerData.FireMode.Single)
		{
			if (weapon.boltAction || weapon.pumpAction)
			{
				firemode.text = "SINGLE";
			}
			else
			{
				firemode.text = "SEMI";
			}
		}
		if (playerData.fireMode == PlayerData.FireMode.Auto)
		{
			firemode.text = "AUTO";
		}
		sight.text = weapon.sight;
		muzzle.text = weapon.muzzle;
		ammoType.text = weapon.ammoType;
	}

	public void InventoryUse()
	{
		inventoryCursorMode = InventoryCursorMode.Use;
		inventoryUseIndicator.enabled = true;
		inventoryEquipIndicator.enabled = false;
		inventoryPlaceIndicator.enabled = false;
		inventoryTransferIndicator.enabled = false;
		inventoryDropIndicator.enabled = false;
		inventoryDestroyIndicator.enabled = false;
	}

	public void InventoryEquip()
	{
		inventoryCursorMode = InventoryCursorMode.Inspect;
		inventoryUseIndicator.enabled = false;
		inventoryEquipIndicator.enabled = true;
		inventoryPlaceIndicator.enabled = false;
		inventoryTransferIndicator.enabled = false;
		inventoryDropIndicator.enabled = false;
		inventoryDestroyIndicator.enabled = false;
	}

	public void InventoryInspect()
	{
		inventoryCursorMode = InventoryCursorMode.Place;
		inventoryUseIndicator.enabled = false;
		inventoryEquipIndicator.enabled = false;
		inventoryPlaceIndicator.enabled = true;
		inventoryTransferIndicator.enabled = false;
		inventoryDropIndicator.enabled = false;
		inventoryDestroyIndicator.enabled = false;
	}

	public void InventoryTransfer()
	{
		inventoryCursorMode = InventoryCursorMode.Transfer;
		inventoryUseIndicator.enabled = false;
		inventoryEquipIndicator.enabled = false;
		inventoryPlaceIndicator.enabled = false;
		inventoryTransferIndicator.enabled = true;
		inventoryDropIndicator.enabled = false;
		inventoryDestroyIndicator.enabled = false;
	}

	public void InventoryDrop()
	{
		inventoryCursorMode = InventoryCursorMode.Drop;
		inventoryUseIndicator.enabled = false;
		inventoryEquipIndicator.enabled = false;
		inventoryPlaceIndicator.enabled = false;
		inventoryTransferIndicator.enabled = false;
		inventoryDropIndicator.enabled = true;
		inventoryDestroyIndicator.enabled = false;
	}

	public void InventoryDestroy()
	{
		inventoryCursorMode = InventoryCursorMode.Destroy;
		inventoryUseIndicator.enabled = false;
		inventoryEquipIndicator.enabled = false;
		inventoryPlaceIndicator.enabled = false;
		inventoryTransferIndicator.enabled = false;
		inventoryDropIndicator.enabled = false;
		inventoryDestroyIndicator.enabled = true;
	}

	public void InventoryName()
	{
		inventorySortMode = InventorySortMode.Name;
		inventoryNameIndicator.enabled = true;
		inventoryTypeIndicator.enabled = false;
		inventoryVolumeIndicator.enabled = false;
		inventoryWeightIndicator.enabled = false;
		inventoryValueIndicator.enabled = false;
		inventoryRarityIndicator.enabled = false;
		SortInventory();
	}

	public void InventoryType()
	{
		inventorySortMode = InventorySortMode.Type;
		inventoryNameIndicator.enabled = false;
		inventoryTypeIndicator.enabled = true;
		inventoryVolumeIndicator.enabled = false;
		inventoryWeightIndicator.enabled = false;
		inventoryValueIndicator.enabled = false;
		inventoryRarityIndicator.enabled = false;
		SortInventory();
	}

	public void InventoryVolume()
	{
		inventorySortMode = InventorySortMode.Volume;
		inventoryNameIndicator.enabled = false;
		inventoryTypeIndicator.enabled = false;
		inventoryVolumeIndicator.enabled = true;
		inventoryWeightIndicator.enabled = false;
		inventoryValueIndicator.enabled = false;
		inventoryRarityIndicator.enabled = false;
		SortInventory();
	}

	public void InventoryWeight()
	{
		inventorySortMode = InventorySortMode.Weight;
		inventoryNameIndicator.enabled = false;
		inventoryTypeIndicator.enabled = false;
		inventoryVolumeIndicator.enabled = false;
		inventoryWeightIndicator.enabled = true;
		inventoryValueIndicator.enabled = false;
		inventoryRarityIndicator.enabled = false;
		SortInventory();
	}

	public void InventoryValue()
	{
		inventorySortMode = InventorySortMode.Value;
		inventoryNameIndicator.enabled = false;
		inventoryTypeIndicator.enabled = false;
		inventoryVolumeIndicator.enabled = false;
		inventoryWeightIndicator.enabled = false;
		inventoryValueIndicator.enabled = true;
		inventoryRarityIndicator.enabled = false;
		SortInventory();
	}

	public void InventoryRarity()
	{
		inventorySortMode = InventorySortMode.Rarity;
		inventoryNameIndicator.enabled = false;
		inventoryTypeIndicator.enabled = false;
		inventoryVolumeIndicator.enabled = false;
		inventoryWeightIndicator.enabled = false;
		inventoryValueIndicator.enabled = false;
		inventoryRarityIndicator.enabled = true;
		SortInventory();
	}

	public void ContainerUse()
	{
		containerCursorMode = ContainerCursorMode.Use;
		containerUseIndicator.enabled = true;
		containerEquipIndicator.enabled = false;
		containerPlaceIndicator.enabled = false;
		containerTransferIndicator.enabled = false;
		containerDropIndicator.enabled = false;
		containerDestroyIndicator.enabled = false;
	}

	public void ContainerEquip()
	{
		containerCursorMode = ContainerCursorMode.Inspect;
		containerUseIndicator.enabled = false;
		containerEquipIndicator.enabled = true;
		containerPlaceIndicator.enabled = false;
		containerTransferIndicator.enabled = false;
		containerDropIndicator.enabled = false;
		containerDestroyIndicator.enabled = false;
	}

	public void ContainerInspect()
	{
		containerCursorMode = ContainerCursorMode.Place;
		containerUseIndicator.enabled = false;
		containerEquipIndicator.enabled = false;
		containerPlaceIndicator.enabled = true;
		containerTransferIndicator.enabled = false;
		containerDropIndicator.enabled = false;
		containerDestroyIndicator.enabled = false;
	}

	public void ContainerTransfer()
	{
		containerCursorMode = ContainerCursorMode.Transfer;
		containerUseIndicator.enabled = false;
		containerEquipIndicator.enabled = false;
		containerPlaceIndicator.enabled = false;
		containerTransferIndicator.enabled = true;
		containerDropIndicator.enabled = false;
		containerDestroyIndicator.enabled = false;
	}

	public void ContainerDrop()
	{
		containerCursorMode = ContainerCursorMode.Drop;
		containerUseIndicator.enabled = false;
		containerEquipIndicator.enabled = false;
		containerPlaceIndicator.enabled = false;
		containerTransferIndicator.enabled = false;
		containerDropIndicator.enabled = true;
		containerDestroyIndicator.enabled = false;
	}

	public void ContainerDestroy()
	{
		containerCursorMode = ContainerCursorMode.Destroy;
		containerUseIndicator.enabled = false;
		containerEquipIndicator.enabled = false;
		containerPlaceIndicator.enabled = false;
		containerTransferIndicator.enabled = false;
		containerDropIndicator.enabled = false;
		containerDestroyIndicator.enabled = true;
	}

	public void ContainerName()
	{
		containerSortMode = ContainerSortMode.Name;
		containerNameIndicator.enabled = true;
		containerTypeIndicator.enabled = false;
		containerVolumeIndicator.enabled = false;
		containerWeightIndicator.enabled = false;
		containerValueIndicator.enabled = false;
		containerRarityIndicator.enabled = false;
		SortContainer();
	}

	public void ContainerType()
	{
		containerSortMode = ContainerSortMode.Type;
		containerNameIndicator.enabled = false;
		containerTypeIndicator.enabled = true;
		containerVolumeIndicator.enabled = false;
		containerWeightIndicator.enabled = false;
		containerValueIndicator.enabled = false;
		containerRarityIndicator.enabled = false;
		SortContainer();
	}

	public void ContainerVolume()
	{
		containerSortMode = ContainerSortMode.Volume;
		containerNameIndicator.enabled = false;
		containerTypeIndicator.enabled = false;
		containerVolumeIndicator.enabled = true;
		containerWeightIndicator.enabled = false;
		containerValueIndicator.enabled = false;
		containerRarityIndicator.enabled = false;
		SortContainer();
	}

	public void ContainerWeight()
	{
		containerSortMode = ContainerSortMode.Weight;
		containerNameIndicator.enabled = false;
		containerTypeIndicator.enabled = false;
		containerVolumeIndicator.enabled = false;
		containerWeightIndicator.enabled = true;
		containerValueIndicator.enabled = false;
		containerRarityIndicator.enabled = false;
		SortContainer();
	}

	public void ContainerValue()
	{
		containerSortMode = ContainerSortMode.Value;
		containerNameIndicator.enabled = false;
		containerTypeIndicator.enabled = false;
		containerVolumeIndicator.enabled = false;
		containerWeightIndicator.enabled = false;
		containerValueIndicator.enabled = true;
		containerRarityIndicator.enabled = false;
		SortContainer();
	}

	public void ContainerRarity()
	{
		containerSortMode = ContainerSortMode.Rarity;
		containerNameIndicator.enabled = false;
		containerTypeIndicator.enabled = false;
		containerVolumeIndicator.enabled = false;
		containerWeightIndicator.enabled = false;
		containerValueIndicator.enabled = false;
		containerRarityIndicator.enabled = true;
		SortContainer();
	}

	public void SortContainer()
	{
		if (containerSortMode == ContainerSortMode.Name)
		{
			container.NameSort();
		}
		if (containerSortMode == ContainerSortMode.Type)
		{
			container.TypeSort();
		}
		if (containerSortMode == ContainerSortMode.Volume)
		{
			container.VolumeSort();
		}
		if (containerSortMode == ContainerSortMode.Weight)
		{
			container.WeightSort();
		}
		if (containerSortMode == ContainerSortMode.Value)
		{
			container.ValueSort();
		}
		if (containerSortMode == ContainerSortMode.Rarity)
		{
			container.RaritySort();
		}
	}

	public void SortInventory()
	{
		if (inventorySortMode == InventorySortMode.Name)
		{
			inventory.NameSort();
		}
		if (inventorySortMode == InventorySortMode.Type)
		{
			inventory.TypeSort();
		}
		if (inventorySortMode == InventorySortMode.Volume)
		{
			inventory.VolumeSort();
		}
		if (inventorySortMode == InventorySortMode.Weight)
		{
			inventory.WeightSort();
		}
		if (inventorySortMode == InventorySortMode.Value)
		{
			inventory.ValueSort();
		}
		if (inventorySortMode == InventorySortMode.Rarity)
		{
			inventory.RaritySort();
		}
	}

	public void CheckWeaponUI()
	{
		if (playerData.isInspecting)
		{
			weaponUI.enabled = true;
		}
		else
		{
			weaponUI.enabled = false;
		}
	}
}
