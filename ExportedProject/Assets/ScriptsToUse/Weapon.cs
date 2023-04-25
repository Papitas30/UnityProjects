using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	[Header("Data")]
	public PlayerData playerData;

	public ItemData itemData;

	[Header("Weapon Data")]
	public float damage = 20f;

	public float weaponRange = 100f;

	public float fireRate = 0.1f;

	[Header("Ammo")]
	public float ammoPerMag = 30f;

	public float ammoLeft = 200f;

	public float currentAmmo;

	public float magazinePercentage;

	private Transform firepoints;

	private Transform sight;

	private Transform hip;

	private UIManager UIManager;

	private AudioManager audioManager;

	private WeaponRecoil recoil;

	private bool boltHasStriked;

	private bool ammoHasChanged;

	private float fireTimer;

	[Header("Bullet Decal")]
	public GameObject decal;

	private Vector3 decalPosition;

	private Vector3 decalRotation;

	private Transform decalParent;

	private Transform bloodParent;

	public GameObject cartidge;

	public GameObject impact;

	public GameObject spatterDecal;

	public GameObject bloodDecal;

	public GameObject bloodPuff;

	private Transform ejector;

	public ParticleSystem flash;

	public ParticleSystem smoke;

	[Header("Penetration")]
	public float penetrationAmount;

	public Vector3 endPoint;

	public Vector3? penetrationPoint;

	public Vector3? impactPoint;

	public Vector3 exitPosition;

	[Header("Reload")]
	public float reloadTime;

	public float reloadSpeed = 4f;

	public bool reloadTimer;

	public float reloadPercentage;

	private bool canEnterPhase1;

	private bool canEnterPhase2;

	private bool canEnterPhase3;

	public Transform weaponHolder;

	public GameObject laser;

	private MeshRenderer laserRenderer;

	public bool enemy;

	public bool spatter;

	public LayerMask layerMask;

	public List<SlideKick> slideKicks;

	public float maximumForce;

	private Transform firePosition;

	private Vector3 fireDirection;

	private bool hipFire;

	public bool debugReload;

	public bool chamberEmpty;

	private float spread;

	public Light lightFPS;

	public Light lightWorld;

	private Camera mainCamera;

	private Sound soundwave;

	private void Start()
	{
		mainCamera = Camera.main;
		soundwave = GetComponent<Sound>();
		slideKicks.Clear();
		recoil = GetComponent<WeaponRecoil>();
		UIManager = GameObject.FindWithTag("UI Manager").GetComponent<UIManager>();
		audioManager = GameObject.FindWithTag("Audio Manager").GetComponent<AudioManager>();
		ammoHasChanged = true;
		canEnterPhase1 = true;
		laserRenderer = laser.GetComponent<MeshRenderer>();
	}

	public void SetReferences()
	{
		firepoints = weaponHolder.GetChild(0).transform.Find("Firepoints");
		sight = firepoints.transform.Find("FP_Sight");
		hip = firepoints.transform.Find("FP_Barrel");
		ammoPerMag = itemData.magazineSize;
		currentAmmo = ammoPerMag;
		recoil.weapon = weaponHolder.GetChild(0);
		flash = weaponHolder.GetChild(0).transform.Find("VFX").transform.GetChild(0).GetComponent<ParticleSystem>();
		smoke = weaponHolder.GetChild(0).transform.Find("VFX").transform.GetChild(1).GetComponent<ParticleSystem>();
		lightFPS = weaponHolder.GetChild(0).transform.Find("Lights").transform.GetChild(0).GetComponent<Light>();
		lightWorld = weaponHolder.GetChild(0).transform.Find("Lights").transform.GetChild(1).GetComponent<Light>();
		ejector = weaponHolder.GetChild(0).transform.Find("Ejector");
		chamberEmpty = false;
		playerData.magazineEmpty = false;
		boltHasStriked = false;
	}

	private void Update()
	{
		Laser();
		if (playerData.primaryActive || playerData.secondaryActive)
		{
			FireInput();
			Ammo();
			Reload();
		}
		if (playerData.isInspecting && ammoHasChanged)
		{
			CalculateAmmo();
			ammoHasChanged = false;
		}
	}

	private void Reload()
	{
		if (playerData.isReloading && ammoLeft != 0f)
		{
			reloadTimer = true;
			if (reloadPercentage > 10f && canEnterPhase1)
			{
				if (!itemData.pumpAction)
				{
					itemData.unload.Play(audioManager.weapon, 1f);
				}
				if (itemData.boltAction)
				{
					itemData.unload.Play(audioManager.weapon, 1f);
				}
				canEnterPhase1 = false;
				canEnterPhase2 = true;
			}
			if (reloadPercentage > 50f && canEnterPhase2)
			{
				if (!itemData.pumpAction && !itemData.boltAction)
				{
					itemData.load.Play(audioManager.weapon, 1f);
				}
				else
				{
					if (itemData.hasSlideKick)
					{
						foreach (SlideKick slideKick in slideKicks)
						{
							slideKick.ApplySlideKick();
						}
					}
					itemData.cock.Play(audioManager.weapon, 1f);
					if (itemData.pumpAction)
					{
						Object.Instantiate(itemData.cartridge, ejector.position, ejector.rotation).GetComponent<Cartridge>().Eject(0.15f, 2, audioManager);
					}
					if (itemData.boltAction)
					{
						Object.Instantiate(itemData.cartridge, ejector.position, ejector.rotation).GetComponent<Cartridge>().Eject(0.1f, 1, audioManager);
					}
				}
				canEnterPhase2 = false;
				canEnterPhase3 = true;
			}
			if (reloadPercentage > 80f && canEnterPhase3)
			{
				if (!itemData.pumpAction && !itemData.boltAction)
				{
					itemData.cock.Play(audioManager.weapon, 1f);
					if (itemData.type == ItemData.ItemType.Secondary)
					{
						foreach (SlideKick slideKick2 in slideKicks)
						{
							slideKick2.ReleaseSlide();
						}
					}
				}
				chamberEmpty = false;
				canEnterPhase3 = false;
			}
		}
		if (!reloadTimer)
		{
			return;
		}
		reloadTime += Time.deltaTime;
		reloadPercentage = reloadTime * 100f / itemData.reloadSpeed;
		if (reloadTime >= itemData.reloadSpeed)
		{
			float num = ammoPerMag - currentAmmo;
			float num2 = ((ammoLeft >= num) ? num : ammoLeft);
			ammoLeft -= num2;
			currentAmmo += num2;
			reloadTime = 0f;
			boltHasStriked = false;
			playerData.magazineEmpty = false;
			if (!debugReload)
			{
				playerData.isReloading = false;
			}
			reloadPercentage = 0f;
			canEnterPhase1 = true;
			canEnterPhase2 = false;
			canEnterPhase3 = false;
			CalculateAmmo();
			reloadTimer = false;
		}
	}

	private void FireInput()
	{
		if (playerData.isFiring && !chamberEmpty)
		{
			Fire();
			playerData.armStamina -= 0.1f;
		}
		if (playerData.fireMode == PlayerData.FireMode.Single)
		{
			fireRate = 0.1f;
		}
		if (playerData.fireMode == PlayerData.FireMode.Auto)
		{
			fireRate = itemData.fireRate;
		}
		if (fireTimer < fireRate)
		{
			fireTimer += Time.deltaTime;
		}
	}

	public void Fire()
	{
		if (fireTimer < fireRate || playerData.isReloading || currentAmmo == 0f || playerData.isDead)
		{
			return;
		}
		if (itemData.pumpAction)
		{
			for (int i = 0; i < 6; i++)
			{
				Raycast(2f);
			}
			recoil.ApplyRecoil();
		}
		else
		{
			Raycast(1f);
			recoil.ApplyRecoil();
		}
		if (itemData.fireType == ItemData.FireType.Single)
		{
			chamberEmpty = true;
		}
		if (itemData.hasSlideKick)
		{
			SlideKick();
		}
		currentAmmo -= 1f;
		ammoHasChanged = true;
		fireTimer = 0f;
		flash.Play();
		smoke.Play();
		soundwave.SetSoundwave();
		CalculateAmmo();
		MuzzleFlash();
		Invoke("ResetFlash", 0.02f);
		Cartridge();
		audioManager.PlayWeaponFire(itemData.fire);
	}

	private void Raycast(float multiplier)
	{
		if (playerData.weaponPosition == 2 && !playerData.isAiming && !playerData.isJumping)
		{
			if (playerData.laserActive)
			{
				if (itemData.pumpAction)
				{
					spread = 0.025f;
				}
				else
				{
					spread = 0f;
				}
			}
			else
			{
				spread = 0.01f;
			}
			hipFire = true;
			firePosition = hip;
		}
		else if (playerData.weaponPosition == 2 && !playerData.isAiming && playerData.isJumping)
		{
			spread = 0.1f;
			hipFire = true;
			firePosition = hip;
		}
		else if (playerData.isAiming && !playerData.isJumping)
		{
			if (itemData.pumpAction)
			{
				spread = 0.025f;
				hipFire = false;
				firePosition = sight;
			}
			else
			{
				spread = 0f;
				hipFire = false;
				firePosition = sight;
			}
		}
		else if (playerData.isAiming && playerData.isJumping)
		{
			spread = 0.05f;
			hipFire = false;
			firePosition = sight;
		}
		float num = Random.Range((0f - spread) * multiplier, spread * multiplier);
		float num2 = Random.Range(spread * multiplier, (0f - spread) * multiplier);
		Vector3 direction = firePosition.forward + firePosition.right * num + firePosition.transform.up * num2;
		RaycastHit hitInfo;
		if (!Physics.Raycast(firePosition.position, direction, out hitInfo, weaponRange, layerMask) || !(hitInfo.collider != null))
		{
			return;
		}
		decalPosition = hitInfo.point;
		decalRotation = hitInfo.normal;
		decalParent = hitInfo.collider.gameObject.transform;
		if (hitInfo.collider.tag == "Enemy")
		{
			Vector3 hitDirection = hitInfo.collider.transform.position - base.transform.position;
			hitDirection.y = 1f;
			hitDirection.Normalize();
			hitInfo.collider.GetComponent<EnemyHit>().Hit(itemData.damage, itemData.impactForce, hitDirection, hitInfo.point);
			if (hitInfo.collider.name == "Hips")
			{
				bloodParent = hitInfo.collider.transform.Find("Spine").transform;
			}
			else
			{
				bloodParent = hitInfo.collider.transform;
			}
			enemy = true;
			BloodDecal();
		}
		else
		{
			Object.Instantiate(impact, decalPosition, Quaternion.FromToRotation(Vector3.up, decalRotation));
			HitDecal();
		}
	}

	private void SlideKick()
	{
		foreach (SlideKick slideKick in slideKicks)
		{
			if (!itemData.pumpAction)
			{
				slideKick.ApplySlideKick();
			}
		}
	}

	private void Cartridge()
	{
		if (itemData.fireType != 0)
		{
			Object.Instantiate(itemData.cartridge, ejector.position, ejector.rotation).GetComponent<Cartridge>().Eject(0.2f, 1, audioManager);
		}
	}

	private void Ammo()
	{
		if (currentAmmo == 0f && !boltHasStriked)
		{
			if (playerData.secondaryActive)
			{
				foreach (SlideKick slideKick in slideKicks)
				{
					slideKick.ApplyEmpty();
					Invoke("Empty", 0.1f);
				}
			}
			playerData.isFiring = false;
			playerData.magazineEmpty = true;
			boltHasStriked = true;
		}
		if (currentAmmo == ammoPerMag)
		{
			playerData.magazineFull = true;
		}
		else
		{
			playerData.magazineFull = false;
		}
	}

	private void Empty()
	{
		audioManager.PlayWeaponSound(itemData.empty);
	}

	private void BloodDecal()
	{
		if (spatter)
		{
			GameObject obj = Object.Instantiate(spatterDecal, decalPosition, Quaternion.LookRotation(decalRotation * -180f));
			float num = Random.Range(0.5f, 2f);
			obj.transform.localScale = new Vector3(num, num, 0.5f);
			obj.transform.SetParent(bloodParent);
			float num2 = Random.Range(0f, 360f);
			obj.transform.Rotate(Vector3.forward * num2);
		}
		else
		{
			decalRotation = firePosition.forward;
			GameObject obj2 = Object.Instantiate(bloodDecal, decalPosition, Quaternion.LookRotation(decalRotation));
			float num3 = Random.Range(0.025f, 0.25f);
			obj2.transform.localScale = new Vector3(num3, num3, 0.5f);
			obj2.transform.SetParent(bloodParent);
			float num4 = Random.Range(0f, 360f);
			obj2.transform.Rotate(Vector3.forward * num4);
			Object.Instantiate(bloodPuff, decalPosition, Quaternion.FromToRotation(Vector3.up, decalRotation));
		}
	}

	private void HitDecal()
	{
		GameObject obj = Object.Instantiate(decal, decalPosition, Quaternion.LookRotation(decalRotation * -180f));
		float num = Random.Range(0.1f, 0.2f);
		obj.transform.localScale = new Vector3(num, num, 0.05f);
		obj.transform.SetParent(decalParent);
		float num2 = Random.Range(0f, 360f);
		obj.transform.Rotate(Vector3.forward * num2);
		Material material = obj.GetComponent<MeshRenderer>().material;
		float[] obj2 = new float[2] { 0f, 0.5f };
		int num3 = Random.Range(0, 2);
		float x = obj2[num3];
		float[] obj3 = new float[2] { 0f, 0.5f };
		int num4 = Random.Range(0, 2);
		float y = obj3[num4];
		Vector2 value = new Vector2(x, y);
		material.SetTextureOffset("_MainTex", value);
	}

	public void CalculateAmmo()
	{
		magazinePercentage = currentAmmo / ammoPerMag * 100f;
		if (magazinePercentage == 100f)
		{
			UIManager.magazine.text = "FULL";
		}
		else if (magazinePercentage >= 80f && magazinePercentage < 100f)
		{
			UIManager.magazine.text = "~ 4/4";
		}
		else if (magazinePercentage >= 60f && magazinePercentage < 80f)
		{
			UIManager.magazine.text = "~ 3/4";
		}
		else if (magazinePercentage >= 40f && magazinePercentage < 60f)
		{
			UIManager.magazine.text = "~ 1/2";
		}
		else if (magazinePercentage >= 1f && magazinePercentage < 40f)
		{
			UIManager.magazine.text = "~ 1/4";
		}
		else
		{
			UIManager.magazine.text = "EMPTY";
		}
		UIManager.UpdateWeaponUI();
	}

	private void UpdatePenetration()
	{
		Ray ray = new Ray(firePosition.position, firePosition.forward);
		RaycastHit hitInfo;
		if (Physics.Raycast(ray, out hitInfo))
		{
			impactPoint = hitInfo.point;
			Ray ray2 = new Ray(hitInfo.point + ray.direction * penetrationAmount, -ray.direction);
			RaycastHit hitInfo2;
			if (hitInfo.collider.Raycast(ray2, out hitInfo2, penetrationAmount))
			{
				decalPosition = hitInfo.point;
				decalRotation = hitInfo.normal;
				decalParent = hitInfo.collider.gameObject.transform;
				if (!enemy)
				{
					HitDecal();
				}
				penetrationPoint = hitInfo2.point;
				endPoint = firePosition.position + firePosition.position * 100f;
				if (enemy)
				{
					BloodDecal();
				}
				decalPosition = hitInfo2.point;
				decalRotation = hitInfo2.normal;
				decalParent = hitInfo.collider.gameObject.transform;
				if (!enemy)
				{
					HitDecal();
				}
				if (enemy)
				{
					BloodDecal();
				}
				exitPosition = hitInfo2.point;
				PenetrationRay();
			}
			else
			{
				endPoint = impactPoint.Value + ray.direction * penetrationAmount;
				penetrationPoint = endPoint;
			}
		}
		else
		{
			endPoint = firePosition.position + firePosition.position * 100f;
			penetrationPoint = null;
			impactPoint = null;
		}
		if (!penetrationPoint.HasValue || !impactPoint.HasValue)
		{
			Debug.DrawLine(firePosition.position, endPoint, Color.green);
			return;
		}
		Debug.DrawLine(firePosition.position, impactPoint.Value, Color.green, 8f);
		Debug.DrawLine(impactPoint.Value, penetrationPoint.Value, Color.red, 8f);
	}

	private void PenetrationRay()
	{
		RaycastHit hitInfo;
		if (Physics.Raycast(new Ray(exitPosition, firePosition.forward), out hitInfo) && hitInfo.collider != null)
		{
			Debug.Log(hitInfo.distance);
			decalPosition = hitInfo.point;
			decalRotation = hitInfo.normal;
			decalParent = hitInfo.collider.gameObject.transform;
			HitDecal();
			if (hitInfo.collider.tag != "Enemy" && enemy)
			{
				bloodParent = decalParent;
				if (hitInfo.distance < 2f)
				{
					spatter = true;
					BloodDecal();
					spatter = false;
				}
				enemy = false;
			}
		}
		Debug.DrawLine(exitPosition, hitInfo.point, Color.yellow, 8f);
	}

	private void MuzzleFlash()
	{
		if (playerData.isNight)
		{
			if (itemData.hasSuppressor)
			{
				lightFPS.intensity = 0.5f;
				lightWorld.intensity = 0.5f;
			}
			else
			{
				lightFPS.intensity = 1f;
				lightWorld.intensity = 1f;
			}
		}
		else if (itemData.hasSuppressor)
		{
			lightFPS.intensity = 0.1f;
			lightWorld.intensity = 0.1f;
		}
		else
		{
			lightFPS.intensity = 0.2f;
			lightWorld.intensity = 0.2f;
		}
		lightFPS.enabled = true;
		lightWorld.enabled = true;
	}

	private void ResetFlash()
	{
		lightFPS.enabled = false;
		lightWorld.enabled = false;
	}

	private void Laser()
	{
		if (!playerData.laserActive)
		{
			laserRenderer.enabled = false;
			return;
		}
		laserRenderer.enabled = true;
		RaycastHit hitInfo;
		if (!playerData.primaryActive && !playerData.secondaryActive)
		{
			if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hitInfo, 100f, layerMask))
			{
				if (hitInfo.collider != null)
				{
					laserRenderer.enabled = true;
					float num = 0.25f;
					Vector3 position = hitInfo.point - mainCamera.transform.forward.normalized * num;
					laser.transform.position = position;
				}
			}
			else
			{
				laserRenderer.enabled = false;
			}
		}
		if (!playerData.primaryActive && !playerData.secondaryActive)
		{
			return;
		}
		if (Physics.Raycast(hip.transform.position, sight.forward, out hitInfo, 100f, layerMask))
		{
			if (hitInfo.collider != null)
			{
				laserRenderer.enabled = true;
				float num2 = 0.25f;
				Vector3 position2 = hitInfo.point - sight.forward.normalized * num2;
				laser.transform.position = position2;
			}
		}
		else
		{
			laserRenderer.enabled = false;
		}
	}
}
