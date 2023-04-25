using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class GunSystem : MonoBehaviour
{
    //Gun stats
    public int damage;
    public float reloadTime, timeBetweenShots, burstShotTime;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold, readyToShoot;
    int bulletsLeft, bulletsShot;

    //bools 
    bool shooting, reloading;

    //Reference

    public Transform muzzleFlashPoint;
    public Transform attackPoint;
    private RaycastHit rayHit;

    public LayerMask whatIsEnemy;
    public Recoil recoil;

    //Graphics
    public GameObject muzzleFlash;
    public TextMeshProUGUI TMP;
    [SerializeField] private DecalDestroyer decalPrefab;
    [HideInInspector] private IObjectPool<DecalDestroyer> decalPool;
    private void Start()
    {
        decalPool = new ObjectPool<DecalDestroyer>(CreateDecal, OnGetDecal, OnReleaseDecal, OnDestroyDecal, maxSize: 30);
    }
    private void Update()
    {
        MyInput();

        TMP.text = bulletsLeft + " / " + magazineSize;
    }

    private void MyInput()
    {

        if (allowButtonHold)
        {
            shooting = Input.GetMouseButton(0);
            recoil.oneTap = false;
        }
        else
        {
            shooting = Input.GetMouseButtonDown(0);
            recoil.oneTap = true;
        }

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        if (bulletsLeft == 0 && !reloading && !recoil.firing) Reload();

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();

        }

        if (Input.GetMouseButtonUp(0) && !recoil.oneTap)
        {
            recoil.firing = false;
        }

    }

    void Shoot()
    {
        recoil.firing = true;
        recoil.Fire();

        //RayCast
        if (Physics.Raycast(attackPoint.position, attackPoint.forward, out rayHit))
        {

            decalPool?.Get();


            Vector3 forward = attackPoint.forward * 20;
            Debug.DrawRay(attackPoint.position, forward, Color.green, 10);

            if (rayHit.collider.CompareTag("Enemy"))
            {

                rayHit.collider.GetComponent<EnemyAI>().TakeDamage(damage);//Gets the EnemyAI script from the gameobject the ray hits

            }

        }

        //Graphics

        bulletsLeft--;
        bulletsShot--;
        readyToShoot = false;

        Invoke(nameof(ResetShot), timeBetweenShots);
        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke(nameof(Shoot), burstShotTime);
    }

    private void ResetShot()
    {
        readyToShoot = true;
        recoil.firing = false;
    }

    private void Reload()
    {
        recoil.firing = false;
        reloading = true;
        Invoke(nameof(ReloadFinished), reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
    private DecalDestroyer CreateDecal()
    {
        DecalDestroyer decal = Instantiate(decalPrefab, rayHit.point, Quaternion.LookRotation(rayHit.normal));

        decal.transform.SetParent(rayHit.transform);
        decal.SetPool(decalPool);
        return decal;
    }
    private void OnGetDecal(DecalDestroyer decal)
    {
        decal.gameObject.SetActive(true);

        decal.transform.SetPositionAndRotation(rayHit.point, Quaternion.LookRotation(rayHit.normal));
        decal.transform.SetParent(rayHit.transform);
    }

    private void OnReleaseDecal(DecalDestroyer decal)
    {
        decal.gameObject.SetActive(false);
    }

    private void OnDestroyDecal(DecalDestroyer decal)
    {
        Destroy(decal.gameObject);
    }
}
