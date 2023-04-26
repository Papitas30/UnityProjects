using UnityEngine;

public class Character : MonoBehaviour
{
	public PlayerData playerData;

	[Header("Day")]
	public int day;

	[Header("Vitals")]
	public float health;

	public float stamina;

	public float hydration;

	public float energy;

	[Header("Conditions")]
	public bool fatique;

	public bool pain;

	public bool bleeding;

	public bool fracture;

	public bool delirium;

	public bool poisoning;

	public bool rupture;

	public bool hypovolemia;
}
