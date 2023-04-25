using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public static class Extensions
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass0_0
		{
			public bool valid;

			internal void _003CIsValid_003Eb__0(object sender, ValidationEventArgs args)
			{
				valid = false;
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass1_0
		{
			public IList<string> errors;

			internal void _003CIsValid_003Eb__0(object sender, ValidationEventArgs args)
			{
				errors.Add(args.Message);
			}
		}

		[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
		public static bool IsValid(this JToken source, JsonSchema schema)
		{
			_003C_003Ec__DisplayClass0_0 _003C_003Ec__DisplayClass0_ = new _003C_003Ec__DisplayClass0_0();
			_003C_003Ec__DisplayClass0_.valid = true;
			source.Validate(schema, _003C_003Ec__DisplayClass0_._003CIsValid_003Eb__0);
			return _003C_003Ec__DisplayClass0_.valid;
		}

		[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
		public static bool IsValid(this JToken source, JsonSchema schema, out IList<string> errorMessages)
		{
			_003C_003Ec__DisplayClass1_0 _003C_003Ec__DisplayClass1_ = new _003C_003Ec__DisplayClass1_0();
			_003C_003Ec__DisplayClass1_.errors = new List<string>();
			source.Validate(schema, _003C_003Ec__DisplayClass1_._003CIsValid_003Eb__0);
			errorMessages = _003C_003Ec__DisplayClass1_.errors;
			return errorMessages.Count == 0;
		}

		[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
		public static void Validate(this JToken source, JsonSchema schema)
		{
			source.Validate(schema, null);
		}

		[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
		public static void Validate(this JToken source, JsonSchema schema, ValidationEventHandler validationEventHandler)
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			ValidationUtils.ArgumentNotNull(schema, "schema");
			using (JsonValidatingReader jsonValidatingReader = new JsonValidatingReader(source.CreateReader()))
			{
				jsonValidatingReader.Schema = schema;
				if (validationEventHandler != null)
				{
					jsonValidatingReader.ValidationEventHandler += validationEventHandler;
				}
				while (jsonValidatingReader.Read())
				{
				}
			}
		}
	}
}
