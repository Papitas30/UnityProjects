using UnityEngine;

public class Cartridge : MonoBehaviour
{
	public PlayerData playerData;

	public AudioManager manager;

	private Rigidbody rb;

	private bool hasDropped;

	private int cartridge;

	public void Eject(float multiplier, int cartridgeType, AudioManager audioManager)
	{
		cartridge = cartridgeType;
		manager = audioManager;
		rb = GetComponent<Rigidbody>();
		rb.AddRelativeForce((float)Random.Range(20, 25) * multiplier, (float)Random.Range(2, 4) * multiplier, (float)Random.Range(15, 20) * multiplier, ForceMode.Impulse);
		rb.AddRelativeTorque((float)Random.Range(20, 100) * multiplier, (float)Random.Range(4, 10) * multiplier, (float)Random.Range(5, 20) * multiplier, ForceMode.Impulse);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (hasDropped)
		{
			return;
		}
		if (playerData.hardsurface)
		{
			if (playerData.woodSurface)
			{
				if (cartridge == 1)
				{
					manager.PlayCasingDropWood();
				}
				if (cartridge == 2)
				{
					manager.PlayShellDropWood();
				}
			}
			else
			{
				if (cartridge == 1)
				{
					manager.PlayCasingDropHard();
				}
				if (cartridge == 2)
				{
					manager.PlayShellDropHard();
				}
			}
		}
		else
		{
			if (cartridge == 1)
			{
				manager.PlayCasingDropSoft();
			}
			if (cartridge == 2)
			{
				manager.PlayShellDropSoft();
			}
		}
		hasDropped = true;
	}
}
