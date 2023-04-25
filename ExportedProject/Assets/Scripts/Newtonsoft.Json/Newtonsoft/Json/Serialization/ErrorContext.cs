using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	[NullableContext(1)]
	[Nullable(0)]
	public class ErrorContext
	{
		[CompilerGenerated]
		private readonly Exception _003CError_003Ek__BackingField;

		[Nullable(2)]
		[CompilerGenerated]
		private readonly object _003COriginalObject_003Ek__BackingField;

		[Nullable(2)]
		[CompilerGenerated]
		private readonly object _003CMember_003Ek__BackingField;

		[CompilerGenerated]
		private readonly string _003CPath_003Ek__BackingField;

		internal bool Traced { get; set; }

		public Exception Error
		{
			[CompilerGenerated]
			get
			{
				return _003CError_003Ek__BackingField;
			}
		}

		[Nullable(2)]
		public object OriginalObject
		{
			[NullableContext(2)]
			[CompilerGenerated]
			get
			{
				return _003COriginalObject_003Ek__BackingField;
			}
		}

		[Nullable(2)]
		public object Member
		{
			[NullableContext(2)]
			[CompilerGenerated]
			get
			{
				return _003CMember_003Ek__BackingField;
			}
		}

		public string Path
		{
			[CompilerGenerated]
			get
			{
				return _003CPath_003Ek__BackingField;
			}
		}

		public bool Handled { get; set; }

		internal ErrorContext([Nullable(2)] object originalObject, [Nullable(2)] object member, string path, Exception error)
		{
			_003COriginalObject_003Ek__BackingField = originalObject;
			_003CMember_003Ek__BackingField = member;
			_003CError_003Ek__BackingField = error;
			_003CPath_003Ek__BackingField = path;
		}
	}
}
