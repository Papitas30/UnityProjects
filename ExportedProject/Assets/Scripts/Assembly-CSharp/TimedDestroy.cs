using UnityEngine;

public class TimedDestroy : MonoBehaviour
{
	public float destroyTime = 5f;

	public bool lerpScale;

	private float time;

	private void Update()
	{
		time += Time.deltaTime;
		if (time > destroyTime)
		{
			if (lerpScale)
			{
				base.transform.localScale = Vector3.Lerp(base.transform.localScale, Vector3.zero, Time.deltaTime * 4f);
				Invoke("LateDestroy", 1f);
			}
			else
			{
				Object.Destroy(base.gameObject);
			}
		}
	}

	private void LateDestroy()
	{
		Object.Destroy(base.gameObject);
	}
}
