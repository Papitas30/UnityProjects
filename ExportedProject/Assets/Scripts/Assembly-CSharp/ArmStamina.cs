using UnityEngine;
using UnityEngine.UI;

public class ArmStamina : MonoBehaviour
{
	public PlayerData playerData;

	public FloatData armStamina;

	public float aimConsumption;

	public float highConsumption;

	public float recoverySpeed;

	public float inversion;

	public float multiplier;

	public Text debug;

	private void Update()
	{
		if (playerData.primaryActive || playerData.secondaryActive)
		{
			if (playerData.isAiming && playerData.armStamina > 0f && !playerData.isSprinting && !playerData.isOutOfStamina)
			{
				playerData.armStamina -= Time.deltaTime * aimConsumption;
			}
			else if (playerData.weaponPosition == 2 && playerData.armStamina > 0f && !playerData.isSprinting && !playerData.isOutOfStamina)
			{
				playerData.armStamina -= Time.deltaTime * highConsumption;
			}
			else if (playerData.weaponPosition == 1 && playerData.armStamina < 100f && !playerData.isSprinting && !playerData.isOutOfStamina)
			{
				playerData.armStamina += Time.deltaTime * recoverySpeed;
			}
			if (playerData.isSprinting)
			{
				playerData.armStamina -= Time.deltaTime * highConsumption;
			}
			if (playerData.isOutOfStamina)
			{
				playerData.armStamina -= Time.deltaTime * 10f;
			}
		}
		else
		{
			playerData.armStamina += Time.deltaTime * recoverySpeed;
		}
		if (playerData.armStamina > 100f)
		{
			playerData.armStamina = 100f;
		}
		if (playerData.armStamina < 0f)
		{
			playerData.armStamina = 0f;
		}
		armStamina.SetValue(playerData.armStamina);
		inversion = Mathf.InverseLerp(100f, 0f, playerData.armStamina);
		multiplier = 1f + inversion;
		debug.text = playerData.armStamina.ToString("F0") + "%";
	}

	public void ModifyArmStamina(bool direction, float amount)
	{
		if (direction)
		{
			playerData.armStamina += amount;
			if (playerData.armStamina > 100f)
			{
				playerData.armStamina = 100f;
			}
		}
		else
		{
			playerData.armStamina -= amount;
			if (playerData.armStamina < 0f)
			{
				playerData.armStamina = 0f;
			}
		}
	}
}
