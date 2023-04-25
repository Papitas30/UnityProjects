using UnityEngine;

public class FighterJet : MonoBehaviour
{
	private Vector3 startRotation;

	public bool canFly;

	public bool flyover;

	public bool hasPassed;

	public float speed;

	public float distanceToCenter;

	public float distanceThreshold;

	public AudioSource audioSource;

	public float timer;

	private void Start()
	{
		if ((float)Random.Range(0, 2) == 1f)
		{
			canFly = true;
			timer = Random.Range(60, 120);
		}
	}

	private void Update()
	{
		if (canFly && !flyover)
		{
			timer -= Time.deltaTime;
			if (timer <= 0f)
			{
				flyover = true;
				hasPassed = false;
				float z = Random.Range(-1000, 1000);
				base.transform.position = new Vector3(-1000f, 200f, z);
				base.transform.LookAt(new Vector3(0f, 200f, 0f));
				flyover = true;
			}
		}
		if (flyover)
		{
			base.transform.Translate(Vector3.forward * Time.deltaTime * speed);
			distanceToCenter = Vector3.Distance(base.transform.position, new Vector3(0f, 200f, 0f));
			if (distanceToCenter < distanceThreshold && !hasPassed)
			{
				audioSource.Play();
				hasPassed = true;
			}
			if (distanceToCenter > 2000f)
			{
				Object.Destroy(base.gameObject);
			}
		}
	}
}
