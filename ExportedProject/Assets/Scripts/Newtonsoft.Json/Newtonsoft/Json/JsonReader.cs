using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	[NullableContext(2)]
	[Nullable(0)]
	public abstract class JsonReader : IDisposable
	{
		[NullableContext(0)]
		protected internal enum State
		{
			Start = 0,
			Complete = 1,
			Property = 2,
			ObjectStart = 3,
			Object = 4,
			ArrayStart = 5,
			Array = 6,
			Closed = 7,
			PostValue = 8,
			ConstructorStart = 9,
			Constructor = 10,
			Error = 11,
			Finished = 12
		}

		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CSkipAsync_003Ed__1 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public JsonReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private int _003Cdepth_003E5__2;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonReader jsonReader = _003C_003E4__this;
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
							goto IL_0108;
						}
						if (jsonReader.TokenType != JsonToken.PropertyName)
						{
							goto IL_008c;
						}
						awaiter = jsonReader.ReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
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
					awaiter.GetResult();
					goto IL_008c;
					IL_008c:
					if (JsonTokenUtils.IsStartToken(jsonReader.TokenType))
					{
						_003Cdepth_003E5__2 = jsonReader.Depth;
						goto IL_00a8;
					}
					goto end_IL_000e;
					IL_00a8:
					awaiter = jsonReader.ReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_003C_003E1__state = 1);
						_003C_003Eu__1 = awaiter;
						_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_0108;
					IL_0108:
					if (awaiter.GetResult() && _003Cdepth_003E5__2 < jsonReader.Depth)
					{
						goto IL_00a8;
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
		private struct _003CReaderReadAndAssertAsync_003Ed__2 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public JsonReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonReader jsonReader = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						awaiter = jsonReader.ReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
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
						throw jsonReader.CreateUnexpectedEndException();
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
		private struct _003CReadArrayIntoByteArrayAsync_003Ed__5 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			[Nullable(0)]
			public AsyncTaskMethodBuilder<byte[]> _003C_003Et__builder;

			[Nullable(0)]
			public JsonReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			[Nullable(0)]
			private List<byte> _003Cbuffer_003E5__2;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonReader jsonReader = _003C_003E4__this;
				byte[] result;
				try
				{
					if (num != 0)
					{
						_003Cbuffer_003E5__2 = new List<byte>();
						goto IL_001c;
					}
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter = _003C_003Eu__1;
					_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_0080;
					IL_0080:
					if (!awaiter.GetResult())
					{
						jsonReader.SetToken(JsonToken.None);
					}
					if (!jsonReader.ReadArrayElementIntoByteArrayReportDone(_003Cbuffer_003E5__2))
					{
						goto IL_001c;
					}
					byte[] array = _003Cbuffer_003E5__2.ToArray();
					jsonReader.SetToken(JsonToken.Bytes, array, false);
					result = array;
					goto end_IL_000e;
					IL_001c:
					awaiter = jsonReader.ReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_003C_003E1__state = 0);
						_003C_003Eu__1 = awaiter;
						_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_0080;
					end_IL_000e:;
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003Cbuffer_003E5__2 = null;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003Cbuffer_003E5__2 = null;
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
		private struct _003CReadAndMoveToContentAsync_003Ed__12 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			[Nullable(0)]
			public AsyncTaskMethodBuilder<bool> _003C_003Et__builder;

			[Nullable(0)]
			public JsonReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonReader jsonReader = _003C_003E4__this;
				bool result2;
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
							goto IL_00ee;
						}
						awaiter = jsonReader.ReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
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
					if (result)
					{
						awaiter = jsonReader.MoveToContentAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 1);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_00ee;
					}
					goto IL_00f6;
					IL_00ee:
					result = awaiter.GetResult();
					goto IL_00f6;
					IL_00f6:
					result2 = result;
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
		private struct _003CMoveToContentFromNonContentAsync_003Ed__14 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			[Nullable(0)]
			public AsyncTaskMethodBuilder<bool> _003C_003Et__builder;

			[Nullable(0)]
			public JsonReader _003C_003E4__this;

			public CancellationToken cancellationToken;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JsonReader jsonReader = _003C_003E4__this;
				bool result;
				try
				{
					if (num != 0)
					{
						goto IL_0011;
					}
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter = _003C_003Eu__1;
					_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_0072;
					IL_0011:
					awaiter = jsonReader.ReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_003C_003E1__state = 0);
						_003C_003Eu__1 = awaiter;
						_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_0072;
					IL_0072:
					if (!awaiter.GetResult())
					{
						result = false;
					}
					else
					{
						JsonToken tokenType = jsonReader.TokenType;
						if (tokenType == JsonToken.None || tokenType == JsonToken.Comment)
						{
							goto IL_0011;
						}
						result = true;
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

		private JsonToken _tokenType;

		private object _value;

		internal char _quoteChar;

		internal State _currentState;

		private JsonPosition _currentPosition;

		private CultureInfo _culture;

		private DateTimeZoneHandling _dateTimeZoneHandling;

		private int? _maxDepth;

		private bool _hasExceededMaxDepth;

		internal DateParseHandling _dateParseHandling;

		internal FloatParseHandling _floatParseHandling;

		private string _dateFormatString;

		private List<JsonPosition> _stack;

		protected State CurrentState
		{
			get
			{
				return _currentState;
			}
		}

		public bool CloseInput { get; set; }

		public bool SupportMultipleContent { get; set; }

		public virtual char QuoteChar
		{
			get
			{
				return _quoteChar;
			}
			protected internal set
			{
				_quoteChar = value;
			}
		}

		public DateTimeZoneHandling DateTimeZoneHandling
		{
			get
			{
				return _dateTimeZoneHandling;
			}
			set
			{
				if (value < DateTimeZoneHandling.Local || value > DateTimeZoneHandling.RoundtripKind)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				_dateTimeZoneHandling = value;
			}
		}

		public DateParseHandling DateParseHandling
		{
			get
			{
				return _dateParseHandling;
			}
			set
			{
				if (value < DateParseHandling.None || value > DateParseHandling.DateTimeOffset)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				_dateParseHandling = value;
			}
		}

		public FloatParseHandling FloatParseHandling
		{
			get
			{
				return _floatParseHandling;
			}
			set
			{
				if (value < FloatParseHandling.Double || value > FloatParseHandling.Decimal)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				_floatParseHandling = value;
			}
		}

		public string DateFormatString
		{
			get
			{
				return _dateFormatString;
			}
			set
			{
				_dateFormatString = value;
			}
		}

		public int? MaxDepth
		{
			get
			{
				return _maxDepth;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentException("Value must be positive.", "value");
				}
				_maxDepth = value;
			}
		}

		public virtual JsonToken TokenType
		{
			get
			{
				return _tokenType;
			}
		}

		public virtual object Value
		{
			get
			{
				return _value;
			}
		}

		public virtual Type ValueType
		{
			get
			{
				object value = _value;
				if (value == null)
				{
					return null;
				}
				return value.GetType();
			}
		}

		public virtual int Depth
		{
			get
			{
				List<JsonPosition> stack = _stack;
				int num = ((stack != null) ? stack.Count : 0);
				if (JsonTokenUtils.IsStartToken(TokenType) || _currentPosition.Type == JsonContainerType.None)
				{
					return num;
				}
				return num + 1;
			}
		}

		[Nullable(1)]
		public virtual string Path
		{
			[NullableContext(1)]
			get
			{
				if (_currentPosition.Type == JsonContainerType.None)
				{
					return string.Empty;
				}
				JsonPosition? currentPosition = ((_currentState != State.ArrayStart && _currentState != State.ConstructorStart && _currentState != State.ObjectStart) ? new JsonPosition?(_currentPosition) : null);
				return JsonPosition.BuildPath(_stack, currentPosition);
			}
		}

		[Nullable(1)]
		public CultureInfo Culture
		{
			[NullableContext(1)]
			get
			{
				return _culture ?? CultureInfo.InvariantCulture;
			}
			[NullableContext(1)]
			set
			{
				_culture = value;
			}
		}

		[NullableContext(1)]
		public virtual Task<bool> ReadAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return cancellationToken.CancelIfRequestedAsync<bool>() ?? Read().ToAsync();
		}

		[NullableContext(1)]
		[AsyncStateMachine(typeof(_003CSkipAsync_003Ed__1))]
		public Task SkipAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			_003CSkipAsync_003Ed__1 stateMachine = default(_003CSkipAsync_003Ed__1);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[NullableContext(1)]
		[AsyncStateMachine(typeof(_003CReaderReadAndAssertAsync_003Ed__2))]
		internal Task ReaderReadAndAssertAsync(CancellationToken cancellationToken)
		{
			_003CReaderReadAndAssertAsync_003Ed__2 stateMachine = default(_003CReaderReadAndAssertAsync_003Ed__2);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[NullableContext(1)]
		public virtual Task<bool?> ReadAsBooleanAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return cancellationToken.CancelIfRequestedAsync<bool?>() ?? Task.FromResult(ReadAsBoolean());
		}

		[return: Nullable(new byte[] { 1, 2 })]
		public virtual Task<byte[]> ReadAsBytesAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return cancellationToken.CancelIfRequestedAsync<byte[]>() ?? Task.FromResult(ReadAsBytes());
		}

		[AsyncStateMachine(typeof(_003CReadArrayIntoByteArrayAsync_003Ed__5))]
		[return: Nullable(new byte[] { 1, 2 })]
		internal Task<byte[]> ReadArrayIntoByteArrayAsync(CancellationToken cancellationToken)
		{
			_003CReadArrayIntoByteArrayAsync_003Ed__5 stateMachine = default(_003CReadArrayIntoByteArrayAsync_003Ed__5);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<byte[]>.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[NullableContext(1)]
		public virtual Task<DateTime?> ReadAsDateTimeAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return cancellationToken.CancelIfRequestedAsync<DateTime?>() ?? Task.FromResult(ReadAsDateTime());
		}

		[NullableContext(1)]
		public virtual Task<DateTimeOffset?> ReadAsDateTimeOffsetAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return cancellationToken.CancelIfRequestedAsync<DateTimeOffset?>() ?? Task.FromResult(ReadAsDateTimeOffset());
		}

		[NullableContext(1)]
		public virtual Task<decimal?> ReadAsDecimalAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return cancellationToken.CancelIfRequestedAsync<decimal?>() ?? Task.FromResult(ReadAsDecimal());
		}

		[NullableContext(1)]
		public virtual Task<double?> ReadAsDoubleAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return Task.FromResult(ReadAsDouble());
		}

		[NullableContext(1)]
		public virtual Task<int?> ReadAsInt32Async(CancellationToken cancellationToken = default(CancellationToken))
		{
			return cancellationToken.CancelIfRequestedAsync<int?>() ?? Task.FromResult(ReadAsInt32());
		}

		[return: Nullable(new byte[] { 1, 2 })]
		public virtual Task<string> ReadAsStringAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return cancellationToken.CancelIfRequestedAsync<string>() ?? Task.FromResult(ReadAsString());
		}

		[NullableContext(1)]
		[AsyncStateMachine(typeof(_003CReadAndMoveToContentAsync_003Ed__12))]
		internal Task<bool> ReadAndMoveToContentAsync(CancellationToken cancellationToken)
		{
			_003CReadAndMoveToContentAsync_003Ed__12 stateMachine = default(_003CReadAndMoveToContentAsync_003Ed__12);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<bool>.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[NullableContext(1)]
		internal Task<bool> MoveToContentAsync(CancellationToken cancellationToken)
		{
			JsonToken tokenType = TokenType;
			if (tokenType == JsonToken.None || tokenType == JsonToken.Comment)
			{
				return MoveToContentFromNonContentAsync(cancellationToken);
			}
			return AsyncUtils.True;
		}

		[NullableContext(1)]
		[AsyncStateMachine(typeof(_003CMoveToContentFromNonContentAsync_003Ed__14))]
		private Task<bool> MoveToContentFromNonContentAsync(CancellationToken cancellationToken)
		{
			_003CMoveToContentFromNonContentAsync_003Ed__14 stateMachine = default(_003CMoveToContentFromNonContentAsync_003Ed__14);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<bool>.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		internal JsonPosition GetPosition(int depth)
		{
			if (_stack != null && depth < _stack.Count)
			{
				return _stack[depth];
			}
			return _currentPosition;
		}

		protected JsonReader()
		{
			_currentState = State.Start;
			_dateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;
			_dateParseHandling = DateParseHandling.DateTime;
			_floatParseHandling = FloatParseHandling.Double;
			_maxDepth = 64;
			CloseInput = true;
		}

		private void Push(JsonContainerType value)
		{
			UpdateScopeWithFinishedValue();
			if (_currentPosition.Type == JsonContainerType.None)
			{
				_currentPosition = new JsonPosition(value);
				return;
			}
			if (_stack == null)
			{
				_stack = new List<JsonPosition>();
			}
			_stack.Add(_currentPosition);
			_currentPosition = new JsonPosition(value);
			if (!_maxDepth.HasValue || !(Depth + 1 > _maxDepth) || _hasExceededMaxDepth)
			{
				return;
			}
			_hasExceededMaxDepth = true;
			throw JsonReaderException.Create(this, "The reader's MaxDepth of {0} has been exceeded.".FormatWith(CultureInfo.InvariantCulture, _maxDepth));
		}

		private JsonContainerType Pop()
		{
			JsonPosition currentPosition;
			if (_stack != null && _stack.Count > 0)
			{
				currentPosition = _currentPosition;
				_currentPosition = _stack[_stack.Count - 1];
				_stack.RemoveAt(_stack.Count - 1);
			}
			else
			{
				currentPosition = _currentPosition;
				_currentPosition = default(JsonPosition);
			}
			if (_maxDepth.HasValue && Depth <= _maxDepth)
			{
				_hasExceededMaxDepth = false;
			}
			return currentPosition.Type;
		}

		private JsonContainerType Peek()
		{
			return _currentPosition.Type;
		}

		public abstract bool Read();

		public virtual int? ReadAsInt32()
		{
			JsonToken contentToken = GetContentToken();
			switch (contentToken)
			{
			case JsonToken.None:
			case JsonToken.Null:
			case JsonToken.EndArray:
				return null;
			case JsonToken.Integer:
			case JsonToken.Float:
			{
				object value = Value;
				if (value is int)
				{
					return (int)value;
				}
				int num;
				if (value is BigInteger)
				{
					BigInteger bigInteger = (BigInteger)value;
					num = (int)bigInteger;
				}
				else
				{
					try
					{
						num = Convert.ToInt32(value, CultureInfo.InvariantCulture);
					}
					catch (Exception ex)
					{
						throw JsonReaderException.Create(this, "Could not convert to integer: {0}.".FormatWith(CultureInfo.InvariantCulture, value), ex);
					}
				}
				SetToken(JsonToken.Integer, num, false);
				return num;
			}
			case JsonToken.String:
			{
				string s = (string)Value;
				return ReadInt32String(s);
			}
			default:
				throw JsonReaderException.Create(this, "Error reading integer. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
			}
		}

		internal int? ReadInt32String(string s)
		{
			if (StringUtils.IsNullOrEmpty(s))
			{
				SetToken(JsonToken.Null, null, false);
				return null;
			}
			int result;
			if (int.TryParse(s, NumberStyles.Integer, Culture, out result))
			{
				SetToken(JsonToken.Integer, result, false);
				return result;
			}
			SetToken(JsonToken.String, s, false);
			throw JsonReaderException.Create(this, "Could not convert string to integer: {0}.".FormatWith(CultureInfo.InvariantCulture, s));
		}

		public virtual string ReadAsString()
		{
			JsonToken contentToken = GetContentToken();
			switch (contentToken)
			{
			case JsonToken.None:
			case JsonToken.Null:
			case JsonToken.EndArray:
				return null;
			case JsonToken.String:
				return (string)Value;
			default:
				if (JsonTokenUtils.IsPrimitiveToken(contentToken))
				{
					object value = Value;
					if (value != null)
					{
						IFormattable formattable = value as IFormattable;
						string text;
						if (formattable != null)
						{
							text = formattable.ToString(null, Culture);
						}
						else
						{
							Uri uri = value as Uri;
							text = (((object)uri != null) ? uri.OriginalString : value.ToString());
						}
						SetToken(JsonToken.String, text, false);
						return text;
					}
				}
				throw JsonReaderException.Create(this, "Error reading string. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
			}
		}

		public virtual byte[] ReadAsBytes()
		{
			JsonToken contentToken = GetContentToken();
			switch (contentToken)
			{
			case JsonToken.StartObject:
			{
				ReadIntoWrappedTypeObject();
				byte[] array3 = ReadAsBytes();
				ReaderReadAndAssert();
				if (TokenType != JsonToken.EndObject)
				{
					throw JsonReaderException.Create(this, "Error reading bytes. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, TokenType));
				}
				SetToken(JsonToken.Bytes, array3, false);
				return array3;
			}
			case JsonToken.String:
			{
				string text = (string)Value;
				Guid g;
				byte[] array2 = ((text.Length == 0) ? CollectionUtils.ArrayEmpty<byte>() : ((!ConvertUtils.TryConvertGuid(text, out g)) ? Convert.FromBase64String(text) : g.ToByteArray()));
				SetToken(JsonToken.Bytes, array2, false);
				return array2;
			}
			case JsonToken.None:
			case JsonToken.Null:
			case JsonToken.EndArray:
				return null;
			case JsonToken.Bytes:
			{
				object value = Value;
				if (value is Guid)
				{
					byte[] array = ((Guid)value).ToByteArray();
					SetToken(JsonToken.Bytes, array, false);
					return array;
				}
				return (byte[])Value;
			}
			case JsonToken.StartArray:
				return ReadArrayIntoByteArray();
			default:
				throw JsonReaderException.Create(this, "Error reading bytes. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
			}
		}

		[NullableContext(1)]
		internal byte[] ReadArrayIntoByteArray()
		{
			List<byte> list = new List<byte>();
			do
			{
				if (!Read())
				{
					SetToken(JsonToken.None);
				}
			}
			while (!ReadArrayElementIntoByteArrayReportDone(list));
			byte[] array = list.ToArray();
			SetToken(JsonToken.Bytes, array, false);
			return array;
		}

		[NullableContext(1)]
		private bool ReadArrayElementIntoByteArrayReportDone(List<byte> buffer)
		{
			switch (TokenType)
			{
			case JsonToken.None:
				throw JsonReaderException.Create(this, "Unexpected end when reading bytes.");
			case JsonToken.Integer:
				buffer.Add(Convert.ToByte(Value, CultureInfo.InvariantCulture));
				return false;
			case JsonToken.EndArray:
				return true;
			case JsonToken.Comment:
				return false;
			default:
				throw JsonReaderException.Create(this, "Unexpected token when reading bytes: {0}.".FormatWith(CultureInfo.InvariantCulture, TokenType));
			}
		}

		public virtual double? ReadAsDouble()
		{
			JsonToken contentToken = GetContentToken();
			switch (contentToken)
			{
			case JsonToken.None:
			case JsonToken.Null:
			case JsonToken.EndArray:
				return null;
			case JsonToken.Integer:
			case JsonToken.Float:
			{
				object value = Value;
				if (value is double)
				{
					return (double)value;
				}
				double num;
				if (value is BigInteger)
				{
					BigInteger bigInteger = (BigInteger)value;
					num = (double)bigInteger;
				}
				else
				{
					num = Convert.ToDouble(value, CultureInfo.InvariantCulture);
				}
				SetToken(JsonToken.Float, num, false);
				return num;
			}
			case JsonToken.String:
				return ReadDoubleString((string)Value);
			default:
				throw JsonReaderException.Create(this, "Error reading double. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
			}
		}

		internal double? ReadDoubleString(string s)
		{
			if (StringUtils.IsNullOrEmpty(s))
			{
				SetToken(JsonToken.Null, null, false);
				return null;
			}
			double result;
			if (double.TryParse(s, NumberStyles.Float | NumberStyles.AllowThousands, Culture, out result))
			{
				SetToken(JsonToken.Float, result, false);
				return result;
			}
			SetToken(JsonToken.String, s, false);
			throw JsonReaderException.Create(this, "Could not convert string to double: {0}.".FormatWith(CultureInfo.InvariantCulture, s));
		}

		public virtual bool? ReadAsBoolean()
		{
			JsonToken contentToken = GetContentToken();
			switch (contentToken)
			{
			case JsonToken.None:
			case JsonToken.Null:
			case JsonToken.EndArray:
				return null;
			case JsonToken.Integer:
			case JsonToken.Float:
			{
				object value = Value;
				bool flag;
				if (value is BigInteger)
				{
					BigInteger bigInteger = (BigInteger)value;
					flag = bigInteger != 0L;
				}
				else
				{
					flag = Convert.ToBoolean(Value, CultureInfo.InvariantCulture);
				}
				SetToken(JsonToken.Boolean, flag, false);
				return flag;
			}
			case JsonToken.String:
				return ReadBooleanString((string)Value);
			case JsonToken.Boolean:
				return (bool)Value;
			default:
				throw JsonReaderException.Create(this, "Error reading boolean. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
			}
		}

		internal bool? ReadBooleanString(string s)
		{
			if (StringUtils.IsNullOrEmpty(s))
			{
				SetToken(JsonToken.Null, null, false);
				return null;
			}
			bool result;
			if (bool.TryParse(s, out result))
			{
				SetToken(JsonToken.Boolean, result, false);
				return result;
			}
			SetToken(JsonToken.String, s, false);
			throw JsonReaderException.Create(this, "Could not convert string to boolean: {0}.".FormatWith(CultureInfo.InvariantCulture, s));
		}

		public virtual decimal? ReadAsDecimal()
		{
			JsonToken contentToken = GetContentToken();
			switch (contentToken)
			{
			case JsonToken.None:
			case JsonToken.Null:
			case JsonToken.EndArray:
				return null;
			case JsonToken.Integer:
			case JsonToken.Float:
			{
				object value = Value;
				if (value is decimal)
				{
					return (decimal)value;
				}
				decimal num;
				if (value is BigInteger)
				{
					BigInteger bigInteger = (BigInteger)value;
					num = (decimal)bigInteger;
				}
				else
				{
					try
					{
						num = Convert.ToDecimal(value, CultureInfo.InvariantCulture);
					}
					catch (Exception ex)
					{
						throw JsonReaderException.Create(this, "Could not convert to decimal: {0}.".FormatWith(CultureInfo.InvariantCulture, value), ex);
					}
				}
				SetToken(JsonToken.Float, num, false);
				return num;
			}
			case JsonToken.String:
				return ReadDecimalString((string)Value);
			default:
				throw JsonReaderException.Create(this, "Error reading decimal. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
			}
		}

		internal decimal? ReadDecimalString(string s)
		{
			if (StringUtils.IsNullOrEmpty(s))
			{
				SetToken(JsonToken.Null, null, false);
				return null;
			}
			decimal result;
			if (decimal.TryParse(s, NumberStyles.Number, Culture, out result))
			{
				SetToken(JsonToken.Float, result, false);
				return result;
			}
			if (ConvertUtils.DecimalTryParse(s.ToCharArray(), 0, s.Length, out result) == ParseResult.Success)
			{
				SetToken(JsonToken.Float, result, false);
				return result;
			}
			SetToken(JsonToken.String, s, false);
			throw JsonReaderException.Create(this, "Could not convert string to decimal: {0}.".FormatWith(CultureInfo.InvariantCulture, s));
		}

		public virtual DateTime? ReadAsDateTime()
		{
			switch (GetContentToken())
			{
			case JsonToken.None:
			case JsonToken.Null:
			case JsonToken.EndArray:
				return null;
			case JsonToken.Date:
			{
				object value = Value;
				if (value is DateTimeOffset)
				{
					SetToken(JsonToken.Date, ((DateTimeOffset)value).DateTime, false);
				}
				return (DateTime)Value;
			}
			case JsonToken.String:
				return ReadDateTimeString((string)Value);
			default:
				throw JsonReaderException.Create(this, "Error reading date. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, TokenType));
			}
		}

		internal DateTime? ReadDateTimeString(string s)
		{
			if (StringUtils.IsNullOrEmpty(s))
			{
				SetToken(JsonToken.Null, null, false);
				return null;
			}
			DateTime dt;
			if (DateTimeUtils.TryParseDateTime(s, DateTimeZoneHandling, _dateFormatString, Culture, out dt))
			{
				dt = DateTimeUtils.EnsureDateTime(dt, DateTimeZoneHandling);
				SetToken(JsonToken.Date, dt, false);
				return dt;
			}
			if (DateTime.TryParse(s, Culture, DateTimeStyles.RoundtripKind, out dt))
			{
				dt = DateTimeUtils.EnsureDateTime(dt, DateTimeZoneHandling);
				SetToken(JsonToken.Date, dt, false);
				return dt;
			}
			throw JsonReaderException.Create(this, "Could not convert string to DateTime: {0}.".FormatWith(CultureInfo.InvariantCulture, s));
		}

		public virtual DateTimeOffset? ReadAsDateTimeOffset()
		{
			JsonToken contentToken = GetContentToken();
			switch (contentToken)
			{
			case JsonToken.None:
			case JsonToken.Null:
			case JsonToken.EndArray:
				return null;
			case JsonToken.Date:
			{
				object value = Value;
				if (value is DateTime)
				{
					DateTime dateTime = (DateTime)value;
					SetToken(JsonToken.Date, new DateTimeOffset(dateTime), false);
				}
				return (DateTimeOffset)Value;
			}
			case JsonToken.String:
			{
				string s = (string)Value;
				return ReadDateTimeOffsetString(s);
			}
			default:
				throw JsonReaderException.Create(this, "Error reading date. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
			}
		}

		internal DateTimeOffset? ReadDateTimeOffsetString(string s)
		{
			if (StringUtils.IsNullOrEmpty(s))
			{
				SetToken(JsonToken.Null, null, false);
				return null;
			}
			DateTimeOffset dt;
			if (DateTimeUtils.TryParseDateTimeOffset(s, _dateFormatString, Culture, out dt))
			{
				SetToken(JsonToken.Date, dt, false);
				return dt;
			}
			if (DateTimeOffset.TryParse(s, Culture, DateTimeStyles.RoundtripKind, out dt))
			{
				SetToken(JsonToken.Date, dt, false);
				return dt;
			}
			SetToken(JsonToken.String, s, false);
			throw JsonReaderException.Create(this, "Could not convert string to DateTimeOffset: {0}.".FormatWith(CultureInfo.InvariantCulture, s));
		}

		internal void ReaderReadAndAssert()
		{
			if (!Read())
			{
				throw CreateUnexpectedEndException();
			}
		}

		[NullableContext(1)]
		internal JsonReaderException CreateUnexpectedEndException()
		{
			return JsonReaderException.Create(this, "Unexpected end when reading JSON.");
		}

		internal void ReadIntoWrappedTypeObject()
		{
			ReaderReadAndAssert();
			if (Value != null && Value.ToString() == "$type")
			{
				ReaderReadAndAssert();
				if (Value != null && Value.ToString().StartsWith("System.Byte[]", StringComparison.Ordinal))
				{
					ReaderReadAndAssert();
					if (Value.ToString() == "$value")
					{
						return;
					}
				}
			}
			throw JsonReaderException.Create(this, "Error reading bytes. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, JsonToken.StartObject));
		}

		public void Skip()
		{
			if (TokenType == JsonToken.PropertyName)
			{
				Read();
			}
			if (JsonTokenUtils.IsStartToken(TokenType))
			{
				int depth = Depth;
				while (Read() && depth < Depth)
				{
				}
			}
		}

		protected void SetToken(JsonToken newToken)
		{
			SetToken(newToken, null, true);
		}

		protected void SetToken(JsonToken newToken, object value)
		{
			SetToken(newToken, value, true);
		}

		protected void SetToken(JsonToken newToken, object value, bool updateIndex)
		{
			_tokenType = newToken;
			_value = value;
			switch (newToken)
			{
			case JsonToken.StartObject:
				_currentState = State.ObjectStart;
				Push(JsonContainerType.Object);
				break;
			case JsonToken.StartArray:
				_currentState = State.ArrayStart;
				Push(JsonContainerType.Array);
				break;
			case JsonToken.StartConstructor:
				_currentState = State.ConstructorStart;
				Push(JsonContainerType.Constructor);
				break;
			case JsonToken.EndObject:
				ValidateEnd(JsonToken.EndObject);
				break;
			case JsonToken.EndArray:
				ValidateEnd(JsonToken.EndArray);
				break;
			case JsonToken.EndConstructor:
				ValidateEnd(JsonToken.EndConstructor);
				break;
			case JsonToken.PropertyName:
				_currentState = State.Property;
				_currentPosition.PropertyName = (string)value;
				break;
			case JsonToken.Raw:
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Null:
			case JsonToken.Undefined:
			case JsonToken.Date:
			case JsonToken.Bytes:
				SetPostValueState(updateIndex);
				break;
			case JsonToken.Comment:
				break;
			}
		}

		internal void SetPostValueState(bool updateIndex)
		{
			if (Peek() != 0 || SupportMultipleContent)
			{
				_currentState = State.PostValue;
			}
			else
			{
				SetFinished();
			}
			if (updateIndex)
			{
				UpdateScopeWithFinishedValue();
			}
		}

		private void UpdateScopeWithFinishedValue()
		{
			if (_currentPosition.HasIndex)
			{
				_currentPosition.Position++;
			}
		}

		private void ValidateEnd(JsonToken endToken)
		{
			JsonContainerType jsonContainerType = Pop();
			if (GetTypeForCloseToken(endToken) != jsonContainerType)
			{
				throw JsonReaderException.Create(this, "JsonToken {0} is not valid for closing JsonType {1}.".FormatWith(CultureInfo.InvariantCulture, endToken, jsonContainerType));
			}
			if (Peek() != 0 || SupportMultipleContent)
			{
				_currentState = State.PostValue;
			}
			else
			{
				SetFinished();
			}
		}

		protected void SetStateBasedOnCurrent()
		{
			JsonContainerType jsonContainerType = Peek();
			switch (jsonContainerType)
			{
			case JsonContainerType.Object:
				_currentState = State.Object;
				break;
			case JsonContainerType.Array:
				_currentState = State.Array;
				break;
			case JsonContainerType.Constructor:
				_currentState = State.Constructor;
				break;
			case JsonContainerType.None:
				SetFinished();
				break;
			default:
				throw JsonReaderException.Create(this, "While setting the reader state back to current object an unexpected JsonType was encountered: {0}".FormatWith(CultureInfo.InvariantCulture, jsonContainerType));
			}
		}

		private void SetFinished()
		{
			_currentState = ((!SupportMultipleContent) ? State.Finished : State.Start);
		}

		private JsonContainerType GetTypeForCloseToken(JsonToken token)
		{
			switch (token)
			{
			case JsonToken.EndObject:
				return JsonContainerType.Object;
			case JsonToken.EndArray:
				return JsonContainerType.Array;
			case JsonToken.EndConstructor:
				return JsonContainerType.Constructor;
			default:
				throw JsonReaderException.Create(this, "Not a valid close JsonToken: {0}".FormatWith(CultureInfo.InvariantCulture, token));
			}
		}

		void IDisposable.Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_currentState != State.Closed && disposing)
			{
				Close();
			}
		}

		public virtual void Close()
		{
			_currentState = State.Closed;
			_tokenType = JsonToken.None;
			_value = null;
		}

		internal void ReadAndAssert()
		{
			if (!Read())
			{
				throw JsonSerializationException.Create(this, "Unexpected end when reading JSON.");
			}
		}

		internal void ReadForTypeAndAssert(JsonContract contract, bool hasConverter)
		{
			if (!ReadForType(contract, hasConverter))
			{
				throw JsonSerializationException.Create(this, "Unexpected end when reading JSON.");
			}
		}

		internal bool ReadForType(JsonContract contract, bool hasConverter)
		{
			if (hasConverter)
			{
				return Read();
			}
			switch ((contract != null) ? contract.InternalReadType : ReadType.Read)
			{
			case ReadType.Read:
				return ReadAndMoveToContent();
			case ReadType.ReadAsInt32:
				ReadAsInt32();
				break;
			case ReadType.ReadAsInt64:
			{
				bool result = ReadAndMoveToContent();
				if (TokenType == JsonToken.Undefined)
				{
					throw JsonReaderException.Create(this, "An undefined token is not a valid {0}.".FormatWith(CultureInfo.InvariantCulture, ((contract != null) ? contract.UnderlyingType : null) ?? typeof(long)));
				}
				return result;
			}
			case ReadType.ReadAsDecimal:
				ReadAsDecimal();
				break;
			case ReadType.ReadAsDouble:
				ReadAsDouble();
				break;
			case ReadType.ReadAsBytes:
				ReadAsBytes();
				break;
			case ReadType.ReadAsBoolean:
				ReadAsBoolean();
				break;
			case ReadType.ReadAsString:
				ReadAsString();
				break;
			case ReadType.ReadAsDateTime:
				ReadAsDateTime();
				break;
			case ReadType.ReadAsDateTimeOffset:
				ReadAsDateTimeOffset();
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return TokenType != JsonToken.None;
		}

		internal bool ReadAndMoveToContent()
		{
			if (Read())
			{
				return MoveToContent();
			}
			return false;
		}

		internal bool MoveToContent()
		{
			JsonToken tokenType = TokenType;
			while (tokenType == JsonToken.None || tokenType == JsonToken.Comment)
			{
				if (!Read())
				{
					return false;
				}
				tokenType = TokenType;
			}
			return true;
		}

		private JsonToken GetContentToken()
		{
			JsonToken tokenType;
			do
			{
				if (!Read())
				{
					SetToken(JsonToken.None);
					return JsonToken.None;
				}
				tokenType = TokenType;
			}
			while (tokenType == JsonToken.Comment);
			return tokenType;
		}
	}
}
