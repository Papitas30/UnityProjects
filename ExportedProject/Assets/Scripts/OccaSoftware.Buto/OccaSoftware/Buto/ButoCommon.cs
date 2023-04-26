using UnityEngine;

namespace OccaSoftware.Buto
{
	internal static class ButoCommon
	{
		internal static readonly int _MAXLIGHTCOUNT = 8;

		internal static readonly int _MAXVOLUMECOUNT = 8;

		internal static void CheckMaxLightCount(int c, Object o)
		{
			if (c > _MAXLIGHTCOUNT)
			{
				Debug.LogWarning(string.Format("You have more than {0} Buto Lights in scene. Buto will cull all but the nearest {1} lights, but this will incur additional overhead.", _MAXLIGHTCOUNT, _MAXLIGHTCOUNT), o);
			}
		}

		internal static void CheckMaxFogVolumeCount(int c, Object o)
		{
			if (c > _MAXVOLUMECOUNT)
			{
				Debug.LogWarning(string.Format("You have more than {0} Buto Fog Volumes in scene. Buto will cull all but the nearest {1} fog volumes, but this will incur additional overhead.", _MAXVOLUMECOUNT, _MAXVOLUMECOUNT), o);
			}
		}
	}
}
