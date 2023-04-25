using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace OccaSoftware.Buto
{
	[ExecuteAlways]
	public sealed class FogDensityMask : ButoPlaceableObject
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass6_0
		{
			public Vector3 c;

			internal float _003CSortByDistance_003Eb__0(FogDensityMask x)
			{
				return x.GetSqrMagnitude(c);
			}
		}

		[SerializeField]
		[Min(0f)]
		private float densityMultiplier = 1f;

		[SerializeField]
		[Min(0f)]
		private float radius = 10f;

		private static List<FogDensityMask> fogVolumes = new List<FogDensityMask>();

		public float DensityMultiplier
		{
			get
			{
				return densityMultiplier;
			}
		}

		public float Radius
		{
			get
			{
				return radius;
			}
		}

		public static List<FogDensityMask> FogVolumes
		{
			get
			{
				return fogVolumes;
			}
		}

		public static void SortByDistance(Vector3 c)
		{
			_003C_003Ec__DisplayClass6_0 _003C_003Ec__DisplayClass6_ = new _003C_003Ec__DisplayClass6_0();
			_003C_003Ec__DisplayClass6_.c = c;
			fogVolumes = fogVolumes.OrderBy(_003C_003Ec__DisplayClass6_._003CSortByDistance_003Eb__0).ToList();
		}

		protected override void Reset()
		{
			ButoCommon.CheckMaxFogVolumeCount(FogVolumes.Count, this);
		}

		protected override void OnEnable()
		{
			fogVolumes.Add(this);
		}

		protected override void OnDisable()
		{
			fogVolumes.Remove(this);
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.black;
			Gizmos.DrawWireSphere(base.transform.position, Radius);
		}
	}
}
