using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Parameter, AllowMultiple = false)]
	public sealed class JsonConverterAttribute : Attribute
	{
		private readonly Type _converterType;

		[Nullable(new byte[] { 2, 1 })]
		[CompilerGenerated]
		private readonly object[] _003CConverterParameters_003Ek__BackingField;

		public Type ConverterType
		{
			get
			{
				return _converterType;
			}
		}

		[Nullable(new byte[] { 2, 1 })]
		public object[] ConverterParameters
		{
			[CompilerGenerated]
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return _003CConverterParameters_003Ek__BackingField;
			}
		}

		public JsonConverterAttribute(Type converterType)
		{
			if (converterType == null)
			{
				throw new ArgumentNullException("converterType");
			}
			_converterType = converterType;
		}

		public JsonConverterAttribute(Type converterType, params object[] converterParameters)
			: this(converterType)
		{
			_003CConverterParameters_003Ek__BackingField = converterParameters;
		}
	}
}
