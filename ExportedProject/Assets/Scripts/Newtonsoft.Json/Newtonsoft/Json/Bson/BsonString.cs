using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Bson
{
	internal class BsonString : BsonValue
	{
		[CompilerGenerated]
		private readonly bool _003CIncludeLength_003Ek__BackingField;

		public int ByteCount { get; set; }

		public bool IncludeLength
		{
			[CompilerGenerated]
			get
			{
				return _003CIncludeLength_003Ek__BackingField;
			}
		}

		public BsonString(object value, bool includeLength)
			: base(value, BsonType.String)
		{
			_003CIncludeLength_003Ek__BackingField = includeLength;
		}
	}
}
