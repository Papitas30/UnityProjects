using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
	public enum SlotType
	{
		InventorySlot = 0,
		ContainerSlot = 1,
		EquipmentSlot = 2
	}

	public ItemData itemData;

	public PlayerData playerData;

	public Image image;

	public Text itemTag;

	public Inventory inventory;

	public Equipment equipment;

	public UIManager UIManager;

	public AudioManager audioManager;

	public Placer placer;

	public Vitals vitals;

	public Text condition;

	public Image isUsing;

	public float useTimer;

	public int usage;

	private Transform player;

	[Header("Slot Type")]
	public SlotType slotType;

	public void Start()
	{
		player = GameObject.FindWithTag("Player").transform;
		inventory = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();
		placer = GameObject.FindWithTag("Player").GetComponent<Placer>();
		equipment = GameObject.FindWithTag("Player").GetComponent<Equipment>();
		vitals = GameObject.FindWithTag("Player").GetComponent<Vitals>();
		UIManager = GameObject.FindWithTag("UI Manager").GetComponent<UIManager>();
		audioManager = GameObject.FindWithTag("Audio Manager").GetComponent<AudioManager>();
		itemTag = GetComponentInChildren<Text>();
		image = GetComponent<Image>();
		image.sprite = itemData.icon;
		itemTag.text = itemData.abbreviation.ToUpper();
	}

	private void Used()
	{
		playerData.isUsing = false;
		isUsing.enabled = false;
		if (usage <= 0)
		{
			if (slotType == SlotType.InventorySlot)
			{
				inventory.Remove(itemData);
				Object.Destroy(base.gameObject);
			}
			if (slotType == SlotType.ContainerSlot)
			{
				UIManager.container.Remove(itemData);
				Object.Destroy(base.gameObject);
			}
		}
	}

	public void UpdateUsage(int amount)
	{
		if (itemData.hasUsage)
		{
			usage = amount;
			condition.enabled = true;
			condition.text = usage + "%";
		}
		else
		{
			condition.enabled = false;
		}
	}

	public void Click()
	{
		if (playerData.isUsing || playerData.isReloading)
		{
			return;
		}
		audioManager.PlayClick();
		if (slotType == SlotType.InventorySlot && UIManager.inventoryCursorMode == UIManager.InventoryCursorMode.Use)
		{
			if (itemData.type == ItemData.ItemType.Primary && equipment.equipments[0] == null)
			{
				Transform primarySlot = UIManager.primarySlot;
				primarySlot.GetChild(0).gameObject.SetActive(false);
				GameObject obj = Object.Instantiate(itemData.slot);
				obj.transform.SetParent(primarySlot);
				obj.GetComponent<Slot>().itemData = itemData;
				obj.GetComponent<Slot>().slotType = SlotType.EquipmentSlot;
				obj.transform.localScale = new Vector3(0.95f, 0.95f);
				obj.name = itemData.abbreviation;
				equipment.AddEquipment(itemData, 0);
				inventory.Remove(itemData);
				Object.Destroy(base.gameObject);
			}
			if (itemData.type == ItemData.ItemType.Secondary && equipment.equipments[1] == null)
			{
				Transform secondarySlot = UIManager.secondarySlot;
				secondarySlot.GetChild(0).gameObject.SetActive(false);
				GameObject obj2 = Object.Instantiate(itemData.slot);
				obj2.transform.SetParent(secondarySlot);
				obj2.GetComponent<Slot>().itemData = itemData;
				obj2.GetComponent<Slot>().slotType = SlotType.EquipmentSlot;
				obj2.transform.localScale = new Vector3(0.95f, 0.95f);
				obj2.name = itemData.abbreviation;
				equipment.AddEquipment(itemData, 1);
				inventory.Remove(itemData);
				Object.Destroy(base.gameObject);
			}
			if (itemData.type == ItemData.ItemType.Consumables)
			{
				usage -= 50;
				vitals.ModifyHealth(true, 10f);
				UpdateUsage(usage);
				audioManager.PlayUseSound(itemData.useSound);
				playerData.isUsing = true;
				isUsing.enabled = true;
				Invoke("Used", 2f);
			}
		}
		if (slotType == SlotType.ContainerSlot && UIManager.containerCursorMode == UIManager.ContainerCursorMode.Use)
		{
			if (itemData.type == ItemData.ItemType.Primary && equipment.equipments[0] == null)
			{
				Transform primarySlot2 = UIManager.primarySlot;
				primarySlot2.GetChild(0).gameObject.SetActive(false);
				GameObject obj3 = Object.Instantiate(itemData.slot);
				obj3.transform.SetParent(primarySlot2);
				obj3.GetComponent<Slot>().itemData = itemData;
				obj3.GetComponent<Slot>().slotType = SlotType.EquipmentSlot;
				obj3.transform.localScale = new Vector3(0.95f, 0.95f);
				obj3.name = itemData.abbreviation;
				equipment.AddEquipment(itemData, 0);
				UIManager.container.Remove(itemData);
				Object.Destroy(base.gameObject);
			}
			if (itemData.type == ItemData.ItemType.Secondary && equipment.equipments[1] == null)
			{
				Transform secondarySlot2 = UIManager.secondarySlot;
				secondarySlot2.GetChild(0).gameObject.SetActive(false);
				GameObject obj4 = Object.Instantiate(itemData.slot);
				obj4.transform.SetParent(secondarySlot2);
				obj4.GetComponent<Slot>().itemData = itemData;
				obj4.GetComponent<Slot>().slotType = SlotType.EquipmentSlot;
				obj4.transform.localScale = new Vector3(0.95f, 0.95f);
				obj4.name = itemData.abbreviation;
				equipment.AddEquipment(itemData, 1);
				UIManager.container.Remove(itemData);
				Object.Destroy(base.gameObject);
			}
			if (itemData.type == ItemData.ItemType.Consumables)
			{
				usage -= 50;
				vitals.ModifyHealth(true, 10f);
				UpdateUsage(usage);
				audioManager.PlayUseSound(itemData.useSound);
				playerData.isUsing = true;
				isUsing.enabled = true;
				Invoke("Used", 2f);
			}
		}
		if (slotType == SlotType.EquipmentSlot)
		{
			if (itemData.type == ItemData.ItemType.Primary)
			{
				if (inventory.CanFit(itemData))
				{
					if (playerData.isInspecting)
					{
						UIManager.weaponUI.enabled = false;
						playerData.isInspecting = false;
					}
					UIManager.primarySlot.GetChild(0).gameObject.SetActive(true);
					inventory.Add(itemData, usage);
					equipment.RemoveEquipment(0);
					Object.Destroy(base.gameObject);
				}
				else
				{
					audioManager.PlayError();
					UIManager.primarySlot.GetChild(0).gameObject.SetActive(true);
					Vector3 vector = player.GetComponent<Collider>().bounds.center + new Vector3(0f, 0.25f, 0f);
					Vector3 forward = player.transform.forward;
					Quaternion rotation = player.transform.rotation;
					float num = 0.2f;
					Vector3 position = vector + forward * num;
					GameObject obj5 = Object.Instantiate(itemData.pickup, position, rotation);
					obj5.GetComponent<Rigidbody>().AddForce(forward * 100f);
					obj5.name = itemData.name;
					obj5.GetComponent<Pickup>().item = itemData;
					obj5.GetComponent<Pickup>().usage = usage;
					Physics.IgnoreLayerCollision(7, 9, true);
					placer.ResetSlotDropCollision();
					equipment.RemoveEquipment(0);
					Object.Destroy(base.gameObject);
				}
			}
			if (itemData.type == ItemData.ItemType.Secondary)
			{
				if (inventory.CanFit(itemData))
				{
					if (playerData.isInspecting)
					{
						UIManager.weaponUI.enabled = false;
						playerData.isInspecting = false;
					}
					UIManager.secondarySlot.GetChild(0).gameObject.SetActive(true);
					inventory.Add(itemData, usage);
					equipment.RemoveEquipment(1);
					Object.Destroy(base.gameObject);
				}
				else
				{
					audioManager.PlayError();
					UIManager.secondarySlot.GetChild(0).gameObject.SetActive(true);
					Vector3 vector2 = player.GetComponent<Collider>().bounds.center + new Vector3(0f, 0.25f, 0f);
					Vector3 forward2 = player.transform.forward;
					Quaternion rotation2 = player.transform.rotation;
					float num2 = 0.2f;
					Vector3 position2 = vector2 + forward2 * num2;
					GameObject obj6 = Object.Instantiate(itemData.pickup, position2, rotation2);
					obj6.GetComponent<Rigidbody>().AddForce(forward2 * 100f);
					obj6.name = itemData.name;
					obj6.GetComponent<Pickup>().item = itemData;
					obj6.GetComponent<Pickup>().usage = usage;
					Physics.IgnoreLayerCollision(7, 9, true);
					placer.ResetSlotDropCollision();
					equipment.RemoveEquipment(1);
					Object.Destroy(base.gameObject);
				}
			}
		}
		if (UIManager.inventoryCursorMode == UIManager.InventoryCursorMode.Transfer && slotType == SlotType.InventorySlot && UIManager.containerIsOpen)
		{
			if (UIManager.container.CanFit(itemData))
			{
				UIManager.container.Add(itemData, usage);
				inventory.Remove(itemData);
				Object.Destroy(base.gameObject);
			}
			else
			{
				audioManager.PlayError();
			}
		}
		if (UIManager.containerCursorMode == UIManager.ContainerCursorMode.Transfer && slotType == SlotType.ContainerSlot)
		{
			if (inventory.CanFit(itemData))
			{
				inventory.Add(itemData, usage);
				UIManager.container.Remove(itemData);
				Object.Destroy(base.gameObject);
			}
			else
			{
				audioManager.PlayError();
			}
		}
		if (UIManager.inventoryCursorMode == UIManager.InventoryCursorMode.Drop && slotType == SlotType.InventorySlot)
		{
			Vector3 vector3 = player.GetComponent<Collider>().bounds.center + new Vector3(0f, 0.25f, 0f);
			Vector3 forward3 = player.transform.forward;
			Quaternion rotation3 = player.transform.rotation;
			float num3 = 0.2f;
			Vector3 position3 = vector3 + forward3 * num3;
			GameObject obj7 = Object.Instantiate(itemData.pickup, position3, rotation3);
			obj7.GetComponent<Rigidbody>().AddForce(forward3 * 100f);
			obj7.name = itemData.name;
			obj7.GetComponent<Pickup>().item = itemData;
			obj7.GetComponent<Pickup>().usage = usage;
			Physics.IgnoreLayerCollision(7, 9, true);
			placer.ResetSlotDropCollision();
			inventory.Remove(itemData);
			Object.Destroy(base.gameObject);
		}
		if (UIManager.inventoryCursorMode == UIManager.InventoryCursorMode.Place && slotType == SlotType.InventorySlot && !playerData.isPlacing)
		{
			Vector3 position4 = placer.placer.position;
			Vector3 forward4 = player.transform.forward;
			Quaternion rotation4 = player.transform.rotation;
			Vector3 vector4 = position4 + forward4;
			GameObject gameObject = Object.Instantiate(itemData.pickup, position4, rotation4);
			gameObject.name = itemData.name;
			gameObject.GetComponent<Pickup>().item = itemData;
			placer.PlaceObject(gameObject.GetComponent<Placeable>());
			inventory.Remove(itemData);
			Object.Destroy(base.gameObject);
		}
		if (UIManager.containerCursorMode == UIManager.ContainerCursorMode.Place && slotType == SlotType.ContainerSlot && !playerData.isPlacing)
		{
			Vector3 position5 = placer.placer.position;
			Vector3 forward5 = player.transform.forward;
			Quaternion rotation5 = player.transform.rotation;
			Vector3 vector5 = position5 + forward5;
			GameObject gameObject2 = Object.Instantiate(itemData.pickup, position5, rotation5);
			gameObject2.name = itemData.name;
			gameObject2.GetComponent<Pickup>().item = itemData;
			placer.PlaceObject(gameObject2.GetComponent<Placeable>());
			UIManager.container.Remove(itemData);
			Object.Destroy(base.gameObject);
		}
		if (UIManager.containerCursorMode == UIManager.ContainerCursorMode.Drop && slotType == SlotType.ContainerSlot)
		{
			Transform obj8 = UIManager.container.transform;
			Vector3 center = obj8.GetComponent<Collider>().bounds.center;
			Vector3 forward6 = obj8.transform.forward;
			Quaternion rotation6 = obj8.transform.rotation;
			float num4 = 1f;
			Vector3 position6 = center + forward6 * num4;
			float x = Random.Range(-200, 200);
			float y = Random.Range(-200, 200);
			float z = Random.Range(-200, 200);
			GameObject obj9 = Object.Instantiate(itemData.pickup, position6, rotation6);
			obj9.GetComponent<Rigidbody>().AddForce(forward6 * 100f);
			obj9.GetComponent<Rigidbody>().AddTorque(x, y, z);
			obj9.name = itemData.name;
			obj9.GetComponent<Pickup>().item = itemData;
			UIManager.container.Remove(itemData);
			Object.Destroy(base.gameObject);
		}
		if (UIManager.inventoryCursorMode == UIManager.InventoryCursorMode.Destroy && slotType == SlotType.InventorySlot)
		{
			inventory.Remove(itemData);
			Object.Destroy(base.gameObject);
		}
		if (UIManager.containerCursorMode == UIManager.ContainerCursorMode.Destroy && slotType == SlotType.ContainerSlot)
		{
			UIManager.container.Remove(itemData);
			Object.Destroy(base.gameObject);
		}
	}
}
