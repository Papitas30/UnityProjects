using UnityEngine;

public class Boat : MonoBehaviour
{
	public PlayerData playerData;

	public AudioSource motorIdle;

	public AudioSource motorThrust;

	private BoatController boatController;

	public bool isActive;

	public Transform boat;

	public Transform player;

	public Transform originalParent;

	public float idleVolume;

	public float thrustVolume;

	private void Start()
	{
		boat = base.transform.root;
		player = GameObject.FindWithTag("Player").GetComponent<Transform>();
		originalParent = player.parent;
		boatController = GetComponentInParent<BoatController>();
	}

	private void Update()
	{
		if (isActive)
		{
			motorThrust.volume = thrustVolume;
			motorIdle.volume = idleVolume;
			if (boatController.verticalInput == 1f)
			{
				idleVolume = Mathf.Lerp(idleVolume, 0.1f, Time.deltaTime * 4f);
				thrustVolume = Mathf.Lerp(thrustVolume, 0.75f, Time.deltaTime * 4f);
			}
			else
			{
				idleVolume = Mathf.Lerp(idleVolume, 0.5f, Time.deltaTime * 1f);
				thrustVolume = Mathf.Lerp(thrustVolume, 0f, Time.deltaTime * 2f);
			}
		}
	}

	public void TurnOn()
	{
		motorIdle.Play();
		motorThrust.Play();
	}

	public void TurnOff()
	{
		motorIdle.Stop();
		motorThrust.Stop();
	}

	public void Selected()
	{
		isActive = !isActive;
		if (isActive)
		{
			player.SetParent(boat);
			playerData.isFreezed = true;
			boatController.isActive = true;
			TurnOn();
		}
		else
		{
			player.SetParent(originalParent);
			playerData.isFreezed = false;
			boatController.isActive = false;
			TurnOff();
		}
	}
}
