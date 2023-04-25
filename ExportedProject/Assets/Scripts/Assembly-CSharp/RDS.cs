using System.Collections;
using UnityEngine;

public class RDS : MonoBehaviour
{
	public Material reticle;

	public void StartShake(float duration, float magnitude)
	{
		StartCoroutine(Shake(duration, magnitude));
	}

	public IEnumerator Shake(float duration, float magnitude)
	{
		Vector2 originalPos = new Vector2(0f, 0f);
		float elapsedTime = 0f;
		while (elapsedTime < duration)
		{
			float x = Random.Range(-0.5f, 0.5f) * magnitude;
			float y = Random.Range(-0.5f, 0.5f) * magnitude;
			reticle.SetVector("_ReticlePosition", new Vector2(x, y));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		reticle.SetVector("_ReticlePosition", originalPos);
	}
}
