using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	[NullableContext(1)]
	[Nullable(0)]
	internal class TypeInformation
	{
		[CompilerGenerated]
		private readonly Type _003CType_003Ek__BackingField;

		[CompilerGenerated]
		private readonly PrimitiveTypeCode _003CTypeCode_003Ek__BackingField;

		public Type Type
		{
			[CompilerGenerated]
			get
			{
				return _003CType_003Ek__BackingField;
			}
		}

		public PrimitiveTypeCode TypeCode
		{
			[CompilerGenerated]
			get
			{
				return _003CTypeCode_003Ek__BackingField;
			}
		}

		public TypeInformation(Type type, PrimitiveTypeCode typeCode)
		{
			_003CType_003Ek__BackingField = type;
			_003CTypeCode_003Ek__BackingField = typeCode;
		}
	}
}
