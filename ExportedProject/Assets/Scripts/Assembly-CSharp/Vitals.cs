using UnityEngine;
using UnityEngine.UI;

public class Vitals : MonoBehaviour
{
	[Header("Player Data")]
	public PlayerData playerData;

	[Header("UI Image Fills")]
	public FloatData healthFill;

	public FloatData bodyFill;

	[Header("UI Degug Texts")]
	public Text debugStaminaUI;

	public Text debugHealthUI;

	public Text healthUI;

	public float staminaMultiplier;

	public float breathingVolume;

	public float inversion;

	public float multiplier;

	public float runConsumption;

	public float sprintConsumption;

	public AudioSource audioSource;

	private void Start()
	{
		staminaMultiplier = 1f;
		playerData.bodyStamina = 100f;
	}

	private void Update()
	{
		Stamina();
		UpdateDebugUI();
	}

	private void Stamina()
	{
		if (playerData.isSprinting && playerData.bodyStamina > 0f)
		{
			playerData.bodyStamina -= Time.deltaTime * sprintConsumption;
		}
		else if (playerData.isRunning && playerData.bodyStamina > 0f)
		{
			playerData.bodyStamina -= Time.deltaTime * runConsumption;
		}
		else if (playerData.bodyStamina < 100f && !playerData.isJumping && !playerData.isOutOfStamina)
		{
			playerData.bodyStamina += Time.deltaTime * 10f;
		}
		else if (playerData.bodyStamina < 100f && !playerData.isJumping && playerData.isOutOfStamina)
		{
			playerData.bodyStamina += Time.deltaTime * 0.025f;
		}
		inversion = Mathf.InverseLerp(100f, 0f, playerData.bodyStamina);
		if (playerData.bodyStamina < 50f)
		{
			if (breathingVolume < 1f)
			{
				breathingVolume += Time.deltaTime * 0.1f;
			}
		}
		else if (breathingVolume > 0f)
		{
			breathingVolume -= Time.deltaTime * 0.2f;
		}
		if ((double)playerData.bodyStamina < 0.1)
		{
			playerData.isOutOfStamina = true;
			if (staminaMultiplier > 0f)
			{
				staminaMultiplier -= Time.deltaTime * 0.1f;
			}
		}
		else if ((double)playerData.bodyStamina > 0.1)
		{
			playerData.isOutOfStamina = false;
			if (staminaMultiplier < 1f)
			{
				staminaMultiplier += Time.deltaTime * 0.1f;
			}
		}
		audioSource.volume = breathingVolume;
		multiplier = 1f + inversion;
	}

	public void ModifyBodyStamina(bool direction, float amount)
	{
		if (direction)
		{
			playerData.bodyStamina += amount;
			if (playerData.bodyStamina > 100f)
			{
				playerData.bodyStamina = 100f;
			}
		}
		else
		{
			playerData.bodyStamina -= amount;
			if (playerData.bodyStamina < 0f)
			{
				playerData.bodyStamina = 0f;
			}
		}
	}

	public void ModifyHealth(bool direction, float amount)
	{
		if (direction)
		{
			playerData.health += amount;
			if (playerData.health > 100f)
			{
				playerData.health = 100f;
			}
			return;
		}
		playerData.health -= amount;
		if (playerData.health <= 0f)
		{
			GetComponent<Death>().PlayerDeath();
			playerData.health = 0f;
		}
	}

	private void UpdateDebugUI()
	{
		healthUI.text = playerData.health.ToString("F0") + " / 100";
		debugHealthUI.text = playerData.health.ToString("F0") + "%";
		debugStaminaUI.text = playerData.bodyStamina.ToString("F0") + "%";
		healthFill.SetValue(playerData.health);
		bodyFill.SetValue(playerData.bodyStamina);
	}
}
