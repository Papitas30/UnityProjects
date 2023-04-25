using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	[NullableContext(1)]
	[Nullable(0)]
	public class JsonTextWriter : JsonWriter
	{
		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CDoCloseAsync_003Ed__8 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextWriter _003C_003E4__this;

			public CancellationToken cancellationToken;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextWriter jsonTextWriter = _003C_003E4__this;
				try
				{
					if (num != 0)
					{
						if (jsonTextWriter.Top == 0)
						{
							cancellationToken.ThrowIfCancellationRequested();
						}
						goto IL_008d;
					}
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter = _003C_003Eu__1;
					_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_0086;
					IL_0086:
					awaiter.GetResult();
					goto IL_008d;
					IL_008d:
					if (jsonTextWriter.Top > 0)
					{
						awaiter = jsonTextWriter.WriteEndAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0086;
					}
					jsonTextWriter.CloseBufferAndWriter();
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

		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CWriteIndentAsync_003Ed__12 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextWriter _003C_003E4__this;

			public int newLineLen;

			public int currentIndentCount;

			public CancellationToken cancellationToken;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextWriter jsonTextWriter = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = _003C_003Eu__1;
							_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							goto IL_0125;
						}
						awaiter = jsonTextWriter._writer.WriteAsync(jsonTextWriter._indentChars, 0, newLineLen + Math.Min(currentIndentCount, 12), cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
					}
					awaiter.GetResult();
					goto IL_012c;
					IL_012c:
					if ((currentIndentCount -= 12) > 0)
					{
						awaiter = jsonTextWriter._writer.WriteAsync(jsonTextWriter._indentChars, newLineLen, Math.Min(currentIndentCount, 12), cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 1);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0125;
					}
					goto end_IL_000e;
					IL_0125:
					awaiter.GetResult();
					goto IL_012c;
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

		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CWriteValueInternalAsync_003Ed__14 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public Task task;

			[Nullable(0)]
			public JsonTextWriter _003C_003E4__this;

			[Nullable(0)]
			public string value;

			public CancellationToken cancellationToken;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextWriter jsonTextWriter = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = _003C_003Eu__1;
							_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							goto IL_00e7;
						}
						awaiter = task.ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
					}
					awaiter.GetResult();
					awaiter = jsonTextWriter._writer.WriteAsync(value, cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_003C_003E1__state = 1);
						_003C_003Eu__1 = awaiter;
						_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_00e7;
					IL_00e7:
					awaiter.GetResult();
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

		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CWriteIntegerValueAsync_003Ed__23 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public Task task;

			[Nullable(0)]
			public JsonTextWriter _003C_003E4__this;

			public ulong uvalue;

			public bool negative;

			public CancellationToken cancellationToken;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextWriter jsonTextWriter = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = _003C_003Eu__1;
							_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							goto IL_00e8;
						}
						awaiter = task.ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
					}
					awaiter.GetResult();
					awaiter = jsonTextWriter.WriteDigitsAsync(uvalue, negative, cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_003C_003E1__state = 1);
						_003C_003Eu__1 = awaiter;
						_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_00e8;
					IL_00e8:
					awaiter.GetResult();
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

		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CDoWritePropertyNameAsync_003Ed__29 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public Task task;

			[Nullable(0)]
			public JsonTextWriter _003C_003E4__this;

			[Nullable(0)]
			public string name;

			public CancellationToken cancellationToken;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextWriter jsonTextWriter = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					default:
						awaiter = task.ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_007d;
					case 0:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_007d;
					case 1:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_00f3;
					case 2:
						{
							awaiter = _003C_003Eu__1;
							_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							break;
						}
						IL_00f3:
						awaiter.GetResult();
						awaiter = jsonTextWriter._writer.WriteAsync(':').ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 2);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						break;
						IL_007d:
						awaiter.GetResult();
						awaiter = jsonTextWriter.WriteEscapedStringAsync(name, jsonTextWriter._quoteName, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 1);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_00f3;
					}
					awaiter.GetResult();
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

		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CDoWritePropertyNameAsync_003Ed__31 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextWriter _003C_003E4__this;

			[Nullable(0)]
			public string name;

			public CancellationToken cancellationToken;

			public bool escape;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextWriter jsonTextWriter = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					default:
						awaiter = jsonTextWriter.InternalWritePropertyNameAsync(name, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0095;
					case 0:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0095;
					case 1:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0113;
					case 2:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_018f;
					case 3:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0204;
					case 4:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_027b;
					case 5:
						{
							awaiter = _003C_003Eu__1;
							_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							break;
						}
						IL_027b:
						awaiter.GetResult();
						goto IL_0282;
						IL_0095:
						awaiter.GetResult();
						if (escape)
						{
							awaiter = jsonTextWriter.WriteEscapedStringAsync(name, jsonTextWriter._quoteName, cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_003C_003E1__state = 1);
								_003C_003Eu__1 = awaiter;
								_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_0113;
						}
						if (jsonTextWriter._quoteName)
						{
							awaiter = jsonTextWriter._writer.WriteAsync(jsonTextWriter._quoteChar).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_003C_003E1__state = 2);
								_003C_003Eu__1 = awaiter;
								_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_018f;
						}
						goto IL_0196;
						IL_018f:
						awaiter.GetResult();
						goto IL_0196;
						IL_0282:
						awaiter = jsonTextWriter._writer.WriteAsync(':').ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 5);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						break;
						IL_0113:
						awaiter.GetResult();
						goto IL_0282;
						IL_0196:
						awaiter = jsonTextWriter._writer.WriteAsync(name, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 3);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0204;
						IL_0204:
						awaiter.GetResult();
						if (jsonTextWriter._quoteName)
						{
							awaiter = jsonTextWriter._writer.WriteAsync(jsonTextWriter._quoteChar).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_003C_003E1__state = 4);
								_003C_003Eu__1 = awaiter;
								_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_027b;
						}
						goto IL_0282;
					}
					awaiter.GetResult();
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

		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CDoWriteStartArrayAsync_003Ed__34 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public Task task;

			[Nullable(0)]
			public JsonTextWriter _003C_003E4__this;

			public CancellationToken cancellationToken;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextWriter jsonTextWriter = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = _003C_003Eu__1;
							_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							goto IL_00e3;
						}
						awaiter = task.ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
					}
					awaiter.GetResult();
					awaiter = jsonTextWriter._writer.WriteAsync('[', cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_003C_003E1__state = 1);
						_003C_003Eu__1 = awaiter;
						_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_00e3;
					IL_00e3:
					awaiter.GetResult();
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

		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CDoWriteStartObjectAsync_003Ed__37 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public Task task;

			[Nullable(0)]
			public JsonTextWriter _003C_003E4__this;

			public CancellationToken cancellationToken;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextWriter jsonTextWriter = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = _003C_003Eu__1;
							_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							goto IL_00e3;
						}
						awaiter = task.ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
					}
					awaiter.GetResult();
					awaiter = jsonTextWriter._writer.WriteAsync('{', cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_003C_003E1__state = 1);
						_003C_003Eu__1 = awaiter;
						_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_00e3;
					IL_00e3:
					awaiter.GetResult();
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

		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CDoWriteStartConstructorAsync_003Ed__39 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextWriter _003C_003E4__this;

			public CancellationToken cancellationToken;

			[Nullable(0)]
			public string name;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextWriter jsonTextWriter = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					default:
						awaiter = jsonTextWriter.InternalWriteStartAsync(JsonToken.StartConstructor, JsonContainerType.Constructor, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0089;
					case 0:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0089;
					case 1:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_00fd;
					case 2:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0172;
					case 3:
						{
							awaiter = _003C_003Eu__1;
							_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							break;
						}
						IL_00fd:
						awaiter.GetResult();
						awaiter = jsonTextWriter._writer.WriteAsync(name, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 2);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0172;
						IL_0089:
						awaiter.GetResult();
						awaiter = jsonTextWriter._writer.WriteAsync("new ", cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 1);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_00fd;
						IL_0172:
						awaiter.GetResult();
						awaiter = jsonTextWriter._writer.WriteAsync('(').ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 3);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						break;
					}
					awaiter.GetResult();
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

		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CDoWriteUndefinedAsync_003Ed__42 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public Task task;

			[Nullable(0)]
			public JsonTextWriter _003C_003E4__this;

			public CancellationToken cancellationToken;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextWriter jsonTextWriter = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = _003C_003Eu__1;
							_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							goto IL_00e6;
						}
						awaiter = task.ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
					}
					awaiter.GetResult();
					awaiter = jsonTextWriter._writer.WriteAsync(JsonConvert.Undefined, cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_003C_003E1__state = 1);
						_003C_003Eu__1 = awaiter;
						_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_00e6;
					IL_00e6:
					awaiter.GetResult();
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

		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CWriteValueNonNullAsync_003Ed__53 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextWriter _003C_003E4__this;

			public CancellationToken cancellationToken;

			[Nullable(0)]
			public byte[] value;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextWriter jsonTextWriter = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					default:
						awaiter = jsonTextWriter.InternalWriteValueAsync(JsonToken.Bytes, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_008d;
					case 0:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_008d;
					case 1:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_00fc;
					case 2:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_017a;
					case 3:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_01e9;
					case 4:
						{
							awaiter = _003C_003Eu__1;
							_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							break;
						}
						IL_01e9:
						awaiter.GetResult();
						awaiter = jsonTextWriter._writer.WriteAsync(jsonTextWriter._quoteChar).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 4);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						break;
						IL_008d:
						awaiter.GetResult();
						awaiter = jsonTextWriter._writer.WriteAsync(jsonTextWriter._quoteChar).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 1);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_00fc;
						IL_017a:
						awaiter.GetResult();
						awaiter = jsonTextWriter.Base64Encoder.FlushAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 3);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_01e9;
						IL_00fc:
						awaiter.GetResult();
						awaiter = jsonTextWriter.Base64Encoder.EncodeAsync(value, 0, value.Length, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 2);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_017a;
					}
					awaiter.GetResult();
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

		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CDoWriteValueAsync_003Ed__59 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextWriter _003C_003E4__this;

			public CancellationToken cancellationToken;

			public DateTime value;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextWriter jsonTextWriter = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					default:
						awaiter = jsonTextWriter.InternalWriteValueAsync(JsonToken.Date, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_008d;
					case 0:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_008d;
					case 1:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_013a;
					case 2:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_01ae;
					case 3:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0234;
					case 4:
						{
							awaiter = _003C_003Eu__1;
							_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							break;
						}
						IL_013a:
						awaiter.GetResult();
						goto end_IL_000e;
						IL_008d:
						awaiter.GetResult();
						value = DateTimeUtils.EnsureDateTime(value, jsonTextWriter.DateTimeZoneHandling);
						if (StringUtils.IsNullOrEmpty(jsonTextWriter.DateFormatString))
						{
							int count = jsonTextWriter.WriteValueToBuffer(value);
							awaiter = jsonTextWriter._writer.WriteAsync(jsonTextWriter._writeBuffer, 0, count, cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_003C_003E1__state = 1);
								_003C_003Eu__1 = awaiter;
								_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_013a;
						}
						awaiter = jsonTextWriter._writer.WriteAsync(jsonTextWriter._quoteChar).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 2);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_01ae;
						IL_0234:
						awaiter.GetResult();
						awaiter = jsonTextWriter._writer.WriteAsync(jsonTextWriter._quoteChar).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 4);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						break;
						IL_01ae:
						awaiter.GetResult();
						awaiter = jsonTextWriter._writer.WriteAsync(value.ToString(jsonTextWriter.DateFormatString, jsonTextWriter.Culture), cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 3);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0234;
					}
					awaiter.GetResult();
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

		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CDoWriteValueAsync_003Ed__63 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextWriter _003C_003E4__this;

			public CancellationToken cancellationToken;

			public DateTimeOffset value;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextWriter jsonTextWriter = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					default:
						awaiter = jsonTextWriter.InternalWriteValueAsync(JsonToken.Date, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_008d;
					case 0:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_008d;
					case 1:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0123;
					case 2:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0197;
					case 3:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_021d;
					case 4:
						{
							awaiter = _003C_003Eu__1;
							_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							break;
						}
						IL_0123:
						awaiter.GetResult();
						goto end_IL_000e;
						IL_008d:
						awaiter.GetResult();
						if (StringUtils.IsNullOrEmpty(jsonTextWriter.DateFormatString))
						{
							int count = jsonTextWriter.WriteValueToBuffer(value);
							awaiter = jsonTextWriter._writer.WriteAsync(jsonTextWriter._writeBuffer, 0, count, cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_003C_003E1__state = 1);
								_003C_003Eu__1 = awaiter;
								_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_0123;
						}
						awaiter = jsonTextWriter._writer.WriteAsync(jsonTextWriter._quoteChar).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 2);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0197;
						IL_021d:
						awaiter.GetResult();
						awaiter = jsonTextWriter._writer.WriteAsync(jsonTextWriter._quoteChar).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 4);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						break;
						IL_0197:
						awaiter.GetResult();
						awaiter = jsonTextWriter._writer.WriteAsync(value.ToString(jsonTextWriter.DateFormatString, jsonTextWriter.Culture), cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 3);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_021d;
					}
					awaiter.GetResult();
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

		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CDoWriteValueAsync_003Ed__77 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextWriter _003C_003E4__this;

			public CancellationToken cancellationToken;

			public Guid value;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextWriter jsonTextWriter = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					default:
						awaiter = jsonTextWriter.InternalWriteValueAsync(JsonToken.String, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0089;
					case 0:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0089;
					case 1:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_00f8;
					case 2:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_017c;
					case 3:
						{
							awaiter = _003C_003Eu__1;
							_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							break;
						}
						IL_00f8:
						awaiter.GetResult();
						awaiter = jsonTextWriter._writer.WriteAsync(value.ToString("D", CultureInfo.InvariantCulture), cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 2);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_017c;
						IL_0089:
						awaiter.GetResult();
						awaiter = jsonTextWriter._writer.WriteAsync(jsonTextWriter._quoteChar).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 1);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_00f8;
						IL_017c:
						awaiter.GetResult();
						awaiter = jsonTextWriter._writer.WriteAsync(jsonTextWriter._quoteChar).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 3);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						break;
					}
					awaiter.GetResult();
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

		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CDoWriteValueAsync_003Ed__96 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public Task task;

			[Nullable(0)]
			public string value;

			[Nullable(0)]
			public JsonTextWriter _003C_003E4__this;

			public CancellationToken cancellationToken;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextWriter jsonTextWriter = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = _003C_003Eu__1;
							_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							goto IL_0103;
						}
						awaiter = task.ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
					}
					awaiter.GetResult();
					awaiter = ((value == null) ? jsonTextWriter._writer.WriteAsync(JsonConvert.Null, cancellationToken) : jsonTextWriter.WriteEscapedStringAsync(value, true, cancellationToken)).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_003C_003E1__state = 1);
						_003C_003Eu__1 = awaiter;
						_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_0103;
					IL_0103:
					awaiter.GetResult();
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

		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CDoWriteValueAsync_003Ed__98 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextWriter _003C_003E4__this;

			public CancellationToken cancellationToken;

			public TimeSpan value;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextWriter jsonTextWriter = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					default:
						awaiter = jsonTextWriter.InternalWriteValueAsync(JsonToken.String, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0089;
					case 0:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0089;
					case 1:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_00fe;
					case 2:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_017e;
					case 3:
						{
							awaiter = _003C_003Eu__1;
							_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							break;
						}
						IL_00fe:
						awaiter.GetResult();
						awaiter = jsonTextWriter._writer.WriteAsync(value.ToString(null, CultureInfo.InvariantCulture), cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 2);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_017e;
						IL_0089:
						awaiter.GetResult();
						awaiter = jsonTextWriter._writer.WriteAsync(jsonTextWriter._quoteChar, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 1);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_00fe;
						IL_017e:
						awaiter.GetResult();
						awaiter = jsonTextWriter._writer.WriteAsync(jsonTextWriter._quoteChar, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 3);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						break;
					}
					awaiter.GetResult();
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

		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CWriteValueNotNullAsync_003Ed__109 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public Task task;

			[Nullable(0)]
			public JsonTextWriter _003C_003E4__this;

			[Nullable(0)]
			public Uri value;

			public CancellationToken cancellationToken;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextWriter jsonTextWriter = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = _003C_003Eu__1;
							_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							goto IL_00e8;
						}
						awaiter = task.ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
					}
					awaiter.GetResult();
					awaiter = jsonTextWriter.WriteEscapedStringAsync(value.OriginalString, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_003C_003E1__state = 1);
						_003C_003Eu__1 = awaiter;
						_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_00e8;
					IL_00e8:
					awaiter.GetResult();
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

		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CDoWriteCommentAsync_003Ed__114 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextWriter _003C_003E4__this;

			public CancellationToken cancellationToken;

			[Nullable(0)]
			public string text;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextWriter jsonTextWriter = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					default:
						awaiter = jsonTextWriter.InternalWriteCommentAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0087;
					case 0:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0087;
					case 1:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_00fb;
					case 2:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0179;
					case 3:
						{
							awaiter = _003C_003Eu__1;
							_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							break;
						}
						IL_00fb:
						awaiter.GetResult();
						awaiter = jsonTextWriter._writer.WriteAsync(text ?? string.Empty, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 2);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0179;
						IL_0087:
						awaiter.GetResult();
						awaiter = jsonTextWriter._writer.WriteAsync("/*", cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 1);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_00fb;
						IL_0179:
						awaiter.GetResult();
						awaiter = jsonTextWriter._writer.WriteAsync("*/", cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 3);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						break;
					}
					awaiter.GetResult();
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

		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CDoWriteRawValueAsync_003Ed__120 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public Task task;

			[Nullable(0)]
			public JsonTextWriter _003C_003E4__this;

			[Nullable(0)]
			public string json;

			public CancellationToken cancellationToken;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextWriter jsonTextWriter = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = _003C_003Eu__1;
							_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							goto IL_00e2;
						}
						awaiter = task.ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
					}
					else
					{
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
					}
					awaiter.GetResult();
					awaiter = jsonTextWriter.WriteRawAsync(json, cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_003C_003E1__state = 1);
						_003C_003Eu__1 = awaiter;
						_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_00e2;
					IL_00e2:
					awaiter.GetResult();
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

		private readonly bool _safeAsync;

		private const int IndentCharBufferSize = 12;

		private readonly TextWriter _writer;

		[Nullable(2)]
		private Base64Encoder _base64Encoder;

		private char _indentChar;

		private int _indentation;

		private char _quoteChar;

		private bool _quoteName;

		[Nullable(2)]
		private bool[] _charEscapeFlags;

		[Nullable(2)]
		private char[] _writeBuffer;

		[Nullable(2)]
		private IArrayPool<char> _arrayPool;

		[Nullable(2)]
		private char[] _indentChars;

		private Base64Encoder Base64Encoder
		{
			get
			{
				if (_base64Encoder == null)
				{
					_base64Encoder = new Base64Encoder(_writer);
				}
				return _base64Encoder;
			}
		}

		[Nullable(2)]
		public IArrayPool<char> ArrayPool
		{
			[NullableContext(2)]
			get
			{
				return _arrayPool;
			}
			[NullableContext(2)]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				_arrayPool = value;
			}
		}

		public int Indentation
		{
			get
			{
				return _indentation;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("Indentation value must be greater than 0.");
				}
				_indentation = value;
			}
		}

		public char QuoteChar
		{
			get
			{
				return _quoteChar;
			}
			set
			{
				if (value != '"' && value != '\'')
				{
					throw new ArgumentException("Invalid JavaScript string quote character. Valid quote characters are ' and \".");
				}
				_quoteChar = value;
				UpdateCharEscapeFlags();
			}
		}

		public char IndentChar
		{
			get
			{
				return _indentChar;
			}
			set
			{
				if (value != _indentChar)
				{
					_indentChar = value;
					_indentChars = null;
				}
			}
		}

		public bool QuoteName
		{
			get
			{
				return _quoteName;
			}
			set
			{
				_quoteName = value;
			}
		}

		public override Task FlushAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.FlushAsync(cancellationToken);
			}
			return DoFlushAsync(cancellationToken);
		}

		internal Task DoFlushAsync(CancellationToken cancellationToken)
		{
			return cancellationToken.CancelIfRequestedAsync() ?? _writer.FlushAsync();
		}

		protected override Task WriteValueDelimiterAsync(CancellationToken cancellationToken)
		{
			if (!_safeAsync)
			{
				return base.WriteValueDelimiterAsync(cancellationToken);
			}
			return DoWriteValueDelimiterAsync(cancellationToken);
		}

		internal Task DoWriteValueDelimiterAsync(CancellationToken cancellationToken)
		{
			return _writer.WriteAsync(',', cancellationToken);
		}

		protected override Task WriteEndAsync(JsonToken token, CancellationToken cancellationToken)
		{
			if (!_safeAsync)
			{
				return base.WriteEndAsync(token, cancellationToken);
			}
			return DoWriteEndAsync(token, cancellationToken);
		}

		internal Task DoWriteEndAsync(JsonToken token, CancellationToken cancellationToken)
		{
			switch (token)
			{
			case JsonToken.EndObject:
				return _writer.WriteAsync('}', cancellationToken);
			case JsonToken.EndArray:
				return _writer.WriteAsync(']', cancellationToken);
			case JsonToken.EndConstructor:
				return _writer.WriteAsync(')', cancellationToken);
			default:
				throw JsonWriterException.Create(this, "Invalid JsonToken: " + token, null);
			}
		}

		public override Task CloseAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.CloseAsync(cancellationToken);
			}
			return DoCloseAsync(cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CDoCloseAsync_003Ed__8))]
		internal Task DoCloseAsync(CancellationToken cancellationToken)
		{
			_003CDoCloseAsync_003Ed__8 stateMachine = default(_003CDoCloseAsync_003Ed__8);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public override Task WriteEndAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteEndAsync(cancellationToken);
			}
			return WriteEndInternalAsync(cancellationToken);
		}

		protected override Task WriteIndentAsync(CancellationToken cancellationToken)
		{
			if (!_safeAsync)
			{
				return base.WriteIndentAsync(cancellationToken);
			}
			return DoWriteIndentAsync(cancellationToken);
		}

		internal Task DoWriteIndentAsync(CancellationToken cancellationToken)
		{
			int num = base.Top * _indentation;
			int num2 = SetIndentChars();
			if (num <= 12)
			{
				return _writer.WriteAsync(_indentChars, 0, num2 + num, cancellationToken);
			}
			return WriteIndentAsync(num, num2, cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CWriteIndentAsync_003Ed__12))]
		private Task WriteIndentAsync(int currentIndentCount, int newLineLen, CancellationToken cancellationToken)
		{
			_003CWriteIndentAsync_003Ed__12 stateMachine = default(_003CWriteIndentAsync_003Ed__12);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.currentIndentCount = currentIndentCount;
			stateMachine.newLineLen = newLineLen;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		private Task WriteValueInternalAsync(JsonToken token, string value, CancellationToken cancellationToken)
		{
			Task task = InternalWriteValueAsync(token, cancellationToken);
			if (task.IsCompletedSucessfully())
			{
				return _writer.WriteAsync(value, cancellationToken);
			}
			return WriteValueInternalAsync(task, value, cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CWriteValueInternalAsync_003Ed__14))]
		private Task WriteValueInternalAsync(Task task, string value, CancellationToken cancellationToken)
		{
			_003CWriteValueInternalAsync_003Ed__14 stateMachine = default(_003CWriteValueInternalAsync_003Ed__14);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.task = task;
			stateMachine.value = value;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		protected override Task WriteIndentSpaceAsync(CancellationToken cancellationToken)
		{
			if (!_safeAsync)
			{
				return base.WriteIndentSpaceAsync(cancellationToken);
			}
			return DoWriteIndentSpaceAsync(cancellationToken);
		}

		internal Task DoWriteIndentSpaceAsync(CancellationToken cancellationToken)
		{
			return _writer.WriteAsync(' ', cancellationToken);
		}

		public override Task WriteRawAsync([Nullable(2)] string json, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteRawAsync(json, cancellationToken);
			}
			return DoWriteRawAsync(json, cancellationToken);
		}

		internal Task DoWriteRawAsync([Nullable(2)] string json, CancellationToken cancellationToken)
		{
			return _writer.WriteAsync(json, cancellationToken);
		}

		public override Task WriteNullAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteNullAsync(cancellationToken);
			}
			return DoWriteNullAsync(cancellationToken);
		}

		internal Task DoWriteNullAsync(CancellationToken cancellationToken)
		{
			return WriteValueInternalAsync(JsonToken.Null, JsonConvert.Null, cancellationToken);
		}

		private Task WriteDigitsAsync(ulong uvalue, bool negative, CancellationToken cancellationToken)
		{
			if (uvalue <= 9 && !negative)
			{
				return _writer.WriteAsync((char)(48 + uvalue), cancellationToken);
			}
			int count = WriteNumberToBuffer(uvalue, negative);
			return _writer.WriteAsync(_writeBuffer, 0, count, cancellationToken);
		}

		private Task WriteIntegerValueAsync(ulong uvalue, bool negative, CancellationToken cancellationToken)
		{
			Task task = InternalWriteValueAsync(JsonToken.Integer, cancellationToken);
			if (task.IsCompletedSucessfully())
			{
				return WriteDigitsAsync(uvalue, negative, cancellationToken);
			}
			return WriteIntegerValueAsync(task, uvalue, negative, cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CWriteIntegerValueAsync_003Ed__23))]
		private Task WriteIntegerValueAsync(Task task, ulong uvalue, bool negative, CancellationToken cancellationToken)
		{
			_003CWriteIntegerValueAsync_003Ed__23 stateMachine = default(_003CWriteIntegerValueAsync_003Ed__23);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.task = task;
			stateMachine.uvalue = uvalue;
			stateMachine.negative = negative;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		internal Task WriteIntegerValueAsync(long value, CancellationToken cancellationToken)
		{
			bool flag = value < 0;
			if (flag)
			{
				value = -value;
			}
			return WriteIntegerValueAsync((ulong)value, flag, cancellationToken);
		}

		internal Task WriteIntegerValueAsync(ulong uvalue, CancellationToken cancellationToken)
		{
			return WriteIntegerValueAsync(uvalue, false, cancellationToken);
		}

		private Task WriteEscapedStringAsync(string value, bool quote, CancellationToken cancellationToken)
		{
			return JavaScriptUtils.WriteEscapedJavaScriptStringAsync(_writer, value, _quoteChar, quote, _charEscapeFlags, base.StringEscapeHandling, this, _writeBuffer, cancellationToken);
		}

		public override Task WritePropertyNameAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WritePropertyNameAsync(name, cancellationToken);
			}
			return DoWritePropertyNameAsync(name, cancellationToken);
		}

		internal Task DoWritePropertyNameAsync(string name, CancellationToken cancellationToken)
		{
			Task task = InternalWritePropertyNameAsync(name, cancellationToken);
			if (!task.IsCompletedSucessfully())
			{
				return DoWritePropertyNameAsync(task, name, cancellationToken);
			}
			task = WriteEscapedStringAsync(name, _quoteName, cancellationToken);
			if (task.IsCompletedSucessfully())
			{
				return _writer.WriteAsync(':', cancellationToken);
			}
			return JavaScriptUtils.WriteCharAsync(task, _writer, ':', cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CDoWritePropertyNameAsync_003Ed__29))]
		private Task DoWritePropertyNameAsync(Task task, string name, CancellationToken cancellationToken)
		{
			_003CDoWritePropertyNameAsync_003Ed__29 stateMachine = default(_003CDoWritePropertyNameAsync_003Ed__29);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.task = task;
			stateMachine.name = name;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public override Task WritePropertyNameAsync(string name, bool escape, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WritePropertyNameAsync(name, escape, cancellationToken);
			}
			return DoWritePropertyNameAsync(name, escape, cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CDoWritePropertyNameAsync_003Ed__31))]
		internal Task DoWritePropertyNameAsync(string name, bool escape, CancellationToken cancellationToken)
		{
			_003CDoWritePropertyNameAsync_003Ed__31 stateMachine = default(_003CDoWritePropertyNameAsync_003Ed__31);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.name = name;
			stateMachine.escape = escape;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public override Task WriteStartArrayAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteStartArrayAsync(cancellationToken);
			}
			return DoWriteStartArrayAsync(cancellationToken);
		}

		internal Task DoWriteStartArrayAsync(CancellationToken cancellationToken)
		{
			Task task = InternalWriteStartAsync(JsonToken.StartArray, JsonContainerType.Array, cancellationToken);
			if (task.IsCompletedSucessfully())
			{
				return _writer.WriteAsync('[', cancellationToken);
			}
			return DoWriteStartArrayAsync(task, cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CDoWriteStartArrayAsync_003Ed__34))]
		internal Task DoWriteStartArrayAsync(Task task, CancellationToken cancellationToken)
		{
			_003CDoWriteStartArrayAsync_003Ed__34 stateMachine = default(_003CDoWriteStartArrayAsync_003Ed__34);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.task = task;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public override Task WriteStartObjectAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteStartObjectAsync(cancellationToken);
			}
			return DoWriteStartObjectAsync(cancellationToken);
		}

		internal Task DoWriteStartObjectAsync(CancellationToken cancellationToken)
		{
			Task task = InternalWriteStartAsync(JsonToken.StartObject, JsonContainerType.Object, cancellationToken);
			if (task.IsCompletedSucessfully())
			{
				return _writer.WriteAsync('{', cancellationToken);
			}
			return DoWriteStartObjectAsync(task, cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CDoWriteStartObjectAsync_003Ed__37))]
		internal Task DoWriteStartObjectAsync(Task task, CancellationToken cancellationToken)
		{
			_003CDoWriteStartObjectAsync_003Ed__37 stateMachine = default(_003CDoWriteStartObjectAsync_003Ed__37);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.task = task;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public override Task WriteStartConstructorAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteStartConstructorAsync(name, cancellationToken);
			}
			return DoWriteStartConstructorAsync(name, cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CDoWriteStartConstructorAsync_003Ed__39))]
		internal Task DoWriteStartConstructorAsync(string name, CancellationToken cancellationToken)
		{
			_003CDoWriteStartConstructorAsync_003Ed__39 stateMachine = default(_003CDoWriteStartConstructorAsync_003Ed__39);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.name = name;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public override Task WriteUndefinedAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteUndefinedAsync(cancellationToken);
			}
			return DoWriteUndefinedAsync(cancellationToken);
		}

		internal Task DoWriteUndefinedAsync(CancellationToken cancellationToken)
		{
			Task task = InternalWriteValueAsync(JsonToken.Undefined, cancellationToken);
			if (task.IsCompletedSucessfully())
			{
				return _writer.WriteAsync(JsonConvert.Undefined, cancellationToken);
			}
			return DoWriteUndefinedAsync(task, cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CDoWriteUndefinedAsync_003Ed__42))]
		private Task DoWriteUndefinedAsync(Task task, CancellationToken cancellationToken)
		{
			_003CDoWriteUndefinedAsync_003Ed__42 stateMachine = default(_003CDoWriteUndefinedAsync_003Ed__42);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.task = task;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public override Task WriteWhitespaceAsync(string ws, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteWhitespaceAsync(ws, cancellationToken);
			}
			return DoWriteWhitespaceAsync(ws, cancellationToken);
		}

		internal Task DoWriteWhitespaceAsync(string ws, CancellationToken cancellationToken)
		{
			InternalWriteWhitespace(ws);
			return _writer.WriteAsync(ws, cancellationToken);
		}

		public override Task WriteValueAsync(bool value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(bool value, CancellationToken cancellationToken)
		{
			return WriteValueInternalAsync(JsonToken.Boolean, JsonConvert.ToString(value), cancellationToken);
		}

		public override Task WriteValueAsync(bool? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(bool? value, CancellationToken cancellationToken)
		{
			if (value.HasValue)
			{
				return DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return DoWriteNullAsync(cancellationToken);
		}

		public override Task WriteValueAsync(byte value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return WriteIntegerValueAsync(value, cancellationToken);
		}

		public override Task WriteValueAsync(byte? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(byte? value, CancellationToken cancellationToken)
		{
			if (value.HasValue)
			{
				return WriteIntegerValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return DoWriteNullAsync(cancellationToken);
		}

		public override Task WriteValueAsync([Nullable(2)] byte[] value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			if (value != null)
			{
				return WriteValueNonNullAsync(value, cancellationToken);
			}
			return WriteNullAsync(cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CWriteValueNonNullAsync_003Ed__53))]
		internal Task WriteValueNonNullAsync(byte[] value, CancellationToken cancellationToken)
		{
			_003CWriteValueNonNullAsync_003Ed__53 stateMachine = default(_003CWriteValueNonNullAsync_003Ed__53);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.value = value;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public override Task WriteValueAsync(char value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(char value, CancellationToken cancellationToken)
		{
			return WriteValueInternalAsync(JsonToken.String, JsonConvert.ToString(value), cancellationToken);
		}

		public override Task WriteValueAsync(char? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(char? value, CancellationToken cancellationToken)
		{
			if (value.HasValue)
			{
				return DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return DoWriteNullAsync(cancellationToken);
		}

		public override Task WriteValueAsync(DateTime value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CDoWriteValueAsync_003Ed__59))]
		internal Task DoWriteValueAsync(DateTime value, CancellationToken cancellationToken)
		{
			_003CDoWriteValueAsync_003Ed__59 stateMachine = default(_003CDoWriteValueAsync_003Ed__59);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.value = value;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public override Task WriteValueAsync(DateTime? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(DateTime? value, CancellationToken cancellationToken)
		{
			if (value.HasValue)
			{
				return DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return DoWriteNullAsync(cancellationToken);
		}

		public override Task WriteValueAsync(DateTimeOffset value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CDoWriteValueAsync_003Ed__63))]
		internal Task DoWriteValueAsync(DateTimeOffset value, CancellationToken cancellationToken)
		{
			_003CDoWriteValueAsync_003Ed__63 stateMachine = default(_003CDoWriteValueAsync_003Ed__63);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.value = value;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public override Task WriteValueAsync(DateTimeOffset? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(DateTimeOffset? value, CancellationToken cancellationToken)
		{
			if (value.HasValue)
			{
				return DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return DoWriteNullAsync(cancellationToken);
		}

		public override Task WriteValueAsync(decimal value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(decimal value, CancellationToken cancellationToken)
		{
			return WriteValueInternalAsync(JsonToken.Float, JsonConvert.ToString(value), cancellationToken);
		}

		public override Task WriteValueAsync(decimal? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(decimal? value, CancellationToken cancellationToken)
		{
			if (value.HasValue)
			{
				return DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return DoWriteNullAsync(cancellationToken);
		}

		public override Task WriteValueAsync(double value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return WriteValueAsync(value, false, cancellationToken);
		}

		internal Task WriteValueAsync(double value, bool nullable, CancellationToken cancellationToken)
		{
			return WriteValueInternalAsync(JsonToken.Float, JsonConvert.ToString(value, base.FloatFormatHandling, QuoteChar, nullable), cancellationToken);
		}

		public override Task WriteValueAsync(double? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			if (!value.HasValue)
			{
				return WriteNullAsync(cancellationToken);
			}
			return WriteValueAsync(value.GetValueOrDefault(), true, cancellationToken);
		}

		public override Task WriteValueAsync(float value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return WriteValueAsync(value, false, cancellationToken);
		}

		internal Task WriteValueAsync(float value, bool nullable, CancellationToken cancellationToken)
		{
			return WriteValueInternalAsync(JsonToken.Float, JsonConvert.ToString(value, base.FloatFormatHandling, QuoteChar, nullable), cancellationToken);
		}

		public override Task WriteValueAsync(float? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			if (!value.HasValue)
			{
				return WriteNullAsync(cancellationToken);
			}
			return WriteValueAsync(value.GetValueOrDefault(), true, cancellationToken);
		}

		public override Task WriteValueAsync(Guid value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CDoWriteValueAsync_003Ed__77))]
		internal Task DoWriteValueAsync(Guid value, CancellationToken cancellationToken)
		{
			_003CDoWriteValueAsync_003Ed__77 stateMachine = default(_003CDoWriteValueAsync_003Ed__77);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.value = value;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public override Task WriteValueAsync(Guid? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(Guid? value, CancellationToken cancellationToken)
		{
			if (value.HasValue)
			{
				return DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return DoWriteNullAsync(cancellationToken);
		}

		public override Task WriteValueAsync(int value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return WriteIntegerValueAsync(value, cancellationToken);
		}

		public override Task WriteValueAsync(int? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(int? value, CancellationToken cancellationToken)
		{
			if (value.HasValue)
			{
				return WriteIntegerValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return DoWriteNullAsync(cancellationToken);
		}

		public override Task WriteValueAsync(long value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return WriteIntegerValueAsync(value, cancellationToken);
		}

		public override Task WriteValueAsync(long? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(long? value, CancellationToken cancellationToken)
		{
			if (value.HasValue)
			{
				return WriteIntegerValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return DoWriteNullAsync(cancellationToken);
		}

		internal Task WriteValueAsync(BigInteger value, CancellationToken cancellationToken)
		{
			return WriteValueInternalAsync(JsonToken.Integer, value.ToString(CultureInfo.InvariantCulture), cancellationToken);
		}

		public override Task WriteValueAsync([Nullable(2)] object value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (_safeAsync)
			{
				if (value == null)
				{
					return WriteNullAsync(cancellationToken);
				}
				if (value is BigInteger)
				{
					BigInteger value2 = (BigInteger)value;
					return WriteValueAsync(value2, cancellationToken);
				}
				return JsonWriter.WriteValueAsync(this, ConvertUtils.GetTypeCode(value.GetType()), value, cancellationToken);
			}
			return base.WriteValueAsync(value, cancellationToken);
		}

		[CLSCompliant(false)]
		public override Task WriteValueAsync(sbyte value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return WriteIntegerValueAsync(value, cancellationToken);
		}

		[CLSCompliant(false)]
		public override Task WriteValueAsync(sbyte? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(sbyte? value, CancellationToken cancellationToken)
		{
			if (value.HasValue)
			{
				return WriteIntegerValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return DoWriteNullAsync(cancellationToken);
		}

		public override Task WriteValueAsync(short value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return WriteIntegerValueAsync(value, cancellationToken);
		}

		public override Task WriteValueAsync(short? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(short? value, CancellationToken cancellationToken)
		{
			if (value.HasValue)
			{
				return WriteIntegerValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return DoWriteNullAsync(cancellationToken);
		}

		public override Task WriteValueAsync([Nullable(2)] string value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync([Nullable(2)] string value, CancellationToken cancellationToken)
		{
			Task task = InternalWriteValueAsync(JsonToken.String, cancellationToken);
			if (task.IsCompletedSucessfully())
			{
				if (value != null)
				{
					return WriteEscapedStringAsync(value, true, cancellationToken);
				}
				return _writer.WriteAsync(JsonConvert.Null, cancellationToken);
			}
			return DoWriteValueAsync(task, value, cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CDoWriteValueAsync_003Ed__96))]
		private Task DoWriteValueAsync(Task task, [Nullable(2)] string value, CancellationToken cancellationToken)
		{
			_003CDoWriteValueAsync_003Ed__96 stateMachine = default(_003CDoWriteValueAsync_003Ed__96);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.task = task;
			stateMachine.value = value;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public override Task WriteValueAsync(TimeSpan value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CDoWriteValueAsync_003Ed__98))]
		internal Task DoWriteValueAsync(TimeSpan value, CancellationToken cancellationToken)
		{
			_003CDoWriteValueAsync_003Ed__98 stateMachine = default(_003CDoWriteValueAsync_003Ed__98);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.value = value;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public override Task WriteValueAsync(TimeSpan? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(TimeSpan? value, CancellationToken cancellationToken)
		{
			if (value.HasValue)
			{
				return DoWriteValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return DoWriteNullAsync(cancellationToken);
		}

		[CLSCompliant(false)]
		public override Task WriteValueAsync(uint value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return WriteIntegerValueAsync(value, cancellationToken);
		}

		[CLSCompliant(false)]
		public override Task WriteValueAsync(uint? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(uint? value, CancellationToken cancellationToken)
		{
			if (value.HasValue)
			{
				return WriteIntegerValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return DoWriteNullAsync(cancellationToken);
		}

		[CLSCompliant(false)]
		public override Task WriteValueAsync(ulong value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return WriteIntegerValueAsync(value, cancellationToken);
		}

		[CLSCompliant(false)]
		public override Task WriteValueAsync(ulong? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(ulong? value, CancellationToken cancellationToken)
		{
			if (value.HasValue)
			{
				return WriteIntegerValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return DoWriteNullAsync(cancellationToken);
		}

		public override Task WriteValueAsync([Nullable(2)] Uri value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			if (!(value == null))
			{
				return WriteValueNotNullAsync(value, cancellationToken);
			}
			return WriteNullAsync(cancellationToken);
		}

		internal Task WriteValueNotNullAsync(Uri value, CancellationToken cancellationToken)
		{
			Task task = InternalWriteValueAsync(JsonToken.String, cancellationToken);
			if (task.IsCompletedSucessfully())
			{
				return WriteEscapedStringAsync(value.OriginalString, true, cancellationToken);
			}
			return WriteValueNotNullAsync(task, value, cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CWriteValueNotNullAsync_003Ed__109))]
		internal Task WriteValueNotNullAsync(Task task, Uri value, CancellationToken cancellationToken)
		{
			_003CWriteValueNotNullAsync_003Ed__109 stateMachine = default(_003CWriteValueNotNullAsync_003Ed__109);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.task = task;
			stateMachine.value = value;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[CLSCompliant(false)]
		public override Task WriteValueAsync(ushort value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return WriteIntegerValueAsync(value, cancellationToken);
		}

		[CLSCompliant(false)]
		public override Task WriteValueAsync(ushort? value, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteValueAsync(value, cancellationToken);
			}
			return DoWriteValueAsync(value, cancellationToken);
		}

		internal Task DoWriteValueAsync(ushort? value, CancellationToken cancellationToken)
		{
			if (value.HasValue)
			{
				return WriteIntegerValueAsync(value.GetValueOrDefault(), cancellationToken);
			}
			return DoWriteNullAsync(cancellationToken);
		}

		public override Task WriteCommentAsync([Nullable(2)] string text, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteCommentAsync(text, cancellationToken);
			}
			return DoWriteCommentAsync(text, cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CDoWriteCommentAsync_003Ed__114))]
		internal Task DoWriteCommentAsync([Nullable(2)] string text, CancellationToken cancellationToken)
		{
			_003CDoWriteCommentAsync_003Ed__114 stateMachine = default(_003CDoWriteCommentAsync_003Ed__114);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.text = text;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public override Task WriteEndArrayAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteEndArrayAsync(cancellationToken);
			}
			return InternalWriteEndAsync(JsonContainerType.Array, cancellationToken);
		}

		public override Task WriteEndConstructorAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteEndConstructorAsync(cancellationToken);
			}
			return InternalWriteEndAsync(JsonContainerType.Constructor, cancellationToken);
		}

		public override Task WriteEndObjectAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteEndObjectAsync(cancellationToken);
			}
			return InternalWriteEndAsync(JsonContainerType.Object, cancellationToken);
		}

		public override Task WriteRawValueAsync([Nullable(2)] string json, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.WriteRawValueAsync(json, cancellationToken);
			}
			return DoWriteRawValueAsync(json, cancellationToken);
		}

		internal Task DoWriteRawValueAsync([Nullable(2)] string json, CancellationToken cancellationToken)
		{
			UpdateScopeWithFinishedValue();
			Task task = AutoCompleteAsync(JsonToken.Undefined, cancellationToken);
			if (task.IsCompletedSucessfully())
			{
				return WriteRawAsync(json, cancellationToken);
			}
			return DoWriteRawValueAsync(task, json, cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CDoWriteRawValueAsync_003Ed__120))]
		private Task DoWriteRawValueAsync(Task task, [Nullable(2)] string json, CancellationToken cancellationToken)
		{
			_003CDoWriteRawValueAsync_003Ed__120 stateMachine = default(_003CDoWriteRawValueAsync_003Ed__120);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.task = task;
			stateMachine.json = json;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		internal char[] EnsureWriteBuffer(int length, int copyTo)
		{
			if (length < 35)
			{
				length = 35;
			}
			char[] writeBuffer = _writeBuffer;
			if (writeBuffer == null)
			{
				return _writeBuffer = BufferUtils.RentBuffer(_arrayPool, length);
			}
			if (writeBuffer.Length >= length)
			{
				return writeBuffer;
			}
			char[] array = BufferUtils.RentBuffer(_arrayPool, length);
			if (copyTo != 0)
			{
				Array.Copy(writeBuffer, array, copyTo);
			}
			BufferUtils.ReturnBuffer(_arrayPool, writeBuffer);
			_writeBuffer = array;
			return array;
		}

		public JsonTextWriter(TextWriter textWriter)
		{
			if (textWriter == null)
			{
				throw new ArgumentNullException("textWriter");
			}
			_writer = textWriter;
			_quoteChar = '"';
			_quoteName = true;
			_indentChar = ' ';
			_indentation = 2;
			UpdateCharEscapeFlags();
			_safeAsync = GetType() == typeof(JsonTextWriter);
		}

		public override void Flush()
		{
			_writer.Flush();
		}

		public override void Close()
		{
			base.Close();
			CloseBufferAndWriter();
		}

		private void CloseBufferAndWriter()
		{
			if (_writeBuffer != null)
			{
				BufferUtils.ReturnBuffer(_arrayPool, _writeBuffer);
				_writeBuffer = null;
			}
			if (base.CloseOutput)
			{
				TextWriter writer = _writer;
				if (writer != null)
				{
					writer.Close();
				}
			}
		}

		public override void WriteStartObject()
		{
			InternalWriteStart(JsonToken.StartObject, JsonContainerType.Object);
			_writer.Write('{');
		}

		public override void WriteStartArray()
		{
			InternalWriteStart(JsonToken.StartArray, JsonContainerType.Array);
			_writer.Write('[');
		}

		public override void WriteStartConstructor(string name)
		{
			InternalWriteStart(JsonToken.StartConstructor, JsonContainerType.Constructor);
			_writer.Write("new ");
			_writer.Write(name);
			_writer.Write('(');
		}

		protected override void WriteEnd(JsonToken token)
		{
			switch (token)
			{
			case JsonToken.EndObject:
				_writer.Write('}');
				break;
			case JsonToken.EndArray:
				_writer.Write(']');
				break;
			case JsonToken.EndConstructor:
				_writer.Write(')');
				break;
			default:
				throw JsonWriterException.Create(this, "Invalid JsonToken: " + token, null);
			}
		}

		public override void WritePropertyName(string name)
		{
			InternalWritePropertyName(name);
			WriteEscapedString(name, _quoteName);
			_writer.Write(':');
		}

		public override void WritePropertyName(string name, bool escape)
		{
			InternalWritePropertyName(name);
			if (escape)
			{
				WriteEscapedString(name, _quoteName);
			}
			else
			{
				if (_quoteName)
				{
					_writer.Write(_quoteChar);
				}
				_writer.Write(name);
				if (_quoteName)
				{
					_writer.Write(_quoteChar);
				}
			}
			_writer.Write(':');
		}

		internal override void OnStringEscapeHandlingChanged()
		{
			UpdateCharEscapeFlags();
		}

		private void UpdateCharEscapeFlags()
		{
			_charEscapeFlags = JavaScriptUtils.GetCharEscapeFlags(base.StringEscapeHandling, _quoteChar);
		}

		protected override void WriteIndent()
		{
			int num = base.Top * _indentation;
			int num2 = SetIndentChars();
			_writer.Write(_indentChars, 0, num2 + Math.Min(num, 12));
			while ((num -= 12) > 0)
			{
				_writer.Write(_indentChars, num2, Math.Min(num, 12));
			}
		}

		private int SetIndentChars()
		{
			string newLine = _writer.NewLine;
			int length = newLine.Length;
			bool flag = _indentChars != null && _indentChars.Length == 12 + length;
			if (flag)
			{
				for (int i = 0; i != length; i++)
				{
					if (newLine[i] != _indentChars[i])
					{
						flag = false;
						break;
					}
				}
			}
			if (!flag)
			{
				_indentChars = (newLine + new string(_indentChar, 12)).ToCharArray();
			}
			return length;
		}

		protected override void WriteValueDelimiter()
		{
			_writer.Write(',');
		}

		protected override void WriteIndentSpace()
		{
			_writer.Write(' ');
		}

		private void WriteValueInternal(string value, JsonToken token)
		{
			_writer.Write(value);
		}

		[NullableContext(2)]
		public override void WriteValue(object value)
		{
			if (value is BigInteger)
			{
				BigInteger bigInteger = (BigInteger)value;
				InternalWriteValue(JsonToken.Integer);
				WriteValueInternal(bigInteger.ToString(CultureInfo.InvariantCulture), JsonToken.String);
			}
			else
			{
				base.WriteValue(value);
			}
		}

		public override void WriteNull()
		{
			InternalWriteValue(JsonToken.Null);
			WriteValueInternal(JsonConvert.Null, JsonToken.Null);
		}

		public override void WriteUndefined()
		{
			InternalWriteValue(JsonToken.Undefined);
			WriteValueInternal(JsonConvert.Undefined, JsonToken.Undefined);
		}

		[NullableContext(2)]
		public override void WriteRaw(string json)
		{
			InternalWriteRaw();
			_writer.Write(json);
		}

		[NullableContext(2)]
		public override void WriteValue(string value)
		{
			InternalWriteValue(JsonToken.String);
			if (value == null)
			{
				WriteValueInternal(JsonConvert.Null, JsonToken.Null);
			}
			else
			{
				WriteEscapedString(value, true);
			}
		}

		private void WriteEscapedString(string value, bool quote)
		{
			EnsureWriteBuffer();
			JavaScriptUtils.WriteEscapedJavaScriptString(_writer, value, _quoteChar, quote, _charEscapeFlags, base.StringEscapeHandling, _arrayPool, ref _writeBuffer);
		}

		public override void WriteValue(int value)
		{
			InternalWriteValue(JsonToken.Integer);
			WriteIntegerValue(value);
		}

		[CLSCompliant(false)]
		public override void WriteValue(uint value)
		{
			InternalWriteValue(JsonToken.Integer);
			WriteIntegerValue(value);
		}

		public override void WriteValue(long value)
		{
			InternalWriteValue(JsonToken.Integer);
			WriteIntegerValue(value);
		}

		[CLSCompliant(false)]
		public override void WriteValue(ulong value)
		{
			InternalWriteValue(JsonToken.Integer);
			WriteIntegerValue(value, false);
		}

		public override void WriteValue(float value)
		{
			InternalWriteValue(JsonToken.Float);
			WriteValueInternal(JsonConvert.ToString(value, base.FloatFormatHandling, QuoteChar, false), JsonToken.Float);
		}

		public override void WriteValue(float? value)
		{
			if (!value.HasValue)
			{
				WriteNull();
				return;
			}
			InternalWriteValue(JsonToken.Float);
			WriteValueInternal(JsonConvert.ToString(value.GetValueOrDefault(), base.FloatFormatHandling, QuoteChar, true), JsonToken.Float);
		}

		public override void WriteValue(double value)
		{
			InternalWriteValue(JsonToken.Float);
			WriteValueInternal(JsonConvert.ToString(value, base.FloatFormatHandling, QuoteChar, false), JsonToken.Float);
		}

		public override void WriteValue(double? value)
		{
			if (!value.HasValue)
			{
				WriteNull();
				return;
			}
			InternalWriteValue(JsonToken.Float);
			WriteValueInternal(JsonConvert.ToString(value.GetValueOrDefault(), base.FloatFormatHandling, QuoteChar, true), JsonToken.Float);
		}

		public override void WriteValue(bool value)
		{
			InternalWriteValue(JsonToken.Boolean);
			WriteValueInternal(JsonConvert.ToString(value), JsonToken.Boolean);
		}

		public override void WriteValue(short value)
		{
			InternalWriteValue(JsonToken.Integer);
			WriteIntegerValue(value);
		}

		[CLSCompliant(false)]
		public override void WriteValue(ushort value)
		{
			InternalWriteValue(JsonToken.Integer);
			WriteIntegerValue(value);
		}

		public override void WriteValue(char value)
		{
			InternalWriteValue(JsonToken.String);
			WriteValueInternal(JsonConvert.ToString(value), JsonToken.String);
		}

		public override void WriteValue(byte value)
		{
			InternalWriteValue(JsonToken.Integer);
			WriteIntegerValue(value);
		}

		[CLSCompliant(false)]
		public override void WriteValue(sbyte value)
		{
			InternalWriteValue(JsonToken.Integer);
			WriteIntegerValue(value);
		}

		public override void WriteValue(decimal value)
		{
			InternalWriteValue(JsonToken.Float);
			WriteValueInternal(JsonConvert.ToString(value), JsonToken.Float);
		}

		public override void WriteValue(DateTime value)
		{
			InternalWriteValue(JsonToken.Date);
			value = DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
			if (StringUtils.IsNullOrEmpty(base.DateFormatString))
			{
				int count = WriteValueToBuffer(value);
				_writer.Write(_writeBuffer, 0, count);
			}
			else
			{
				_writer.Write(_quoteChar);
				_writer.Write(value.ToString(base.DateFormatString, base.Culture));
				_writer.Write(_quoteChar);
			}
		}

		private int WriteValueToBuffer(DateTime value)
		{
			EnsureWriteBuffer();
			int start = 0;
			_writeBuffer[start++] = _quoteChar;
			start = DateTimeUtils.WriteDateTimeString(_writeBuffer, start, value, null, value.Kind, base.DateFormatHandling);
			_writeBuffer[start++] = _quoteChar;
			return start;
		}

		[NullableContext(2)]
		public override void WriteValue(byte[] value)
		{
			if (value == null)
			{
				WriteNull();
				return;
			}
			InternalWriteValue(JsonToken.Bytes);
			_writer.Write(_quoteChar);
			Base64Encoder.Encode(value, 0, value.Length);
			Base64Encoder.Flush();
			_writer.Write(_quoteChar);
		}

		public override void WriteValue(DateTimeOffset value)
		{
			InternalWriteValue(JsonToken.Date);
			if (StringUtils.IsNullOrEmpty(base.DateFormatString))
			{
				int count = WriteValueToBuffer(value);
				_writer.Write(_writeBuffer, 0, count);
			}
			else
			{
				_writer.Write(_quoteChar);
				_writer.Write(value.ToString(base.DateFormatString, base.Culture));
				_writer.Write(_quoteChar);
			}
		}

		private int WriteValueToBuffer(DateTimeOffset value)
		{
			EnsureWriteBuffer();
			int start = 0;
			_writeBuffer[start++] = _quoteChar;
			start = DateTimeUtils.WriteDateTimeString(_writeBuffer, start, (base.DateFormatHandling == DateFormatHandling.IsoDateFormat) ? value.DateTime : value.UtcDateTime, value.Offset, DateTimeKind.Local, base.DateFormatHandling);
			_writeBuffer[start++] = _quoteChar;
			return start;
		}

		public override void WriteValue(Guid value)
		{
			InternalWriteValue(JsonToken.String);
			string value2 = value.ToString("D", CultureInfo.InvariantCulture);
			_writer.Write(_quoteChar);
			_writer.Write(value2);
			_writer.Write(_quoteChar);
		}

		public override void WriteValue(TimeSpan value)
		{
			InternalWriteValue(JsonToken.String);
			string value2 = value.ToString(null, CultureInfo.InvariantCulture);
			_writer.Write(_quoteChar);
			_writer.Write(value2);
			_writer.Write(_quoteChar);
		}

		[NullableContext(2)]
		public override void WriteValue(Uri value)
		{
			if (value == null)
			{
				WriteNull();
				return;
			}
			InternalWriteValue(JsonToken.String);
			WriteEscapedString(value.OriginalString, true);
		}

		[NullableContext(2)]
		public override void WriteComment(string text)
		{
			InternalWriteComment();
			_writer.Write("/*");
			_writer.Write(text);
			_writer.Write("*/");
		}

		public override void WriteWhitespace(string ws)
		{
			InternalWriteWhitespace(ws);
			_writer.Write(ws);
		}

		private void EnsureWriteBuffer()
		{
			if (_writeBuffer == null)
			{
				_writeBuffer = BufferUtils.RentBuffer(_arrayPool, 35);
			}
		}

		private void WriteIntegerValue(long value)
		{
			if (value >= 0 && value <= 9)
			{
				_writer.Write((char)(48 + value));
				return;
			}
			bool flag = value < 0;
			WriteIntegerValue((ulong)(flag ? (-value) : value), flag);
		}

		private void WriteIntegerValue(ulong value, bool negative)
		{
			if (!negative && value <= 9)
			{
				_writer.Write((char)(48 + value));
				return;
			}
			int count = WriteNumberToBuffer(value, negative);
			_writer.Write(_writeBuffer, 0, count);
		}

		private int WriteNumberToBuffer(ulong value, bool negative)
		{
			if (value <= uint.MaxValue)
			{
				return WriteNumberToBuffer((uint)value, negative);
			}
			EnsureWriteBuffer();
			int num = MathUtils.IntLength(value);
			if (negative)
			{
				num++;
				_writeBuffer[0] = '-';
			}
			int num2 = num;
			do
			{
				ulong num3 = value / 10uL;
				ulong num4 = value - num3 * 10;
				_writeBuffer[--num2] = (char)(48 + num4);
				value = num3;
			}
			while (value != 0L);
			return num;
		}

		private void WriteIntegerValue(int value)
		{
			if (value >= 0 && value <= 9)
			{
				_writer.Write((char)(48 + value));
				return;
			}
			bool flag = value < 0;
			WriteIntegerValue((uint)(flag ? (-value) : value), flag);
		}

		private void WriteIntegerValue(uint value, bool negative)
		{
			if (!negative && value <= 9)
			{
				_writer.Write((char)(48 + value));
				return;
			}
			int count = WriteNumberToBuffer(value, negative);
			_writer.Write(_writeBuffer, 0, count);
		}

		private int WriteNumberToBuffer(uint value, bool negative)
		{
			EnsureWriteBuffer();
			int num = MathUtils.IntLength(value);
			if (negative)
			{
				num++;
				_writeBuffer[0] = '-';
			}
			int num2 = num;
			do
			{
				uint num3 = value / 10u;
				uint num4 = value - num3 * 10;
				_writeBuffer[--num2] = (char)(48 + num4);
				value = num3;
			}
			while (value != 0);
			return num;
		}
	}
}
