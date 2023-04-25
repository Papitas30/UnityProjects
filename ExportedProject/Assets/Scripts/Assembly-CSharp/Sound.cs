using UnityEngine;

public class Sound : MonoBehaviour
{
	public SphereCollider audiowave;

	public PlayerData playerData;

	private void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "Enemy")
		{
			collider.transform.root.GetComponent<AI>().lastHearPosition = base.transform.position;
		}
	}

	public void SetSoundwave()
	{
		if (playerData.isWalking)
		{
			audiowave.radius = 5f;
		}
		if (playerData.isRunning)
		{
			audiowave.radius = 15f;
		}
		if (playerData.isSprinting)
		{
			audiowave.radius = 25f;
		}
		if (playerData.isFiring)
		{
			audiowave.radius = 200f;
		}
		Invoke("Fade", 0.2f);
	}

	private void Fade()
	{
		audiowave.radius = 0f;
	}
}
