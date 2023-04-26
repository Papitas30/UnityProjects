using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq.JsonPath
{
	[NullableContext(1)]
	[Nullable(0)]
	internal class FieldMultipleFilter : PathFilter
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003C_003Ec
		{
			[Nullable(0)]
			public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

			[Nullable(0)]
			public static Func<string, string> _003C_003E9__2_0;

			[NullableContext(0)]
			internal string _003CExecuteFilter_003Eb__2_0(string n)
			{
				return "'" + n + "'";
			}
		}

		internal List<string> Names;

		public FieldMultipleFilter(List<string> names)
		{
			Names = names;
		}

		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, [Nullable(2)] JsonSelectSettings settings)
		{
			foreach (JToken item in current)
			{
				JObject o = item as JObject;
				if (o != null)
				{
					foreach (string name in Names)
					{
						JToken jToken = o[name];
						if (jToken != null)
						{
							yield return jToken;
						}
						if (settings != null && settings.ErrorWhenNoMatch)
						{
							throw new JsonException("Property '{0}' does not exist on JObject.".FormatWith(CultureInfo.InvariantCulture, name));
						}
					}
				}
				else if (settings != null && settings.ErrorWhenNoMatch)
				{
					throw new JsonException("Properties {0} not valid on {1}.".FormatWith(CultureInfo.InvariantCulture, string.Join(", ", Names.Select(_003C_003Ec._003C_003E9__2_0 ?? (_003C_003Ec._003C_003E9__2_0 = _003C_003Ec._003C_003E9._003CExecuteFilter_003Eb__2_0))), item.GetType().Name));
				}
			}
		}
	}
}
