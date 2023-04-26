using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Newtonsoft.Json
{
	[Serializable]
	[NullableContext(1)]
	[Nullable(0)]
	public class JsonReaderException : JsonException
	{
		[CompilerGenerated]
		private readonly int _003CLineNumber_003Ek__BackingField;

		[CompilerGenerated]
		private readonly int _003CLinePosition_003Ek__BackingField;

		[Nullable(2)]
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

		[Nullable(2)]
		public string Path
		{
			[NullableContext(2)]
			[CompilerGenerated]
			get
			{
				return _003CPath_003Ek__BackingField;
			}
		}

		public JsonReaderException()
		{
		}

		public JsonReaderException(string message)
			: base(message)
		{
		}

		public JsonReaderException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		public JsonReaderException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public JsonReaderException(string message, string path, int lineNumber, int linePosition, [Nullable(2)] Exception innerException)
			: base(message, innerException)
		{
			_003CPath_003Ek__BackingField = path;
			_003CLineNumber_003Ek__BackingField = lineNumber;
			_003CLinePosition_003Ek__BackingField = linePosition;
		}

		internal static JsonReaderException Create(JsonReader reader, string message)
		{
			return Create(reader, message, null);
		}

		internal static JsonReaderException Create(JsonReader reader, string message, [Nullable(2)] Exception ex)
		{
			return Create(reader as IJsonLineInfo, reader.Path, message, ex);
		}

		internal static JsonReaderException Create([Nullable(2)] IJsonLineInfo lineInfo, string path, string message, [Nullable(2)] Exception ex)
		{
			message = JsonPosition.FormatMessage(lineInfo, path, message);
			int lineNumber;
			int linePosition;
			if (lineInfo != null && lineInfo.HasLineInfo())
			{
				lineNumber = lineInfo.LineNumber;
				linePosition = lineInfo.LinePosition;
			}
			else
			{
				lineNumber = 0;
				linePosition = 0;
			}
			return new JsonReaderException(message, path, lineNumber, linePosition, ex);
		}
	}
}
