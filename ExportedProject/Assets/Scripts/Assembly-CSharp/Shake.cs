using System.Collections;
using UnityEngine;

public class Shake : MonoBehaviour
{
	public Transform target;

	public bool start;

	public AnimationCurve curve;

	public float duration = 1f;

	private void Update()
	{
		if (start)
		{
			start = false;
			StartCoroutine(Shaking());
		}
	}

	private IEnumerator Shaking()
	{
		Vector3 startPosition = target.position;
		float elapsedTime = 0f;
		while (elapsedTime < duration)
		{
			elapsedTime += Time.deltaTime;
			float num = curve.Evaluate(elapsedTime / duration);
			target.position = startPosition + Random.insideUnitSphere * num;
			yield return null;
		}
		target.position = startPosition;
	}
}
