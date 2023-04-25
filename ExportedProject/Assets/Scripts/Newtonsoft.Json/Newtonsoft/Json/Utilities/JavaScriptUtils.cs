using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Newtonsoft.Json.Utilities
{
	[NullableContext(1)]
	[Nullable(0)]
	internal static class JavaScriptUtils
	{
		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CWriteEscapedJavaScriptStringWithDelimitersAsync_003Ed__13 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public Task task;

			[Nullable(0)]
			public string s;

			[Nullable(0)]
			public TextWriter writer;

			[Nullable(0)]
			public bool[] charEscapeFlags;

			public StringEscapeHandling stringEscapeHandling;

			[Nullable(0)]
			public JsonTextWriter client;

			[Nullable(0)]
			public char[] writeBuffer;

			public CancellationToken cancellationToken;

			public char delimiter;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
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
						goto IL_0076;
					case 0:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0076;
					case 1:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0113;
					case 2:
						{
							awaiter = _003C_003Eu__1;
							_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							break;
						}
						IL_011a:
						awaiter = writer.WriteAsync(delimiter).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 2);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						break;
						IL_0076:
						awaiter.GetResult();
						if (!StringUtils.IsNullOrEmpty(s))
						{
							awaiter = WriteEscapedJavaScriptStringWithoutDelimitersAsync(writer, s, charEscapeFlags, stringEscapeHandling, client, writeBuffer, cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_003C_003E1__state = 1);
								_003C_003Eu__1 = awaiter;
								_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_0113;
						}
						goto IL_011a;
						IL_0113:
						awaiter.GetResult();
						goto IL_011a;
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
		private struct _003CWriteCharAsync_003Ed__14 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public Task task;

			[Nullable(0)]
			public TextWriter writer;

			public char c;

			public CancellationToken cancellationToken;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
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
							goto IL_00e0;
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
					awaiter = writer.WriteAsync(c, cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_003C_003E1__state = 1);
						_003C_003Eu__1 = awaiter;
						_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_00e0;
					IL_00e0:
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
		private struct _003CWriteDefinitelyEscapedJavaScriptStringWithoutDelimitersAsync_003Ed__16 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public char[] writeBuffer;

			public int lastWritePosition;

			[Nullable(0)]
			public JsonTextWriter client;

			[Nullable(0)]
			public string s;

			[Nullable(0)]
			public TextWriter writer;

			public CancellationToken cancellationToken;

			[Nullable(0)]
			public bool[] charEscapeFlags;

			public StringEscapeHandling stringEscapeHandling;

			private bool _003CisEscapedUnicodeText_003E5__2;

			[Nullable(0)]
			private string _003CescapedValue_003E5__3;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__1;

			private int _003Ci_003E5__4;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					int num3;
					int num2;
					switch (num)
					{
					default:
						if (writeBuffer == null || writeBuffer.Length < lastWritePosition)
						{
							writeBuffer = client.EnsureWriteBuffer(lastWritePosition, 6);
						}
						if (lastWritePosition != 0)
						{
							s.CopyTo(0, writeBuffer, 0, lastWritePosition);
							awaiter = writer.WriteAsync(writeBuffer, 0, lastWritePosition, cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_003C_003E1__state = 0);
								_003C_003Eu__1 = awaiter;
								_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_00ea;
						}
						goto IL_00f1;
					case 0:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_00ea;
					case 1:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0390;
					case 2:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_041b;
					case 3:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0494;
					case 4:
						{
							awaiter = _003C_003Eu__1;
							_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							break;
						}
						IL_0494:
						awaiter.GetResult();
						_003CisEscapedUnicodeText_003E5__2 = false;
						goto IL_04a2;
						IL_04a2:
						_003Ci_003E5__4++;
						goto IL_04b4;
						IL_02b8:
						num2 = _003Ci_003E5__4 - lastWritePosition + (_003CisEscapedUnicodeText_003E5__2 ? 6 : 0);
						num3 = (_003CisEscapedUnicodeText_003E5__2 ? 6 : 0);
						if (writeBuffer.Length < num2)
						{
							writeBuffer = client.EnsureWriteBuffer(num2, 6);
						}
						s.CopyTo(lastWritePosition, writeBuffer, num3, num2 - num3);
						awaiter = writer.WriteAsync(writeBuffer, num3, num2 - num3, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 1);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0390;
						IL_041b:
						awaiter.GetResult();
						goto IL_04a2;
						IL_0390:
						awaiter.GetResult();
						goto IL_0397;
						IL_00ea:
						awaiter.GetResult();
						goto IL_00f1;
						IL_00f1:
						_003CisEscapedUnicodeText_003E5__2 = false;
						_003CescapedValue_003E5__3 = null;
						_003Ci_003E5__4 = lastWritePosition;
						goto IL_04b4;
						IL_04b4:
						if (_003Ci_003E5__4 < s.Length)
						{
							char c = s[_003Ci_003E5__4];
							if (c >= charEscapeFlags.Length || charEscapeFlags[(uint)c])
							{
								switch (c)
								{
								case '\t':
									_003CescapedValue_003E5__3 = "\\t";
									goto IL_02a7;
								case '\n':
									_003CescapedValue_003E5__3 = "\\n";
									goto IL_02a7;
								case '\r':
									_003CescapedValue_003E5__3 = "\\r";
									goto IL_02a7;
								case '\f':
									_003CescapedValue_003E5__3 = "\\f";
									goto IL_02a7;
								case '\b':
									_003CescapedValue_003E5__3 = "\\b";
									goto IL_02a7;
								case '\\':
									_003CescapedValue_003E5__3 = "\\\\";
									goto IL_02a7;
								case '\u0085':
									_003CescapedValue_003E5__3 = "\\u0085";
									goto IL_02a7;
								case '\u2028':
									_003CescapedValue_003E5__3 = "\\u2028";
									goto IL_02a7;
								case '\u2029':
									_003CescapedValue_003E5__3 = "\\u2029";
									goto IL_02a7;
								default:
									{
										if (c >= charEscapeFlags.Length && stringEscapeHandling != StringEscapeHandling.EscapeNonAscii)
										{
											break;
										}
										if (c == '\'' && stringEscapeHandling != StringEscapeHandling.EscapeHtml)
										{
											_003CescapedValue_003E5__3 = "\\'";
										}
										else if (c == '"' && stringEscapeHandling != StringEscapeHandling.EscapeHtml)
										{
											_003CescapedValue_003E5__3 = "\\\"";
										}
										else
										{
											if (writeBuffer.Length < 6)
											{
												writeBuffer = client.EnsureWriteBuffer(6, 0);
											}
											StringUtils.ToCharAsUnicode(c, writeBuffer);
											_003CisEscapedUnicodeText_003E5__2 = true;
										}
										goto IL_02a7;
									}
									IL_02a7:
									if (_003Ci_003E5__4 > lastWritePosition)
									{
										goto IL_02b8;
									}
									goto IL_0397;
								}
							}
							goto IL_04a2;
						}
						num2 = s.Length - lastWritePosition;
						if (num2 != 0)
						{
							if (writeBuffer.Length < num2)
							{
								writeBuffer = client.EnsureWriteBuffer(num2, 0);
							}
							s.CopyTo(lastWritePosition, writeBuffer, 0, num2);
							awaiter = writer.WriteAsync(writeBuffer, 0, num2, cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_003C_003E1__state = 4);
								_003C_003Eu__1 = awaiter;
								_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							break;
						}
						goto end_IL_0007;
						IL_0397:
						lastWritePosition = _003Ci_003E5__4 + 1;
						if (!_003CisEscapedUnicodeText_003E5__2)
						{
							awaiter = writer.WriteAsync(_003CescapedValue_003E5__3, cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_003C_003E1__state = 2);
								_003C_003Eu__1 = awaiter;
								_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_041b;
						}
						awaiter = writer.WriteAsync(writeBuffer, 0, 6, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 3);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0494;
					}
					awaiter.GetResult();
					end_IL_0007:;
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003CescapedValue_003E5__3 = null;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003CescapedValue_003E5__3 = null;
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

		internal static readonly bool[] SingleQuoteCharEscapeFlags;

		internal static readonly bool[] DoubleQuoteCharEscapeFlags;

		internal static readonly bool[] HtmlCharEscapeFlags;

		private const int UnicodeTextLength = 6;

		private const string EscapedUnicodeText = "!";

		static JavaScriptUtils()
		{
			SingleQuoteCharEscapeFlags = new bool[128];
			DoubleQuoteCharEscapeFlags = new bool[128];
			HtmlCharEscapeFlags = new bool[128];
			IList<char> list = new List<char> { '\n', '\r', '\t', '\\', '\f', '\b' };
			for (int i = 0; i < 32; i++)
			{
				list.Add((char)i);
			}
			foreach (char item in list.Union(new char[1] { '\'' }))
			{
				SingleQuoteCharEscapeFlags[(uint)item] = true;
			}
			foreach (char item2 in list.Union(new char[1] { '"' }))
			{
				DoubleQuoteCharEscapeFlags[(uint)item2] = true;
			}
			foreach (char item3 in list.Union(new char[5] { '"', '\'', '<', '>', '&' }))
			{
				HtmlCharEscapeFlags[(uint)item3] = true;
			}
		}

		public static bool[] GetCharEscapeFlags(StringEscapeHandling stringEscapeHandling, char quoteChar)
		{
			if (stringEscapeHandling == StringEscapeHandling.EscapeHtml)
			{
				return HtmlCharEscapeFlags;
			}
			if (quoteChar == '"')
			{
				return DoubleQuoteCharEscapeFlags;
			}
			return SingleQuoteCharEscapeFlags;
		}

		public static bool ShouldEscapeJavaScriptString([Nullable(2)] string s, bool[] charEscapeFlags)
		{
			if (s == null)
			{
				return false;
			}
			foreach (char c in s)
			{
				if (c >= charEscapeFlags.Length || charEscapeFlags[(uint)c])
				{
					return true;
				}
			}
			return false;
		}

		[NullableContext(2)]
		public static void WriteEscapedJavaScriptString([Nullable(1)] TextWriter writer, string s, char delimiter, bool appendDelimiters, [Nullable(1)] bool[] charEscapeFlags, StringEscapeHandling stringEscapeHandling, IArrayPool<char> bufferPool, ref char[] writeBuffer)
		{
			if (appendDelimiters)
			{
				writer.Write(delimiter);
			}
			if (!StringUtils.IsNullOrEmpty(s))
			{
				int num = FirstCharToEscape(s, charEscapeFlags, stringEscapeHandling);
				if (num == -1)
				{
					writer.Write(s);
				}
				else
				{
					if (num != 0)
					{
						if (writeBuffer == null || writeBuffer.Length < num)
						{
							writeBuffer = BufferUtils.EnsureBufferSize(bufferPool, num, writeBuffer);
						}
						s.CopyTo(0, writeBuffer, 0, num);
						writer.Write(writeBuffer, 0, num);
					}
					int num2;
					for (int i = num; i < s.Length; i++)
					{
						char c = s[i];
						if (c < charEscapeFlags.Length && !charEscapeFlags[(uint)c])
						{
							continue;
						}
						string text;
						switch (c)
						{
						case '\t':
							text = "\\t";
							break;
						case '\n':
							text = "\\n";
							break;
						case '\r':
							text = "\\r";
							break;
						case '\f':
							text = "\\f";
							break;
						case '\b':
							text = "\\b";
							break;
						case '\\':
							text = "\\\\";
							break;
						case '\u0085':
							text = "\\u0085";
							break;
						case '\u2028':
							text = "\\u2028";
							break;
						case '\u2029':
							text = "\\u2029";
							break;
						default:
							if (c < charEscapeFlags.Length || stringEscapeHandling == StringEscapeHandling.EscapeNonAscii)
							{
								if (c == '\'' && stringEscapeHandling != StringEscapeHandling.EscapeHtml)
								{
									text = "\\'";
									break;
								}
								if (c == '"' && stringEscapeHandling != StringEscapeHandling.EscapeHtml)
								{
									text = "\\\"";
									break;
								}
								if (writeBuffer == null || writeBuffer.Length < 6)
								{
									writeBuffer = BufferUtils.EnsureBufferSize(bufferPool, 6, writeBuffer);
								}
								StringUtils.ToCharAsUnicode(c, writeBuffer);
								text = "!";
							}
							else
							{
								text = null;
							}
							break;
						}
						if (text == null)
						{
							continue;
						}
						bool flag = string.Equals(text, "!", StringComparison.Ordinal);
						if (i > num)
						{
							num2 = i - num + (flag ? 6 : 0);
							int num3 = (flag ? 6 : 0);
							if (writeBuffer == null || writeBuffer.Length < num2)
							{
								char[] array = BufferUtils.RentBuffer(bufferPool, num2);
								if (flag)
								{
									Array.Copy(writeBuffer, array, 6);
								}
								BufferUtils.ReturnBuffer(bufferPool, writeBuffer);
								writeBuffer = array;
							}
							s.CopyTo(num, writeBuffer, num3, num2 - num3);
							writer.Write(writeBuffer, num3, num2 - num3);
						}
						num = i + 1;
						if (!flag)
						{
							writer.Write(text);
						}
						else
						{
							writer.Write(writeBuffer, 0, 6);
						}
					}
					num2 = s.Length - num;
					if (num2 > 0)
					{
						if (writeBuffer == null || writeBuffer.Length < num2)
						{
							writeBuffer = BufferUtils.EnsureBufferSize(bufferPool, num2, writeBuffer);
						}
						s.CopyTo(num, writeBuffer, 0, num2);
						writer.Write(writeBuffer, 0, num2);
					}
				}
			}
			if (appendDelimiters)
			{
				writer.Write(delimiter);
			}
		}

		public static string ToEscapedJavaScriptString([Nullable(2)] string value, char delimiter, bool appendDelimiters, StringEscapeHandling stringEscapeHandling)
		{
			bool[] charEscapeFlags = GetCharEscapeFlags(stringEscapeHandling, delimiter);
			using (StringWriter stringWriter = StringUtils.CreateStringWriter((value != null) ? value.Length : 16))
			{
				char[] writeBuffer = null;
				WriteEscapedJavaScriptString(stringWriter, value, delimiter, appendDelimiters, charEscapeFlags, stringEscapeHandling, null, ref writeBuffer);
				return stringWriter.ToString();
			}
		}

		private static int FirstCharToEscape(string s, bool[] charEscapeFlags, StringEscapeHandling stringEscapeHandling)
		{
			for (int i = 0; i != s.Length; i++)
			{
				char c = s[i];
				if (c < charEscapeFlags.Length)
				{
					if (charEscapeFlags[(uint)c])
					{
						return i;
					}
					continue;
				}
				if (stringEscapeHandling == StringEscapeHandling.EscapeNonAscii)
				{
					return i;
				}
				if (c == '\u0085' || c == '\u2028' || c == '\u2029')
				{
					return i;
				}
			}
			return -1;
		}

		public static Task WriteEscapedJavaScriptStringAsync(TextWriter writer, string s, char delimiter, bool appendDelimiters, bool[] charEscapeFlags, StringEscapeHandling stringEscapeHandling, JsonTextWriter client, char[] writeBuffer, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			if (appendDelimiters)
			{
				return WriteEscapedJavaScriptStringWithDelimitersAsync(writer, s, delimiter, charEscapeFlags, stringEscapeHandling, client, writeBuffer, cancellationToken);
			}
			if (StringUtils.IsNullOrEmpty(s))
			{
				return cancellationToken.CancelIfRequestedAsync() ?? AsyncUtils.CompletedTask;
			}
			return WriteEscapedJavaScriptStringWithoutDelimitersAsync(writer, s, charEscapeFlags, stringEscapeHandling, client, writeBuffer, cancellationToken);
		}

		private static Task WriteEscapedJavaScriptStringWithDelimitersAsync(TextWriter writer, string s, char delimiter, bool[] charEscapeFlags, StringEscapeHandling stringEscapeHandling, JsonTextWriter client, char[] writeBuffer, CancellationToken cancellationToken)
		{
			Task task = writer.WriteAsync(delimiter, cancellationToken);
			if (!task.IsCompletedSucessfully())
			{
				return WriteEscapedJavaScriptStringWithDelimitersAsync(task, writer, s, delimiter, charEscapeFlags, stringEscapeHandling, client, writeBuffer, cancellationToken);
			}
			if (!StringUtils.IsNullOrEmpty(s))
			{
				task = WriteEscapedJavaScriptStringWithoutDelimitersAsync(writer, s, charEscapeFlags, stringEscapeHandling, client, writeBuffer, cancellationToken);
				if (task.IsCompletedSucessfully())
				{
					return writer.WriteAsync(delimiter, cancellationToken);
				}
			}
			return WriteCharAsync(task, writer, delimiter, cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CWriteEscapedJavaScriptStringWithDelimitersAsync_003Ed__13))]
		private static Task WriteEscapedJavaScriptStringWithDelimitersAsync(Task task, TextWriter writer, string s, char delimiter, bool[] charEscapeFlags, StringEscapeHandling stringEscapeHandling, JsonTextWriter client, char[] writeBuffer, CancellationToken cancellationToken)
		{
			_003CWriteEscapedJavaScriptStringWithDelimitersAsync_003Ed__13 stateMachine = default(_003CWriteEscapedJavaScriptStringWithDelimitersAsync_003Ed__13);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine.task = task;
			stateMachine.writer = writer;
			stateMachine.s = s;
			stateMachine.delimiter = delimiter;
			stateMachine.charEscapeFlags = charEscapeFlags;
			stateMachine.stringEscapeHandling = stringEscapeHandling;
			stateMachine.client = client;
			stateMachine.writeBuffer = writeBuffer;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[AsyncStateMachine(typeof(_003CWriteCharAsync_003Ed__14))]
		public static Task WriteCharAsync(Task task, TextWriter writer, char c, CancellationToken cancellationToken)
		{
			_003CWriteCharAsync_003Ed__14 stateMachine = default(_003CWriteCharAsync_003Ed__14);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine.task = task;
			stateMachine.writer = writer;
			stateMachine.c = c;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		private static Task WriteEscapedJavaScriptStringWithoutDelimitersAsync(TextWriter writer, string s, bool[] charEscapeFlags, StringEscapeHandling stringEscapeHandling, JsonTextWriter client, char[] writeBuffer, CancellationToken cancellationToken)
		{
			int num = FirstCharToEscape(s, charEscapeFlags, stringEscapeHandling);
			if (num != -1)
			{
				return WriteDefinitelyEscapedJavaScriptStringWithoutDelimitersAsync(writer, s, num, charEscapeFlags, stringEscapeHandling, client, writeBuffer, cancellationToken);
			}
			return writer.WriteAsync(s, cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CWriteDefinitelyEscapedJavaScriptStringWithoutDelimitersAsync_003Ed__16))]
		private static Task WriteDefinitelyEscapedJavaScriptStringWithoutDelimitersAsync(TextWriter writer, string s, int lastWritePosition, bool[] charEscapeFlags, StringEscapeHandling stringEscapeHandling, JsonTextWriter client, char[] writeBuffer, CancellationToken cancellationToken)
		{
			_003CWriteDefinitelyEscapedJavaScriptStringWithoutDelimitersAsync_003Ed__16 stateMachine = default(_003CWriteDefinitelyEscapedJavaScriptStringWithoutDelimitersAsync_003Ed__16);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine.writer = writer;
			stateMachine.s = s;
			stateMachine.lastWritePosition = lastWritePosition;
			stateMachine.charEscapeFlags = charEscapeFlags;
			stateMachine.stringEscapeHandling = stringEscapeHandling;
			stateMachine.client = client;
			stateMachine.writeBuffer = writeBuffer;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public static bool TryGetDateFromConstructorJson(JsonReader reader, out DateTime dateTime, [Nullable(2)][NotNullWhen(false)] out string errorMessage)
		{
			dateTime = default(DateTime);
			errorMessage = null;
			long? integer;
			if (!TryGetDateConstructorValue(reader, out integer, out errorMessage) || !integer.HasValue)
			{
				errorMessage = errorMessage ?? "Date constructor has no arguments.";
				return false;
			}
			long? integer2;
			if (!TryGetDateConstructorValue(reader, out integer2, out errorMessage))
			{
				return false;
			}
			if (integer2.HasValue)
			{
				List<long> list = new List<long> { integer.Value, integer2.Value };
				while (true)
				{
					long? integer3;
					if (!TryGetDateConstructorValue(reader, out integer3, out errorMessage))
					{
						return false;
					}
					if (!integer3.HasValue)
					{
						break;
					}
					list.Add(integer3.Value);
				}
				if (list.Count > 7)
				{
					errorMessage = "Unexpected number of arguments when reading date constructor.";
					return false;
				}
				while (list.Count < 7)
				{
					list.Add(0L);
				}
				dateTime = new DateTime((int)list[0], (int)list[1] + 1, (int)((list[2] == 0L) ? 1 : list[2]), (int)list[3], (int)list[4], (int)list[5], (int)list[6]);
			}
			else
			{
				dateTime = DateTimeUtils.ConvertJavaScriptTicksToDateTime(integer.Value);
			}
			return true;
		}

		private static bool TryGetDateConstructorValue(JsonReader reader, out long? integer, [Nullable(2)][NotNullWhen(false)] out string errorMessage)
		{
			integer = null;
			errorMessage = null;
			if (!reader.Read())
			{
				errorMessage = "Unexpected end when reading date constructor.";
				return false;
			}
			if (reader.TokenType == JsonToken.EndConstructor)
			{
				return true;
			}
			if (reader.TokenType != JsonToken.Integer)
			{
				errorMessage = "Unexpected token when reading date constructor. Expected Integer, got " + reader.TokenType;
				return false;
			}
			integer = (long)reader.Value;
			return true;
		}
	}
}
