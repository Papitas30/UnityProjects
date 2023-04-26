using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	[NullableContext(1)]
	[Nullable(0)]
	public class ErrorEventArgs : EventArgs
	{
		[Nullable(2)]
		[CompilerGenerated]
		private readonly object _003CCurrentObject_003Ek__BackingField;

		[CompilerGenerated]
		private readonly ErrorContext _003CErrorContext_003Ek__BackingField;

		[Nullable(2)]
		public object CurrentObject
		{
			[NullableContext(2)]
			[CompilerGenerated]
			get
			{
				return _003CCurrentObject_003Ek__BackingField;
			}
		}

		public ErrorContext ErrorContext
		{
			[CompilerGenerated]
			get
			{
				return _003CErrorContext_003Ek__BackingField;
			}
		}

		public ErrorEventArgs([Nullable(2)] object currentObject, ErrorContext errorContext)
		{
			_003CCurrentObject_003Ek__BackingField = currentObject;
			_003CErrorContext_003Ek__BackingField = errorContext;
		}
	}
}
