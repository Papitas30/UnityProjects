using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Newtonsoft.Json.Schema
{
	[Serializable]
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public class JsonSchemaException : JsonException
	{
		[CompilerGenerated]
		private readonly int _003CLineNumber_003Ek__BackingField;

		[CompilerGenerated]
		private readonly int _003CLinePosition_003Ek__BackingField;

		[CompilerGenerated]
		private readonly string _003CPath_003Ek__BackingField;

		public int LineNumber
		{
			[CompilerGenerated]
			get
			{
				return _003CLineNumber_003Ek__BackingField;
			}
		}

		public int LinePosition
		{
			[CompilerGenerated]
			get
			{
				return _003CLinePosition_003Ek__BackingField;
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

		public JsonSchemaException()
		{
		}

		public JsonSchemaException(string message)
			: base(message)
		{
		}

		public JsonSchemaException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		public JsonSchemaException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		internal JsonSchemaException(string message, Exception innerException, string path, int lineNumber, int linePosition)
			: base(message, innerException)
		{
			_003CPath_003Ek__BackingField = path;
			_003CLineNumber_003Ek__BackingField = lineNumber;
			_003CLinePosition_003Ek__BackingField = linePosition;
		}
	}
}
