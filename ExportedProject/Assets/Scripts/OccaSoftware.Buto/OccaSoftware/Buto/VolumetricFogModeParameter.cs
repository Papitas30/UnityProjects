using System;
using UnityEngine.Rendering;

namespace OccaSoftware.Buto
{
	[Serializable]
	public sealed class VolumetricFogModeParameter : VolumeParameter<VolumetricFogMode>
	{
		public VolumetricFogModeParameter(VolumetricFogMode value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
