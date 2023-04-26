using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	internal class DoesNotReturnIfAttribute : Attribute
	{
		[CompilerGenerated]
		private readonly bool _003CParameterValue_003Ek__BackingField;

		public bool ParameterValue
		{
			[CompilerGenerated]
			get
			{
				return _003CParameterValue_003Ek__BackingField;
			}
		}

		public DoesNotReturnIfAttribute(bool parameterValue)
		{
			_003CParameterValue_003Ek__BackingField = parameterValue;
		}
	}
}
