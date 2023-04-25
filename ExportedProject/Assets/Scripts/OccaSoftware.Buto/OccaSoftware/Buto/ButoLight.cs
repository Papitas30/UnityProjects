using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace OccaSoftware.Buto
{
	[ExecuteAlways]
	public sealed class ButoLight : ButoPlaceableObject
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass8_0
		{
			public Vector3 c;

			internal float _003CSortByDistance_003Eb__0(ButoLight x)
			{
				return x.GetSqrMagnitude(c);
			}
		}

		[SerializeField]
		private bool inheritDataFromLightComponent;

		[SerializeField]
		private Light lightComponent;

		[SerializeField]
		[ColorUsage(false, false)]
		private Color lightColor = Color.white;

		[SerializeField]
		[Min(0f)]
		private float lightIntensity = 10f;

		private static List<ButoLight> _Lights = new List<ButoLight>();

		public Vector4 LightColor
		{
			get
			{
				if (inheritDataFromLightComponent && lightComponent != null)
				{
					return lightComponent.color;
				}
				return lightColor;
			}
		}

		public float LightIntensity
		{
			get
			{
				if (inheritDataFromLightComponent && lightComponent != null)
				{
					return lightComponent.intensity;
				}
				return lightIntensity;
			}
		}

		public static List<ButoLight> Lights
		{
			get
			{
				return _Lights;
			}
		}

		public static void SortByDistance(Vector3 c)
		{
			_003C_003Ec__DisplayClass8_0 _003C_003Ec__DisplayClass8_ = new _003C_003Ec__DisplayClass8_0();
			_003C_003Ec__DisplayClass8_.c = c;
			_Lights = _Lights.OrderBy(_003C_003Ec__DisplayClass8_._003CSortByDistance_003Eb__0).ToList();
		}

		protected override void Reset()
		{
			ButoCommon.CheckMaxLightCount(Lights.Count, this);
		}

		private void OnValidate()
		{
			lightComponent = GetComponent<Light>();
		}

		protected override void OnEnable()
		{
			lightComponent = GetComponent<Light>();
			_Lights.Add(this);
		}

		protected override void OnDisable()
		{
			_Lights.Remove(this);
		}

		public void CheckForLight()
		{
			lightComponent = GetComponent<Light>();
		}

		public void SetInheritance(bool state)
		{
			inheritDataFromLightComponent = state;
		}

		private void OnDrawGizmosSelected()
		{
			if (!inheritDataFromLightComponent)
			{
				Gizmos.color = LightColor;
				Gizmos.DrawWireSphere(base.transform.position, lightIntensity);
			}
		}
	}
}
