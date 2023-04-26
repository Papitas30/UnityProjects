using UnityEngine;

public class DistantSound : MonoBehaviour
{
	[Header("Recoil Transforms")]
	public Transform weaponRecoil;

	public Transform weapon;

	public AudioClip closeSound;

	public AudioClip distantSound;

	public AudioSource close;

	public AudioSource distant;

	public Animator animator;

	public float fireTimer;

	public bool fire;

	private float recoil;

	private Vector3 RecoilRotation_Aim;

	private Vector3 RecoilKickBack_Aim;

	private Vector3 currentKick;

	private Vector3 currentRotation;

	private Vector3 RotationOutput;

	private Weapon weaponSystem;

	public bool isFiring;

	public ParticleSystem flash;

	public ParticleSystem smoke;

	public ParticleSystem ejector;

	private void Start()
	{
		Invoke("Fire", 4f);
		weaponSystem = GetComponent<Weapon>();
		recoil = 2f;
	}

	private void Fire()
	{
		animator.enabled = true;
		fire = true;
	}

	private void Update()
	{
		if (fire)
		{
			fireTimer += Time.deltaTime;
			if (fireTimer > 1f)
			{
				isFiring = true;
				flash.Play();
				smoke.Play();
				ejector.Play();
				ApplyRecoil();
				close.PlayOneShot(closeSound);
				distant.PlayOneShot(distantSound);
				fireTimer = 0f;
			}
			currentRotation = Vector3.Lerp(currentRotation, Vector3.zero, 5f * Time.deltaTime);
			currentKick = Vector3.Lerp(currentKick, Vector3.zero, 10f * Time.deltaTime);
			weaponRecoil.localPosition = Vector3.Slerp(weaponRecoil.localPosition, currentKick, 80f * Time.deltaTime);
			RotationOutput = Vector3.Slerp(RotationOutput, currentRotation, 10f * Time.fixedDeltaTime);
			weapon.localRotation = Quaternion.Euler(RotationOutput * recoil);
			if (isFiring)
			{
				recoil += 1f * Time.deltaTime;
				isFiring = false;
			}
			else if (recoil > 2f)
			{
				recoil -= Time.deltaTime * recoil;
			}
		}
	}

	public void ApplyRecoil()
	{
		currentRotation += new Vector3(RecoilRotation_Aim.x, Random.Range(0f - RecoilRotation_Aim.y, RecoilRotation_Aim.y), Random.Range(0f - RecoilRotation_Aim.z, RecoilRotation_Aim.z));
		currentKick += new Vector3(Random.Range(0f - RecoilKickBack_Aim.x, RecoilKickBack_Aim.x), Random.Range(0f - RecoilKickBack_Aim.y, RecoilKickBack_Aim.y), RecoilKickBack_Aim.z);
		currentRotation += new Vector3(-2f, Random.Range(-1, 1), 0f);
		currentKick += new Vector3(Random.Range(0, 0), Random.Range(0, 0), -0.02f);
	}
}
