using UnityEngine;
using UnityEngine.UI;

public class ImageFill : MonoBehaviour
{
	private Image image;

	public FloatData floatData;

	public bool useColors;

	public bool invertedColors;

	private void Start()
	{
		image = GetComponent<Image>();
	}

	private void Update()
	{
		image.fillAmount = Mathf.Clamp01(Mathf.InverseLerp(0f, 100f, floatData.Value));
		if (!useColors)
		{
			return;
		}
		if (invertedColors)
		{
			if (floatData.Value < 25f)
			{
				image.color = Color.red;
			}
			if (floatData.Value >= 25f && floatData.Value < 50f)
			{
				image.color = Color.yellow;
			}
			if (floatData.Value >= 50f)
			{
				image.color = Color.green;
			}
		}
		else
		{
			if (floatData.Value < 50f)
			{
				image.color = Color.green;
			}
			if (floatData.Value >= 50f && floatData.Value < 90f)
			{
				image.color = Color.yellow;
			}
			if (floatData.Value >= 90f)
			{
				image.color = Color.red;
			}
		}
	}
}
