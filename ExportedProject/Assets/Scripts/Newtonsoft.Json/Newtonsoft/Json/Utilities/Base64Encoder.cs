using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Newtonsoft.Json.Utilities
{
	[NullableContext(1)]
	[Nullable(0)]
	internal class Base64Encoder
	{
		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CEncodeAsync_003Ed__13 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public Base64Encoder _003C_003E4__this;

			[Nullable(0)]
			public byte[] buffer;

			public int index;

			public int count;

			public CancellationToken cancellationToken;

			private int _003Cnum4_003E5__2;

			private int _003Clength_003E5__3;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				Base64Encoder base64Encoder = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_00df;
					}
					if (num == 1)
					{
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_01d0;
					}
					base64Encoder.ValidateEncode(buffer, index, count);
					if (base64Encoder._leftOverBytesCount <= 0)
					{
						goto IL_00e6;
					}
					if (!base64Encoder.FulfillFromLeftover(buffer, index, ref count))
					{
						int num2 = Convert.ToBase64CharArray(base64Encoder._leftOverBytes, 0, 3, base64Encoder._charsLine, 0);
						awaiter = base64Encoder.WriteCharsAsync(base64Encoder._charsLine, 0, num2, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_00df;
					}
					goto end_IL_000e;
					IL_01d0:
					awaiter.GetResult();
					index += _003Clength_003E5__3;
					goto IL_01ea;
					IL_00df:
					awaiter.GetResult();
					goto IL_00e6;
					IL_00e6:
					base64Encoder.StoreLeftOverBytes(buffer, index, ref count);
					_003Cnum4_003E5__2 = index + count;
					_003Clength_003E5__3 = 57;
					goto IL_01ea;
					IL_01ea:
					if (index < _003Cnum4_003E5__2)
					{
						if (index + _003Clength_003E5__3 > _003Cnum4_003E5__2)
						{
							_003Clength_003E5__3 = _003Cnum4_003E5__2 - index;
						}
						int num3 = Convert.ToBase64CharArray(buffer, index, _003Clength_003E5__3, base64Encoder._charsLine, 0);
						awaiter = base64Encoder.WriteCharsAsync(base64Encoder._charsLine, 0, num3, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 1);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_01d0;
					}
					end_IL_000e:;
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult();
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine([Nullable(0)] IAsyncStateMachine stateMachine)
			{
				_003C_003Et__builder.SetStateMachine(stateMachine);
			}

			void IAsyncStateMachine.SetStateMachine([Nullable(0)] IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		private const int Base64LineSize = 76;

		private const int LineSizeInBytes = 57;

		private readonly char[] _charsLine = new char[76];

		private readonly TextWriter _writer;

		[Nullable(2)]
		private byte[] _leftOverBytes;

		private int _leftOverBytesCount;

		public Base64Encoder(TextWriter writer)
		{
			ValidationUtils.ArgumentNotNull(writer, "writer");
			_writer = writer;
		}

		private void ValidateEncode(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count > buffer.Length - index)
			{
				throw new ArgumentOutOfRangeException("count");
			}
		}

		public void Encode(byte[] buffer, int index, int count)
		{
			ValidateEncode(buffer, index, count);
			if (_leftOverBytesCount > 0)
			{
				if (FulfillFromLeftover(buffer, index, ref count))
				{
					return;
				}
				int count2 = Convert.ToBase64CharArray(_leftOverBytes, 0, 3, _charsLine, 0);
				WriteChars(_charsLine, 0, count2);
			}
			StoreLeftOverBytes(buffer, index, ref count);
			int num = index + count;
			int num2 = 57;
			while (index < num)
			{
				if (index + num2 > num)
				{
					num2 = num - index;
				}
				int count3 = Convert.ToBase64CharArray(buffer, index, num2, _charsLine, 0);
				WriteChars(_charsLine, 0, count3);
				index += num2;
			}
		}

		private void StoreLeftOverBytes(byte[] buffer, int index, ref int count)
		{
			int num = count % 3;
			if (num > 0)
			{
				count -= num;
				if (_leftOverBytes == null)
				{
					_leftOverBytes = new byte[3];
				}
				for (int i = 0; i < num; i++)
				{
					_leftOverBytes[i] = buffer[index + count + i];
				}
			}
			_leftOverBytesCount = num;
		}

		private bool FulfillFromLeftover(byte[] buffer, int index, ref int count)
		{
			int leftOverBytesCount = _leftOverBytesCount;
			while (leftOverBytesCount < 3 && count > 0)
			{
				_leftOverBytes[leftOverBytesCount++] = buffer[index++];
				count--;
			}
			if (count == 0 && leftOverBytesCount < 3)
			{
				_leftOverBytesCount = leftOverBytesCount;
				return true;
			}
			return false;
		}

		public void Flush()
		{
			if (_leftOverBytesCount > 0)
			{
				int count = Convert.ToBase64CharArray(_leftOverBytes, 0, _leftOverBytesCount, _charsLine, 0);
				WriteChars(_charsLine, 0, count);
				_leftOverBytesCount = 0;
			}
		}

		private void WriteChars(char[] chars, int index, int count)
		{
			_writer.Write(chars, index, count);
		}

		[AsyncStateMachine(typeof(_003CEncodeAsync_003Ed__13))]
		public Task EncodeAsync(byte[] buffer, int index, int count, CancellationToken cancellationToken)
		{
			_003CEncodeAsync_003Ed__13 stateMachine = default(_003CEncodeAsync_003Ed__13);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.buffer = buffer;
			stateMachine.index = index;
			stateMachine.count = count;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		private Task WriteCharsAsync(char[] chars, int index, int count, CancellationToken cancellationToken)
		{
			return _writer.WriteAsync(chars, index, count, cancellationToken);
		}

		public Task FlushAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			if (_leftOverBytesCount > 0)
			{
				int count = Convert.ToBase64CharArray(_leftOverBytes, 0, _leftOverBytesCount, _charsLine, 0);
				_leftOverBytesCount = 0;
				return WriteCharsAsync(_charsLine, 0, count, cancellationToken);
			}
			return AsyncUtils.CompletedTask;
		}
	}
}
