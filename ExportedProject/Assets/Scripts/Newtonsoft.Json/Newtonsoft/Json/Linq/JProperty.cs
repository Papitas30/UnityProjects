using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	[NullableContext(1)]
	[Nullable(0)]
	public class JProperty : JContainer
	{
		[Nullable(0)]
		private class JPropertyList : IList<JToken>, ICollection<JToken>, IEnumerable<JToken>, IEnumerable
		{
			[Nullable(2)]
			internal JToken _token;

			public int Count
			{
				get
				{
					if (_token == null)
					{
						return 0;
					}
					return 1;
				}
			}

			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			public JToken this[int index]
			{
				get
				{
					if (index != 0)
					{
						throw new IndexOutOfRangeException();
					}
					return _token;
				}
				set
				{
					if (index != 0)
					{
						throw new IndexOutOfRangeException();
					}
					_token = value;
				}
			}

			public IEnumerator<JToken> GetEnumerator()
			{
				if (_token != null)
				{
					yield return _token;
				}
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			public void Add(JToken item)
			{
				_token = item;
			}

			public void Clear()
			{
				_token = null;
			}

			public bool Contains(JToken item)
			{
				return _token == item;
			}

			public void CopyTo(JToken[] array, int arrayIndex)
			{
				if (_token != null)
				{
					array[arrayIndex] = _token;
				}
			}

			public bool Remove(JToken item)
			{
				if (_token == item)
				{
					_token = null;
					return true;
				}
				return false;
			}

			public int IndexOf(JToken item)
			{
				if (_token != item)
				{
					return -1;
				}
				return 0;
			}

			public void Insert(int index, JToken item)
			{
				if (index == 0)
				{
					_token = item;
				}
			}

			public void RemoveAt(int index)
			{
				if (index == 0)
				{
					_token = null;
				}
			}
		}

		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CWriteToAsync_003Ed__1 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public Task task;

			[Nullable(0)]
			public JProperty _003C_003E4__this;

			[Nullable(0)]
			public JsonWriter writer;

			public CancellationToken cancellationToken;

			[Nullable(new byte[] { 0, 1 })]
			public JsonConverter[] converters;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JProperty jProperty = _003C_003E4__this;
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
					awaiter = jProperty.WriteValueAsync(writer, cancellationToken, converters).ConfigureAwait(false).GetAwaiter();
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
		private struct _003CLoadAsync_003Ed__4 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			[Nullable(0)]
			public AsyncTaskMethodBuilder<JProperty> _003C_003Et__builder;

			[Nullable(0)]
			public JsonReader reader;

			public CancellationToken cancellationToken;

			[Nullable(0)]
			public JsonLoadSettings settings;

			[Nullable(0)]
			private JProperty _003Cp_003E5__2;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__2;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JProperty result;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter2;
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					default:
						if (reader.TokenType == JsonToken.None)
						{
							awaiter2 = reader.ReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
							if (!awaiter2.IsCompleted)
							{
								num = (_003C_003E1__state = 0);
								_003C_003Eu__1 = awaiter2;
								_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
								return;
							}
							goto IL_0091;
						}
						goto IL_00ab;
					case 0:
						awaiter2 = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0091;
					case 1:
						awaiter2 = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0113;
					case 2:
						{
							awaiter = _003C_003Eu__2;
							_003C_003Eu__2 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							break;
						}
						IL_0113:
						awaiter2.GetResult();
						if (reader.TokenType != JsonToken.PropertyName)
						{
							throw JsonReaderException.Create(reader, "Error reading JProperty from JsonReader. Current JsonReader item is not a property: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
						}
						_003Cp_003E5__2 = new JProperty((string)reader.Value);
						_003Cp_003E5__2.SetLineInfo(reader as IJsonLineInfo, settings);
						awaiter = _003Cp_003E5__2.ReadTokenFromAsync(reader, settings, cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 2);
							_003C_003Eu__2 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						break;
						IL_00ab:
						awaiter2 = reader.MoveToContentAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter2.IsCompleted)
						{
							num = (_003C_003E1__state = 1);
							_003C_003Eu__1 = awaiter2;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
							return;
						}
						goto IL_0113;
						IL_0091:
						if (!awaiter2.GetResult())
						{
							throw JsonReaderException.Create(reader, "Error reading JProperty from JsonReader.");
						}
						goto IL_00ab;
					}
					awaiter.GetResult();
					result = _003Cp_003E5__2;
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003Cp_003E5__2 = null;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003Cp_003E5__2 = null;
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

		private readonly JPropertyList _content = new JPropertyList();

		private readonly string _name;

		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return _content;
			}
		}

		public string Name
		{
			[DebuggerStepThrough]
			get
			{
				return _name;
			}
		}

		public new JToken Value
		{
			[DebuggerStepThrough]
			get
			{
				return _content._token;
			}
			set
			{
				CheckReentrancy();
				JToken item = value ?? JValue.CreateNull();
				if (_content._token == null)
				{
					InsertItem(0, item, false);
				}
				else
				{
					SetItem(0, item);
				}
			}
		}

		public override JTokenType Type
		{
			[DebuggerStepThrough]
			get
			{
				return JTokenType.Property;
			}
		}

		public override Task WriteToAsync(JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			Task task = writer.WritePropertyNameAsync(_name, cancellationToken);
			if (task.IsCompletedSucessfully())
			{
				return WriteValueAsync(writer, cancellationToken, converters);
			}
			return WriteToAsync(task, writer, cancellationToken, converters);
		}

		[AsyncStateMachine(typeof(_003CWriteToAsync_003Ed__1))]
		private Task WriteToAsync(Task task, JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			_003CWriteToAsync_003Ed__1 stateMachine = default(_003CWriteToAsync_003Ed__1);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.task = task;
			stateMachine.writer = writer;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine.converters = converters;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		private Task WriteValueAsync(JsonWriter writer, CancellationToken cancellationToken, JsonConverter[] converters)
		{
			JToken value = Value;
			if (value == null)
			{
				return writer.WriteNullAsync(cancellationToken);
			}
			return value.WriteToAsync(writer, cancellationToken, converters);
		}

		public new static Task<JProperty> LoadAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			return LoadAsync(reader, null, cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CLoadAsync_003Ed__4))]
		public new static Task<JProperty> LoadAsync(JsonReader reader, [Nullable(2)] JsonLoadSettings settings, CancellationToken cancellationToken = default(CancellationToken))
		{
			_003CLoadAsync_003Ed__4 stateMachine = default(_003CLoadAsync_003Ed__4);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<JProperty>.Create();
			stateMachine.reader = reader;
			stateMachine.settings = settings;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public JProperty(JProperty other)
			: base(other)
		{
			_name = other.Name;
		}

		internal override JToken GetItem(int index)
		{
			if (index != 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			return Value;
		}

		[NullableContext(2)]
		internal override void SetItem(int index, JToken item)
		{
			if (index != 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (!JContainer.IsTokenUnchanged(Value, item))
			{
				JObject obj = (JObject)base.Parent;
				if (obj != null)
				{
					obj.InternalPropertyChanging(this);
				}
				base.SetItem(0, item);
				JObject obj2 = (JObject)base.Parent;
				if (obj2 != null)
				{
					obj2.InternalPropertyChanged(this);
				}
			}
		}

		[NullableContext(2)]
		internal override bool RemoveItem(JToken item)
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		internal override void RemoveItemAt(int index)
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		[NullableContext(2)]
		internal override int IndexOfItem(JToken item)
		{
			if (item == null)
			{
				return -1;
			}
			return _content.IndexOf(item);
		}

		[NullableContext(2)]
		internal override bool InsertItem(int index, JToken item, bool skipParentCheck)
		{
			if (item != null && item.Type == JTokenType.Comment)
			{
				return false;
			}
			if (Value != null)
			{
				throw new JsonException("{0} cannot have multiple values.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
			}
			return base.InsertItem(0, item, false);
		}

		[NullableContext(2)]
		internal override bool ContainsItem(JToken item)
		{
			return Value == item;
		}

		internal override void MergeItem(object content, [Nullable(2)] JsonMergeSettings settings)
		{
			JProperty obj = content as JProperty;
			JToken jToken = ((obj != null) ? obj.Value : null);
			if (jToken != null && jToken.Type != JTokenType.Null)
			{
				Value = jToken;
			}
		}

		internal override void ClearItems()
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		internal override bool DeepEquals(JToken node)
		{
			JProperty jProperty = node as JProperty;
			if (jProperty != null && _name == jProperty.Name)
			{
				return ContentsEqual(jProperty);
			}
			return false;
		}

		internal override JToken CloneToken()
		{
			return new JProperty(this);
		}

		internal JProperty(string name)
		{
			ValidationUtils.ArgumentNotNull(name, "name");
			_name = name;
		}

		public JProperty(string name, params object[] content)
			: this(name, (object)content)
		{
		}

		public JProperty(string name, [Nullable(2)] object content)
		{
			ValidationUtils.ArgumentNotNull(name, "name");
			_name = name;
			Value = (IsMultiContent(content) ? new JArray(content) : JContainer.CreateFromContent(content));
		}

		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WritePropertyName(_name);
			JToken value = Value;
			if (value != null)
			{
				value.WriteTo(writer, converters);
			}
			else
			{
				writer.WriteNull();
			}
		}

		internal override int GetDeepHashCode()
		{
			int hashCode = _name.GetHashCode();
			JToken value = Value;
			return hashCode ^ ((value != null) ? value.GetDeepHashCode() : 0);
		}

		public new static JProperty Load(JsonReader reader)
		{
			return Load(reader, null);
		}

		public new static JProperty Load(JsonReader reader, [Nullable(2)] JsonLoadSettings settings)
		{
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading JProperty from JsonReader.");
			}
			reader.MoveToContent();
			if (reader.TokenType != JsonToken.PropertyName)
			{
				throw JsonReaderException.Create(reader, "Error reading JProperty from JsonReader. Current JsonReader item is not a property: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JProperty jProperty = new JProperty((string)reader.Value);
			jProperty.SetLineInfo(reader as IJsonLineInfo, settings);
			jProperty.ReadTokenFrom(reader, settings);
			return jProperty;
		}
	}
}
