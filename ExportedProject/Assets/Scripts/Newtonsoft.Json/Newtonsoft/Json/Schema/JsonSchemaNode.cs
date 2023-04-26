using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Schema
{
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaNode
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

			public static Func<JsonSchema, string> _003C_003E9__26_0;

			public static Func<string, string> _003C_003E9__26_1;

			internal string _003CGetId_003Eb__26_0(JsonSchema s)
			{
				return s.InternalId;
			}

			internal string _003CGetId_003Eb__26_1(string id)
			{
				return id;
			}
		}

		[CompilerGenerated]
		private readonly string _003CId_003Ek__BackingField;

		[CompilerGenerated]
		private readonly ReadOnlyCollection<JsonSchema> _003CSchemas_003Ek__BackingField;

		[CompilerGenerated]
		private readonly Dictionary<string, JsonSchemaNode> _003CProperties_003Ek__BackingField;

		[CompilerGenerated]
		private readonly Dictionary<string, JsonSchemaNode> _003CPatternProperties_003Ek__BackingField;

		[CompilerGenerated]
		private readonly List<JsonSchemaNode> _003CItems_003Ek__BackingField;

		public string Id
		{
			[CompilerGenerated]
			get
			{
				return _003CId_003Ek__BackingField;
			}
		}

		public ReadOnlyCollection<JsonSchema> Schemas
		{
			[CompilerGenerated]
			get
			{
				return _003CSchemas_003Ek__BackingField;
			}
		}

		public Dictionary<string, JsonSchemaNode> Properties
		{
			[CompilerGenerated]
			get
			{
				return _003CProperties_003Ek__BackingField;
			}
		}

		public Dictionary<string, JsonSchemaNode> PatternProperties
		{
			[CompilerGenerated]
			get
			{
				return _003CPatternProperties_003Ek__BackingField;
			}
		}

		public List<JsonSchemaNode> Items
		{
			[CompilerGenerated]
			get
			{
				return _003CItems_003Ek__BackingField;
			}
		}

		public JsonSchemaNode AdditionalProperties { get; set; }

		public JsonSchemaNode AdditionalItems { get; set; }

		public JsonSchemaNode(JsonSchema schema)
		{
			_003CSchemas_003Ek__BackingField = new ReadOnlyCollection<JsonSchema>(new JsonSchema[1] { schema });
			_003CProperties_003Ek__BackingField = new Dictionary<string, JsonSchemaNode>();
			_003CPatternProperties_003Ek__BackingField = new Dictionary<string, JsonSchemaNode>();
			_003CItems_003Ek__BackingField = new List<JsonSchemaNode>();
			_003CId_003Ek__BackingField = GetId(Schemas);
		}

		private JsonSchemaNode(JsonSchemaNode source, JsonSchema schema)
		{
			_003CSchemas_003Ek__BackingField = new ReadOnlyCollection<JsonSchema>(source.Schemas.Union(new JsonSchema[1] { schema }).ToList());
			_003CProperties_003Ek__BackingField = new Dictionary<string, JsonSchemaNode>(source.Properties);
			_003CPatternProperties_003Ek__BackingField = new Dictionary<string, JsonSchemaNode>(source.PatternProperties);
			_003CItems_003Ek__BackingField = new List<JsonSchemaNode>(source.Items);
			AdditionalProperties = source.AdditionalProperties;
			AdditionalItems = source.AdditionalItems;
			_003CId_003Ek__BackingField = GetId(Schemas);
		}

		public JsonSchemaNode Combine(JsonSchema schema)
		{
			return new JsonSchemaNode(this, schema);
		}

		public static string GetId(IEnumerable<JsonSchema> schemata)
		{
			return string.Join("-", schemata.Select(_003C_003Ec._003C_003E9__26_0 ?? (_003C_003Ec._003C_003E9__26_0 = _003C_003Ec._003C_003E9._003CGetId_003Eb__26_0)).OrderBy(_003C_003Ec._003C_003E9__26_1 ?? (_003C_003Ec._003C_003E9__26_1 = _003C_003Ec._003C_003E9._003CGetId_003Eb__26_1), StringComparer.Ordinal));
		}
	}
}
