using UnityEngine;

namespace OccaSoftware.Buto.Demo.Runtime
{
	[ExecuteAlways]
	public class LightIntensityByRotation : MonoBehaviour
	{
		private Light l;

		[SerializeField]
		private float intensity = 1f;

		private void Update()
		{
			if (l == null)
			{
				l = GetComponent<Light>();
			}
			float num = base.transform.rotation.eulerAngles.x;
			if (Mathf.Abs(num) > 90f)
			{
				num = 0f;
			}
			num = Mathf.Clamp(num, 0f, 90f);
			num /= 90f;
			l.intensity = Mathf.Lerp(0.001f, intensity, num);
		}
	}
}
