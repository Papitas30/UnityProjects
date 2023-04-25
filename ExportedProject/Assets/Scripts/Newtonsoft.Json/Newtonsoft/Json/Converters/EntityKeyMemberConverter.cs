using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	[NullableContext(1)]
	[Nullable(0)]
	public class EntityKeyMemberConverter : JsonConverter
	{
		private const string EntityKeyMemberFullTypeName = "System.Data.EntityKeyMember";

		private const string KeyPropertyName = "Key";

		private const string TypePropertyName = "Type";

		private const string ValuePropertyName = "Value";

		[Nullable(2)]
		private static ReflectionObject _reflectionObject;

		public override void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			EnsureReflectionObject(value.GetType());
			DefaultContractResolver defaultContractResolver = serializer.ContractResolver as DefaultContractResolver;
			string value2 = (string)_reflectionObject.GetValue(value, "Key");
			object value3 = _reflectionObject.GetValue(value, "Value");
			Type type = ((value3 != null) ? value3.GetType() : null);
			writer.WriteStartObject();
			writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName("Key") : "Key");
			writer.WriteValue(value2);
			writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName("Type") : "Type");
			writer.WriteValue(((object)type != null) ? type.FullName : null);
			writer.WritePropertyName((defaultContractResolver != null) ? defaultContractResolver.GetResolvedPropertyName("Value") : "Value");
			if (type != null)
			{
				string s;
				if (JsonSerializerInternalWriter.TryConvertToString(value3, type, out s))
				{
					writer.WriteValue(s);
				}
				else
				{
					writer.WriteValue(value3);
				}
			}
			else
			{
				writer.WriteNull();
			}
			writer.WriteEndObject();
		}

		private static void ReadAndAssertProperty(JsonReader reader, string propertyName)
		{
			reader.ReadAndAssert();
			if (reader.TokenType == JsonToken.PropertyName)
			{
				object value = reader.Value;
				if (string.Equals((value != null) ? value.ToString() : null, propertyName, StringComparison.OrdinalIgnoreCase))
				{
					return;
				}
			}
			throw new JsonSerializationException("Expected JSON property '{0}'.".FormatWith(CultureInfo.InvariantCulture, propertyName));
		}

		[return: Nullable(2)]
		public override object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer)
		{
			EnsureReflectionObject(objectType);
			object obj = _reflectionObject.Creator();
			ReadAndAssertProperty(reader, "Key");
			reader.ReadAndAssert();
			ReflectionObject reflectionObject = _reflectionObject;
			object value = reader.Value;
			reflectionObject.SetValue(obj, "Key", (value != null) ? value.ToString() : null);
			ReadAndAssertProperty(reader, "Type");
			reader.ReadAndAssert();
			object value2 = reader.Value;
			Type type = Type.GetType((value2 != null) ? value2.ToString() : null);
			ReadAndAssertProperty(reader, "Value");
			reader.ReadAndAssert();
			_reflectionObject.SetValue(obj, "Value", serializer.Deserialize(reader, type));
			reader.ReadAndAssert();
			return obj;
		}

		private static void EnsureReflectionObject(Type objectType)
		{
			if (_reflectionObject == null)
			{
				_reflectionObject = ReflectionObject.Create(objectType, "Key", "Value");
			}
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType.AssignableToTypeName("System.Data.EntityKeyMember", false);
		}
	}
}
