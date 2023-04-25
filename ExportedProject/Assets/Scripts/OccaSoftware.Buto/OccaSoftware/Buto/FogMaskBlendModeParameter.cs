using System;
using UnityEngine.Rendering;

namespace OccaSoftware.Buto
{
	[Serializable]
	public sealed class FogMaskBlendModeParameter : VolumeParameter<FogMaskBlendMode>
	{
		public FogMaskBlendModeParameter(FogMaskBlendMode value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
