using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Newtonsoft.Json.Utilities
{
	[NullableContext(1)]
	[Nullable(0)]
	internal static class AsyncUtils
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003C_003Ec
		{
			[Nullable(0)]
			public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

			[Nullable(0)]
			public static Action _003C_003E9__5_0;

			internal void _003CFromCanceled_003Eb__5_0()
			{
			}
		}

		[Serializable]
		[CompilerGenerated]
		private sealed class _003C_003Ec__6<T>
		{
			[Nullable(0)]
			public static readonly _003C_003Ec__6<T> _003C_003E9 = new _003C_003Ec__6<T>();

			[Nullable(0)]
			public static Func<T> _003C_003E9__6_0;

			[NullableContext(0)]
			internal T _003CFromCanceled_003Eb__6_0()
			{
				return default(T);
			}
		}

		public static readonly Task<bool> False = Task.FromResult(false);

		public static readonly Task<bool> True = Task.FromResult(true);

		internal static readonly Task CompletedTask = Task.Delay(0);

		internal static Task<bool> ToAsync(this bool value)
		{
			if (!value)
			{
				return False;
			}
			return True;
		}

		[NullableContext(2)]
		public static Task CancelIfRequestedAsync(this CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return null;
			}
			return cancellationToken.FromCanceled();
		}

		[NullableContext(2)]
		[return: Nullable(new byte[] { 2, 1 })]
		public static Task<T> CancelIfRequestedAsync<T>(this CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return null;
			}
			return cancellationToken.FromCanceled<T>();
		}

		public static Task FromCanceled(this CancellationToken cancellationToken)
		{
			return new Task(_003C_003Ec._003C_003E9__5_0 ?? (_003C_003Ec._003C_003E9__5_0 = _003C_003Ec._003C_003E9._003CFromCanceled_003Eb__5_0), cancellationToken);
		}

		public static Task<T> FromCanceled<[Nullable(2)] T>(this CancellationToken cancellationToken)
		{
			return new Task<T>(_003C_003Ec__6<T>._003C_003E9__6_0 ?? (_003C_003Ec__6<T>._003C_003E9__6_0 = _003C_003Ec__6<T>._003C_003E9._003CFromCanceled_003Eb__6_0), cancellationToken);
		}

		public static Task WriteAsync(this TextWriter writer, char value, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return writer.WriteAsync(value);
			}
			return cancellationToken.FromCanceled();
		}

		public static Task WriteAsync(this TextWriter writer, [Nullable(2)] string value, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return writer.WriteAsync(value);
			}
			return cancellationToken.FromCanceled();
		}

		public static Task WriteAsync(this TextWriter writer, char[] value, int start, int count, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return writer.WriteAsync(value, start, count);
			}
			return cancellationToken.FromCanceled();
		}

		public static Task<int> ReadAsync(this TextReader reader, char[] buffer, int index, int count, CancellationToken cancellationToken)
		{
			if (!cancellationToken.IsCancellationRequested)
			{
				return reader.ReadAsync(buffer, index, count);
			}
			return cancellationToken.FromCanceled<int>();
		}

		public static bool IsCompletedSucessfully(this Task task)
		{
			return task.Status == TaskStatus.RanToCompletion;
		}
	}
}
