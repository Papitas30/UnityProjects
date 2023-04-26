using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Schema
{
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public class JsonSchemaResolver
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass5_0
		{
			public string reference;

			internal bool _003CGetSchema_003Eb__0(JsonSchema s)
			{
				return string.Equals(s.Id, reference, StringComparison.Ordinal);
			}

			internal bool _003CGetSchema_003Eb__1(JsonSchema s)
			{
				return string.Equals(s.Location, reference, StringComparison.Ordinal);
			}
		}

		public IList<JsonSchema> LoadedSchemas { get; protected set; }

		public JsonSchemaResolver()
		{
			LoadedSchemas = new List<JsonSchema>();
		}

		public virtual JsonSchema GetSchema(string reference)
		{
			_003C_003Ec__DisplayClass5_0 _003C_003Ec__DisplayClass5_ = new _003C_003Ec__DisplayClass5_0();
			_003C_003Ec__DisplayClass5_.reference = reference;
			JsonSchema jsonSchema = LoadedSchemas.SingleOrDefault(_003C_003Ec__DisplayClass5_._003CGetSchema_003Eb__0);
			if (jsonSchema == null)
			{
				jsonSchema = LoadedSchemas.SingleOrDefault(_003C_003Ec__DisplayClass5_._003CGetSchema_003Eb__1);
			}
			return jsonSchema;
		}
	}
}
