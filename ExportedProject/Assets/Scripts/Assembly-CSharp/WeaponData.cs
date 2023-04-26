using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Data", menuName = "Weapon Data", order = 3)]
public class WeaponData : ScriptableObject
{
	[Header("Weapon Audio")]
	public AudioEvent fire;

	public AudioEvent reload;

	[Header("Weapon Parameters")]
	public float damage;

	public float range;

	public float fireRate;

	public float handlingSpeed;

	public float reloadSpeed;
}
