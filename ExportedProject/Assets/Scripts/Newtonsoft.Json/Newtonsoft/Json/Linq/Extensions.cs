using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	[NullableContext(1)]
	[Nullable(0)]
	public static class Extensions
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003C_003Ec__0<T> where T : JToken
		{
			[Nullable(0)]
			public static readonly _003C_003Ec__0<T> _003C_003E9 = new _003C_003Ec__0<T>();

			[Nullable(0)]
			public static Func<T, IEnumerable<JToken>> _003C_003E9__0_0;

			[NullableContext(0)]
			internal IEnumerable<JToken> _003CAncestors_003Eb__0_0(T j)
			{
				return j.Ancestors();
			}
		}

		[Serializable]
		[CompilerGenerated]
		private sealed class _003C_003Ec__1<T> where T : JToken
		{
			[Nullable(0)]
			public static readonly _003C_003Ec__1<T> _003C_003E9 = new _003C_003Ec__1<T>();

			[Nullable(0)]
			public static Func<T, IEnumerable<JToken>> _003C_003E9__1_0;

			[NullableContext(0)]
			internal IEnumerable<JToken> _003CAncestorsAndSelf_003Eb__1_0(T j)
			{
				return j.AncestorsAndSelf();
			}
		}

		[Serializable]
		[CompilerGenerated]
		private sealed class _003C_003Ec__2<T> where T : JContainer
		{
			[Nullable(0)]
			public static readonly _003C_003Ec__2<T> _003C_003E9 = new _003C_003Ec__2<T>();

			[Nullable(0)]
			public static Func<T, IEnumerable<JToken>> _003C_003E9__2_0;

			[NullableContext(0)]
			internal IEnumerable<JToken> _003CDescendants_003Eb__2_0(T j)
			{
				return j.Descendants();
			}
		}

		[Serializable]
		[CompilerGenerated]
		private sealed class _003C_003Ec__3<T> where T : JContainer
		{
			[Nullable(0)]
			public static readonly _003C_003Ec__3<T> _003C_003E9 = new _003C_003Ec__3<T>();

			[Nullable(0)]
			public static Func<T, IEnumerable<JToken>> _003C_003E9__3_0;

			[NullableContext(0)]
			internal IEnumerable<JToken> _003CDescendantsAndSelf_003Eb__3_0(T j)
			{
				return j.DescendantsAndSelf();
			}
		}

		[Serializable]
		[CompilerGenerated]
		private sealed class _003C_003Ec
		{
			[Nullable(0)]
			public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

			[Nullable(0)]
			public static Func<JObject, IEnumerable<JProperty>> _003C_003E9__4_0;

			[NullableContext(0)]
			internal IEnumerable<JProperty> _003CProperties_003Eb__4_0(JObject d)
			{
				return d.Properties();
			}
		}

		[Serializable]
		[CompilerGenerated]
		private sealed class _003C_003Ec__13<T, U> where T : JToken
		{
			[Nullable(0)]
			public static readonly _003C_003Ec__13<T, U> _003C_003E9 = new _003C_003Ec__13<T, U>();

			[Nullable(0)]
			public static Func<T, IEnumerable<JToken>> _003C_003E9__13_0;

			[NullableContext(0)]
			internal IEnumerable<JToken> _003CChildren_003Eb__13_0(T c)
			{
				return c.Children();
			}
		}

		public static IJEnumerable<JToken> Ancestors<[Nullable(0)] T>(this IEnumerable<T> source) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return source.SelectMany(_003C_003Ec__0<T>._003C_003E9__0_0 ?? (_003C_003Ec__0<T>._003C_003E9__0_0 = _003C_003Ec__0<T>._003C_003E9._003CAncestors_003Eb__0_0)).AsJEnumerable();
		}

		public static IJEnumerable<JToken> AncestorsAndSelf<[Nullable(0)] T>(this IEnumerable<T> source) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return source.SelectMany(_003C_003Ec__1<T>._003C_003E9__1_0 ?? (_003C_003Ec__1<T>._003C_003E9__1_0 = _003C_003Ec__1<T>._003C_003E9._003CAncestorsAndSelf_003Eb__1_0)).AsJEnumerable();
		}

		public static IJEnumerable<JToken> Descendants<[Nullable(0)] T>(this IEnumerable<T> source) where T : JContainer
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return source.SelectMany(_003C_003Ec__2<T>._003C_003E9__2_0 ?? (_003C_003Ec__2<T>._003C_003E9__2_0 = _003C_003Ec__2<T>._003C_003E9._003CDescendants_003Eb__2_0)).AsJEnumerable();
		}

		public static IJEnumerable<JToken> DescendantsAndSelf<[Nullable(0)] T>(this IEnumerable<T> source) where T : JContainer
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return source.SelectMany(_003C_003Ec__3<T>._003C_003E9__3_0 ?? (_003C_003Ec__3<T>._003C_003E9__3_0 = _003C_003Ec__3<T>._003C_003E9._003CDescendantsAndSelf_003Eb__3_0)).AsJEnumerable();
		}

		public static IJEnumerable<JProperty> Properties(this IEnumerable<JObject> source)
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return source.SelectMany(_003C_003Ec._003C_003E9__4_0 ?? (_003C_003Ec._003C_003E9__4_0 = _003C_003Ec._003C_003E9._003CProperties_003Eb__4_0)).AsJEnumerable();
		}

		public static IJEnumerable<JToken> Values(this IEnumerable<JToken> source, [Nullable(2)] object key)
		{
			return source.Values<JToken, JToken>(key).AsJEnumerable();
		}

		public static IJEnumerable<JToken> Values(this IEnumerable<JToken> source)
		{
			return source.Values(null);
		}

		[return: Nullable(new byte[] { 1, 2 })]
		public static IEnumerable<U> Values<[Nullable(2)] U>(this IEnumerable<JToken> source, object key)
		{
			return source.Values<JToken, U>(key);
		}

		[return: Nullable(new byte[] { 1, 2 })]
		public static IEnumerable<U> Values<[Nullable(2)] U>(this IEnumerable<JToken> source)
		{
			return source.Values<JToken, U>(null);
		}

		[NullableContext(2)]
		public static U Value<U>([Nullable(1)] this IEnumerable<JToken> value)
		{
			return value.Value<JToken, U>();
		}

		[return: Nullable(2)]
		public static U Value<[Nullable(0)] T, [Nullable(2)] U>(this IEnumerable<T> value) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			JToken obj = value as JToken;
			if (obj == null)
			{
				throw new ArgumentException("Source value must be a JToken.");
			}
			return obj.Convert<JToken, U>();
		}

		[return: Nullable(new byte[] { 1, 2 })]
		internal static IEnumerable<U> Values<[Nullable(0)] T, [Nullable(2)] U>(this IEnumerable<T> source, [Nullable(2)] object key) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			if (key == null)
			{
				foreach (T item in source)
				{
					JValue jValue = item as JValue;
					if (jValue != null)
					{
						yield return jValue.Convert<JValue, U>();
						continue;
					}
					foreach (JToken item2 in item.Children())
					{
						yield return item2.Convert<JToken, U>();
					}
				}
				yield break;
			}
			foreach (T item3 in source)
			{
				JToken jToken = item3[key];
				if (jToken != null)
				{
					yield return jToken.Convert<JToken, U>();
				}
			}
		}

		public static IJEnumerable<JToken> Children<[Nullable(0)] T>(this IEnumerable<T> source) where T : JToken
		{
			return source.Children<T, JToken>().AsJEnumerable();
		}

		[return: Nullable(new byte[] { 1, 2 })]
		public static IEnumerable<U> Children<[Nullable(0)] T, [Nullable(2)] U>(this IEnumerable<T> source) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return source.SelectMany(_003C_003Ec__13<T, U>._003C_003E9__13_0 ?? (_003C_003Ec__13<T, U>._003C_003E9__13_0 = _003C_003Ec__13<T, U>._003C_003E9._003CChildren_003Eb__13_0)).Convert<JToken, U>();
		}

		[return: Nullable(new byte[] { 1, 2 })]
		internal static IEnumerable<U> Convert<[Nullable(0)] T, [Nullable(2)] U>(this IEnumerable<T> source) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			foreach (T item in source)
			{
				yield return item.Convert<JToken, U>();
			}
		}

		[NullableContext(2)]
		internal static U Convert<[Nullable(0)] T, U>([Nullable(1)] this T token) where T : JToken
		{
			if (token == null)
			{
				return default(U);
			}
			if (token is U)
			{
				U result = (U)(object)((token is U) ? token : null);
				if (typeof(U) != typeof(IComparable) && typeof(U) != typeof(IFormattable))
				{
					return result;
				}
			}
			JValue jValue = token as JValue;
			if (jValue == null)
			{
				throw new InvalidCastException("Cannot cast {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, token.GetType(), typeof(T)));
			}
			object value = jValue.Value;
			if (value is U)
			{
				return (U)value;
			}
			Type type = typeof(U);
			if (ReflectionUtils.IsNullableType(type))
			{
				if (jValue.Value == null)
				{
					return default(U);
				}
				type = Nullable.GetUnderlyingType(type);
			}
			return (U)System.Convert.ChangeType(jValue.Value, type, CultureInfo.InvariantCulture);
		}

		public static IJEnumerable<JToken> AsJEnumerable(this IEnumerable<JToken> source)
		{
			return source.AsJEnumerable<JToken>();
		}

		public static IJEnumerable<T> AsJEnumerable<[Nullable(0)] T>(this IEnumerable<T> source) where T : JToken
		{
			if (source == null)
			{
				return null;
			}
			IJEnumerable<T> iJEnumerable = source as IJEnumerable<T>;
			if (iJEnumerable != null)
			{
				return iJEnumerable;
			}
			return new JEnumerable<T>(source);
		}
	}
}
