using UnityEngine;

public class BulletPen : MonoBehaviour
{
	public float penetrationAmount;

	public Vector3 endPoint;

	public Vector3? penetrationPoint;

	public Vector3? impactPoint;

	[Header("Bullet Decal")]
	public GameObject decal;

	private Vector3 decalPosition;

	private Vector3 decalRotation;

	private Transform decalParent;

	public Vector3 exitPosition;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.K))
		{
			UpdatePenetration();
		}
	}

	private void UpdatePenetration()
	{
		Ray ray = new Ray(base.transform.position, base.transform.forward);
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
				HitDecal();
				penetrationPoint = hitInfo2.point;
				endPoint = base.transform.position + base.transform.forward * 100f;
				decalPosition = hitInfo2.point;
				decalRotation = hitInfo2.normal;
				decalParent = hitInfo.collider.gameObject.transform;
				HitDecal();
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
			endPoint = base.transform.position + base.transform.forward * 100f;
			penetrationPoint = null;
			impactPoint = null;
		}
		if (!penetrationPoint.HasValue || !impactPoint.HasValue)
		{
			Debug.DrawLine(base.transform.position, endPoint, Color.green);
			return;
		}
		Debug.DrawLine(base.transform.position, impactPoint.Value, Color.green, 10f);
		Debug.DrawLine(impactPoint.Value, penetrationPoint.Value, Color.red, 10f);
	}

	private void PenetrationRay()
	{
		RaycastHit hitInfo;
		if (Physics.Raycast(new Ray(exitPosition, base.transform.forward), out hitInfo) && hitInfo.collider != null)
		{
			decalPosition = hitInfo.point;
			decalRotation = hitInfo.normal;
			decalParent = hitInfo.collider.gameObject.transform;
			HitDecal();
		}
		Debug.DrawLine(exitPosition, hitInfo.point, Color.yellow, 10f);
	}

	private void HitDecal()
	{
		GameObject obj = Object.Instantiate(decal, decalPosition, Quaternion.LookRotation(decalRotation * -180f));
		float num = Random.Range(0.1f, 0.2f);
		obj.transform.localScale = new Vector3(num, num, 0.1f);
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
}
