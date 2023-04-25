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
	public class JsonTextReader : JsonReader, IJsonLineInfo
	{
		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CDoReadAsync_003Ed__3 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			[Nullable(0)]
			public AsyncTaskMethodBuilder<bool> _003C_003Et__builder;

			[Nullable(0)]
			public Task<bool> task;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				bool result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = _003C_003Eu__1;
							_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
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
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
					}
					if (!awaiter.GetResult())
					{
						awaiter = jsonTextReader.DoReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 1);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_00e7;
					}
					result = true;
					goto end_IL_000e;
					IL_00e7:
					result = awaiter.GetResult();
					end_IL_000e:;
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult(result);
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
		private struct _003CParsePostValueAsync_003Ed__4 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			[Nullable(0)]
			public AsyncTaskMethodBuilder<bool> _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			public bool ignoreComments;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__2;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				bool result;
				try
				{
					char c;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter2;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					default:
						while (true)
						{
							c = jsonTextReader._chars[jsonTextReader._charPos];
							switch (c)
							{
							case '\0':
								break;
							case '}':
								jsonTextReader._charPos++;
								jsonTextReader.SetToken(JsonToken.EndObject);
								result = true;
								goto end_IL_000f;
							case ']':
								jsonTextReader._charPos++;
								jsonTextReader.SetToken(JsonToken.EndArray);
								result = true;
								goto end_IL_000f;
							case ')':
								jsonTextReader._charPos++;
								jsonTextReader.SetToken(JsonToken.EndConstructor);
								result = true;
								goto end_IL_000f;
							case '/':
								goto end_IL_0020;
							case ',':
								jsonTextReader._charPos++;
								jsonTextReader.SetStateBasedOnCurrent();
								result = false;
								goto end_IL_000f;
							case '\t':
							case ' ':
								jsonTextReader._charPos++;
								continue;
							case '\r':
								goto IL_0256;
							case '\n':
								jsonTextReader.ProcessLineFeed();
								continue;
							default:
								goto IL_02d5;
							}
							if (jsonTextReader._charsUsed != jsonTextReader._charPos)
							{
								jsonTextReader._charPos++;
								continue;
							}
							goto IL_00b2;
							IL_02d5:
							if (char.IsWhiteSpace(c))
							{
								jsonTextReader._charPos++;
								continue;
							}
							goto IL_02f0;
							continue;
							end_IL_0020:
							break;
						}
						awaiter = jsonTextReader.ParseCommentAsync(!ignoreComments, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 1);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_020f;
					case 0:
						awaiter2 = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_011a;
					case 1:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_020f;
					case 2:
						{
							awaiter = _003C_003Eu__2;
							_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							goto IL_02be;
						}
						IL_02f0:
						if (jsonTextReader.SupportMultipleContent && jsonTextReader.Depth == 0)
						{
							jsonTextReader.SetStateBasedOnCurrent();
							result = false;
							break;
						}
						throw JsonReaderException.Create(jsonTextReader, "After parsing a value an unexpected character was encountered: {0}.".FormatWith(CultureInfo.InvariantCulture, c));
						IL_020f:
						awaiter.GetResult();
						if (!ignoreComments)
						{
							result = true;
							break;
						}
						goto default;
						IL_011a:
						if (awaiter2.GetResult() == 0)
						{
							jsonTextReader._currentState = State.Finished;
							result = false;
							break;
						}
						goto default;
						IL_02be:
						awaiter.GetResult();
						goto default;
						IL_0256:
						awaiter = jsonTextReader.ProcessCarriageReturnAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 2);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_02be;
						IL_00b2:
						awaiter2 = jsonTextReader.ReadDataAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_011a;
						end_IL_000f:
						break;
					}
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult(result);
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
		private struct _003CReadFromFinishedAsync_003Ed__5 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			[Nullable(0)]
			public AsyncTaskMethodBuilder<bool> _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__2;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				bool result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter2;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					default:
						awaiter2 = jsonTextReader.EnsureCharsAsync(0, false, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_0086;
					case 0:
						awaiter2 = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0086;
					case 1:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_00f9;
					case 2:
						{
							awaiter = _003C_003Eu__2;
							_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							goto IL_018f;
						}
						IL_018f:
						awaiter.GetResult();
						result = true;
						break;
						IL_0086:
						if (awaiter2.GetResult())
						{
							awaiter = jsonTextReader.EatWhitespaceAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_003C_003E1__state = 1);
								_003C_003Eu__2 = awaiter;
								_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_00f9;
						}
						jsonTextReader.SetToken(JsonToken.None);
						result = false;
						break;
						IL_00f9:
						awaiter.GetResult();
						if (jsonTextReader._isEndOfFile)
						{
							jsonTextReader.SetToken(JsonToken.None);
							result = false;
							break;
						}
						if (jsonTextReader._chars[jsonTextReader._charPos] == '/')
						{
							awaiter = jsonTextReader.ParseCommentAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_003C_003E1__state = 2);
								_003C_003Eu__2 = awaiter;
								_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_018f;
						}
						throw JsonReaderException.Create(jsonTextReader, "Additional text encountered after finished reading JSON content: {0}.".FormatWith(CultureInfo.InvariantCulture, jsonTextReader._chars[jsonTextReader._charPos]));
					}
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult(result);
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
		private struct _003CReadDataAsync_003Ed__7 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			[Nullable(0)]
			public AsyncTaskMethodBuilder<int> _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public bool append;

			public int charsRequired;

			public CancellationToken cancellationToken;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				int result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num == 0)
					{
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_00bb;
					}
					if (!jsonTextReader._isEndOfFile)
					{
						jsonTextReader.PrepareBufferForReadData(append, charsRequired);
						awaiter = jsonTextReader._reader.ReadAsync(jsonTextReader._chars, jsonTextReader._charsUsed, jsonTextReader._chars.Length - jsonTextReader._charsUsed - 1, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_00bb;
					}
					result = 0;
					goto end_IL_000e;
					IL_00bb:
					int result2 = awaiter.GetResult();
					jsonTextReader._charsUsed += result2;
					if (result2 == 0)
					{
						jsonTextReader._isEndOfFile = true;
					}
					jsonTextReader._chars[jsonTextReader._charsUsed] = '\0';
					result = result2;
					end_IL_000e:;
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult(result);
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
		private struct _003CParseValueAsync_003Ed__8 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			[Nullable(0)]
			public AsyncTaskMethodBuilder<bool> _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__2;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter _003C_003Eu__3;

			[Nullable(new byte[] { 0, 1 })]
			private ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter _003C_003Eu__4;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				bool result;
				try
				{
					char c;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter4;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter3;
					ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter awaiter2;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					default:
						while (true)
						{
							c = jsonTextReader._chars[jsonTextReader._charPos];
							switch (c)
							{
							case '\0':
								break;
							case '"':
							case '\'':
								goto end_IL_0054;
							case 't':
								goto IL_0257;
							case 'f':
								goto IL_02cc;
							case 'n':
								goto IL_0341;
							case 'N':
								goto IL_04e9;
							case 'I':
								goto IL_0560;
							case '-':
								goto IL_05d7;
							case '/':
								goto IL_074a;
							case 'u':
								goto IL_07c1;
							case '{':
								jsonTextReader._charPos++;
								jsonTextReader.SetToken(JsonToken.StartObject);
								result = true;
								goto end_IL_000f;
							case '[':
								jsonTextReader._charPos++;
								jsonTextReader.SetToken(JsonToken.StartArray);
								result = true;
								goto end_IL_000f;
							case ']':
								jsonTextReader._charPos++;
								jsonTextReader.SetToken(JsonToken.EndArray);
								result = true;
								goto end_IL_000f;
							case ',':
								jsonTextReader.SetToken(JsonToken.Undefined);
								result = true;
								goto end_IL_000f;
							case ')':
								jsonTextReader._charPos++;
								jsonTextReader.SetToken(JsonToken.EndConstructor);
								result = true;
								goto end_IL_000f;
							case '\r':
								goto IL_08b8;
							case '\n':
								jsonTextReader.ProcessLineFeed();
								continue;
							case '\t':
							case ' ':
								jsonTextReader._charPos++;
								continue;
							default:
								goto IL_094b;
							}
							if (jsonTextReader._charsUsed != jsonTextReader._charPos)
							{
								jsonTextReader._charPos++;
								continue;
							}
							goto IL_0152;
							IL_094b:
							if (char.IsWhiteSpace(c))
							{
								jsonTextReader._charPos++;
								continue;
							}
							goto IL_0966;
							continue;
							end_IL_0054:
							break;
						}
						awaiter = jsonTextReader.ParseStringAsync(c, ReadType.Read, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 1);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0249;
					case 0:
						awaiter4 = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_01ba;
					case 1:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0249;
					case 2:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_02be;
					case 3:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0333;
					case 4:
						awaiter3 = _003C_003Eu__3;
						_003C_003Eu__3 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_03aa;
					case 5:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_043d;
					case 6:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_04b0;
					case 7:
						awaiter2 = _003C_003Eu__4;
						_003C_003Eu__4 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0551;
					case 8:
						awaiter2 = _003C_003Eu__4;
						_003C_003Eu__4 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_05c8;
					case 9:
						awaiter3 = _003C_003Eu__3;
						_003C_003Eu__3 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0641;
					case 10:
						awaiter2 = _003C_003Eu__4;
						_003C_003Eu__4 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_06c9;
					case 11:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_073c;
					case 12:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_07b3;
					case 13:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0829;
					case 14:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0921;
					case 15:
						{
							awaiter = _003C_003Eu__2;
							_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							goto IL_09de;
						}
						IL_0966:
						if (char.IsNumber(c) || c == '-' || c == '.')
						{
							awaiter = jsonTextReader.ParseNumberAsync(ReadType.Read, cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_003C_003E1__state = 15);
								_003C_003Eu__2 = awaiter;
								_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_09de;
						}
						throw jsonTextReader.CreateUnexpectedCharacterException(c);
						IL_0551:
						awaiter2.GetResult();
						result = true;
						break;
						IL_0341:
						awaiter3 = jsonTextReader.EnsureCharsAsync(1, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (_003C_003E1__state = 4);
							_003C_003Eu__3 = awaiter3;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
							return;
						}
						goto IL_03aa;
						IL_05c8:
						awaiter2.GetResult();
						result = true;
						break;
						IL_0249:
						awaiter.GetResult();
						result = true;
						break;
						IL_03aa:
						if (awaiter3.GetResult())
						{
							char c2 = jsonTextReader._chars[jsonTextReader._charPos + 1];
							if (c2 != 'e')
							{
								if (c2 == 'u')
								{
									awaiter = jsonTextReader.ParseNullAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
									if (!awaiter.IsCompleted)
									{
										num = (_003C_003E1__state = 5);
										_003C_003Eu__2 = awaiter;
										_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
										return;
									}
									goto IL_043d;
								}
								throw jsonTextReader.CreateUnexpectedCharacterException(jsonTextReader._chars[jsonTextReader._charPos]);
							}
							awaiter = jsonTextReader.ParseConstructorAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_003C_003E1__state = 6);
								_003C_003Eu__2 = awaiter;
								_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_04b0;
						}
						jsonTextReader._charPos++;
						throw jsonTextReader.CreateUnexpectedEndException();
						IL_09de:
						awaiter.GetResult();
						result = true;
						break;
						IL_08b8:
						awaiter = jsonTextReader.ProcessCarriageReturnAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 14);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0921;
						IL_04e9:
						awaiter2 = jsonTextReader.ParseNumberNaNAsync(ReadType.Read, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_003C_003E1__state = 7);
							_003C_003Eu__4 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_0551;
						IL_0921:
						awaiter.GetResult();
						goto default;
						IL_07c1:
						awaiter = jsonTextReader.ParseUndefinedAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 13);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0829;
						IL_0152:
						awaiter4 = jsonTextReader.ReadDataAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter4.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter4;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter4, ref this);
							return;
						}
						goto IL_01ba;
						IL_0829:
						awaiter.GetResult();
						result = true;
						break;
						IL_074a:
						awaiter = jsonTextReader.ParseCommentAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 12);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_07b3;
						IL_01ba:
						if (awaiter4.GetResult() == 0)
						{
							result = false;
							break;
						}
						goto default;
						IL_07b3:
						awaiter.GetResult();
						result = true;
						break;
						IL_05d7:
						awaiter3 = jsonTextReader.EnsureCharsAsync(1, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (_003C_003E1__state = 9);
							_003C_003Eu__3 = awaiter3;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
							return;
						}
						goto IL_0641;
						IL_043d:
						awaiter.GetResult();
						goto IL_04e2;
						IL_0641:
						if (awaiter3.GetResult() && jsonTextReader._chars[jsonTextReader._charPos + 1] == 'I')
						{
							awaiter2 = jsonTextReader.ParseNumberNegativeInfinityAsync(ReadType.Read, cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (_003C_003E1__state = 10);
								_003C_003Eu__4 = awaiter2;
								_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
								return;
							}
							goto IL_06c9;
						}
						awaiter = jsonTextReader.ParseNumberAsync(ReadType.Read, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 11);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_073c;
						IL_04b0:
						awaiter.GetResult();
						goto IL_04e2;
						IL_0333:
						awaiter.GetResult();
						result = true;
						break;
						IL_0257:
						awaiter = jsonTextReader.ParseTrueAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 2);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_02be;
						IL_06c9:
						awaiter2.GetResult();
						goto IL_0743;
						IL_02cc:
						awaiter = jsonTextReader.ParseFalseAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 3);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0333;
						IL_04e2:
						result = true;
						break;
						IL_073c:
						awaiter.GetResult();
						goto IL_0743;
						IL_0743:
						result = true;
						break;
						IL_0560:
						awaiter2 = jsonTextReader.ParseNumberPositiveInfinityAsync(ReadType.Read, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_003C_003E1__state = 8);
							_003C_003Eu__4 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_05c8;
						IL_02be:
						awaiter.GetResult();
						result = true;
						break;
						end_IL_000f:
						break;
					}
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult(result);
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
		private struct _003CReadStringIntoBufferAsync_003Ed__9 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			public char quote;

			private int _003CcharPos_003E5__2;

			private int _003CinitialPosition_003E5__3;

			private int _003ClastWritePosition_003E5__4;

			private int _003CescapeStartPos_003E5__5;

			private char _003CwriteChar_003E5__6;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter _003C_003Eu__1;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter _003C_003Eu__2;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<char>.ConfiguredTaskAwaiter _003C_003Eu__3;

			private bool _003CanotherHighSurrogate_003E5__7;

			private char _003ChighSurrogate_003E5__8;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__4;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter4;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter3;
					ConfiguredTaskAwaitable<char>.ConfiguredTaskAwaiter awaiter2;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					char c2;
					char result;
					switch (num)
					{
					default:
						_003CcharPos_003E5__2 = jsonTextReader._charPos;
						_003CinitialPosition_003E5__3 = jsonTextReader._charPos;
						_003ClastWritePosition_003E5__4 = jsonTextReader._charPos;
						jsonTextReader._stringBuffer.Position = 0;
						goto IL_005c;
					case 0:
						awaiter4 = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0136;
					case 1:
						awaiter3 = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_01e4;
					case 2:
						awaiter2 = _003C_003Eu__3;
						_003C_003Eu__3 = default(ConfiguredTaskAwaitable<char>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0370;
					case 3:
						awaiter3 = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_041e;
					case 4:
						awaiter2 = _003C_003Eu__3;
						_003C_003Eu__3 = default(ConfiguredTaskAwaitable<char>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_04d5;
					case 5:
						{
							awaiter = _003C_003Eu__4;
							_003C_003Eu__4 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							goto IL_0647;
						}
						IL_005c:
						while (true)
						{
							char c = jsonTextReader._chars[_003CcharPos_003E5__2++];
							if ((uint)c <= 13u)
							{
								if (c != 0)
								{
									if (c != '\n')
									{
										if (c == '\r')
										{
											break;
										}
									}
									else
									{
										jsonTextReader._charPos = _003CcharPos_003E5__2 - 1;
										jsonTextReader.ProcessLineFeed();
										_003CcharPos_003E5__2 = jsonTextReader._charPos;
									}
									continue;
								}
								if (jsonTextReader._charsUsed != _003CcharPos_003E5__2 - 1)
								{
									continue;
								}
								goto IL_00bc;
							}
							if (c != '"' && c != '\'')
							{
								if (c != '\\')
								{
									continue;
								}
								goto IL_016f;
							}
							if (jsonTextReader._chars[_003CcharPos_003E5__2 - 1] == quote)
							{
								jsonTextReader.FinishReadStringIntoBuffer(_003CcharPos_003E5__2 - 1, _003CinitialPosition_003E5__3, _003ClastWritePosition_003E5__4);
								goto end_IL_000f;
							}
						}
						jsonTextReader._charPos = _003CcharPos_003E5__2 - 1;
						awaiter = jsonTextReader.ProcessCarriageReturnAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 5);
							_003C_003Eu__4 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0647;
						IL_041e:
						if (awaiter3.GetResult() && jsonTextReader._chars[jsonTextReader._charPos] == '\\' && jsonTextReader._chars[jsonTextReader._charPos + 1] == 'u')
						{
							_003ChighSurrogate_003E5__8 = _003CwriteChar_003E5__6;
							jsonTextReader._charPos += 2;
							awaiter2 = jsonTextReader.ParseUnicodeAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (_003C_003E1__state = 4);
								_003C_003Eu__3 = awaiter2;
								_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
								return;
							}
							goto IL_04d5;
						}
						_003CwriteChar_003E5__6 = '\ufffd';
						goto IL_0556;
						IL_0561:
						_003CcharPos_003E5__2 = jsonTextReader._charPos;
						goto IL_05a2;
						IL_0647:
						awaiter.GetResult();
						_003CcharPos_003E5__2 = jsonTextReader._charPos;
						goto IL_005c;
						IL_00bc:
						_003CcharPos_003E5__2--;
						awaiter4 = jsonTextReader.ReadDataAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter4.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter4;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter4, ref this);
							return;
						}
						goto IL_0136;
						IL_02e4:
						_003CwriteChar_003E5__6 = '\\';
						goto IL_05a2;
						IL_0136:
						if (awaiter4.GetResult() == 0)
						{
							jsonTextReader._charPos = _003CcharPos_003E5__2;
							throw JsonReaderException.Create(jsonTextReader, "Unterminated string. Expected delimiter: {0}.".FormatWith(CultureInfo.InvariantCulture, quote));
						}
						goto IL_005c;
						IL_05a2:
						jsonTextReader.EnsureBufferNotEmpty();
						jsonTextReader.WriteCharToBuffer(_003CwriteChar_003E5__6, _003ClastWritePosition_003E5__4, _003CescapeStartPos_003E5__5);
						_003ClastWritePosition_003E5__4 = _003CcharPos_003E5__2;
						goto IL_005c;
						IL_016f:
						jsonTextReader._charPos = _003CcharPos_003E5__2;
						awaiter3 = jsonTextReader.EnsureCharsAsync(0, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (_003C_003E1__state = 1);
							_003C_003Eu__2 = awaiter3;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
							return;
						}
						goto IL_01e4;
						IL_02f1:
						_003CwriteChar_003E5__6 = c2;
						goto IL_05a2;
						IL_01e4:
						if (!awaiter3.GetResult())
						{
							throw JsonReaderException.Create(jsonTextReader, "Unterminated string. Expected delimiter: {0}.".FormatWith(CultureInfo.InvariantCulture, quote));
						}
						_003CescapeStartPos_003E5__5 = _003CcharPos_003E5__2 - 1;
						c2 = jsonTextReader._chars[_003CcharPos_003E5__2];
						_003CcharPos_003E5__2++;
						switch (c2)
						{
						case 'b':
							break;
						case 't':
							goto IL_02b0;
						case 'n':
							goto IL_02bd;
						case 'f':
							goto IL_02ca;
						case 'r':
							goto IL_02d7;
						case '\\':
							goto IL_02e4;
						case '"':
						case '\'':
						case '/':
							goto IL_02f1;
						case 'u':
							goto IL_02fd;
						default:
							jsonTextReader._charPos = _003CcharPos_003E5__2;
							throw JsonReaderException.Create(jsonTextReader, "Bad JSON escape sequence: {0}.".FormatWith(CultureInfo.InvariantCulture, "\\" + c2));
						}
						_003CwriteChar_003E5__6 = '\b';
						goto IL_05a2;
						IL_02d7:
						_003CwriteChar_003E5__6 = '\r';
						goto IL_05a2;
						IL_04d5:
						result = awaiter2.GetResult();
						_003CwriteChar_003E5__6 = result;
						if (!StringUtils.IsLowSurrogate(_003CwriteChar_003E5__6))
						{
							if (StringUtils.IsHighSurrogate(_003CwriteChar_003E5__6))
							{
								_003ChighSurrogate_003E5__8 = '\ufffd';
								_003CanotherHighSurrogate_003E5__7 = true;
							}
							else
							{
								_003ChighSurrogate_003E5__8 = '\ufffd';
							}
						}
						jsonTextReader.EnsureBufferNotEmpty();
						jsonTextReader.WriteCharToBuffer(_003ChighSurrogate_003E5__8, _003ClastWritePosition_003E5__4, _003CescapeStartPos_003E5__5);
						_003ClastWritePosition_003E5__4 = jsonTextReader._charPos;
						goto IL_0556;
						IL_02fd:
						jsonTextReader._charPos = _003CcharPos_003E5__2;
						awaiter2 = jsonTextReader.ParseUnicodeAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_003C_003E1__state = 2);
							_003C_003Eu__3 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_0370;
						IL_02b0:
						_003CwriteChar_003E5__6 = '\t';
						goto IL_05a2;
						IL_0370:
						result = awaiter2.GetResult();
						_003CwriteChar_003E5__6 = result;
						if (StringUtils.IsLowSurrogate(_003CwriteChar_003E5__6))
						{
							_003CwriteChar_003E5__6 = '\ufffd';
						}
						else if (StringUtils.IsHighSurrogate(_003CwriteChar_003E5__6))
						{
							goto IL_03ae;
						}
						goto IL_0561;
						IL_02bd:
						_003CwriteChar_003E5__6 = '\n';
						goto IL_05a2;
						IL_02ca:
						_003CwriteChar_003E5__6 = '\f';
						goto IL_05a2;
						IL_03ae:
						_003CanotherHighSurrogate_003E5__7 = false;
						awaiter3 = jsonTextReader.EnsureCharsAsync(2, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (_003C_003E1__state = 3);
							_003C_003Eu__2 = awaiter3;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
							return;
						}
						goto IL_041e;
						IL_0556:
						if (_003CanotherHighSurrogate_003E5__7)
						{
							goto IL_03ae;
						}
						goto IL_0561;
						end_IL_000f:
						break;
					}
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
		private struct _003CProcessCarriageReturnAsync_003Ed__11 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public Task<bool> task;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
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
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
					}
					bool result = awaiter.GetResult();
					jsonTextReader.SetNewLine(result);
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
		private struct _003CParseUnicodeAsync_003Ed__12 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			[Nullable(0)]
			public AsyncTaskMethodBuilder<char> _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				char result2;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = jsonTextReader.EnsureCharsAsync(4, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
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
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
					}
					bool result = awaiter.GetResult();
					result2 = jsonTextReader.ConvertUnicode(result);
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult(result2);
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
		private struct _003CReadCharsAsync_003Ed__14 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			[Nullable(0)]
			public AsyncTaskMethodBuilder<bool> _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public int relativePosition;

			public bool append;

			public CancellationToken cancellationToken;

			private int _003CcharsRequired_003E5__2;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				bool result2;
				try
				{
					if (num != 0)
					{
						_003CcharsRequired_003E5__2 = jsonTextReader._charPos + relativePosition - jsonTextReader._charsUsed + 1;
						goto IL_002d;
					}
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter = _003C_003Eu__1;
					_003C_003Eu__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_009d;
					IL_009d:
					int result = awaiter.GetResult();
					if (result == 0)
					{
						result2 = false;
					}
					else
					{
						_003CcharsRequired_003E5__2 -= result;
						if (_003CcharsRequired_003E5__2 > 0)
						{
							goto IL_002d;
						}
						result2 = true;
					}
					goto end_IL_000e;
					IL_002d:
					awaiter = jsonTextReader.ReadDataAsync(append, _003CcharsRequired_003E5__2, cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_003C_003E1__state = 0);
						_003C_003Eu__1 = awaiter;
						_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_009d;
					end_IL_000e:;
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult(result2);
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
		private struct _003CParseObjectAsync_003Ed__15 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			[Nullable(0)]
			public AsyncTaskMethodBuilder<bool> _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__2;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter _003C_003Eu__3;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				bool result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter3;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					default:
						while (true)
						{
							char c = jsonTextReader._chars[jsonTextReader._charPos];
							switch (c)
							{
							case '\0':
								break;
							case '}':
								jsonTextReader.SetToken(JsonToken.EndObject);
								jsonTextReader._charPos++;
								result = true;
								goto end_IL_000e;
							case '/':
								goto end_IL_0024;
							case '\r':
								goto IL_01a8;
							case '\n':
								jsonTextReader.ProcessLineFeed();
								continue;
							case '\t':
							case ' ':
								jsonTextReader._charPos++;
								continue;
							default:
								goto IL_023a;
							}
							if (jsonTextReader._charsUsed != jsonTextReader._charPos)
							{
								jsonTextReader._charPos++;
								continue;
							}
							goto IL_0087;
							IL_023a:
							if (char.IsWhiteSpace(c))
							{
								jsonTextReader._charPos++;
								continue;
							}
							goto IL_0255;
							continue;
							end_IL_0024:
							break;
						}
						awaiter2 = jsonTextReader.ParseCommentAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_003C_003E1__state = 1);
							_003C_003Eu__2 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_019a;
					case 0:
						awaiter3 = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_00ef;
					case 1:
						awaiter2 = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_019a;
					case 2:
						awaiter2 = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0210;
					case 3:
						{
							awaiter = _003C_003Eu__3;
							_003C_003Eu__3 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							break;
						}
						IL_0255:
						awaiter = jsonTextReader.ParsePropertyAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 3);
							_003C_003Eu__3 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						break;
						IL_0087:
						awaiter3 = jsonTextReader.ReadDataAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter3;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
							return;
						}
						goto IL_00ef;
						IL_01a8:
						awaiter2 = jsonTextReader.ProcessCarriageReturnAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_003C_003E1__state = 2);
							_003C_003Eu__2 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_0210;
						IL_00ef:
						if (awaiter3.GetResult() != 0)
						{
							goto default;
						}
						result = false;
						goto end_IL_000e;
						IL_0210:
						awaiter2.GetResult();
						goto default;
						IL_019a:
						awaiter2.GetResult();
						result = true;
						goto end_IL_000e;
					}
					result = awaiter.GetResult();
					end_IL_000e:;
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult(result);
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
		private struct _003CParseCommentAsync_003Ed__16 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			public bool setToken;

			private bool _003CsinglelineComment_003E5__2;

			private int _003CinitialPosition_003E5__3;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter _003C_003Eu__1;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter _003C_003Eu__2;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__3;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter3;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter2;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					default:
						jsonTextReader._charPos++;
						awaiter2 = jsonTextReader.EnsureCharsAsync(1, false, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_0097;
					case 0:
						awaiter2 = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0097;
					case 1:
						awaiter3 = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_01d9;
					case 2:
						awaiter2 = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_02a7;
					case 3:
						{
							awaiter = _003C_003Eu__3;
							_003C_003Eu__3 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							goto IL_0381;
						}
						IL_0122:
						while (true)
						{
							char c = jsonTextReader._chars[jsonTextReader._charPos];
							if ((uint)c <= 10u)
							{
								if (c == '\0')
								{
									if (jsonTextReader._charsUsed != jsonTextReader._charPos)
									{
										jsonTextReader._charPos++;
										continue;
									}
									goto IL_0171;
								}
								if (c == '\n')
								{
									if (_003CsinglelineComment_003E5__2)
									{
										jsonTextReader.EndComment(setToken, _003CinitialPosition_003E5__3, jsonTextReader._charPos);
										break;
									}
									jsonTextReader.ProcessLineFeed();
									continue;
								}
							}
							else
							{
								if (c == '\r')
								{
									goto IL_02f4;
								}
								if (c == '*')
								{
									jsonTextReader._charPos++;
									if (_003CsinglelineComment_003E5__2)
									{
										continue;
									}
									goto IL_0242;
								}
							}
							jsonTextReader._charPos++;
						}
						break;
						IL_0097:
						if (!awaiter2.GetResult())
						{
							throw JsonReaderException.Create(jsonTextReader, "Unexpected end while parsing comment.");
						}
						if (jsonTextReader._chars[jsonTextReader._charPos] == '*')
						{
							_003CsinglelineComment_003E5__2 = false;
						}
						else
						{
							if (jsonTextReader._chars[jsonTextReader._charPos] != '/')
							{
								throw JsonReaderException.Create(jsonTextReader, "Error parsing comment. Expected: *, got {0}.".FormatWith(CultureInfo.InvariantCulture, jsonTextReader._chars[jsonTextReader._charPos]));
							}
							_003CsinglelineComment_003E5__2 = true;
						}
						jsonTextReader._charPos++;
						_003CinitialPosition_003E5__3 = jsonTextReader._charPos;
						goto IL_0122;
						IL_0171:
						awaiter3 = jsonTextReader.ReadDataAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (_003C_003E1__state = 1);
							_003C_003Eu__2 = awaiter3;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
							return;
						}
						goto IL_01d9;
						IL_02f4:
						if (_003CsinglelineComment_003E5__2)
						{
							jsonTextReader.EndComment(setToken, _003CinitialPosition_003E5__3, jsonTextReader._charPos);
							break;
						}
						awaiter = jsonTextReader.ProcessCarriageReturnAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 3);
							_003C_003Eu__3 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0381;
						IL_02a7:
						if (awaiter2.GetResult() && jsonTextReader._chars[jsonTextReader._charPos] == '/')
						{
							jsonTextReader.EndComment(setToken, _003CinitialPosition_003E5__3, jsonTextReader._charPos - 1);
							jsonTextReader._charPos++;
							break;
						}
						goto IL_0122;
						IL_0381:
						awaiter.GetResult();
						goto IL_0122;
						IL_0242:
						awaiter2 = jsonTextReader.EnsureCharsAsync(0, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_003C_003E1__state = 2);
							_003C_003Eu__1 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_02a7;
						IL_01d9:
						if (awaiter3.GetResult() == 0)
						{
							if (!_003CsinglelineComment_003E5__2)
							{
								throw JsonReaderException.Create(jsonTextReader, "Unexpected end while parsing comment.");
							}
							jsonTextReader.EndComment(setToken, _003CinitialPosition_003E5__3, jsonTextReader._charPos);
							break;
						}
						goto IL_0122;
					}
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
		private struct _003CEatWhitespaceAsync_003Ed__17 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__2;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num != 1)
						{
							goto IL_001b;
						}
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0140;
					}
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter2 = _003C_003Eu__1;
					_003C_003Eu__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_00b4;
					IL_001b:
					while (true)
					{
						char c = jsonTextReader._chars[jsonTextReader._charPos];
						if (c == '\0')
						{
							if (jsonTextReader._charsUsed != jsonTextReader._charPos)
							{
								jsonTextReader._charPos++;
								continue;
							}
							goto IL_004f;
						}
						if (c == '\n')
						{
							jsonTextReader.ProcessLineFeed();
							continue;
						}
						if (c == '\r')
						{
							break;
						}
						if (c == ' ' || char.IsWhiteSpace(c))
						{
							jsonTextReader._charPos++;
							continue;
						}
						goto end_IL_000e;
					}
					awaiter = jsonTextReader.ProcessCarriageReturnAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_003C_003E1__state = 1);
						_003C_003Eu__2 = awaiter;
						_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_0140;
					IL_0140:
					awaiter.GetResult();
					goto IL_001b;
					IL_004f:
					awaiter2 = jsonTextReader.ReadDataAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						num = (_003C_003E1__state = 0);
						_003C_003Eu__1 = awaiter2;
						_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
						return;
					}
					goto IL_00b4;
					IL_00b4:
					if (awaiter2.GetResult() != 0)
					{
						goto IL_001b;
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

		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CParseStringAsync_003Ed__18 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public CancellationToken cancellationToken;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public char quote;

			public ReadType readType;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						cancellationToken.ThrowIfCancellationRequested();
						jsonTextReader._charPos++;
						jsonTextReader.ShiftBufferIfNeeded();
						awaiter = jsonTextReader.ReadStringIntoBufferAsync(quote, cancellationToken).ConfigureAwait(false).GetAwaiter();
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
					jsonTextReader.ParseReadString(quote, readType);
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
		private struct _003CMatchValueAsync_003Ed__19 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			[Nullable(0)]
			public AsyncTaskMethodBuilder<bool> _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			[Nullable(0)]
			public string value;

			public CancellationToken cancellationToken;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				bool result2;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = jsonTextReader.EnsureCharsAsync(value.Length - 1, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
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
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
					}
					bool result = awaiter.GetResult();
					result2 = jsonTextReader.MatchValue(result, value);
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult(result2);
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
		private struct _003CMatchValueWithTrailingSeparatorAsync_003Ed__20 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			[Nullable(0)]
			public AsyncTaskMethodBuilder<bool> _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			[Nullable(0)]
			public string value;

			public CancellationToken cancellationToken;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				bool result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = _003C_003Eu__1;
							_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							goto IL_00f8;
						}
						awaiter = jsonTextReader.MatchValueAsync(value, cancellationToken).ConfigureAwait(false).GetAwaiter();
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
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
					}
					if (awaiter.GetResult())
					{
						awaiter = jsonTextReader.EnsureCharsAsync(0, false, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 1);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_00f8;
					}
					result = false;
					goto end_IL_000e;
					IL_00f8:
					result = !awaiter.GetResult() || jsonTextReader.IsSeparator(jsonTextReader._chars[jsonTextReader._charPos]) || jsonTextReader._chars[jsonTextReader._charPos] == '\0';
					end_IL_000e:;
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult(result);
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
		private struct _003CMatchAndSetAsync_003Ed__21 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			[Nullable(0)]
			public string value;

			public CancellationToken cancellationToken;

			public JsonToken newToken;

			[Nullable(0)]
			public object tokenValue;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = jsonTextReader.MatchValueWithTrailingSeparatorAsync(value, cancellationToken).ConfigureAwait(false).GetAwaiter();
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
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
					}
					if (!awaiter.GetResult())
					{
						throw JsonReaderException.Create(jsonTextReader, "Error parsing " + newToken.ToString().ToLowerInvariant() + " value.");
					}
					jsonTextReader.SetToken(newToken, tokenValue);
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
		private struct _003CParseConstructorAsync_003Ed__25 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private int _003CinitialPosition_003E5__2;

			private int _003CendPosition_003E5__3;

			[Nullable(0)]
			private string _003CconstructorName_003E5__4;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__2;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter _003C_003Eu__3;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter3;
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter2;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					char c;
					switch (num)
					{
					default:
						awaiter3 = jsonTextReader.MatchValueWithTrailingSeparatorAsync("new", cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter3;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
							return;
						}
						goto IL_0090;
					case 0:
						awaiter3 = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0090;
					case 1:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0103;
					case 2:
						awaiter2 = _003C_003Eu__3;
						_003C_003Eu__3 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_01a5;
					case 3:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0272;
					case 4:
						{
							awaiter = _003C_003Eu__2;
							_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							goto IL_038d;
						}
						IL_0272:
						awaiter.GetResult();
						goto IL_02eb;
						IL_0090:
						if (awaiter3.GetResult())
						{
							awaiter = jsonTextReader.EatWhitespaceAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_003C_003E1__state = 1);
								_003C_003Eu__2 = awaiter;
								_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_0103;
						}
						throw JsonReaderException.Create(jsonTextReader, "Unexpected content while parsing JSON.");
						IL_01a5:
						if (awaiter2.GetResult() == 0)
						{
							throw JsonReaderException.Create(jsonTextReader, "Unexpected end while parsing constructor.");
						}
						goto IL_0116;
						IL_02eb:
						jsonTextReader._stringReference = new StringReference(jsonTextReader._chars, _003CinitialPosition_003E5__2, _003CendPosition_003E5__3 - _003CinitialPosition_003E5__2);
						_003CconstructorName_003E5__4 = jsonTextReader._stringReference.ToString();
						awaiter = jsonTextReader.EatWhitespaceAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 4);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_038d;
						IL_0103:
						awaiter.GetResult();
						_003CinitialPosition_003E5__2 = jsonTextReader._charPos;
						goto IL_0116;
						IL_0116:
						while (true)
						{
							c = jsonTextReader._chars[jsonTextReader._charPos];
							if (c == '\0')
							{
								break;
							}
							if (char.IsLetterOrDigit(c))
							{
								jsonTextReader._charPos++;
								continue;
							}
							goto IL_01f8;
						}
						if (jsonTextReader._charsUsed == jsonTextReader._charPos)
						{
							awaiter2 = jsonTextReader.ReadDataAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (_003C_003E1__state = 2);
								_003C_003Eu__3 = awaiter2;
								_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
								return;
							}
							goto IL_01a5;
						}
						_003CendPosition_003E5__3 = jsonTextReader._charPos;
						jsonTextReader._charPos++;
						goto IL_02eb;
						IL_01f8:
						if (c == '\r')
						{
							_003CendPosition_003E5__3 = jsonTextReader._charPos;
							awaiter = jsonTextReader.ProcessCarriageReturnAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_003C_003E1__state = 3);
								_003C_003Eu__2 = awaiter;
								_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_0272;
						}
						if (c == '\n')
						{
							_003CendPosition_003E5__3 = jsonTextReader._charPos;
							jsonTextReader.ProcessLineFeed();
						}
						else if (char.IsWhiteSpace(c))
						{
							_003CendPosition_003E5__3 = jsonTextReader._charPos;
							jsonTextReader._charPos++;
						}
						else
						{
							if (c != '(')
							{
								throw JsonReaderException.Create(jsonTextReader, "Unexpected character while parsing constructor: {0}.".FormatWith(CultureInfo.InvariantCulture, c));
							}
							_003CendPosition_003E5__3 = jsonTextReader._charPos;
						}
						goto IL_02eb;
						IL_038d:
						awaiter.GetResult();
						if (jsonTextReader._chars[jsonTextReader._charPos] != '(')
						{
							throw JsonReaderException.Create(jsonTextReader, "Unexpected character while parsing constructor: {0}.".FormatWith(CultureInfo.InvariantCulture, jsonTextReader._chars[jsonTextReader._charPos]));
						}
						jsonTextReader._charPos++;
						jsonTextReader.ClearRecentString();
						jsonTextReader.SetToken(JsonToken.StartConstructor, _003CconstructorName_003E5__4);
						_003CconstructorName_003E5__4 = null;
						break;
					}
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
		private struct _003CParseNumberNaNAsync_003Ed__26 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			[Nullable(0)]
			public AsyncTaskMethodBuilder<object> _003C_003Et__builder;

			public ReadType readType;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			private ReadType _003C_003E7__wrap1;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				object result2;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						_003C_003E7__wrap1 = readType;
						awaiter = jsonTextReader.MatchValueWithTrailingSeparatorAsync(JsonConvert.NaN, cancellationToken).ConfigureAwait(false).GetAwaiter();
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
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
					}
					bool result = awaiter.GetResult();
					result2 = jsonTextReader.ParseNumberNaN(_003C_003E7__wrap1, result);
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult(result2);
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
		private struct _003CParseNumberPositiveInfinityAsync_003Ed__27 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			[Nullable(0)]
			public AsyncTaskMethodBuilder<object> _003C_003Et__builder;

			public ReadType readType;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			private ReadType _003C_003E7__wrap1;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				object result2;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						_003C_003E7__wrap1 = readType;
						awaiter = jsonTextReader.MatchValueWithTrailingSeparatorAsync(JsonConvert.PositiveInfinity, cancellationToken).ConfigureAwait(false).GetAwaiter();
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
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
					}
					bool result = awaiter.GetResult();
					result2 = jsonTextReader.ParseNumberPositiveInfinity(_003C_003E7__wrap1, result);
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult(result2);
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
		private struct _003CParseNumberNegativeInfinityAsync_003Ed__28 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			[Nullable(0)]
			public AsyncTaskMethodBuilder<object> _003C_003Et__builder;

			public ReadType readType;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			private ReadType _003C_003E7__wrap1;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				object result2;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						_003C_003E7__wrap1 = readType;
						awaiter = jsonTextReader.MatchValueWithTrailingSeparatorAsync(JsonConvert.NegativeInfinity, cancellationToken).ConfigureAwait(false).GetAwaiter();
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
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
					}
					bool result = awaiter.GetResult();
					result2 = jsonTextReader.ParseNumberNegativeInfinity(_003C_003E7__wrap1, result);
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult(result2);
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
		private struct _003CParseNumberAsync_003Ed__29 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			public ReadType readType;

			private char _003CfirstChar_003E5__2;

			private int _003CinitialPosition_003E5__3;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						jsonTextReader.ShiftBufferIfNeeded();
						_003CfirstChar_003E5__2 = jsonTextReader._chars[jsonTextReader._charPos];
						_003CinitialPosition_003E5__3 = jsonTextReader._charPos;
						awaiter = jsonTextReader.ReadNumberIntoBufferAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
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
					jsonTextReader.ParseReadNumber(readType, _003CfirstChar_003E5__2, _003CinitialPosition_003E5__3);
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
		private struct _003CParsePropertyAsync_003Ed__31 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			[Nullable(0)]
			public AsyncTaskMethodBuilder<bool> _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			private char _003CquoteChar_003E5__2;

			[Nullable(0)]
			private string _003CpropertyName_003E5__3;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				bool result;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					default:
					{
						char c = jsonTextReader._chars[jsonTextReader._charPos];
						if (c == '"' || c == '\'')
						{
							jsonTextReader._charPos++;
							_003CquoteChar_003E5__2 = c;
							jsonTextReader.ShiftBufferIfNeeded();
							awaiter = jsonTextReader.ReadStringIntoBufferAsync(_003CquoteChar_003E5__2, cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_003C_003E1__state = 0);
								_003C_003Eu__1 = awaiter;
								_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_00c3;
						}
						if (jsonTextReader.ValidIdentifierChar(c))
						{
							_003CquoteChar_003E5__2 = '\0';
							jsonTextReader.ShiftBufferIfNeeded();
							awaiter = jsonTextReader.ParseUnquotedPropertyAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_003C_003E1__state = 1);
								_003C_003Eu__1 = awaiter;
								_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_014c;
						}
						throw JsonReaderException.Create(jsonTextReader, "Invalid property identifier character: {0}.".FormatWith(CultureInfo.InvariantCulture, jsonTextReader._chars[jsonTextReader._charPos]));
					}
					case 0:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_00c3;
					case 1:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_014c;
					case 2:
						{
							awaiter = _003C_003Eu__1;
							_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							break;
						}
						IL_014c:
						awaiter.GetResult();
						goto IL_017d;
						IL_017d:
						if (jsonTextReader.PropertyNameTable != null)
						{
							_003CpropertyName_003E5__3 = jsonTextReader.PropertyNameTable.Get(jsonTextReader._stringReference.Chars, jsonTextReader._stringReference.StartIndex, jsonTextReader._stringReference.Length) ?? jsonTextReader._stringReference.ToString();
						}
						else
						{
							_003CpropertyName_003E5__3 = jsonTextReader._stringReference.ToString();
						}
						awaiter = jsonTextReader.EatWhitespaceAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 2);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						break;
						IL_00c3:
						awaiter.GetResult();
						goto IL_017d;
					}
					awaiter.GetResult();
					if (jsonTextReader._chars[jsonTextReader._charPos] != ':')
					{
						throw JsonReaderException.Create(jsonTextReader, "Invalid character after parsing property name. Expected ':' but got: {0}.".FormatWith(CultureInfo.InvariantCulture, jsonTextReader._chars[jsonTextReader._charPos]));
					}
					jsonTextReader._charPos++;
					jsonTextReader.SetToken(JsonToken.PropertyName, _003CpropertyName_003E5__3);
					jsonTextReader._quoteChar = _003CquoteChar_003E5__2;
					jsonTextReader.ClearRecentString();
					result = true;
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003CpropertyName_003E5__3 = null;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003CpropertyName_003E5__3 = null;
				_003C_003Et__builder.SetResult(result);
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
		private struct _003CReadNumberIntoBufferAsync_003Ed__32 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			private int _003CcharPos_003E5__2;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				try
				{
					if (num != 0)
					{
						_003CcharPos_003E5__2 = jsonTextReader._charPos;
						goto IL_0020;
					}
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter = _003C_003Eu__1;
					_003C_003Eu__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_00b0;
					IL_00b0:
					if (awaiter.GetResult() != 0)
					{
						goto IL_0020;
					}
					goto end_IL_000e;
					IL_0020:
					while (true)
					{
						char c = jsonTextReader._chars[_003CcharPos_003E5__2];
						if (c == '\0')
						{
							jsonTextReader._charPos = _003CcharPos_003E5__2;
							if (jsonTextReader._charsUsed == _003CcharPos_003E5__2)
							{
								awaiter = jsonTextReader.ReadDataAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									num = (_003C_003E1__state = 0);
									_003C_003Eu__1 = awaiter;
									_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
									return;
								}
								goto IL_00b0;
							}
							break;
						}
						if (jsonTextReader.ReadNumberCharIntoBuffer(c, _003CcharPos_003E5__2))
						{
							break;
						}
						_003CcharPos_003E5__2++;
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

		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CParseUnquotedPropertyAsync_003Ed__33 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			private int _003CinitialPosition_003E5__2;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				try
				{
					if (num != 0)
					{
						_003CinitialPosition_003E5__2 = jsonTextReader._charPos;
						goto IL_001d;
					}
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter = _003C_003Eu__1;
					_003C_003Eu__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_00a4;
					IL_00a4:
					if (awaiter.GetResult() == 0)
					{
						throw JsonReaderException.Create(jsonTextReader, "Unexpected end while parsing unquoted property name.");
					}
					goto IL_001d;
					IL_001d:
					while (true)
					{
						char c = jsonTextReader._chars[jsonTextReader._charPos];
						if (c == '\0')
						{
							if (jsonTextReader._charsUsed == jsonTextReader._charPos)
							{
								awaiter = jsonTextReader.ReadDataAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (awaiter.IsCompleted)
								{
									break;
								}
								num = (_003C_003E1__state = 0);
								_003C_003Eu__1 = awaiter;
								_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							jsonTextReader._stringReference = new StringReference(jsonTextReader._chars, _003CinitialPosition_003E5__2, jsonTextReader._charPos - _003CinitialPosition_003E5__2);
						}
						else if (!jsonTextReader.ReadUnquotedPropertyReportIfDone(c, _003CinitialPosition_003E5__2))
						{
							continue;
						}
						goto end_IL_000e;
					}
					goto IL_00a4;
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
		private struct _003CReadNullCharAsync_003Ed__34 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			[Nullable(0)]
			public AsyncTaskMethodBuilder<bool> _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				bool result;
				try
				{
					ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (jsonTextReader._charsUsed != jsonTextReader._charPos)
						{
							jsonTextReader._charPos++;
							goto IL_00a3;
						}
						awaiter = jsonTextReader.ReadDataAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
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
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<int>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
					}
					if (awaiter.GetResult() != 0)
					{
						goto IL_00a3;
					}
					jsonTextReader._isEndOfFile = true;
					result = true;
					goto end_IL_000e;
					IL_00a3:
					result = false;
					end_IL_000e:;
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult(result);
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
		private struct _003CHandleNullAsync_003Ed__35 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__2;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter2;
					if (num != 0)
					{
						if (num == 1)
						{
							awaiter = _003C_003Eu__2;
							_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							goto IL_0103;
						}
						awaiter2 = jsonTextReader.EnsureCharsAsync(1, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
					}
					else
					{
						awaiter2 = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
					}
					if (awaiter2.GetResult())
					{
						if (jsonTextReader._chars[jsonTextReader._charPos + 1] == 'u')
						{
							awaiter = jsonTextReader.ParseNullAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_003C_003E1__state = 1);
								_003C_003Eu__2 = awaiter;
								_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_0103;
						}
						jsonTextReader._charPos += 2;
						throw jsonTextReader.CreateUnexpectedCharacterException(jsonTextReader._chars[jsonTextReader._charPos - 1]);
					}
					jsonTextReader._charPos = jsonTextReader._charsUsed;
					throw jsonTextReader.CreateUnexpectedEndException();
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
		private struct _003CReadFinishedAsync_003Ed__36 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__2;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter2;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					default:
						awaiter2 = jsonTextReader.EnsureCharsAsync(0, false, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_0085;
					case 0:
						awaiter2 = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0085;
					case 1:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_00f8;
					case 2:
						{
							awaiter = _003C_003Eu__2;
							_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							goto IL_018c;
						}
						IL_018c:
						awaiter.GetResult();
						break;
						IL_0085:
						if (!awaiter2.GetResult())
						{
							break;
						}
						awaiter = jsonTextReader.EatWhitespaceAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 1);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_00f8;
						IL_00f8:
						awaiter.GetResult();
						if (!jsonTextReader._isEndOfFile)
						{
							if (jsonTextReader._chars[jsonTextReader._charPos] == '/')
							{
								awaiter = jsonTextReader.ParseCommentAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
								if (!awaiter.IsCompleted)
								{
									num = (_003C_003E1__state = 2);
									_003C_003Eu__2 = awaiter;
									_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
									return;
								}
								goto IL_018c;
							}
							throw JsonReaderException.Create(jsonTextReader, "Additional text encountered after finished reading JSON content: {0}.".FormatWith(CultureInfo.InvariantCulture, jsonTextReader._chars[jsonTextReader._charPos]));
						}
						jsonTextReader.SetToken(JsonToken.None);
						goto end_IL_000e;
					}
					jsonTextReader.SetToken(JsonToken.None);
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
		private struct _003CReadStringValueAsync_003Ed__37 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			[Nullable(0)]
			public AsyncTaskMethodBuilder<object> _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			public ReadType readType;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter _003C_003Eu__1;

			[Nullable(0)]
			private string _003Cexpected_003E5__2;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__2;

			[Nullable(new byte[] { 0, 1 })]
			private ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter _003C_003Eu__3;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				object result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter3;
					ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter awaiter2;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					char c;
					switch (num)
					{
					default:
						jsonTextReader.EnsureBuffer();
						switch (jsonTextReader._currentState)
						{
						case State.PostValue:
							break;
						case State.Start:
						case State.Property:
						case State.ArrayStart:
						case State.Array:
						case State.ConstructorStart:
						case State.Constructor:
							goto IL_010c;
						case State.Finished:
							goto IL_088b;
						default:
							throw JsonReaderException.Create(jsonTextReader, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, jsonTextReader.CurrentState));
						}
						awaiter3 = jsonTextReader.ParsePostValueAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter3;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
							return;
						}
						goto IL_00fc;
					case 0:
						awaiter3 = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_00fc;
					case 1:
						awaiter3 = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0278;
					case 2:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0303;
					case 3:
						awaiter3 = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0385;
					case 4:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0420;
					case 5:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_04bb;
					case 6:
						awaiter3 = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0573;
					case 7:
						awaiter2 = _003C_003Eu__3;
						_003C_003Eu__3 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0617;
					case 8:
						awaiter2 = _003C_003Eu__3;
						_003C_003Eu__3 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0691;
					case 9:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0706;
					case 10:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_077d;
					case 11:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_083e;
					case 12:
						{
							awaiter = _003C_003Eu__2;
							_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							break;
						}
						IL_088b:
						awaiter = jsonTextReader.ReadFinishedAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 12);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						break;
						IL_0617:
						result = awaiter2.GetResult();
						goto end_IL_000e;
						IL_05aa:
						awaiter2 = jsonTextReader.ParseNumberPositiveInfinityAsync(readType, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_003C_003E1__state = 7);
							_003C_003Eu__3 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_0617;
						IL_04bb:
						awaiter.GetResult();
						result = jsonTextReader.Value;
						goto end_IL_000e;
						IL_00fc:
						if (!awaiter3.GetResult())
						{
							goto IL_010c;
						}
						result = null;
						goto end_IL_000e;
						IL_04ce:
						if (readType != ReadType.ReadAsString)
						{
							jsonTextReader._charPos++;
							throw jsonTextReader.CreateUnexpectedCharacterException(c);
						}
						_003Cexpected_003E5__2 = ((c == 't') ? JsonConvert.True : JsonConvert.False);
						awaiter3 = jsonTextReader.MatchValueWithTrailingSeparatorAsync(_003Cexpected_003E5__2, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (_003C_003E1__state = 6);
							_003C_003Eu__1 = awaiter3;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
							return;
						}
						goto IL_0573;
						IL_010c:
						c = jsonTextReader._chars[jsonTextReader._charPos];
						switch (c)
						{
						case '\0':
							break;
						case '"':
						case '\'':
							goto IL_0294;
						case '-':
							goto IL_031c;
						case '.':
						case '0':
						case '1':
						case '2':
						case '3':
						case '4':
						case '5':
						case '6':
						case '7':
						case '8':
						case '9':
							goto IL_0433;
						case 'f':
						case 't':
							goto IL_04ce;
						case 'I':
							goto IL_05aa;
						case 'N':
							goto IL_0624;
						case 'n':
							goto IL_069e;
						case '/':
							goto IL_0714;
						case ',':
							goto IL_0789;
						case ']':
							goto IL_0794;
						case '\r':
							goto IL_07d5;
						case '\n':
							goto IL_0847;
						case '\t':
						case ' ':
							goto IL_084f;
						default:
							goto IL_085f;
						}
						awaiter3 = jsonTextReader.ReadNullCharAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (_003C_003E1__state = 1);
							_003C_003Eu__1 = awaiter3;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
							return;
						}
						goto IL_0278;
						IL_085f:
						jsonTextReader._charPos++;
						if (!char.IsWhiteSpace(c))
						{
							throw jsonTextReader.CreateUnexpectedCharacterException(c);
						}
						goto IL_087f;
						IL_0420:
						awaiter.GetResult();
						result = jsonTextReader.Value;
						goto end_IL_000e;
						IL_084f:
						jsonTextReader._charPos++;
						goto IL_087f;
						IL_0847:
						jsonTextReader.ProcessLineFeed();
						goto IL_087f;
						IL_07d5:
						awaiter = jsonTextReader.ProcessCarriageReturnAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 11);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_083e;
						IL_0385:
						if (!awaiter3.GetResult() || jsonTextReader._chars[jsonTextReader._charPos + 1] != 'I')
						{
							awaiter = jsonTextReader.ParseNumberAsync(readType, cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_003C_003E1__state = 4);
								_003C_003Eu__2 = awaiter;
								_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_0420;
						}
						result = jsonTextReader.ParseNumberNegativeInfinity(readType);
						goto end_IL_000e;
						IL_083e:
						awaiter.GetResult();
						goto IL_087f;
						IL_0794:
						jsonTextReader._charPos++;
						if (jsonTextReader._currentState != State.Array && jsonTextReader._currentState != State.ArrayStart && jsonTextReader._currentState != State.PostValue)
						{
							throw jsonTextReader.CreateUnexpectedCharacterException(c);
						}
						jsonTextReader.SetToken(JsonToken.EndArray);
						result = null;
						goto end_IL_000e;
						IL_0278:
						if (!awaiter3.GetResult())
						{
							goto IL_087f;
						}
						jsonTextReader.SetToken(JsonToken.None, null, false);
						result = null;
						goto end_IL_000e;
						IL_0573:
						if (!awaiter3.GetResult())
						{
							throw jsonTextReader.CreateUnexpectedCharacterException(jsonTextReader._chars[jsonTextReader._charPos]);
						}
						jsonTextReader.SetToken(JsonToken.String, _003Cexpected_003E5__2);
						result = _003Cexpected_003E5__2;
						goto end_IL_000e;
						IL_0303:
						awaiter.GetResult();
						result = jsonTextReader.FinishReadQuotedStringValue(readType);
						goto end_IL_000e;
						IL_031c:
						awaiter3 = jsonTextReader.EnsureCharsAsync(1, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (_003C_003E1__state = 3);
							_003C_003Eu__1 = awaiter3;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
							return;
						}
						goto IL_0385;
						IL_0789:
						jsonTextReader.ProcessValueComma();
						goto IL_087f;
						IL_0714:
						awaiter = jsonTextReader.ParseCommentAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 10);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_077d;
						IL_087f:
						_003Cexpected_003E5__2 = null;
						goto IL_010c;
						IL_077d:
						awaiter.GetResult();
						goto IL_087f;
						IL_069e:
						awaiter = jsonTextReader.HandleNullAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 9);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0706;
						IL_0433:
						if (readType != ReadType.ReadAsString)
						{
							jsonTextReader._charPos++;
							throw jsonTextReader.CreateUnexpectedCharacterException(c);
						}
						awaiter = jsonTextReader.ParseNumberAsync(ReadType.ReadAsString, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 5);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_04bb;
						IL_0706:
						awaiter.GetResult();
						result = null;
						goto end_IL_000e;
						IL_0624:
						awaiter2 = jsonTextReader.ParseNumberNaNAsync(readType, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_003C_003E1__state = 8);
							_003C_003Eu__3 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_0691;
						IL_0294:
						awaiter = jsonTextReader.ParseStringAsync(c, readType, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 2);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0303;
						IL_0691:
						result = awaiter2.GetResult();
						goto end_IL_000e;
					}
					awaiter.GetResult();
					result = null;
					end_IL_000e:;
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult(result);
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
		private struct _003CReadNumberValueAsync_003Ed__38 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			[Nullable(0)]
			public AsyncTaskMethodBuilder<object> _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			public ReadType readType;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__2;

			[Nullable(new byte[] { 0, 1 })]
			private ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter _003C_003Eu__3;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				object result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter3;
					ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter awaiter2;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					char c;
					switch (num)
					{
					default:
						jsonTextReader.EnsureBuffer();
						switch (jsonTextReader._currentState)
						{
						case State.PostValue:
							break;
						case State.Start:
						case State.Property:
						case State.ArrayStart:
						case State.Array:
						case State.ConstructorStart:
						case State.Constructor:
							goto IL_010c;
						case State.Finished:
							goto IL_07e2;
						default:
							throw JsonReaderException.Create(jsonTextReader, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, jsonTextReader.CurrentState));
						}
						awaiter3 = jsonTextReader.ParsePostValueAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter3;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
							return;
						}
						goto IL_00fc;
					case 0:
						awaiter3 = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_00fc;
					case 1:
						awaiter3 = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_025b;
					case 2:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_02e6;
					case 3:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0366;
					case 4:
						awaiter2 = _003C_003Eu__3;
						_003C_003Eu__3 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_03e1;
					case 5:
						awaiter2 = _003C_003Eu__3;
						_003C_003Eu__3 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_045b;
					case 6:
						awaiter3 = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_04d1;
					case 7:
						awaiter2 = _003C_003Eu__3;
						_003C_003Eu__3 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_055d;
					case 8:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_05d7;
					case 9:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0658;
					case 10:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_06d4;
					case 11:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0795;
					case 12:
						{
							awaiter = _003C_003Eu__2;
							_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							break;
						}
						IL_07e2:
						awaiter = jsonTextReader.ReadFinishedAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 12);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						break;
						IL_02e6:
						awaiter.GetResult();
						result = jsonTextReader.FinishReadQuotedNumber(readType);
						goto end_IL_000e;
						IL_04d1:
						if (awaiter3.GetResult() && jsonTextReader._chars[jsonTextReader._charPos + 1] == 'I')
						{
							awaiter2 = jsonTextReader.ParseNumberNegativeInfinityAsync(readType, cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (_003C_003E1__state = 7);
								_003C_003Eu__3 = awaiter2;
								_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
								return;
							}
							goto IL_055d;
						}
						awaiter = jsonTextReader.ParseNumberAsync(readType, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 8);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_05d7;
						IL_055d:
						result = awaiter2.GetResult();
						goto end_IL_000e;
						IL_00fc:
						if (!awaiter3.GetResult())
						{
							goto IL_010c;
						}
						result = null;
						goto end_IL_000e;
						IL_025b:
						if (!awaiter3.GetResult())
						{
							goto IL_010c;
						}
						jsonTextReader.SetToken(JsonToken.None, null, false);
						result = null;
						goto end_IL_000e;
						IL_010c:
						while (true)
						{
							c = jsonTextReader._chars[jsonTextReader._charPos];
							switch (c)
							{
							case '\0':
								break;
							case '"':
							case '\'':
								goto IL_0277;
							case 'n':
								goto IL_02ff;
							case 'N':
								goto IL_0374;
							case 'I':
								goto IL_03ee;
							case '-':
								goto IL_0468;
							case '.':
							case '0':
							case '1':
							case '2':
							case '3':
							case '4':
							case '5':
							case '6':
							case '7':
							case '8':
							case '9':
								goto IL_05ea;
							case '/':
								goto IL_066b;
							case ',':
								jsonTextReader.ProcessValueComma();
								continue;
							case ']':
								goto IL_06eb;
							case '\r':
								goto IL_072c;
							case '\n':
								jsonTextReader.ProcessLineFeed();
								continue;
							case '\t':
							case ' ':
								jsonTextReader._charPos++;
								continue;
							default:
								jsonTextReader._charPos++;
								if (char.IsWhiteSpace(c))
								{
									continue;
								}
								throw jsonTextReader.CreateUnexpectedCharacterException(c);
							}
							break;
						}
						awaiter3 = jsonTextReader.ReadNullCharAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (_003C_003E1__state = 1);
							_003C_003Eu__1 = awaiter3;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
							return;
						}
						goto IL_025b;
						IL_072c:
						awaiter = jsonTextReader.ProcessCarriageReturnAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 11);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0795;
						IL_0277:
						awaiter = jsonTextReader.ParseStringAsync(c, readType, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 2);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_02e6;
						IL_0795:
						awaiter.GetResult();
						goto IL_010c;
						IL_06eb:
						jsonTextReader._charPos++;
						if (jsonTextReader._currentState != State.Array && jsonTextReader._currentState != State.ArrayStart && jsonTextReader._currentState != State.PostValue)
						{
							throw jsonTextReader.CreateUnexpectedCharacterException(c);
						}
						jsonTextReader.SetToken(JsonToken.EndArray);
						result = null;
						goto end_IL_000e;
						IL_03ee:
						awaiter2 = jsonTextReader.ParseNumberPositiveInfinityAsync(readType, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_003C_003E1__state = 5);
							_003C_003Eu__3 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_045b;
						IL_02ff:
						awaiter = jsonTextReader.HandleNullAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 3);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0366;
						IL_045b:
						result = awaiter2.GetResult();
						goto end_IL_000e;
						IL_05d7:
						awaiter.GetResult();
						result = jsonTextReader.Value;
						goto end_IL_000e;
						IL_066b:
						awaiter = jsonTextReader.ParseCommentAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 10);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_06d4;
						IL_0374:
						awaiter2 = jsonTextReader.ParseNumberNaNAsync(readType, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_003C_003E1__state = 4);
							_003C_003Eu__3 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_03e1;
						IL_06d4:
						awaiter.GetResult();
						goto IL_010c;
						IL_05ea:
						awaiter = jsonTextReader.ParseNumberAsync(readType, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 9);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0658;
						IL_0366:
						awaiter.GetResult();
						result = null;
						goto end_IL_000e;
						IL_0658:
						awaiter.GetResult();
						result = jsonTextReader.Value;
						goto end_IL_000e;
						IL_0468:
						awaiter3 = jsonTextReader.EnsureCharsAsync(1, true, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (_003C_003E1__state = 6);
							_003C_003Eu__1 = awaiter3;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
							return;
						}
						goto IL_04d1;
						IL_03e1:
						result = awaiter2.GetResult();
						goto end_IL_000e;
					}
					awaiter.GetResult();
					result = null;
					end_IL_000e:;
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult(result);
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
		private struct _003CDoReadAsBooleanAsync_003Ed__40 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			[Nullable(0)]
			public AsyncTaskMethodBuilder<bool?> _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private BigInteger _003Ci_003E5__2;

			private bool _003CisTrue_003E5__3;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__2;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				bool? result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter2;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					char c;
					object value;
					bool flag;
					switch (num)
					{
					default:
						jsonTextReader.EnsureBuffer();
						switch (jsonTextReader._currentState)
						{
						case State.PostValue:
							break;
						case State.Start:
						case State.Property:
						case State.ArrayStart:
						case State.Array:
						case State.ConstructorStart:
						case State.Constructor:
							goto IL_0102;
						case State.Finished:
							goto IL_0691;
						default:
							throw JsonReaderException.Create(jsonTextReader, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, jsonTextReader.CurrentState));
						}
						awaiter2 = jsonTextReader.ParsePostValueAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_00ec;
					case 0:
						awaiter2 = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_00ec;
					case 1:
						awaiter2 = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0251;
					case 2:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_02dd;
					case 3:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0368;
					case 4:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_03e4;
					case 5:
						awaiter2 = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_04d0;
					case 6:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0579;
					case 7:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_063f;
					case 8:
						{
							awaiter = _003C_003Eu__2;
							_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							break;
						}
						IL_0691:
						awaiter = jsonTextReader.ReadFinishedAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 8);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						break;
						IL_0251:
						if (!awaiter2.GetResult())
						{
							goto IL_0680;
						}
						jsonTextReader.SetToken(JsonToken.None, null, false);
						result = null;
						goto end_IL_000e;
						IL_0579:
						awaiter.GetResult();
						goto IL_0680;
						IL_0449:
						_003CisTrue_003E5__3 = c == 't';
						awaiter2 = jsonTextReader.MatchValueWithTrailingSeparatorAsync(_003CisTrue_003E5__3 ? JsonConvert.True : JsonConvert.False, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_003C_003E1__state = 5);
							_003C_003Eu__1 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_04d0;
						IL_00ec:
						if (!awaiter2.GetResult())
						{
							goto IL_0102;
						}
						result = null;
						goto end_IL_000e;
						IL_0301:
						awaiter = jsonTextReader.HandleNullAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 3);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0368;
						IL_0102:
						c = jsonTextReader._chars[jsonTextReader._charPos];
						switch (c)
						{
						case '\0':
							break;
						case '"':
						case '\'':
							goto IL_0273;
						case 'n':
							goto IL_0301;
						case '-':
						case '.':
						case '0':
						case '1':
						case '2':
						case '3':
						case '4':
						case '5':
						case '6':
						case '7':
						case '8':
						case '9':
							goto IL_037c;
						case 'f':
						case 't':
							goto IL_0449;
						case '/':
							goto IL_0511;
						case ',':
							goto IL_0585;
						case ']':
							goto IL_0590;
						case '\r':
							goto IL_05d7;
						case '\n':
							goto IL_0648;
						case '\t':
						case ' ':
							goto IL_0650;
						default:
							goto IL_0660;
						}
						awaiter2 = jsonTextReader.ReadNullCharAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_003C_003E1__state = 1);
							_003C_003Eu__1 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_0251;
						IL_0660:
						jsonTextReader._charPos++;
						if (!char.IsWhiteSpace(c))
						{
							throw jsonTextReader.CreateUnexpectedCharacterException(c);
						}
						goto IL_0680;
						IL_04d0:
						if (!awaiter2.GetResult())
						{
							throw jsonTextReader.CreateUnexpectedCharacterException(jsonTextReader._chars[jsonTextReader._charPos]);
						}
						jsonTextReader.SetToken(JsonToken.Boolean, _003CisTrue_003E5__3);
						result = _003CisTrue_003E5__3;
						goto end_IL_000e;
						IL_0650:
						jsonTextReader._charPos++;
						goto IL_0680;
						IL_0648:
						jsonTextReader.ProcessLineFeed();
						goto IL_0680;
						IL_05d7:
						awaiter = jsonTextReader.ProcessCarriageReturnAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 7);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_063f;
						IL_0680:
						_003Ci_003E5__2 = default(BigInteger);
						goto IL_0102;
						IL_063f:
						awaiter.GetResult();
						goto IL_0680;
						IL_0590:
						jsonTextReader._charPos++;
						if (jsonTextReader._currentState != State.Array && jsonTextReader._currentState != State.ArrayStart && jsonTextReader._currentState != State.PostValue)
						{
							throw jsonTextReader.CreateUnexpectedCharacterException(c);
						}
						jsonTextReader.SetToken(JsonToken.EndArray);
						result = null;
						goto end_IL_000e;
						IL_037c:
						awaiter = jsonTextReader.ParseNumberAsync(ReadType.Read, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 4);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_03e4;
						IL_0368:
						awaiter.GetResult();
						result = null;
						goto end_IL_000e;
						IL_03e4:
						awaiter.GetResult();
						value = jsonTextReader.Value;
						if (value is BigInteger)
						{
							_003Ci_003E5__2 = (BigInteger)value;
							flag = _003Ci_003E5__2 != 0L;
						}
						else
						{
							flag = Convert.ToBoolean(jsonTextReader.Value, CultureInfo.InvariantCulture);
						}
						jsonTextReader.SetToken(JsonToken.Boolean, flag, false);
						result = flag;
						goto end_IL_000e;
						IL_0273:
						awaiter = jsonTextReader.ParseStringAsync(c, ReadType.Read, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 2);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_02dd;
						IL_0585:
						jsonTextReader.ProcessValueComma();
						goto IL_0680;
						IL_0511:
						awaiter = jsonTextReader.ParseCommentAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 6);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0579;
						IL_02dd:
						awaiter.GetResult();
						result = jsonTextReader.ReadBooleanString(jsonTextReader._stringReference.ToString());
						goto end_IL_000e;
					}
					awaiter.GetResult();
					result = null;
					end_IL_000e:;
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult(result);
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
		private struct _003CDoReadAsBytesAsync_003Ed__42 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			[Nullable(0)]
			public AsyncTaskMethodBuilder<byte[]> _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			private bool _003CisWrapped_003E5__2;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter _003C_003Eu__1;

			[Nullable(0)]
			private byte[] _003Cdata_003E5__3;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__2;

			[Nullable(new byte[] { 0, 2 })]
			private ConfiguredTaskAwaitable<byte[]>.ConfiguredTaskAwaiter _003C_003Eu__3;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				byte[] result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter3;
					ConfiguredTaskAwaitable<byte[]>.ConfiguredTaskAwaiter awaiter2;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					char c;
					switch (num)
					{
					default:
						jsonTextReader.EnsureBuffer();
						_003CisWrapped_003E5__2 = false;
						switch (jsonTextReader._currentState)
						{
						case State.PostValue:
							break;
						case State.Start:
						case State.Property:
						case State.ArrayStart:
						case State.Array:
						case State.ConstructorStart:
						case State.Constructor:
							goto IL_0107;
						case State.Finished:
							goto IL_0678;
						default:
							throw JsonReaderException.Create(jsonTextReader, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, jsonTextReader.CurrentState));
						}
						awaiter3 = jsonTextReader.ParsePostValueAsync(true, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter3;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
							return;
						}
						goto IL_00f7;
					case 0:
						awaiter3 = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_00f7;
					case 1:
						awaiter3 = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0219;
					case 2:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_029f;
					case 3:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0329;
					case 4:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_03f2;
					case 5:
						awaiter2 = _003C_003Eu__3;
						_003C_003Eu__3 = default(ConfiguredTaskAwaitable<byte[]>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0481;
					case 6:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_04f5;
					case 7:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_056b;
					case 8:
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_062b;
					case 9:
						{
							awaiter = _003C_003Eu__2;
							_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							break;
						}
						IL_0678:
						awaiter = jsonTextReader.ReadFinishedAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 9);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						break;
						IL_0405:
						jsonTextReader._charPos++;
						jsonTextReader.SetToken(JsonToken.StartArray);
						awaiter2 = jsonTextReader.ReadArrayIntoByteArrayAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_003C_003E1__state = 5);
							_003C_003Eu__3 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_0481;
						IL_0329:
						awaiter.GetResult();
						if (jsonTextReader.TokenType != JsonToken.EndObject)
						{
							throw JsonReaderException.Create(jsonTextReader, "Error reading bytes. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, jsonTextReader.TokenType));
						}
						jsonTextReader.SetToken(JsonToken.Bytes, _003Cdata_003E5__3, false);
						goto IL_036a;
						IL_04f5:
						awaiter.GetResult();
						result = null;
						goto end_IL_000e;
						IL_00f7:
						if (!awaiter3.GetResult())
						{
							goto IL_0107;
						}
						result = null;
						goto end_IL_000e;
						IL_0481:
						result = awaiter2.GetResult();
						goto end_IL_000e;
						IL_0107:
						c = jsonTextReader._chars[jsonTextReader._charPos];
						switch (c)
						{
						case '\0':
							break;
						case '"':
						case '\'':
							goto IL_0235;
						case '{':
							goto IL_0376;
						case '[':
							goto IL_0405;
						case 'n':
							goto IL_048e;
						case '/':
							goto IL_0503;
						case ',':
							goto IL_0577;
						case ']':
							goto IL_0582;
						case '\r':
							goto IL_05c3;
						case '\n':
							goto IL_0634;
						case '\t':
						case ' ':
							goto IL_063c;
						default:
							goto IL_064c;
						}
						awaiter3 = jsonTextReader.ReadNullCharAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter3.IsCompleted)
						{
							num = (_003C_003E1__state = 1);
							_003C_003Eu__1 = awaiter3;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter3, ref this);
							return;
						}
						goto IL_0219;
						IL_064c:
						jsonTextReader._charPos++;
						if (!char.IsWhiteSpace(c))
						{
							throw jsonTextReader.CreateUnexpectedCharacterException(c);
						}
						goto IL_066c;
						IL_066c:
						_003Cdata_003E5__3 = null;
						goto IL_0107;
						IL_063c:
						jsonTextReader._charPos++;
						goto IL_066c;
						IL_0634:
						jsonTextReader.ProcessLineFeed();
						goto IL_066c;
						IL_05c3:
						awaiter = jsonTextReader.ProcessCarriageReturnAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 8);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_062b;
						IL_03f2:
						awaiter.GetResult();
						_003CisWrapped_003E5__2 = true;
						goto IL_066c;
						IL_062b:
						awaiter.GetResult();
						goto IL_066c;
						IL_0582:
						jsonTextReader._charPos++;
						if (jsonTextReader._currentState != State.Array && jsonTextReader._currentState != State.ArrayStart && jsonTextReader._currentState != State.PostValue)
						{
							throw jsonTextReader.CreateUnexpectedCharacterException(c);
						}
						jsonTextReader.SetToken(JsonToken.EndArray);
						result = null;
						goto end_IL_000e;
						IL_036a:
						result = _003Cdata_003E5__3;
						goto end_IL_000e;
						IL_029f:
						awaiter.GetResult();
						_003Cdata_003E5__3 = (byte[])jsonTextReader.Value;
						if (_003CisWrapped_003E5__2)
						{
							awaiter = jsonTextReader.ReaderReadAndAssertAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_003C_003E1__state = 3);
								_003C_003Eu__2 = awaiter;
								_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_0329;
						}
						goto IL_036a;
						IL_0376:
						jsonTextReader._charPos++;
						jsonTextReader.SetToken(JsonToken.StartObject);
						awaiter = jsonTextReader.ReadIntoWrappedTypeObjectAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 4);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_03f2;
						IL_0235:
						awaiter = jsonTextReader.ParseStringAsync(c, ReadType.ReadAsBytes, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 2);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_029f;
						IL_0577:
						jsonTextReader.ProcessValueComma();
						goto IL_066c;
						IL_0503:
						awaiter = jsonTextReader.ParseCommentAsync(false, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 7);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_056b;
						IL_0219:
						if (!awaiter3.GetResult())
						{
							goto IL_066c;
						}
						jsonTextReader.SetToken(JsonToken.None, null, false);
						result = null;
						goto end_IL_000e;
						IL_056b:
						awaiter.GetResult();
						goto IL_066c;
						IL_048e:
						awaiter = jsonTextReader.HandleNullAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 6);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_04f5;
					}
					awaiter.GetResult();
					result = null;
					end_IL_000e:;
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult(result);
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
		private struct _003CReadIntoWrappedTypeObjectAsync_003Ed__43 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					default:
						awaiter = jsonTextReader.ReaderReadAndAssertAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0083;
					case 0:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0083;
					case 1:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0112;
					case 2:
						{
							awaiter = _003C_003Eu__1;
							_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							goto IL_01a2;
						}
						IL_0112:
						awaiter.GetResult();
						if (jsonTextReader.Value == null || !jsonTextReader.Value.ToString().StartsWith("System.Byte[]", StringComparison.Ordinal))
						{
							break;
						}
						awaiter = jsonTextReader.ReaderReadAndAssertAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 2);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_01a2;
						IL_0083:
						awaiter.GetResult();
						if (jsonTextReader.Value == null || !(jsonTextReader.Value.ToString() == "$type"))
						{
							break;
						}
						awaiter = jsonTextReader.ReaderReadAndAssertAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 1);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0112;
						IL_01a2:
						awaiter.GetResult();
						if (!(jsonTextReader.Value.ToString() == "$value"))
						{
							break;
						}
						goto end_IL_000e;
					}
					throw JsonReaderException.Create(jsonTextReader, "Error reading bytes. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, JsonToken.StartObject));
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
		private struct _003CDoReadAsDateTimeAsync_003Ed__45 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			[Nullable(0)]
			public AsyncTaskMethodBuilder<DateTime?> _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			[Nullable(new byte[] { 0, 2 })]
			private ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				DateTime? result;
				try
				{
					ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = jsonTextReader.ReadStringValueAsync(ReadType.ReadAsDateTime, cancellationToken).ConfigureAwait(false).GetAwaiter();
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
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
					}
					result = (DateTime?)awaiter.GetResult();
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult(result);
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
		private struct _003CDoReadAsDateTimeOffsetAsync_003Ed__47 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			[Nullable(0)]
			public AsyncTaskMethodBuilder<DateTimeOffset?> _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			[Nullable(new byte[] { 0, 2 })]
			private ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				DateTimeOffset? result;
				try
				{
					ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = jsonTextReader.ReadStringValueAsync(ReadType.ReadAsDateTimeOffset, cancellationToken).ConfigureAwait(false).GetAwaiter();
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
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
					}
					result = (DateTimeOffset?)awaiter.GetResult();
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult(result);
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
		private struct _003CDoReadAsDecimalAsync_003Ed__49 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			[Nullable(0)]
			public AsyncTaskMethodBuilder<decimal?> _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			[Nullable(new byte[] { 0, 2 })]
			private ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				decimal? result;
				try
				{
					ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = jsonTextReader.ReadNumberValueAsync(ReadType.ReadAsDecimal, cancellationToken).ConfigureAwait(false).GetAwaiter();
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
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
					}
					result = (decimal?)awaiter.GetResult();
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult(result);
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
		private struct _003CDoReadAsDoubleAsync_003Ed__51 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			[Nullable(0)]
			public AsyncTaskMethodBuilder<double?> _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			[Nullable(new byte[] { 0, 2 })]
			private ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				double? result;
				try
				{
					ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = jsonTextReader.ReadNumberValueAsync(ReadType.ReadAsDouble, cancellationToken).ConfigureAwait(false).GetAwaiter();
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
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
					}
					result = (double?)awaiter.GetResult();
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult(result);
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
		private struct _003CDoReadAsInt32Async_003Ed__53 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			[Nullable(0)]
			public AsyncTaskMethodBuilder<int?> _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			[Nullable(new byte[] { 0, 2 })]
			private ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				int? result;
				try
				{
					ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = jsonTextReader.ReadNumberValueAsync(ReadType.ReadAsInt32, cancellationToken).ConfigureAwait(false).GetAwaiter();
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
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
					}
					result = (int?)awaiter.GetResult();
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult(result);
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
		private struct _003CDoReadAsStringAsync_003Ed__55 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			[Nullable(0)]
			public AsyncTaskMethodBuilder<string> _003C_003Et__builder;

			[Nullable(0)]
			public JsonTextReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			[Nullable(new byte[] { 0, 2 })]
			private ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonTextReader jsonTextReader = _003C_003E4__this;
				string result;
				try
				{
					ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = jsonTextReader.ReadStringValueAsync(ReadType.ReadAsString, cancellationToken).ConfigureAwait(false).GetAwaiter();
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
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<object>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
					}
					result = (string)awaiter.GetResult();
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003C_003Et__builder.SetResult(result);
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

		private const char UnicodeReplacementChar = '\ufffd';

		private const int MaximumJavascriptIntegerCharacterLength = 380;

		private const int LargeBufferLength = 1073741823;

		private readonly TextReader _reader;

		[Nullable(2)]
		private char[] _chars;

		private int _charsUsed;

		private int _charPos;

		private int _lineStartPos;

		private int _lineNumber;

		private bool _isEndOfFile;

		private StringBuffer _stringBuffer;

		private StringReference _stringReference;

		[Nullable(2)]
		private IArrayPool<char> _arrayPool;

		[Nullable(2)]
		[field: Nullable(2)]
		public JsonNameTable PropertyNameTable
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			set;
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

		public int LineNumber
		{
			get
			{
				if (base.CurrentState == State.Start && LinePosition == 0 && TokenType != JsonToken.Comment)
				{
					return 0;
				}
				return _lineNumber;
			}
		}

		public int LinePosition
		{
			get
			{
				return _charPos - _lineStartPos;
			}
		}

		public override Task<bool> ReadAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.ReadAsync(cancellationToken);
			}
			return DoReadAsync(cancellationToken);
		}

		internal Task<bool> DoReadAsync(CancellationToken cancellationToken)
		{
			EnsureBuffer();
			Task<bool> task;
			do
			{
				switch (_currentState)
				{
				case State.Start:
				case State.Property:
				case State.ArrayStart:
				case State.Array:
				case State.ConstructorStart:
				case State.Constructor:
					return ParseValueAsync(cancellationToken);
				case State.ObjectStart:
				case State.Object:
					return ParseObjectAsync(cancellationToken);
				case State.PostValue:
					task = ParsePostValueAsync(false, cancellationToken);
					if (!task.IsCompletedSucessfully())
					{
						return DoReadAsync(task, cancellationToken);
					}
					break;
				case State.Finished:
					return ReadFromFinishedAsync(cancellationToken);
				default:
					throw JsonReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
				}
			}
			while (!task.Result);
			return AsyncUtils.True;
		}

		[AsyncStateMachine(typeof(_003CDoReadAsync_003Ed__3))]
		private Task<bool> DoReadAsync(Task<bool> task, CancellationToken cancellationToken)
		{
			_003CDoReadAsync_003Ed__3 stateMachine = default(_003CDoReadAsync_003Ed__3);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<bool>.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.task = task;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[AsyncStateMachine(typeof(_003CParsePostValueAsync_003Ed__4))]
		private Task<bool> ParsePostValueAsync(bool ignoreComments, CancellationToken cancellationToken)
		{
			_003CParsePostValueAsync_003Ed__4 stateMachine = default(_003CParsePostValueAsync_003Ed__4);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<bool>.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.ignoreComments = ignoreComments;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[AsyncStateMachine(typeof(_003CReadFromFinishedAsync_003Ed__5))]
		private Task<bool> ReadFromFinishedAsync(CancellationToken cancellationToken)
		{
			_003CReadFromFinishedAsync_003Ed__5 stateMachine = default(_003CReadFromFinishedAsync_003Ed__5);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<bool>.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		private Task<int> ReadDataAsync(bool append, CancellationToken cancellationToken)
		{
			return ReadDataAsync(append, 0, cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CReadDataAsync_003Ed__7))]
		private Task<int> ReadDataAsync(bool append, int charsRequired, CancellationToken cancellationToken)
		{
			_003CReadDataAsync_003Ed__7 stateMachine = default(_003CReadDataAsync_003Ed__7);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<int>.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.append = append;
			stateMachine.charsRequired = charsRequired;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[AsyncStateMachine(typeof(_003CParseValueAsync_003Ed__8))]
		private Task<bool> ParseValueAsync(CancellationToken cancellationToken)
		{
			_003CParseValueAsync_003Ed__8 stateMachine = default(_003CParseValueAsync_003Ed__8);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<bool>.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[AsyncStateMachine(typeof(_003CReadStringIntoBufferAsync_003Ed__9))]
		private Task ReadStringIntoBufferAsync(char quote, CancellationToken cancellationToken)
		{
			_003CReadStringIntoBufferAsync_003Ed__9 stateMachine = default(_003CReadStringIntoBufferAsync_003Ed__9);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.quote = quote;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		private Task ProcessCarriageReturnAsync(bool append, CancellationToken cancellationToken)
		{
			_charPos++;
			Task<bool> task = EnsureCharsAsync(1, append, cancellationToken);
			if (task.IsCompletedSucessfully())
			{
				SetNewLine(task.Result);
				return AsyncUtils.CompletedTask;
			}
			return ProcessCarriageReturnAsync(task);
		}

		[AsyncStateMachine(typeof(_003CProcessCarriageReturnAsync_003Ed__11))]
		private Task ProcessCarriageReturnAsync(Task<bool> task)
		{
			_003CProcessCarriageReturnAsync_003Ed__11 stateMachine = default(_003CProcessCarriageReturnAsync_003Ed__11);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.task = task;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[AsyncStateMachine(typeof(_003CParseUnicodeAsync_003Ed__12))]
		private Task<char> ParseUnicodeAsync(CancellationToken cancellationToken)
		{
			_003CParseUnicodeAsync_003Ed__12 stateMachine = default(_003CParseUnicodeAsync_003Ed__12);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<char>.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		private Task<bool> EnsureCharsAsync(int relativePosition, bool append, CancellationToken cancellationToken)
		{
			if (_charPos + relativePosition < _charsUsed)
			{
				return AsyncUtils.True;
			}
			if (_isEndOfFile)
			{
				return AsyncUtils.False;
			}
			return ReadCharsAsync(relativePosition, append, cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CReadCharsAsync_003Ed__14))]
		private Task<bool> ReadCharsAsync(int relativePosition, bool append, CancellationToken cancellationToken)
		{
			_003CReadCharsAsync_003Ed__14 stateMachine = default(_003CReadCharsAsync_003Ed__14);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<bool>.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.relativePosition = relativePosition;
			stateMachine.append = append;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[AsyncStateMachine(typeof(_003CParseObjectAsync_003Ed__15))]
		private Task<bool> ParseObjectAsync(CancellationToken cancellationToken)
		{
			_003CParseObjectAsync_003Ed__15 stateMachine = default(_003CParseObjectAsync_003Ed__15);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<bool>.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[AsyncStateMachine(typeof(_003CParseCommentAsync_003Ed__16))]
		private Task ParseCommentAsync(bool setToken, CancellationToken cancellationToken)
		{
			_003CParseCommentAsync_003Ed__16 stateMachine = default(_003CParseCommentAsync_003Ed__16);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.setToken = setToken;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[AsyncStateMachine(typeof(_003CEatWhitespaceAsync_003Ed__17))]
		private Task EatWhitespaceAsync(CancellationToken cancellationToken)
		{
			_003CEatWhitespaceAsync_003Ed__17 stateMachine = default(_003CEatWhitespaceAsync_003Ed__17);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[AsyncStateMachine(typeof(_003CParseStringAsync_003Ed__18))]
		private Task ParseStringAsync(char quote, ReadType readType, CancellationToken cancellationToken)
		{
			_003CParseStringAsync_003Ed__18 stateMachine = default(_003CParseStringAsync_003Ed__18);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.quote = quote;
			stateMachine.readType = readType;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[AsyncStateMachine(typeof(_003CMatchValueAsync_003Ed__19))]
		private Task<bool> MatchValueAsync(string value, CancellationToken cancellationToken)
		{
			_003CMatchValueAsync_003Ed__19 stateMachine = default(_003CMatchValueAsync_003Ed__19);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<bool>.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.value = value;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[AsyncStateMachine(typeof(_003CMatchValueWithTrailingSeparatorAsync_003Ed__20))]
		private Task<bool> MatchValueWithTrailingSeparatorAsync(string value, CancellationToken cancellationToken)
		{
			_003CMatchValueWithTrailingSeparatorAsync_003Ed__20 stateMachine = default(_003CMatchValueWithTrailingSeparatorAsync_003Ed__20);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<bool>.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.value = value;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[AsyncStateMachine(typeof(_003CMatchAndSetAsync_003Ed__21))]
		private Task MatchAndSetAsync(string value, JsonToken newToken, [Nullable(2)] object tokenValue, CancellationToken cancellationToken)
		{
			_003CMatchAndSetAsync_003Ed__21 stateMachine = default(_003CMatchAndSetAsync_003Ed__21);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.value = value;
			stateMachine.newToken = newToken;
			stateMachine.tokenValue = tokenValue;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		private Task ParseTrueAsync(CancellationToken cancellationToken)
		{
			return MatchAndSetAsync(JsonConvert.True, JsonToken.Boolean, true, cancellationToken);
		}

		private Task ParseFalseAsync(CancellationToken cancellationToken)
		{
			return MatchAndSetAsync(JsonConvert.False, JsonToken.Boolean, false, cancellationToken);
		}

		private Task ParseNullAsync(CancellationToken cancellationToken)
		{
			return MatchAndSetAsync(JsonConvert.Null, JsonToken.Null, null, cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CParseConstructorAsync_003Ed__25))]
		private Task ParseConstructorAsync(CancellationToken cancellationToken)
		{
			_003CParseConstructorAsync_003Ed__25 stateMachine = default(_003CParseConstructorAsync_003Ed__25);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[AsyncStateMachine(typeof(_003CParseNumberNaNAsync_003Ed__26))]
		private Task<object> ParseNumberNaNAsync(ReadType readType, CancellationToken cancellationToken)
		{
			_003CParseNumberNaNAsync_003Ed__26 stateMachine = default(_003CParseNumberNaNAsync_003Ed__26);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<object>.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.readType = readType;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[AsyncStateMachine(typeof(_003CParseNumberPositiveInfinityAsync_003Ed__27))]
		private Task<object> ParseNumberPositiveInfinityAsync(ReadType readType, CancellationToken cancellationToken)
		{
			_003CParseNumberPositiveInfinityAsync_003Ed__27 stateMachine = default(_003CParseNumberPositiveInfinityAsync_003Ed__27);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<object>.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.readType = readType;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[AsyncStateMachine(typeof(_003CParseNumberNegativeInfinityAsync_003Ed__28))]
		private Task<object> ParseNumberNegativeInfinityAsync(ReadType readType, CancellationToken cancellationToken)
		{
			_003CParseNumberNegativeInfinityAsync_003Ed__28 stateMachine = default(_003CParseNumberNegativeInfinityAsync_003Ed__28);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<object>.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.readType = readType;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[AsyncStateMachine(typeof(_003CParseNumberAsync_003Ed__29))]
		private Task ParseNumberAsync(ReadType readType, CancellationToken cancellationToken)
		{
			_003CParseNumberAsync_003Ed__29 stateMachine = default(_003CParseNumberAsync_003Ed__29);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.readType = readType;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		private Task ParseUndefinedAsync(CancellationToken cancellationToken)
		{
			return MatchAndSetAsync(JsonConvert.Undefined, JsonToken.Undefined, null, cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CParsePropertyAsync_003Ed__31))]
		private Task<bool> ParsePropertyAsync(CancellationToken cancellationToken)
		{
			_003CParsePropertyAsync_003Ed__31 stateMachine = default(_003CParsePropertyAsync_003Ed__31);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<bool>.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[AsyncStateMachine(typeof(_003CReadNumberIntoBufferAsync_003Ed__32))]
		private Task ReadNumberIntoBufferAsync(CancellationToken cancellationToken)
		{
			_003CReadNumberIntoBufferAsync_003Ed__32 stateMachine = default(_003CReadNumberIntoBufferAsync_003Ed__32);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[AsyncStateMachine(typeof(_003CParseUnquotedPropertyAsync_003Ed__33))]
		private Task ParseUnquotedPropertyAsync(CancellationToken cancellationToken)
		{
			_003CParseUnquotedPropertyAsync_003Ed__33 stateMachine = default(_003CParseUnquotedPropertyAsync_003Ed__33);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[AsyncStateMachine(typeof(_003CReadNullCharAsync_003Ed__34))]
		private Task<bool> ReadNullCharAsync(CancellationToken cancellationToken)
		{
			_003CReadNullCharAsync_003Ed__34 stateMachine = default(_003CReadNullCharAsync_003Ed__34);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<bool>.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[AsyncStateMachine(typeof(_003CHandleNullAsync_003Ed__35))]
		private Task HandleNullAsync(CancellationToken cancellationToken)
		{
			_003CHandleNullAsync_003Ed__35 stateMachine = default(_003CHandleNullAsync_003Ed__35);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[AsyncStateMachine(typeof(_003CReadFinishedAsync_003Ed__36))]
		private Task ReadFinishedAsync(CancellationToken cancellationToken)
		{
			_003CReadFinishedAsync_003Ed__36 stateMachine = default(_003CReadFinishedAsync_003Ed__36);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[AsyncStateMachine(typeof(_003CReadStringValueAsync_003Ed__37))]
		[return: Nullable(new byte[] { 1, 2 })]
		private Task<object> ReadStringValueAsync(ReadType readType, CancellationToken cancellationToken)
		{
			_003CReadStringValueAsync_003Ed__37 stateMachine = default(_003CReadStringValueAsync_003Ed__37);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<object>.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.readType = readType;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[AsyncStateMachine(typeof(_003CReadNumberValueAsync_003Ed__38))]
		[return: Nullable(new byte[] { 1, 2 })]
		private Task<object> ReadNumberValueAsync(ReadType readType, CancellationToken cancellationToken)
		{
			_003CReadNumberValueAsync_003Ed__38 stateMachine = default(_003CReadNumberValueAsync_003Ed__38);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<object>.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.readType = readType;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public override Task<bool?> ReadAsBooleanAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.ReadAsBooleanAsync(cancellationToken);
			}
			return DoReadAsBooleanAsync(cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CDoReadAsBooleanAsync_003Ed__40))]
		internal Task<bool?> DoReadAsBooleanAsync(CancellationToken cancellationToken)
		{
			_003CDoReadAsBooleanAsync_003Ed__40 stateMachine = default(_003CDoReadAsBooleanAsync_003Ed__40);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<bool?>.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[return: Nullable(new byte[] { 1, 2 })]
		public override Task<byte[]> ReadAsBytesAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.ReadAsBytesAsync(cancellationToken);
			}
			return DoReadAsBytesAsync(cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CDoReadAsBytesAsync_003Ed__42))]
		[return: Nullable(new byte[] { 1, 2 })]
		internal Task<byte[]> DoReadAsBytesAsync(CancellationToken cancellationToken)
		{
			_003CDoReadAsBytesAsync_003Ed__42 stateMachine = default(_003CDoReadAsBytesAsync_003Ed__42);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<byte[]>.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[AsyncStateMachine(typeof(_003CReadIntoWrappedTypeObjectAsync_003Ed__43))]
		private Task ReadIntoWrappedTypeObjectAsync(CancellationToken cancellationToken)
		{
			_003CReadIntoWrappedTypeObjectAsync_003Ed__43 stateMachine = default(_003CReadIntoWrappedTypeObjectAsync_003Ed__43);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public override Task<DateTime?> ReadAsDateTimeAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.ReadAsDateTimeAsync(cancellationToken);
			}
			return DoReadAsDateTimeAsync(cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CDoReadAsDateTimeAsync_003Ed__45))]
		internal Task<DateTime?> DoReadAsDateTimeAsync(CancellationToken cancellationToken)
		{
			_003CDoReadAsDateTimeAsync_003Ed__45 stateMachine = default(_003CDoReadAsDateTimeAsync_003Ed__45);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<DateTime?>.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public override Task<DateTimeOffset?> ReadAsDateTimeOffsetAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.ReadAsDateTimeOffsetAsync(cancellationToken);
			}
			return DoReadAsDateTimeOffsetAsync(cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CDoReadAsDateTimeOffsetAsync_003Ed__47))]
		internal Task<DateTimeOffset?> DoReadAsDateTimeOffsetAsync(CancellationToken cancellationToken)
		{
			_003CDoReadAsDateTimeOffsetAsync_003Ed__47 stateMachine = default(_003CDoReadAsDateTimeOffsetAsync_003Ed__47);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<DateTimeOffset?>.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public override Task<decimal?> ReadAsDecimalAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.ReadAsDecimalAsync(cancellationToken);
			}
			return DoReadAsDecimalAsync(cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CDoReadAsDecimalAsync_003Ed__49))]
		internal Task<decimal?> DoReadAsDecimalAsync(CancellationToken cancellationToken)
		{
			_003CDoReadAsDecimalAsync_003Ed__49 stateMachine = default(_003CDoReadAsDecimalAsync_003Ed__49);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<decimal?>.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public override Task<double?> ReadAsDoubleAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.ReadAsDoubleAsync(cancellationToken);
			}
			return DoReadAsDoubleAsync(cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CDoReadAsDoubleAsync_003Ed__51))]
		internal Task<double?> DoReadAsDoubleAsync(CancellationToken cancellationToken)
		{
			_003CDoReadAsDoubleAsync_003Ed__51 stateMachine = default(_003CDoReadAsDoubleAsync_003Ed__51);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<double?>.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public override Task<int?> ReadAsInt32Async(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.ReadAsInt32Async(cancellationToken);
			}
			return DoReadAsInt32Async(cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CDoReadAsInt32Async_003Ed__53))]
		internal Task<int?> DoReadAsInt32Async(CancellationToken cancellationToken)
		{
			_003CDoReadAsInt32Async_003Ed__53 stateMachine = default(_003CDoReadAsInt32Async_003Ed__53);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<int?>.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[return: Nullable(new byte[] { 1, 2 })]
		public override Task<string> ReadAsStringAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (!_safeAsync)
			{
				return base.ReadAsStringAsync(cancellationToken);
			}
			return DoReadAsStringAsync(cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CDoReadAsStringAsync_003Ed__55))]
		[return: Nullable(new byte[] { 1, 2 })]
		internal Task<string> DoReadAsStringAsync(CancellationToken cancellationToken)
		{
			_003CDoReadAsStringAsync_003Ed__55 stateMachine = default(_003CDoReadAsStringAsync_003Ed__55);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<string>.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public JsonTextReader(TextReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			_reader = reader;
			_lineNumber = 1;
			_safeAsync = GetType() == typeof(JsonTextReader);
		}

		private void EnsureBufferNotEmpty()
		{
			if (_stringBuffer.IsEmpty)
			{
				_stringBuffer = new StringBuffer(_arrayPool, 1024);
			}
		}

		private void SetNewLine(bool hasNextChar)
		{
			if (hasNextChar && _chars[_charPos] == '\n')
			{
				_charPos++;
			}
			OnNewLine(_charPos);
		}

		private void OnNewLine(int pos)
		{
			_lineNumber++;
			_lineStartPos = pos;
		}

		private void ParseString(char quote, ReadType readType)
		{
			_charPos++;
			ShiftBufferIfNeeded();
			ReadStringIntoBuffer(quote);
			ParseReadString(quote, readType);
		}

		private void ParseReadString(char quote, ReadType readType)
		{
			SetPostValueState(true);
			switch (readType)
			{
			case ReadType.ReadAsBytes:
			{
				Guid g;
				byte[] value2 = ((_stringReference.Length == 0) ? CollectionUtils.ArrayEmpty<byte>() : ((_stringReference.Length != 36 || !ConvertUtils.TryConvertGuid(_stringReference.ToString(), out g)) ? Convert.FromBase64CharArray(_stringReference.Chars, _stringReference.StartIndex, _stringReference.Length) : g.ToByteArray()));
				SetToken(JsonToken.Bytes, value2, false);
				return;
			}
			case ReadType.ReadAsString:
			{
				string value = _stringReference.ToString();
				SetToken(JsonToken.String, value, false);
				_quoteChar = quote;
				return;
			}
			case ReadType.ReadAsInt32:
			case ReadType.ReadAsDecimal:
			case ReadType.ReadAsBoolean:
				return;
			}
			if (_dateParseHandling != 0)
			{
				DateParseHandling dateParseHandling;
				switch (readType)
				{
				case ReadType.ReadAsDateTime:
					dateParseHandling = DateParseHandling.DateTime;
					break;
				case ReadType.ReadAsDateTimeOffset:
					dateParseHandling = DateParseHandling.DateTimeOffset;
					break;
				default:
					dateParseHandling = _dateParseHandling;
					break;
				}
				DateTimeOffset dt2;
				if (dateParseHandling == DateParseHandling.DateTime)
				{
					DateTime dt;
					if (DateTimeUtils.TryParseDateTime(_stringReference, base.DateTimeZoneHandling, base.DateFormatString, base.Culture, out dt))
					{
						SetToken(JsonToken.Date, dt, false);
						return;
					}
				}
				else if (DateTimeUtils.TryParseDateTimeOffset(_stringReference, base.DateFormatString, base.Culture, out dt2))
				{
					SetToken(JsonToken.Date, dt2, false);
					return;
				}
			}
			SetToken(JsonToken.String, _stringReference.ToString(), false);
			_quoteChar = quote;
		}

		private static void BlockCopyChars(char[] src, int srcOffset, char[] dst, int dstOffset, int count)
		{
			Buffer.BlockCopy(src, srcOffset * 2, dst, dstOffset * 2, count * 2);
		}

		private void ShiftBufferIfNeeded()
		{
			int num = _chars.Length;
			if ((double)(num - _charPos) <= (double)num * 0.1 || num >= 1073741823)
			{
				int num2 = _charsUsed - _charPos;
				if (num2 > 0)
				{
					BlockCopyChars(_chars, _charPos, _chars, 0, num2);
				}
				_lineStartPos -= _charPos;
				_charPos = 0;
				_charsUsed = num2;
				_chars[_charsUsed] = '\0';
			}
		}

		private int ReadData(bool append)
		{
			return ReadData(append, 0);
		}

		private void PrepareBufferForReadData(bool append, int charsRequired)
		{
			if (_charsUsed + charsRequired < _chars.Length - 1)
			{
				return;
			}
			if (append)
			{
				int num = _chars.Length * 2;
				int minSize = Math.Max((num < 0) ? int.MaxValue : num, _charsUsed + charsRequired + 1);
				char[] array = BufferUtils.RentBuffer(_arrayPool, minSize);
				BlockCopyChars(_chars, 0, array, 0, _chars.Length);
				BufferUtils.ReturnBuffer(_arrayPool, _chars);
				_chars = array;
				return;
			}
			int num2 = _charsUsed - _charPos;
			if (num2 + charsRequired + 1 >= _chars.Length)
			{
				char[] array2 = BufferUtils.RentBuffer(_arrayPool, num2 + charsRequired + 1);
				if (num2 > 0)
				{
					BlockCopyChars(_chars, _charPos, array2, 0, num2);
				}
				BufferUtils.ReturnBuffer(_arrayPool, _chars);
				_chars = array2;
			}
			else if (num2 > 0)
			{
				BlockCopyChars(_chars, _charPos, _chars, 0, num2);
			}
			_lineStartPos -= _charPos;
			_charPos = 0;
			_charsUsed = num2;
		}

		private int ReadData(bool append, int charsRequired)
		{
			if (_isEndOfFile)
			{
				return 0;
			}
			PrepareBufferForReadData(append, charsRequired);
			int count = _chars.Length - _charsUsed - 1;
			int num = _reader.Read(_chars, _charsUsed, count);
			_charsUsed += num;
			if (num == 0)
			{
				_isEndOfFile = true;
			}
			_chars[_charsUsed] = '\0';
			return num;
		}

		private bool EnsureChars(int relativePosition, bool append)
		{
			if (_charPos + relativePosition >= _charsUsed)
			{
				return ReadChars(relativePosition, append);
			}
			return true;
		}

		private bool ReadChars(int relativePosition, bool append)
		{
			if (_isEndOfFile)
			{
				return false;
			}
			int num = _charPos + relativePosition - _charsUsed + 1;
			int num2 = 0;
			do
			{
				int num3 = ReadData(append, num - num2);
				if (num3 == 0)
				{
					break;
				}
				num2 += num3;
			}
			while (num2 < num);
			if (num2 < num)
			{
				return false;
			}
			return true;
		}

		public override bool Read()
		{
			EnsureBuffer();
			do
			{
				switch (_currentState)
				{
				case State.Start:
				case State.Property:
				case State.ArrayStart:
				case State.Array:
				case State.ConstructorStart:
				case State.Constructor:
					return ParseValue();
				case State.ObjectStart:
				case State.Object:
					return ParseObject();
				case State.PostValue:
					break;
				case State.Finished:
					if (EnsureChars(0, false))
					{
						EatWhitespace();
						if (_isEndOfFile)
						{
							SetToken(JsonToken.None);
							return false;
						}
						if (_chars[_charPos] == '/')
						{
							ParseComment(true);
							return true;
						}
						throw JsonReaderException.Create(this, "Additional text encountered after finished reading JSON content: {0}.".FormatWith(CultureInfo.InvariantCulture, _chars[_charPos]));
					}
					SetToken(JsonToken.None);
					return false;
				default:
					throw JsonReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
				}
			}
			while (!ParsePostValue(false));
			return true;
		}

		public override int? ReadAsInt32()
		{
			return (int?)ReadNumberValue(ReadType.ReadAsInt32);
		}

		public override DateTime? ReadAsDateTime()
		{
			return (DateTime?)ReadStringValue(ReadType.ReadAsDateTime);
		}

		[NullableContext(2)]
		public override string ReadAsString()
		{
			return (string)ReadStringValue(ReadType.ReadAsString);
		}

		[NullableContext(2)]
		public override byte[] ReadAsBytes()
		{
			EnsureBuffer();
			bool flag = false;
			switch (_currentState)
			{
			case State.PostValue:
				if (ParsePostValue(true))
				{
					return null;
				}
				goto case State.Start;
			case State.Start:
			case State.Property:
			case State.ArrayStart:
			case State.Array:
			case State.ConstructorStart:
			case State.Constructor:
				while (true)
				{
					char c = _chars[_charPos];
					switch (c)
					{
					case '\0':
						if (ReadNullChar())
						{
							SetToken(JsonToken.None, null, false);
							return null;
						}
						break;
					case '"':
					case '\'':
					{
						ParseString(c, ReadType.ReadAsBytes);
						byte[] array = (byte[])Value;
						if (flag)
						{
							ReaderReadAndAssert();
							if (TokenType != JsonToken.EndObject)
							{
								throw JsonReaderException.Create(this, "Error reading bytes. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, TokenType));
							}
							SetToken(JsonToken.Bytes, array, false);
						}
						return array;
					}
					case '{':
						_charPos++;
						SetToken(JsonToken.StartObject);
						ReadIntoWrappedTypeObject();
						flag = true;
						break;
					case '[':
						_charPos++;
						SetToken(JsonToken.StartArray);
						return ReadArrayIntoByteArray();
					case 'n':
						HandleNull();
						return null;
					case '/':
						ParseComment(false);
						break;
					case ',':
						ProcessValueComma();
						break;
					case ']':
						_charPos++;
						if (_currentState == State.Array || _currentState == State.ArrayStart || _currentState == State.PostValue)
						{
							SetToken(JsonToken.EndArray);
							return null;
						}
						throw CreateUnexpectedCharacterException(c);
					case '\r':
						ProcessCarriageReturn(false);
						break;
					case '\n':
						ProcessLineFeed();
						break;
					case '\t':
					case ' ':
						_charPos++;
						break;
					default:
						_charPos++;
						if (!char.IsWhiteSpace(c))
						{
							throw CreateUnexpectedCharacterException(c);
						}
						break;
					}
				}
			case State.Finished:
				ReadFinished();
				return null;
			default:
				throw JsonReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
			}
		}

		[NullableContext(2)]
		private object ReadStringValue(ReadType readType)
		{
			EnsureBuffer();
			switch (_currentState)
			{
			case State.PostValue:
				if (ParsePostValue(true))
				{
					return null;
				}
				goto case State.Start;
			case State.Start:
			case State.Property:
			case State.ArrayStart:
			case State.Array:
			case State.ConstructorStart:
			case State.Constructor:
				while (true)
				{
					char c = _chars[_charPos];
					switch (c)
					{
					case '\0':
						if (ReadNullChar())
						{
							SetToken(JsonToken.None, null, false);
							return null;
						}
						break;
					case '"':
					case '\'':
						ParseString(c, readType);
						return FinishReadQuotedStringValue(readType);
					case '-':
						if (EnsureChars(1, true) && _chars[_charPos + 1] == 'I')
						{
							return ParseNumberNegativeInfinity(readType);
						}
						ParseNumber(readType);
						return Value;
					case '.':
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
						if (readType != ReadType.ReadAsString)
						{
							_charPos++;
							throw CreateUnexpectedCharacterException(c);
						}
						ParseNumber(ReadType.ReadAsString);
						return Value;
					case 'f':
					case 't':
					{
						if (readType != ReadType.ReadAsString)
						{
							_charPos++;
							throw CreateUnexpectedCharacterException(c);
						}
						string text = ((c == 't') ? JsonConvert.True : JsonConvert.False);
						if (!MatchValueWithTrailingSeparator(text))
						{
							throw CreateUnexpectedCharacterException(_chars[_charPos]);
						}
						SetToken(JsonToken.String, text);
						return text;
					}
					case 'I':
						return ParseNumberPositiveInfinity(readType);
					case 'N':
						return ParseNumberNaN(readType);
					case 'n':
						HandleNull();
						return null;
					case '/':
						ParseComment(false);
						break;
					case ',':
						ProcessValueComma();
						break;
					case ']':
						_charPos++;
						if (_currentState == State.Array || _currentState == State.ArrayStart || _currentState == State.PostValue)
						{
							SetToken(JsonToken.EndArray);
							return null;
						}
						throw CreateUnexpectedCharacterException(c);
					case '\r':
						ProcessCarriageReturn(false);
						break;
					case '\n':
						ProcessLineFeed();
						break;
					case '\t':
					case ' ':
						_charPos++;
						break;
					default:
						_charPos++;
						if (!char.IsWhiteSpace(c))
						{
							throw CreateUnexpectedCharacterException(c);
						}
						break;
					}
				}
			case State.Finished:
				ReadFinished();
				return null;
			default:
				throw JsonReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
			}
		}

		[NullableContext(2)]
		private object FinishReadQuotedStringValue(ReadType readType)
		{
			switch (readType)
			{
			case ReadType.ReadAsBytes:
			case ReadType.ReadAsString:
				return Value;
			case ReadType.ReadAsDateTime:
			{
				object value = Value;
				if (value is DateTime)
				{
					DateTime dateTime = (DateTime)value;
					return dateTime;
				}
				return ReadDateTimeString((string)Value);
			}
			case ReadType.ReadAsDateTimeOffset:
			{
				object value = Value;
				if (value is DateTimeOffset)
				{
					DateTimeOffset dateTimeOffset = (DateTimeOffset)value;
					return dateTimeOffset;
				}
				return ReadDateTimeOffsetString((string)Value);
			}
			default:
				throw new ArgumentOutOfRangeException("readType");
			}
		}

		private JsonReaderException CreateUnexpectedCharacterException(char c)
		{
			return JsonReaderException.Create(this, "Unexpected character encountered while parsing value: {0}.".FormatWith(CultureInfo.InvariantCulture, c));
		}

		public override bool? ReadAsBoolean()
		{
			EnsureBuffer();
			switch (_currentState)
			{
			case State.PostValue:
				if (ParsePostValue(true))
				{
					return null;
				}
				goto case State.Start;
			case State.Start:
			case State.Property:
			case State.ArrayStart:
			case State.Array:
			case State.ConstructorStart:
			case State.Constructor:
				while (true)
				{
					char c = _chars[_charPos];
					switch (c)
					{
					case '\0':
						if (ReadNullChar())
						{
							SetToken(JsonToken.None, null, false);
							return null;
						}
						break;
					case '"':
					case '\'':
						ParseString(c, ReadType.Read);
						return ReadBooleanString(_stringReference.ToString());
					case 'n':
						HandleNull();
						return null;
					case '-':
					case '.':
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
					{
						ParseNumber(ReadType.Read);
						object value2 = Value;
						bool flag2;
						if (value2 is BigInteger)
						{
							BigInteger bigInteger = (BigInteger)value2;
							flag2 = bigInteger != 0L;
						}
						else
						{
							flag2 = Convert.ToBoolean(Value, CultureInfo.InvariantCulture);
						}
						SetToken(JsonToken.Boolean, flag2, false);
						return flag2;
					}
					case 'f':
					case 't':
					{
						bool flag = c == 't';
						string value = (flag ? JsonConvert.True : JsonConvert.False);
						if (!MatchValueWithTrailingSeparator(value))
						{
							throw CreateUnexpectedCharacterException(_chars[_charPos]);
						}
						SetToken(JsonToken.Boolean, flag);
						return flag;
					}
					case '/':
						ParseComment(false);
						break;
					case ',':
						ProcessValueComma();
						break;
					case ']':
						_charPos++;
						if (_currentState == State.Array || _currentState == State.ArrayStart || _currentState == State.PostValue)
						{
							SetToken(JsonToken.EndArray);
							return null;
						}
						throw CreateUnexpectedCharacterException(c);
					case '\r':
						ProcessCarriageReturn(false);
						break;
					case '\n':
						ProcessLineFeed();
						break;
					case '\t':
					case ' ':
						_charPos++;
						break;
					default:
						_charPos++;
						if (!char.IsWhiteSpace(c))
						{
							throw CreateUnexpectedCharacterException(c);
						}
						break;
					}
				}
			case State.Finished:
				ReadFinished();
				return null;
			default:
				throw JsonReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
			}
		}

		private void ProcessValueComma()
		{
			_charPos++;
			if (_currentState != State.PostValue)
			{
				SetToken(JsonToken.Undefined);
				JsonReaderException ex = CreateUnexpectedCharacterException(',');
				_charPos--;
				throw ex;
			}
			SetStateBasedOnCurrent();
		}

		[NullableContext(2)]
		private object ReadNumberValue(ReadType readType)
		{
			EnsureBuffer();
			switch (_currentState)
			{
			case State.PostValue:
				if (ParsePostValue(true))
				{
					return null;
				}
				goto case State.Start;
			case State.Start:
			case State.Property:
			case State.ArrayStart:
			case State.Array:
			case State.ConstructorStart:
			case State.Constructor:
				while (true)
				{
					char c = _chars[_charPos];
					switch (c)
					{
					case '\0':
						if (ReadNullChar())
						{
							SetToken(JsonToken.None, null, false);
							return null;
						}
						break;
					case '"':
					case '\'':
						ParseString(c, readType);
						return FinishReadQuotedNumber(readType);
					case 'n':
						HandleNull();
						return null;
					case 'N':
						return ParseNumberNaN(readType);
					case 'I':
						return ParseNumberPositiveInfinity(readType);
					case '-':
						if (EnsureChars(1, true) && _chars[_charPos + 1] == 'I')
						{
							return ParseNumberNegativeInfinity(readType);
						}
						ParseNumber(readType);
						return Value;
					case '.':
					case '0':
					case '1':
					case '2':
					case '3':
					case '4':
					case '5':
					case '6':
					case '7':
					case '8':
					case '9':
						ParseNumber(readType);
						return Value;
					case '/':
						ParseComment(false);
						break;
					case ',':
						ProcessValueComma();
						break;
					case ']':
						_charPos++;
						if (_currentState == State.Array || _currentState == State.ArrayStart || _currentState == State.PostValue)
						{
							SetToken(JsonToken.EndArray);
							return null;
						}
						throw CreateUnexpectedCharacterException(c);
					case '\r':
						ProcessCarriageReturn(false);
						break;
					case '\n':
						ProcessLineFeed();
						break;
					case '\t':
					case ' ':
						_charPos++;
						break;
					default:
						_charPos++;
						if (!char.IsWhiteSpace(c))
						{
							throw CreateUnexpectedCharacterException(c);
						}
						break;
					}
				}
			case State.Finished:
				ReadFinished();
				return null;
			default:
				throw JsonReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
			}
		}

		[NullableContext(2)]
		private object FinishReadQuotedNumber(ReadType readType)
		{
			switch (readType)
			{
			case ReadType.ReadAsInt32:
				return ReadInt32String(_stringReference.ToString());
			case ReadType.ReadAsDecimal:
				return ReadDecimalString(_stringReference.ToString());
			case ReadType.ReadAsDouble:
				return ReadDoubleString(_stringReference.ToString());
			default:
				throw new ArgumentOutOfRangeException("readType");
			}
		}

		public override DateTimeOffset? ReadAsDateTimeOffset()
		{
			return (DateTimeOffset?)ReadStringValue(ReadType.ReadAsDateTimeOffset);
		}

		public override decimal? ReadAsDecimal()
		{
			return (decimal?)ReadNumberValue(ReadType.ReadAsDecimal);
		}

		public override double? ReadAsDouble()
		{
			return (double?)ReadNumberValue(ReadType.ReadAsDouble);
		}

		private void HandleNull()
		{
			if (EnsureChars(1, true))
			{
				if (_chars[_charPos + 1] == 'u')
				{
					ParseNull();
					return;
				}
				_charPos += 2;
				throw CreateUnexpectedCharacterException(_chars[_charPos - 1]);
			}
			_charPos = _charsUsed;
			throw CreateUnexpectedEndException();
		}

		private void ReadFinished()
		{
			if (EnsureChars(0, false))
			{
				EatWhitespace();
				if (_isEndOfFile)
				{
					return;
				}
				if (_chars[_charPos] != '/')
				{
					throw JsonReaderException.Create(this, "Additional text encountered after finished reading JSON content: {0}.".FormatWith(CultureInfo.InvariantCulture, _chars[_charPos]));
				}
				ParseComment(false);
			}
			SetToken(JsonToken.None);
		}

		private bool ReadNullChar()
		{
			if (_charsUsed == _charPos)
			{
				if (ReadData(false) == 0)
				{
					_isEndOfFile = true;
					return true;
				}
			}
			else
			{
				_charPos++;
			}
			return false;
		}

		private void EnsureBuffer()
		{
			if (_chars == null)
			{
				_chars = BufferUtils.RentBuffer(_arrayPool, 1024);
				_chars[0] = '\0';
			}
		}

		private void ReadStringIntoBuffer(char quote)
		{
			int num = _charPos;
			int charPos = _charPos;
			int lastWritePosition = _charPos;
			_stringBuffer.Position = 0;
			while (true)
			{
				switch (_chars[num++])
				{
				case '\0':
					if (_charsUsed == num - 1)
					{
						num--;
						if (ReadData(true) == 0)
						{
							_charPos = num;
							throw JsonReaderException.Create(this, "Unterminated string. Expected delimiter: {0}.".FormatWith(CultureInfo.InvariantCulture, quote));
						}
					}
					break;
				case '\\':
				{
					_charPos = num;
					if (!EnsureChars(0, true))
					{
						throw JsonReaderException.Create(this, "Unterminated string. Expected delimiter: {0}.".FormatWith(CultureInfo.InvariantCulture, quote));
					}
					int writeToPosition = num - 1;
					char c = _chars[num];
					num++;
					char c2;
					switch (c)
					{
					case 'b':
						c2 = '\b';
						break;
					case 't':
						c2 = '\t';
						break;
					case 'n':
						c2 = '\n';
						break;
					case 'f':
						c2 = '\f';
						break;
					case 'r':
						c2 = '\r';
						break;
					case '\\':
						c2 = '\\';
						break;
					case '"':
					case '\'':
					case '/':
						c2 = c;
						break;
					case 'u':
						_charPos = num;
						c2 = ParseUnicode();
						if (StringUtils.IsLowSurrogate(c2))
						{
							c2 = '\ufffd';
						}
						else if (StringUtils.IsHighSurrogate(c2))
						{
							bool flag;
							do
							{
								flag = false;
								if (EnsureChars(2, true) && _chars[_charPos] == '\\' && _chars[_charPos + 1] == 'u')
								{
									char writeChar = c2;
									_charPos += 2;
									c2 = ParseUnicode();
									if (!StringUtils.IsLowSurrogate(c2))
									{
										if (StringUtils.IsHighSurrogate(c2))
										{
											writeChar = '\ufffd';
											flag = true;
										}
										else
										{
											writeChar = '\ufffd';
										}
									}
									EnsureBufferNotEmpty();
									WriteCharToBuffer(writeChar, lastWritePosition, writeToPosition);
									lastWritePosition = _charPos;
								}
								else
								{
									c2 = '\ufffd';
								}
							}
							while (flag);
						}
						num = _charPos;
						break;
					default:
						_charPos = num;
						throw JsonReaderException.Create(this, "Bad JSON escape sequence: {0}.".FormatWith(CultureInfo.InvariantCulture, "\\" + c));
					}
					EnsureBufferNotEmpty();
					WriteCharToBuffer(c2, lastWritePosition, writeToPosition);
					lastWritePosition = num;
					break;
				}
				case '\r':
					_charPos = num - 1;
					ProcessCarriageReturn(true);
					num = _charPos;
					break;
				case '\n':
					_charPos = num - 1;
					ProcessLineFeed();
					num = _charPos;
					break;
				case '"':
				case '\'':
					if (_chars[num - 1] == quote)
					{
						FinishReadStringIntoBuffer(num - 1, charPos, lastWritePosition);
						return;
					}
					break;
				}
			}
		}

		private void FinishReadStringIntoBuffer(int charPos, int initialPosition, int lastWritePosition)
		{
			if (initialPosition == lastWritePosition)
			{
				_stringReference = new StringReference(_chars, initialPosition, charPos - initialPosition);
			}
			else
			{
				EnsureBufferNotEmpty();
				if (charPos > lastWritePosition)
				{
					_stringBuffer.Append(_arrayPool, _chars, lastWritePosition, charPos - lastWritePosition);
				}
				_stringReference = new StringReference(_stringBuffer.InternalBuffer, 0, _stringBuffer.Position);
			}
			_charPos = charPos + 1;
		}

		private void WriteCharToBuffer(char writeChar, int lastWritePosition, int writeToPosition)
		{
			if (writeToPosition > lastWritePosition)
			{
				_stringBuffer.Append(_arrayPool, _chars, lastWritePosition, writeToPosition - lastWritePosition);
			}
			_stringBuffer.Append(_arrayPool, writeChar);
		}

		private char ConvertUnicode(bool enoughChars)
		{
			if (enoughChars)
			{
				int value;
				if (ConvertUtils.TryHexTextToInt(_chars, _charPos, _charPos + 4, out value))
				{
					char result = Convert.ToChar(value);
					_charPos += 4;
					return result;
				}
				throw JsonReaderException.Create(this, "Invalid Unicode escape sequence: \\u{0}.".FormatWith(CultureInfo.InvariantCulture, new string(_chars, _charPos, 4)));
			}
			throw JsonReaderException.Create(this, "Unexpected end while parsing Unicode escape sequence.");
		}

		private char ParseUnicode()
		{
			return ConvertUnicode(EnsureChars(4, true));
		}

		private void ReadNumberIntoBuffer()
		{
			int num = _charPos;
			while (true)
			{
				char c = _chars[num];
				if (c == '\0')
				{
					_charPos = num;
					if (_charsUsed != num || ReadData(true) == 0)
					{
						break;
					}
				}
				else
				{
					if (ReadNumberCharIntoBuffer(c, num))
					{
						break;
					}
					num++;
				}
			}
		}

		private bool ReadNumberCharIntoBuffer(char currentChar, int charPos)
		{
			switch (currentChar)
			{
			case '+':
			case '-':
			case '.':
			case '0':
			case '1':
			case '2':
			case '3':
			case '4':
			case '5':
			case '6':
			case '7':
			case '8':
			case '9':
			case 'A':
			case 'B':
			case 'C':
			case 'D':
			case 'E':
			case 'F':
			case 'X':
			case 'a':
			case 'b':
			case 'c':
			case 'd':
			case 'e':
			case 'f':
			case 'x':
				return false;
			default:
				_charPos = charPos;
				if (char.IsWhiteSpace(currentChar) || currentChar == ',' || currentChar == '}' || currentChar == ']' || currentChar == ')' || currentChar == '/')
				{
					return true;
				}
				throw JsonReaderException.Create(this, "Unexpected character encountered while parsing number: {0}.".FormatWith(CultureInfo.InvariantCulture, currentChar));
			}
		}

		private void ClearRecentString()
		{
			_stringBuffer.Position = 0;
			_stringReference = default(StringReference);
		}

		private bool ParsePostValue(bool ignoreComments)
		{
			while (true)
			{
				char c = _chars[_charPos];
				switch (c)
				{
				case '\0':
					if (_charsUsed == _charPos)
					{
						if (ReadData(false) == 0)
						{
							_currentState = State.Finished;
							return false;
						}
					}
					else
					{
						_charPos++;
					}
					continue;
				case '}':
					_charPos++;
					SetToken(JsonToken.EndObject);
					return true;
				case ']':
					_charPos++;
					SetToken(JsonToken.EndArray);
					return true;
				case ')':
					_charPos++;
					SetToken(JsonToken.EndConstructor);
					return true;
				case '/':
					ParseComment(!ignoreComments);
					if (!ignoreComments)
					{
						return true;
					}
					continue;
				case ',':
					_charPos++;
					SetStateBasedOnCurrent();
					return false;
				case '\t':
				case ' ':
					_charPos++;
					continue;
				case '\r':
					ProcessCarriageReturn(false);
					continue;
				case '\n':
					ProcessLineFeed();
					continue;
				}
				if (char.IsWhiteSpace(c))
				{
					_charPos++;
					continue;
				}
				if (base.SupportMultipleContent && Depth == 0)
				{
					SetStateBasedOnCurrent();
					return false;
				}
				throw JsonReaderException.Create(this, "After parsing a value an unexpected character was encountered: {0}.".FormatWith(CultureInfo.InvariantCulture, c));
			}
		}

		private bool ParseObject()
		{
			while (true)
			{
				char c = _chars[_charPos];
				switch (c)
				{
				case '\0':
					if (_charsUsed == _charPos)
					{
						if (ReadData(false) == 0)
						{
							return false;
						}
					}
					else
					{
						_charPos++;
					}
					break;
				case '}':
					SetToken(JsonToken.EndObject);
					_charPos++;
					return true;
				case '/':
					ParseComment(true);
					return true;
				case '\r':
					ProcessCarriageReturn(false);
					break;
				case '\n':
					ProcessLineFeed();
					break;
				case '\t':
				case ' ':
					_charPos++;
					break;
				default:
					if (char.IsWhiteSpace(c))
					{
						_charPos++;
						break;
					}
					return ParseProperty();
				}
			}
		}

		private bool ParseProperty()
		{
			char c = _chars[_charPos];
			char c2;
			if (c == '"' || c == '\'')
			{
				_charPos++;
				c2 = c;
				ShiftBufferIfNeeded();
				ReadStringIntoBuffer(c2);
			}
			else
			{
				if (!ValidIdentifierChar(c))
				{
					throw JsonReaderException.Create(this, "Invalid property identifier character: {0}.".FormatWith(CultureInfo.InvariantCulture, _chars[_charPos]));
				}
				c2 = '\0';
				ShiftBufferIfNeeded();
				ParseUnquotedProperty();
			}
			string text;
			if (PropertyNameTable != null)
			{
				text = PropertyNameTable.Get(_stringReference.Chars, _stringReference.StartIndex, _stringReference.Length);
				if (text == null)
				{
					text = _stringReference.ToString();
				}
			}
			else
			{
				text = _stringReference.ToString();
			}
			EatWhitespace();
			if (_chars[_charPos] != ':')
			{
				throw JsonReaderException.Create(this, "Invalid character after parsing property name. Expected ':' but got: {0}.".FormatWith(CultureInfo.InvariantCulture, _chars[_charPos]));
			}
			_charPos++;
			SetToken(JsonToken.PropertyName, text);
			_quoteChar = c2;
			ClearRecentString();
			return true;
		}

		private bool ValidIdentifierChar(char value)
		{
			if (!char.IsLetterOrDigit(value) && value != '_')
			{
				return value == '$';
			}
			return true;
		}

		private void ParseUnquotedProperty()
		{
			int charPos = _charPos;
			while (true)
			{
				char c = _chars[_charPos];
				if (c == '\0')
				{
					if (_charsUsed != _charPos)
					{
						_stringReference = new StringReference(_chars, charPos, _charPos - charPos);
						break;
					}
					if (ReadData(true) == 0)
					{
						throw JsonReaderException.Create(this, "Unexpected end while parsing unquoted property name.");
					}
				}
				else if (ReadUnquotedPropertyReportIfDone(c, charPos))
				{
					break;
				}
			}
		}

		private bool ReadUnquotedPropertyReportIfDone(char currentChar, int initialPosition)
		{
			if (ValidIdentifierChar(currentChar))
			{
				_charPos++;
				return false;
			}
			if (char.IsWhiteSpace(currentChar) || currentChar == ':')
			{
				_stringReference = new StringReference(_chars, initialPosition, _charPos - initialPosition);
				return true;
			}
			throw JsonReaderException.Create(this, "Invalid JavaScript property identifier character: {0}.".FormatWith(CultureInfo.InvariantCulture, currentChar));
		}

		private bool ParseValue()
		{
			while (true)
			{
				char c = _chars[_charPos];
				switch (c)
				{
				case '\0':
					if (_charsUsed == _charPos)
					{
						if (ReadData(false) == 0)
						{
							return false;
						}
					}
					else
					{
						_charPos++;
					}
					break;
				case '"':
				case '\'':
					ParseString(c, ReadType.Read);
					return true;
				case 't':
					ParseTrue();
					return true;
				case 'f':
					ParseFalse();
					return true;
				case 'n':
					if (EnsureChars(1, true))
					{
						switch (_chars[_charPos + 1])
						{
						case 'u':
							ParseNull();
							break;
						case 'e':
							ParseConstructor();
							break;
						default:
							throw CreateUnexpectedCharacterException(_chars[_charPos]);
						}
						return true;
					}
					_charPos++;
					throw CreateUnexpectedEndException();
				case 'N':
					ParseNumberNaN(ReadType.Read);
					return true;
				case 'I':
					ParseNumberPositiveInfinity(ReadType.Read);
					return true;
				case '-':
					if (EnsureChars(1, true) && _chars[_charPos + 1] == 'I')
					{
						ParseNumberNegativeInfinity(ReadType.Read);
					}
					else
					{
						ParseNumber(ReadType.Read);
					}
					return true;
				case '/':
					ParseComment(true);
					return true;
				case 'u':
					ParseUndefined();
					return true;
				case '{':
					_charPos++;
					SetToken(JsonToken.StartObject);
					return true;
				case '[':
					_charPos++;
					SetToken(JsonToken.StartArray);
					return true;
				case ']':
					_charPos++;
					SetToken(JsonToken.EndArray);
					return true;
				case ',':
					SetToken(JsonToken.Undefined);
					return true;
				case ')':
					_charPos++;
					SetToken(JsonToken.EndConstructor);
					return true;
				case '\r':
					ProcessCarriageReturn(false);
					break;
				case '\n':
					ProcessLineFeed();
					break;
				case '\t':
				case ' ':
					_charPos++;
					break;
				default:
					if (char.IsWhiteSpace(c))
					{
						_charPos++;
						break;
					}
					if (char.IsNumber(c) || c == '-' || c == '.')
					{
						ParseNumber(ReadType.Read);
						return true;
					}
					throw CreateUnexpectedCharacterException(c);
				}
			}
		}

		private void ProcessLineFeed()
		{
			_charPos++;
			OnNewLine(_charPos);
		}

		private void ProcessCarriageReturn(bool append)
		{
			_charPos++;
			SetNewLine(EnsureChars(1, append));
		}

		private void EatWhitespace()
		{
			while (true)
			{
				char c = _chars[_charPos];
				switch (c)
				{
				case '\0':
					if (_charsUsed == _charPos)
					{
						if (ReadData(false) == 0)
						{
							return;
						}
					}
					else
					{
						_charPos++;
					}
					break;
				case '\r':
					ProcessCarriageReturn(false);
					break;
				case '\n':
					ProcessLineFeed();
					break;
				default:
					if (!char.IsWhiteSpace(c))
					{
						return;
					}
					goto case ' ';
				case ' ':
					_charPos++;
					break;
				}
			}
		}

		private void ParseConstructor()
		{
			if (MatchValueWithTrailingSeparator("new"))
			{
				EatWhitespace();
				int charPos = _charPos;
				int charPos2;
				while (true)
				{
					char c = _chars[_charPos];
					if (c == '\0')
					{
						if (_charsUsed == _charPos)
						{
							if (ReadData(true) == 0)
							{
								throw JsonReaderException.Create(this, "Unexpected end while parsing constructor.");
							}
							continue;
						}
						charPos2 = _charPos;
						_charPos++;
						break;
					}
					if (char.IsLetterOrDigit(c))
					{
						_charPos++;
						continue;
					}
					switch (c)
					{
					case '\r':
						charPos2 = _charPos;
						ProcessCarriageReturn(true);
						break;
					case '\n':
						charPos2 = _charPos;
						ProcessLineFeed();
						break;
					default:
						if (char.IsWhiteSpace(c))
						{
							charPos2 = _charPos;
							_charPos++;
							break;
						}
						if (c == '(')
						{
							charPos2 = _charPos;
							break;
						}
						throw JsonReaderException.Create(this, "Unexpected character while parsing constructor: {0}.".FormatWith(CultureInfo.InvariantCulture, c));
					}
					break;
				}
				_stringReference = new StringReference(_chars, charPos, charPos2 - charPos);
				string value = _stringReference.ToString();
				EatWhitespace();
				if (_chars[_charPos] != '(')
				{
					throw JsonReaderException.Create(this, "Unexpected character while parsing constructor: {0}.".FormatWith(CultureInfo.InvariantCulture, _chars[_charPos]));
				}
				_charPos++;
				ClearRecentString();
				SetToken(JsonToken.StartConstructor, value);
				return;
			}
			throw JsonReaderException.Create(this, "Unexpected content while parsing JSON.");
		}

		private void ParseNumber(ReadType readType)
		{
			ShiftBufferIfNeeded();
			char firstChar = _chars[_charPos];
			int charPos = _charPos;
			ReadNumberIntoBuffer();
			ParseReadNumber(readType, firstChar, charPos);
		}

		private void ParseReadNumber(ReadType readType, char firstChar, int initialPosition)
		{
			SetPostValueState(true);
			_stringReference = new StringReference(_chars, initialPosition, _charPos - initialPosition);
			bool flag = char.IsDigit(firstChar) && _stringReference.Length == 1;
			bool flag2 = firstChar == '0' && _stringReference.Length > 1 && _stringReference.Chars[_stringReference.StartIndex + 1] != '.' && _stringReference.Chars[_stringReference.StartIndex + 1] != 'e' && _stringReference.Chars[_stringReference.StartIndex + 1] != 'E';
			JsonToken newToken;
			object value;
			switch (readType)
			{
			case ReadType.ReadAsString:
			{
				string text5 = _stringReference.ToString();
				double result3;
				if (flag2)
				{
					try
					{
						if (text5.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
						{
							Convert.ToInt64(text5, 16);
						}
						else
						{
							Convert.ToInt64(text5, 8);
						}
					}
					catch (Exception ex4)
					{
						throw ThrowReaderError("Input string '{0}' is not a valid number.".FormatWith(CultureInfo.InvariantCulture, text5), ex4);
					}
				}
				else if (!double.TryParse(text5, NumberStyles.Float, CultureInfo.InvariantCulture, out result3))
				{
					throw ThrowReaderError("Input string '{0}' is not a valid number.".FormatWith(CultureInfo.InvariantCulture, _stringReference.ToString()));
				}
				newToken = JsonToken.String;
				value = text5;
				break;
			}
			case ReadType.ReadAsInt32:
				if (flag)
				{
					value = firstChar - 48;
				}
				else if (flag2)
				{
					string text6 = _stringReference.ToString();
					try
					{
						value = (text6.StartsWith("0x", StringComparison.OrdinalIgnoreCase) ? Convert.ToInt32(text6, 16) : Convert.ToInt32(text6, 8));
					}
					catch (Exception ex5)
					{
						throw ThrowReaderError("Input string '{0}' is not a valid integer.".FormatWith(CultureInfo.InvariantCulture, text6), ex5);
					}
				}
				else
				{
					int value5;
					switch (ConvertUtils.Int32TryParse(_stringReference.Chars, _stringReference.StartIndex, _stringReference.Length, out value5))
					{
					case ParseResult.Success:
						break;
					case ParseResult.Overflow:
						throw ThrowReaderError("JSON integer {0} is too large or small for an Int32.".FormatWith(CultureInfo.InvariantCulture, _stringReference.ToString()));
					default:
						throw ThrowReaderError("Input string '{0}' is not a valid integer.".FormatWith(CultureInfo.InvariantCulture, _stringReference.ToString()));
					}
					value = value5;
				}
				newToken = JsonToken.Integer;
				break;
			case ReadType.ReadAsDecimal:
				if (flag)
				{
					value = (decimal)firstChar - 48m;
				}
				else if (flag2)
				{
					string text3 = _stringReference.ToString();
					try
					{
						value = Convert.ToDecimal(text3.StartsWith("0x", StringComparison.OrdinalIgnoreCase) ? Convert.ToInt64(text3, 16) : Convert.ToInt64(text3, 8));
					}
					catch (Exception ex2)
					{
						throw ThrowReaderError("Input string '{0}' is not a valid decimal.".FormatWith(CultureInfo.InvariantCulture, text3), ex2);
					}
				}
				else
				{
					decimal value4;
					if (ConvertUtils.DecimalTryParse(_stringReference.Chars, _stringReference.StartIndex, _stringReference.Length, out value4) != ParseResult.Success)
					{
						throw ThrowReaderError("Input string '{0}' is not a valid decimal.".FormatWith(CultureInfo.InvariantCulture, _stringReference.ToString()));
					}
					value = value4;
				}
				newToken = JsonToken.Float;
				break;
			case ReadType.ReadAsDouble:
				if (flag)
				{
					value = (double)(int)firstChar - 48.0;
				}
				else if (flag2)
				{
					string text4 = _stringReference.ToString();
					try
					{
						value = Convert.ToDouble(text4.StartsWith("0x", StringComparison.OrdinalIgnoreCase) ? Convert.ToInt64(text4, 16) : Convert.ToInt64(text4, 8));
					}
					catch (Exception ex3)
					{
						throw ThrowReaderError("Input string '{0}' is not a valid double.".FormatWith(CultureInfo.InvariantCulture, text4), ex3);
					}
				}
				else
				{
					double result2;
					if (!double.TryParse(_stringReference.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out result2))
					{
						throw ThrowReaderError("Input string '{0}' is not a valid double.".FormatWith(CultureInfo.InvariantCulture, _stringReference.ToString()));
					}
					value = result2;
				}
				newToken = JsonToken.Float;
				break;
			case ReadType.Read:
			case ReadType.ReadAsInt64:
			{
				if (flag)
				{
					value = (long)firstChar - 48L;
					newToken = JsonToken.Integer;
					break;
				}
				if (flag2)
				{
					string text = _stringReference.ToString();
					try
					{
						value = (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase) ? Convert.ToInt64(text, 16) : Convert.ToInt64(text, 8));
					}
					catch (Exception ex)
					{
						throw ThrowReaderError("Input string '{0}' is not a valid number.".FormatWith(CultureInfo.InvariantCulture, text), ex);
					}
					newToken = JsonToken.Integer;
					break;
				}
				long value2;
				switch (ConvertUtils.Int64TryParse(_stringReference.Chars, _stringReference.StartIndex, _stringReference.Length, out value2))
				{
				case ParseResult.Success:
					value = value2;
					newToken = JsonToken.Integer;
					break;
				case ParseResult.Overflow:
				{
					string text2 = _stringReference.ToString();
					if (text2.Length > 380)
					{
						throw ThrowReaderError("JSON integer {0} is too large to parse.".FormatWith(CultureInfo.InvariantCulture, _stringReference.ToString()));
					}
					value = BigIntegerParse(text2, CultureInfo.InvariantCulture);
					newToken = JsonToken.Integer;
					break;
				}
				default:
					if (_floatParseHandling == FloatParseHandling.Decimal)
					{
						decimal value3;
						ParseResult parseResult = ConvertUtils.DecimalTryParse(_stringReference.Chars, _stringReference.StartIndex, _stringReference.Length, out value3);
						if (parseResult != ParseResult.Success)
						{
							throw ThrowReaderError("Input string '{0}' is not a valid decimal.".FormatWith(CultureInfo.InvariantCulture, _stringReference.ToString()));
						}
						value = value3;
					}
					else
					{
						double result;
						if (!double.TryParse(_stringReference.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out result))
						{
							throw ThrowReaderError("Input string '{0}' is not a valid number.".FormatWith(CultureInfo.InvariantCulture, _stringReference.ToString()));
						}
						value = result;
					}
					newToken = JsonToken.Float;
					break;
				}
				break;
			}
			default:
				throw JsonReaderException.Create(this, "Cannot read number value as type.");
			}
			ClearRecentString();
			SetToken(newToken, value, false);
		}

		private JsonReaderException ThrowReaderError(string message, [Nullable(2)] Exception ex = null)
		{
			SetToken(JsonToken.Undefined, null, false);
			return JsonReaderException.Create(this, message, ex);
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		private static object BigIntegerParse(string number, CultureInfo culture)
		{
			return BigInteger.Parse(number, culture);
		}

		private void ParseComment(bool setToken)
		{
			_charPos++;
			if (!EnsureChars(1, false))
			{
				throw JsonReaderException.Create(this, "Unexpected end while parsing comment.");
			}
			bool flag;
			if (_chars[_charPos] == '*')
			{
				flag = false;
			}
			else
			{
				if (_chars[_charPos] != '/')
				{
					throw JsonReaderException.Create(this, "Error parsing comment. Expected: *, got {0}.".FormatWith(CultureInfo.InvariantCulture, _chars[_charPos]));
				}
				flag = true;
			}
			_charPos++;
			int charPos = _charPos;
			while (true)
			{
				switch (_chars[_charPos])
				{
				case '\0':
					if (_charsUsed == _charPos)
					{
						if (ReadData(true) == 0)
						{
							if (!flag)
							{
								throw JsonReaderException.Create(this, "Unexpected end while parsing comment.");
							}
							EndComment(setToken, charPos, _charPos);
							return;
						}
					}
					else
					{
						_charPos++;
					}
					break;
				case '*':
					_charPos++;
					if (!flag && EnsureChars(0, true) && _chars[_charPos] == '/')
					{
						EndComment(setToken, charPos, _charPos - 1);
						_charPos++;
						return;
					}
					break;
				case '\r':
					if (flag)
					{
						EndComment(setToken, charPos, _charPos);
						return;
					}
					ProcessCarriageReturn(true);
					break;
				case '\n':
					if (flag)
					{
						EndComment(setToken, charPos, _charPos);
						return;
					}
					ProcessLineFeed();
					break;
				default:
					_charPos++;
					break;
				}
			}
		}

		private void EndComment(bool setToken, int initialPosition, int endPosition)
		{
			if (setToken)
			{
				SetToken(JsonToken.Comment, new string(_chars, initialPosition, endPosition - initialPosition));
			}
		}

		private bool MatchValue(string value)
		{
			return MatchValue(EnsureChars(value.Length - 1, true), value);
		}

		private bool MatchValue(bool enoughChars, string value)
		{
			if (!enoughChars)
			{
				_charPos = _charsUsed;
				throw CreateUnexpectedEndException();
			}
			for (int i = 0; i < value.Length; i++)
			{
				if (_chars[_charPos + i] != value[i])
				{
					_charPos += i;
					return false;
				}
			}
			_charPos += value.Length;
			return true;
		}

		private bool MatchValueWithTrailingSeparator(string value)
		{
			if (!MatchValue(value))
			{
				return false;
			}
			if (!EnsureChars(0, false))
			{
				return true;
			}
			if (!IsSeparator(_chars[_charPos]))
			{
				return _chars[_charPos] == '\0';
			}
			return true;
		}

		private bool IsSeparator(char c)
		{
			switch (c)
			{
			case ',':
			case ']':
			case '}':
				return true;
			case '/':
			{
				if (!EnsureChars(1, false))
				{
					return false;
				}
				char c2 = _chars[_charPos + 1];
				if (c2 != '*')
				{
					return c2 == '/';
				}
				return true;
			}
			case ')':
				if (base.CurrentState == State.Constructor || base.CurrentState == State.ConstructorStart)
				{
					return true;
				}
				break;
			case '\t':
			case '\n':
			case '\r':
			case ' ':
				return true;
			default:
				if (char.IsWhiteSpace(c))
				{
					return true;
				}
				break;
			}
			return false;
		}

		private void ParseTrue()
		{
			if (MatchValueWithTrailingSeparator(JsonConvert.True))
			{
				SetToken(JsonToken.Boolean, true);
				return;
			}
			throw JsonReaderException.Create(this, "Error parsing boolean value.");
		}

		private void ParseNull()
		{
			if (MatchValueWithTrailingSeparator(JsonConvert.Null))
			{
				SetToken(JsonToken.Null);
				return;
			}
			throw JsonReaderException.Create(this, "Error parsing null value.");
		}

		private void ParseUndefined()
		{
			if (MatchValueWithTrailingSeparator(JsonConvert.Undefined))
			{
				SetToken(JsonToken.Undefined);
				return;
			}
			throw JsonReaderException.Create(this, "Error parsing undefined value.");
		}

		private void ParseFalse()
		{
			if (MatchValueWithTrailingSeparator(JsonConvert.False))
			{
				SetToken(JsonToken.Boolean, false);
				return;
			}
			throw JsonReaderException.Create(this, "Error parsing boolean value.");
		}

		private object ParseNumberNegativeInfinity(ReadType readType)
		{
			return ParseNumberNegativeInfinity(readType, MatchValueWithTrailingSeparator(JsonConvert.NegativeInfinity));
		}

		private object ParseNumberNegativeInfinity(ReadType readType, bool matched)
		{
			if (matched)
			{
				switch (readType)
				{
				case ReadType.Read:
				case ReadType.ReadAsDouble:
					if (_floatParseHandling == FloatParseHandling.Double)
					{
						SetToken(JsonToken.Float, double.NegativeInfinity);
						return double.NegativeInfinity;
					}
					break;
				case ReadType.ReadAsString:
					SetToken(JsonToken.String, JsonConvert.NegativeInfinity);
					return JsonConvert.NegativeInfinity;
				}
				throw JsonReaderException.Create(this, "Cannot read -Infinity value.");
			}
			throw JsonReaderException.Create(this, "Error parsing -Infinity value.");
		}

		private object ParseNumberPositiveInfinity(ReadType readType)
		{
			return ParseNumberPositiveInfinity(readType, MatchValueWithTrailingSeparator(JsonConvert.PositiveInfinity));
		}

		private object ParseNumberPositiveInfinity(ReadType readType, bool matched)
		{
			if (matched)
			{
				switch (readType)
				{
				case ReadType.Read:
				case ReadType.ReadAsDouble:
					if (_floatParseHandling == FloatParseHandling.Double)
					{
						SetToken(JsonToken.Float, double.PositiveInfinity);
						return double.PositiveInfinity;
					}
					break;
				case ReadType.ReadAsString:
					SetToken(JsonToken.String, JsonConvert.PositiveInfinity);
					return JsonConvert.PositiveInfinity;
				}
				throw JsonReaderException.Create(this, "Cannot read Infinity value.");
			}
			throw JsonReaderException.Create(this, "Error parsing Infinity value.");
		}

		private object ParseNumberNaN(ReadType readType)
		{
			return ParseNumberNaN(readType, MatchValueWithTrailingSeparator(JsonConvert.NaN));
		}

		private object ParseNumberNaN(ReadType readType, bool matched)
		{
			if (matched)
			{
				switch (readType)
				{
				case ReadType.Read:
				case ReadType.ReadAsDouble:
					if (_floatParseHandling == FloatParseHandling.Double)
					{
						SetToken(JsonToken.Float, double.NaN);
						return double.NaN;
					}
					break;
				case ReadType.ReadAsString:
					SetToken(JsonToken.String, JsonConvert.NaN);
					return JsonConvert.NaN;
				}
				throw JsonReaderException.Create(this, "Cannot read NaN value.");
			}
			throw JsonReaderException.Create(this, "Error parsing NaN value.");
		}

		public override void Close()
		{
			base.Close();
			if (_chars != null)
			{
				BufferUtils.ReturnBuffer(_arrayPool, _chars);
				_chars = null;
			}
			if (base.CloseInput)
			{
				TextReader reader = _reader;
				if (reader != null)
				{
					reader.Close();
				}
			}
			_stringBuffer.Clear(_arrayPool);
		}

		public bool HasLineInfo()
		{
			return true;
		}
	}
}
