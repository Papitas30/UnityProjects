using UnityEngine;

public class Interactable : MonoBehaviour
{
	public enum InteractableType
	{
		Container = 0,
		Door = 1,
		Window = 2,
		Pickup = 3,
		Radio = 4,
		Fire = 5,
		Boat = 6,
		Television = 7,
		Candle = 8
	}

	[Header("Interaction")]
	public InteractableType interactableType;

	private Container container;

	private Pickup pickup;

	private Door door;

	private Radio radio;

	private Boat boat;

	private Fire fire;

	private Fire candle;

	private Television television;

	private AudioManager audioManager;

	private void Start()
	{
		audioManager = GameObject.FindWithTag("Audio Manager").GetComponent<AudioManager>();
	}

	public void Interact()
	{
		if (interactableType == InteractableType.Container)
		{
			base.gameObject.tag = "Container";
			container = GetComponent<Container>();
			container.OpenContainer();
		}
		if (interactableType == InteractableType.Pickup)
		{
			pickup = GetComponent<Pickup>();
			pickup.Interact();
		}
		if (interactableType == InteractableType.Door)
		{
			door = GetComponent<Door>();
			door.Selected();
		}
		if (interactableType == InteractableType.Radio)
		{
			radio = GetComponent<Radio>();
			radio.Selected();
		}
		if (interactableType == InteractableType.Fire)
		{
			fire = GetComponent<Fire>();
			fire.Selected();
		}
		if (interactableType == InteractableType.Candle)
		{
			candle = GetComponent<Fire>();
			candle.Selected();
		}
		if (interactableType == InteractableType.Boat)
		{
			boat = GetComponent<Boat>();
			boat.Selected();
		}
		if (interactableType == InteractableType.Television)
		{
			television = GetComponent<Television>();
			television.Selected();
		}
	}
}
