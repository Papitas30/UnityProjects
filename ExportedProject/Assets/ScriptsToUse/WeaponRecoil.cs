using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
	[Header("Player Data")]
	public PlayerData playerData;

	[Header("Recoil Transforms")]
	public Transform weaponRecoil;

	public Transform cameraRecoil;

	public Transform weapon;

	public Equipment equipment;

	private float recoil;

	private Vector3 currentKick;

	private Vector3 currentRotation;

	private Vector3 RotationOutput;

	private Weapon weaponSystem;

	private void Start()
	{
		weaponSystem = GetComponent<Weapon>();
		recoil = 2f;
	}

	private void Update()
	{
		if (playerData.primaryActive || playerData.secondaryActive)
		{
			currentRotation = Vector3.Lerp(currentRotation, Vector3.zero, weaponSystem.itemData.rotationRecovery * Time.deltaTime);
			currentKick = Vector3.Lerp(currentKick, Vector3.zero, weaponSystem.itemData.kickRecovery * Time.deltaTime);
			weaponRecoil.localPosition = Vector3.Slerp(weaponRecoil.localPosition, currentKick, weaponSystem.itemData.kickPower * Time.deltaTime);
			cameraRecoil.localPosition = Vector3.Slerp(weaponRecoil.localPosition, currentKick, weaponSystem.itemData.kickPower * Time.deltaTime);
			RotationOutput = Vector3.Slerp(RotationOutput, currentRotation, weaponSystem.itemData.rotationPower * Time.fixedDeltaTime);
			cameraRecoil.localRotation = Quaternion.Euler(RotationOutput * recoil / 2f);
			Quaternion b;
			if (playerData.isAiming && playerData.primaryActive)
			{
				float y = RotationOutput.y * recoil;
				b = Quaternion.Euler(RotationOutput.x * recoil / 20f, y, 0f);
			}
			else
			{
				b = Quaternion.Euler(RotationOutput * recoil);
			}
			weapon.localRotation = Quaternion.Lerp(weapon.localRotation, b, Time.deltaTime * 10f);
			if (playerData.isFiring)
			{
				recoil += 2f * Time.deltaTime;
			}
			else if (recoil > 2f)
			{
				recoil -= Time.deltaTime * recoil;
			}
		}
	}

	public void ApplyRecoil()
	{
		if (playerData.isAiming)
		{
			currentRotation += new Vector3(weaponSystem.itemData.verticalRecoil, Random.Range(0f - weaponSystem.itemData.horizontalRecoil, weaponSystem.itemData.horizontalRecoil), 0f);
			currentKick += new Vector3(Random.Range(0, 0), Random.Range(0, 0), weaponSystem.itemData.weaponKick);
		}
		else
		{
			currentRotation += new Vector3(weaponSystem.itemData.verticalRecoil, Random.Range(0f - weaponSystem.itemData.horizontalRecoil, weaponSystem.itemData.horizontalRecoil), 0f);
			currentKick += new Vector3(Random.Range(0, 0), Random.Range(0, 0), weaponSystem.itemData.weaponKick * 1.5f);
		}
	}
}
