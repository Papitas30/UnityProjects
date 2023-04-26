using UnityEngine;

public class Pickup : MonoBehaviour
{
	public ItemData item;

	private Inventory inventory;

	private UIManager UIManager;

	private AudioManager audioManager;

	public int usage;

	private void Start()
	{
		inventory = GameObject.FindWithTag("Inventory").GetComponent<Inventory>();
		UIManager = GameObject.FindWithTag("UI Manager").GetComponent<UIManager>();
		audioManager = GameObject.FindWithTag("Audio Manager").GetComponent<AudioManager>();
	}

	public void Interact()
	{
		if (inventory.CanFit(item))
		{
			inventory.Add(item, usage);
			audioManager.PlayPickup();
			UIManager.UpdateInventoryStats();
			Object.Destroy(base.gameObject);
		}
		else
		{
			audioManager.PlayError();
		}
	}
}
