using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public class JsonSchemaGenerator
	{
		private class TypeSchema
		{
			[CompilerGenerated]
			private readonly Type _003CType_003Ek__BackingField;

			[CompilerGenerated]
			private readonly JsonSchema _003CSchema_003Ek__BackingField;

			public Type Type
			{
				[CompilerGenerated]
				get
				{
					return _003CType_003Ek__BackingField;
				}
			}

			public JsonSchema Schema
			{
				[CompilerGenerated]
				get
				{
					return _003CSchema_003Ek__BackingField;
				}
			}

			public TypeSchema(Type type, JsonSchema schema)
			{
				ValidationUtils.ArgumentNotNull(type, "type");
				ValidationUtils.ArgumentNotNull(schema, "schema");
				_003CType_003Ek__BackingField = type;
				_003CSchema_003Ek__BackingField = schema;
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass23_0
		{
			public Type type;

			internal bool _003CGenerateInternal_003Eb__0(TypeSchema tc)
			{
				return tc.Type == type;
			}
		}

		private IContractResolver _contractResolver;

		private JsonSchemaResolver _resolver;

		private readonly IList<TypeSchema> _stack = new List<TypeSchema>();

		private JsonSchema _currentSchema;

		public UndefinedSchemaIdHandling UndefinedSchemaIdHandling { get; set; }

		public IContractResolver ContractResolver
		{
			get
			{
				if (_contractResolver == null)
				{
					return DefaultContractResolver.Instance;
				}
				return _contractResolver;
			}
			set
			{
				_contractResolver = value;
			}
		}

		private JsonSchema CurrentSchema
		{
			get
			{
				return _currentSchema;
			}
		}

		private void Push(TypeSchema typeSchema)
		{
			_currentSchema = typeSchema.Schema;
			_stack.Add(typeSchema);
			_resolver.LoadedSchemas.Add(typeSchema.Schema);
		}

		private TypeSchema Pop()
		{
			TypeSchema result = _stack[_stack.Count - 1];
			_stack.RemoveAt(_stack.Count - 1);
			TypeSchema typeSchema = _stack.LastOrDefault();
			if (typeSchema != null)
			{
				_currentSchema = typeSchema.Schema;
				return result;
			}
			_currentSchema = null;
			return result;
		}

		public JsonSchema Generate(Type type)
		{
			return Generate(type, new JsonSchemaResolver(), false);
		}

		public JsonSchema Generate(Type type, JsonSchemaResolver resolver)
		{
			return Generate(type, resolver, false);
		}

		public JsonSchema Generate(Type type, bool rootSchemaNullable)
		{
			return Generate(type, new JsonSchemaResolver(), rootSchemaNullable);
		}

		public JsonSchema Generate(Type type, JsonSchemaResolver resolver, bool rootSchemaNullable)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			ValidationUtils.ArgumentNotNull(resolver, "resolver");
			_resolver = resolver;
			return GenerateInternal(type, (!rootSchemaNullable) ? Required.Always : Required.Default, false);
		}

		private string GetTitle(Type type)
		{
			JsonContainerAttribute cachedAttribute = JsonTypeReflector.GetCachedAttribute<JsonContainerAttribute>(type);
			if (!StringUtils.IsNullOrEmpty((cachedAttribute != null) ? cachedAttribute.Title : null))
			{
				return cachedAttribute.Title;
			}
			return null;
		}

		private string GetDescription(Type type)
		{
			JsonContainerAttribute cachedAttribute = JsonTypeReflector.GetCachedAttribute<JsonContainerAttribute>(type);
			if (!StringUtils.IsNullOrEmpty((cachedAttribute != null) ? cachedAttribute.Description : null))
			{
				return cachedAttribute.Description;
			}
			DescriptionAttribute attribute = ReflectionUtils.GetAttribute<DescriptionAttribute>(type);
			if (attribute == null)
			{
				return null;
			}
			return attribute.Description;
		}

		private string GetTypeId(Type type, bool explicitOnly)
		{
			JsonContainerAttribute cachedAttribute = JsonTypeReflector.GetCachedAttribute<JsonContainerAttribute>(type);
			if (!StringUtils.IsNullOrEmpty((cachedAttribute != null) ? cachedAttribute.Id : null))
			{
				return cachedAttribute.Id;
			}
			if (explicitOnly)
			{
				return null;
			}
			switch (UndefinedSchemaIdHandling)
			{
			case UndefinedSchemaIdHandling.UseTypeName:
				return type.FullName;
			case UndefinedSchemaIdHandling.UseAssemblyQualifiedName:
				return type.AssemblyQualifiedName;
			default:
				return null;
			}
		}

		private JsonSchema GenerateInternal(Type type, Required valueRequired, bool required)
		{
			_003C_003Ec__DisplayClass23_0 _003C_003Ec__DisplayClass23_ = new _003C_003Ec__DisplayClass23_0();
			_003C_003Ec__DisplayClass23_.type = type;
			ValidationUtils.ArgumentNotNull(_003C_003Ec__DisplayClass23_.type, "type");
			string typeId = GetTypeId(_003C_003Ec__DisplayClass23_.type, false);
			string typeId2 = GetTypeId(_003C_003Ec__DisplayClass23_.type, true);
			if (!StringUtils.IsNullOrEmpty(typeId))
			{
				JsonSchema schema = _resolver.GetSchema(typeId);
				if (schema != null)
				{
					if (valueRequired != Required.Always && !HasFlag(schema.Type, JsonSchemaType.Null))
					{
						schema.Type |= JsonSchemaType.Null;
					}
					if (required && schema.Required != true)
					{
						schema.Required = true;
					}
					return schema;
				}
			}
			if (_stack.Any(_003C_003Ec__DisplayClass23_._003CGenerateInternal_003Eb__0))
			{
				throw new JsonException("Unresolved circular reference for type '{0}'. Explicitly define an Id for the type using a JsonObject/JsonArray attribute or automatically generate a type Id using the UndefinedSchemaIdHandling property.".FormatWith(CultureInfo.InvariantCulture, _003C_003Ec__DisplayClass23_.type));
			}
			JsonContract jsonContract = ContractResolver.ResolveContract(_003C_003Ec__DisplayClass23_.type);
			JsonConverter obj = jsonContract.Converter ?? jsonContract.InternalConverter;
			Push(new TypeSchema(_003C_003Ec__DisplayClass23_.type, new JsonSchema()));
			if (typeId2 != null)
			{
				CurrentSchema.Id = typeId2;
			}
			if (required)
			{
				CurrentSchema.Required = true;
			}
			CurrentSchema.Title = GetTitle(_003C_003Ec__DisplayClass23_.type);
			CurrentSchema.Description = GetDescription(_003C_003Ec__DisplayClass23_.type);
			if (obj != null)
			{
				CurrentSchema.Type = JsonSchemaType.Any;
			}
			else
			{
				switch (jsonContract.ContractType)
				{
				case JsonContractType.Object:
					CurrentSchema.Type = AddNullType(JsonSchemaType.Object, valueRequired);
					CurrentSchema.Id = GetTypeId(_003C_003Ec__DisplayClass23_.type, false);
					GenerateObjectSchema(_003C_003Ec__DisplayClass23_.type, (JsonObjectContract)jsonContract);
					break;
				case JsonContractType.Array:
				{
					CurrentSchema.Type = AddNullType(JsonSchemaType.Array, valueRequired);
					CurrentSchema.Id = GetTypeId(_003C_003Ec__DisplayClass23_.type, false);
					JsonArrayAttribute cachedAttribute = JsonTypeReflector.GetCachedAttribute<JsonArrayAttribute>(_003C_003Ec__DisplayClass23_.type);
					bool flag = cachedAttribute == null || cachedAttribute.AllowNullItems;
					Type collectionItemType = ReflectionUtils.GetCollectionItemType(_003C_003Ec__DisplayClass23_.type);
					if (collectionItemType != null)
					{
						CurrentSchema.Items = new List<JsonSchema>();
						CurrentSchema.Items.Add(GenerateInternal(collectionItemType, (!flag) ? Required.Always : Required.Default, false));
					}
					break;
				}
				case JsonContractType.Primitive:
					CurrentSchema.Type = GetJsonSchemaType(_003C_003Ec__DisplayClass23_.type, valueRequired);
					if (CurrentSchema.Type == JsonSchemaType.Integer && _003C_003Ec__DisplayClass23_.type.IsEnum() && !_003C_003Ec__DisplayClass23_.type.IsDefined(typeof(FlagsAttribute), true))
					{
						CurrentSchema.Enum = new List<JToken>();
						EnumInfo enumValuesAndNames = EnumUtils.GetEnumValuesAndNames(_003C_003Ec__DisplayClass23_.type);
						for (int i = 0; i < enumValuesAndNames.Names.Length; i++)
						{
							ulong value = enumValuesAndNames.Values[i];
							JToken item = JToken.FromObject(Enum.ToObject(_003C_003Ec__DisplayClass23_.type, value));
							CurrentSchema.Enum.Add(item);
						}
					}
					break;
				case JsonContractType.String:
				{
					JsonSchemaType value2 = ((!ReflectionUtils.IsNullable(jsonContract.UnderlyingType)) ? JsonSchemaType.String : AddNullType(JsonSchemaType.String, valueRequired));
					CurrentSchema.Type = value2;
					break;
				}
				case JsonContractType.Dictionary:
				{
					CurrentSchema.Type = AddNullType(JsonSchemaType.Object, valueRequired);
					Type keyType;
					Type valueType;
					ReflectionUtils.GetDictionaryKeyValueTypes(_003C_003Ec__DisplayClass23_.type, out keyType, out valueType);
					if (keyType != null && ContractResolver.ResolveContract(keyType).ContractType == JsonContractType.Primitive)
					{
						CurrentSchema.AdditionalProperties = GenerateInternal(valueType, Required.Default, false);
					}
					break;
				}
				case JsonContractType.Serializable:
					CurrentSchema.Type = AddNullType(JsonSchemaType.Object, valueRequired);
					CurrentSchema.Id = GetTypeId(_003C_003Ec__DisplayClass23_.type, false);
					GenerateISerializableContract(_003C_003Ec__DisplayClass23_.type, (JsonISerializableContract)jsonContract);
					break;
				case JsonContractType.Dynamic:
				case JsonContractType.Linq:
					CurrentSchema.Type = JsonSchemaType.Any;
					break;
				default:
					throw new JsonException("Unexpected contract type: {0}".FormatWith(CultureInfo.InvariantCulture, jsonContract));
				}
			}
			return Pop().Schema;
		}

		private JsonSchemaType AddNullType(JsonSchemaType type, Required valueRequired)
		{
			if (valueRequired != Required.Always)
			{
				return type | JsonSchemaType.Null;
			}
			return type;
		}

		private bool HasFlag(DefaultValueHandling value, DefaultValueHandling flag)
		{
			return (value & flag) == flag;
		}

		private void GenerateObjectSchema(Type type, JsonObjectContract contract)
		{
			CurrentSchema.Properties = new Dictionary<string, JsonSchema>();
			foreach (JsonProperty property in contract.Properties)
			{
				if (!property.Ignored)
				{
					bool flag = property.NullValueHandling == NullValueHandling.Ignore || HasFlag(property.DefaultValueHandling.GetValueOrDefault(), DefaultValueHandling.Ignore) || property.ShouldSerialize != null || property.GetIsSpecified != null;
					JsonSchema jsonSchema = GenerateInternal(property.PropertyType, property.Required, !flag);
					if (property.DefaultValue != null)
					{
						jsonSchema.Default = JToken.FromObject(property.DefaultValue);
					}
					CurrentSchema.Properties.Add(property.PropertyName, jsonSchema);
				}
			}
			if (type.IsSealed())
			{
				CurrentSchema.AllowAdditionalProperties = false;
			}
		}

		private void GenerateISerializableContract(Type type, JsonISerializableContract contract)
		{
			CurrentSchema.AllowAdditionalProperties = true;
		}

		internal static bool HasFlag(JsonSchemaType? value, JsonSchemaType flag)
		{
			if (!value.HasValue)
			{
				return true;
			}
			if ((value & flag) == flag)
			{
				return true;
			}
			if (flag == JsonSchemaType.Integer && ((uint?)value & 2u) == 2)
			{
				return true;
			}
			return false;
		}

		private JsonSchemaType GetJsonSchemaType(Type type, Required valueRequired)
		{
			JsonSchemaType jsonSchemaType = JsonSchemaType.None;
			if (valueRequired != Required.Always && ReflectionUtils.IsNullable(type))
			{
				jsonSchemaType = JsonSchemaType.Null;
				if (ReflectionUtils.IsNullableType(type))
				{
					type = Nullable.GetUnderlyingType(type);
				}
			}
			PrimitiveTypeCode typeCode = ConvertUtils.GetTypeCode(type);
			switch (typeCode)
			{
			case PrimitiveTypeCode.Empty:
			case PrimitiveTypeCode.Object:
				return jsonSchemaType | JsonSchemaType.String;
			case PrimitiveTypeCode.DBNull:
				return jsonSchemaType | JsonSchemaType.Null;
			case PrimitiveTypeCode.Boolean:
				return jsonSchemaType | JsonSchemaType.Boolean;
			case PrimitiveTypeCode.Char:
				return jsonSchemaType | JsonSchemaType.String;
			case PrimitiveTypeCode.SByte:
			case PrimitiveTypeCode.Int16:
			case PrimitiveTypeCode.UInt16:
			case PrimitiveTypeCode.Int32:
			case PrimitiveTypeCode.Byte:
			case PrimitiveTypeCode.UInt32:
			case PrimitiveTypeCode.Int64:
			case PrimitiveTypeCode.UInt64:
			case PrimitiveTypeCode.BigInteger:
				return jsonSchemaType | JsonSchemaType.Integer;
			case PrimitiveTypeCode.Single:
			case PrimitiveTypeCode.Double:
			case PrimitiveTypeCode.Decimal:
				return jsonSchemaType | JsonSchemaType.Float;
			case PrimitiveTypeCode.DateTime:
			case PrimitiveTypeCode.DateTimeOffset:
				return jsonSchemaType | JsonSchemaType.String;
			case PrimitiveTypeCode.Guid:
			case PrimitiveTypeCode.TimeSpan:
			case PrimitiveTypeCode.Uri:
			case PrimitiveTypeCode.String:
			case PrimitiveTypeCode.Bytes:
				return jsonSchemaType | JsonSchemaType.String;
			default:
				throw new JsonException("Unexpected type code '{0}' for type '{1}'.".FormatWith(CultureInfo.InvariantCulture, typeCode, type));
			}
		}
	}
}
