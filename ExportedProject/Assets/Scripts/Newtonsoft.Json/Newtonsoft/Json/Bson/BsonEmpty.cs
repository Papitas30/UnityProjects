using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Bson
{
	internal class BsonEmpty : BsonToken
	{
		public static readonly BsonToken Null = new BsonEmpty(BsonType.Null);

		public static readonly BsonToken Undefined = new BsonEmpty(BsonType.Undefined);

		[CompilerGenerated]
		private readonly BsonType _003CType_003Ek__BackingField;

		public override BsonType Type
		{
			[CompilerGenerated]
			get
			{
				return _003CType_003Ek__BackingField;
			}
		}

		private BsonEmpty(BsonType type)
		{
			_003CType_003Ek__BackingField = type;
		}
	}
}
