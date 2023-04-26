using UnityEngine;

public class Ocular : MonoBehaviour
{
	public PlayerData playerData;

	public Material ocular;

	public float lerpSpeed;

	private float opacity;

	private float reflection;

	private void Update()
	{
		if (playerData.isAiming)
		{
			opacity = Mathf.Lerp(opacity, 0.25f, lerpSpeed * Time.deltaTime);
			reflection = Mathf.Lerp(reflection, 0.5f, lerpSpeed * Time.deltaTime);
		}
		else
		{
			opacity = Mathf.Lerp(opacity, 1f, lerpSpeed * Time.deltaTime);
			reflection = Mathf.Lerp(reflection, 0.1f, lerpSpeed * Time.deltaTime);
		}
		ocular.SetFloat("_Opacity", opacity);
		ocular.SetFloat("_Reflection", reflection);
	}
}
