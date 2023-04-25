using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	[NullableContext(2)]
	[Nullable(0)]
	public static class AotHelper
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003C_003Ec__1<T> where T : new()
		{
			[Nullable(0)]
			public static readonly _003C_003Ec__1<T> _003C_003E9 = new _003C_003Ec__1<T>();

			[Nullable(0)]
			public static Action _003C_003E9__1_0;

			internal void _003CEnsureType_003Eb__1_0()
			{
				new T();
			}
		}

		[Serializable]
		[CompilerGenerated]
		private sealed class _003C_003Ec__2<T>
		{
			[Nullable(0)]
			public static readonly _003C_003Ec__2<T> _003C_003E9 = new _003C_003Ec__2<T>();

			[Nullable(0)]
			public static Action _003C_003E9__2_0;

			internal void _003CEnsureList_003Eb__2_0()
			{
				List<T> list = new List<T>();
				new HashSet<T>();
				new CollectionWrapper<T>((IList)list);
				new CollectionWrapper<T>((ICollection<T>)list);
			}
		}

		[Serializable]
		[CompilerGenerated]
		private sealed class _003C_003Ec__3<TKey, TValue>
		{
			[Nullable(0)]
			public static readonly _003C_003Ec__3<TKey, TValue> _003C_003E9 = new _003C_003Ec__3<TKey, TValue>();

			[Nullable(0)]
			public static Action _003C_003E9__3_0;

			internal void _003CEnsureDictionary_003Eb__3_0()
			{
				new Dictionary<TKey, TValue>();
				new DictionaryWrapper<TKey, TValue>((IDictionary)null);
				new DictionaryWrapper<TKey, TValue>((IDictionary<TKey, TValue>)null);
				new DefaultContractResolver.EnumerableDictionaryWrapper<TKey, TValue>(null);
			}
		}

		private static bool s_alwaysFalse = DateTime.UtcNow.Year < 0;

		[NullableContext(1)]
		public static void Ensure(Action action)
		{
			if (IsFalse())
			{
				try
				{
					action();
				}
				catch (Exception innerException)
				{
					throw new InvalidOperationException("", innerException);
				}
			}
		}

		public static void EnsureType<T>() where T : new()
		{
			Ensure(_003C_003Ec__1<T>._003C_003E9__1_0 ?? (_003C_003Ec__1<T>._003C_003E9__1_0 = _003C_003Ec__1<T>._003C_003E9._003CEnsureType_003Eb__1_0));
		}

		public static void EnsureList<T>()
		{
			Ensure(_003C_003Ec__2<T>._003C_003E9__2_0 ?? (_003C_003Ec__2<T>._003C_003E9__2_0 = _003C_003Ec__2<T>._003C_003E9._003CEnsureList_003Eb__2_0));
		}

		public static void EnsureDictionary<TKey, TValue>()
		{
			Ensure(_003C_003Ec__3<TKey, TValue>._003C_003E9__3_0 ?? (_003C_003Ec__3<TKey, TValue>._003C_003E9__3_0 = _003C_003Ec__3<TKey, TValue>._003C_003E9._003CEnsureDictionary_003Eb__3_0));
		}

		public static bool IsFalse()
		{
			return s_alwaysFalse;
		}
	}
}
