using UnityEngine;

public class BarrelKick : MonoBehaviour
{
	[Header("Player Data")]
	public PlayerData playerData;

	[Header("Slide Kick")]
	public float kick;

	public float kickPower;

	public float kickRecovery;

	private float recoil;

	private float kickValue;

	private Vector3 currentKick;

	private Weapon weaponSystem;

	private void Start()
	{
		weaponSystem = GameObject.FindWithTag("Player").GetComponent<Weapon>();
	}

	private void Update()
	{
		if (playerData.primaryActive || playerData.secondaryActive)
		{
			currentKick = Vector3.Lerp(currentKick, Vector3.zero, kickRecovery * Time.deltaTime);
			kickValue = Mathf.Lerp(base.transform.localPosition.y, currentKick.y, kickPower * Time.deltaTime);
			base.transform.localPosition = new Vector3(base.transform.localPosition.x, kickValue, base.transform.localPosition.z);
			if (playerData.isFiring)
			{
				recoil += 1f * Time.deltaTime;
			}
			else if (recoil > 2f)
			{
				recoil -= Time.deltaTime * recoil;
			}
		}
	}

	public void ApplyBarrelKick()
	{
		currentKick += new Vector3(Random.Range(0, 0), kick, Random.Range(0, 0));
	}
}
