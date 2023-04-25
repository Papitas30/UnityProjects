using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
	public enum NoiseAxis
	{
		X = 0,
		Y = 1
	}

	[Header("Axis")]
	public NoiseAxis noiseAxis;

	[Header("Noise Settings")]
	[MinMaxSlider(-10f, 10f)]
	[SerializeField]
	private Vector2 range;

	[SerializeField]
	private float speed;

	private float seed;

	private void Start()
	{
		if (noiseAxis == NoiseAxis.X)
		{
			StartCoroutine(ModulateXPosition());
		}
		else
		{
			StartCoroutine(ModulateYPosition());
		}
		seed = Random.Range(1, 100);
	}

	private IEnumerator ModulateXPosition()
	{
		Vector3 basePosition = base.transform.localPosition;
		while (true)
		{
			float t = Mathf.PerlinNoise(Time.time * speed, seed);
			float x = Mathf.Lerp(range.x, range.y, t);
			base.transform.localPosition = basePosition + new Vector3(x, 0f, 0f);
			yield return null;
		}
	}

	private IEnumerator ModulateYPosition()
	{
		Vector3 basePosition = base.transform.localPosition;
		while (true)
		{
			float t = Mathf.PerlinNoise(Time.time * speed, seed);
			float y = Mathf.Lerp(range.x, range.y, t);
			base.transform.localPosition = basePosition + new Vector3(0f, y, 0f);
			yield return null;
		}
	}
}
