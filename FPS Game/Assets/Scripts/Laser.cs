using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform laserStart;
    public GunSway gunSwayer;
    private SpriteRenderer laserSprite;
    private RaycastHit rayHit;
    private void Start()
    {
        laserSprite = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {

            LaserRay();

        }
        else
            laserSprite.enabled = false;
    }
    private void LaserRay()
    {
        if (Physics.Raycast(laserStart.position, laserStart.forward, out rayHit))
        {
            laserSprite.enabled = true;
            Vector3 pos = rayHit.point + (rayHit.normal * 0.01f);

            transform.SetPositionAndRotation(pos + (gunSwayer.swayVector * 1.25f), Quaternion.LookRotation(rayHit.normal));
        }
        else
            laserSprite.enabled = false;
    }
}
