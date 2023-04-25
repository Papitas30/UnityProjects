using UnityEngine;
using UnityEngine.Rendering;

public class Damage : MonoBehaviour
{
	[Header("Settings")]
	public float lerpSpeed;

	public PlayerData playerData;

	public Volume damageVolume;

	public bool damage;

	public AudioManager audioManager;

	private Vitals vitals;

	private void Start()
	{
		audioManager = GameObject.FindWithTag("Audio Manager").GetComponent<AudioManager>();
		vitals = GetComponent<Vitals>();
	}

	private void Update()
	{
		if (damage)
		{
			damageVolume.weight = Mathf.Lerp(damageVolume.weight, 1f, lerpSpeed * Time.deltaTime);
		}
		else
		{
			damageVolume.weight = Mathf.Lerp(damageVolume.weight, 0f, lerpSpeed * Time.deltaTime);
		}
		if (damageVolume.weight > 0.5f)
		{
			damage = false;
		}
	}

	public void TakeWeaponDamage()
	{
		damage = true;
		audioManager.PlayTakeDamage();
		audioManager.PlayImpactBody();
		playerData.cameraNoiseAmplitude = 40f;
		vitals.ModifyHealth(false, 25f);
	}

	public void TakeFallDamage()
	{
		damage = true;
		audioManager.PlayTakeDamage();
		playerData.cameraNoiseAmplitude = 40f;
		playerData.cameraNoiseFrequency = 1f;
		playerData.currentSpeed /= 10f;
		vitals.ModifyHealth(false, 10f);
	}
}
