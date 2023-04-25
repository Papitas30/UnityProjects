using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
	internal sealed class NotNullWhenAttribute : Attribute
	{
		[CompilerGenerated]
		private readonly bool _003CReturnValue_003Ek__BackingField;

		public bool ReturnValue
		{
			[CompilerGenerated]
			get
			{
				return _003CReturnValue_003Ek__BackingField;
			}
		}

		public NotNullWhenAttribute(bool returnValue)
		{
			_003CReturnValue_003Ek__BackingField = returnValue;
		}
	}
}
