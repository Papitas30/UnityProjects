using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	[NullableContext(1)]
	[Nullable(0)]
	public class JArray : JContainer, IList<JToken>, ICollection<JToken>, IEnumerable<JToken>, IEnumerable
	{
		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CWriteToAsync_003Ed__0 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public JsonWriter writer;

			public CancellationToken cancellationToken;

			[Nullable(0)]
			public JArray _003C_003E4__this;

			[Nullable(new byte[] { 0, 1 })]
			public JsonConverter[] converters;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__1;

			private int _003Ci_003E5__2;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JArray jArray = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
					switch (num)
					{
					default:
						awaiter = writer.WriteStartArrayAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 0);
							_003C_003Eu__1 = awaiter;
							_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
							return;
						}
						goto IL_0088;
					case 0:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_0088;
					case 1:
						awaiter = _003C_003Eu__1;
						_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_011a;
					case 2:
						{
							awaiter = _003C_003Eu__1;
							_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
							num = (_003C_003E1__state = -1);
							break;
						}
						IL_011a:
						awaiter.GetResult();
						_003Ci_003E5__2++;
						goto IL_0133;
						IL_0088:
						awaiter.GetResult();
						_003Ci_003E5__2 = 0;
						goto IL_0133;
						IL_0133:
						if (_003Ci_003E5__2 < jArray._values.Count)
						{
							awaiter = jArray._values[_003Ci_003E5__2].WriteToAsync(writer, cancellationToken, converters).ConfigureAwait(false).GetAwaiter();
							if (!awaiter.IsCompleted)
							{
								num = (_003C_003E1__state = 1);
								_003C_003Eu__1 = awaiter;
								_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
								return;
							}
							goto IL_011a;
						}
						awaiter = writer.WriteEndArrayAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
						if (!awaiter.IsCompleted)
						{
							num = (_003C_003E1__state = 2);
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
		private struct _003CLoadAsync_003Ed__2 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			[Nullable(0)]
			public AsyncTaskMethodBuilder<JArray> _003C_003Et__builder;

			[Nullable(0)]
			public JsonReader reader;

			public CancellationToken cancellationToken;

			[Nullable(0)]
			public JsonLoadSettings settings;

			[Nullable(0)]
			private JArray _003Ca_003E5__2;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__2;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JArray result;
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
						if (reader.TokenType != JsonToken.StartArray)
						{
							throw JsonReaderException.Create(reader, "Error reading JArray from JsonReader. Current JsonReader item is not an array: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
						}
						_003Ca_003E5__2 = new JArray();
						_003Ca_003E5__2.SetLineInfo(reader as IJsonLineInfo, settings);
						awaiter = _003Ca_003E5__2.ReadTokenFromAsync(reader, settings, cancellationToken).ConfigureAwait(false).GetAwaiter();
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
							throw JsonReaderException.Create(reader, "Error reading JArray from JsonReader.");
						}
						goto IL_00ab;
					}
					awaiter.GetResult();
					result = _003Ca_003E5__2;
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003Ca_003E5__2 = null;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003Ca_003E5__2 = null;
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

		private readonly List<JToken> _values = new List<JToken>();

		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return _values;
			}
		}

		public override JTokenType Type
		{
			get
			{
				return JTokenType.Array;
			}
		}

		[Nullable(2)]
		public override JToken this[object key]
		{
			[return: Nullable(2)]
			get
			{
				ValidationUtils.ArgumentNotNull(key, "key");
				if (!(key is int))
				{
					throw new ArgumentException("Accessed JArray values with invalid key value: {0}. Int32 array index expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				return GetItem((int)key);
			}
			[param: Nullable(2)]
			set
			{
				ValidationUtils.ArgumentNotNull(key, "key");
				if (!(key is int))
				{
					throw new ArgumentException("Set JArray values with invalid key value: {0}. Int32 array index expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				SetItem((int)key, value);
			}
		}

		public JToken this[int index]
		{
			get
			{
				return GetItem(index);
			}
			set
			{
				SetItem(index, value);
			}
		}

		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		[AsyncStateMachine(typeof(_003CWriteToAsync_003Ed__0))]
		public override Task WriteToAsync(JsonWriter writer, CancellationToken cancellationToken, params JsonConverter[] converters)
		{
			_003CWriteToAsync_003Ed__0 stateMachine = default(_003CWriteToAsync_003Ed__0);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.writer = writer;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine.converters = converters;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public new static Task<JArray> LoadAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			return LoadAsync(reader, null, cancellationToken);
		}

		[AsyncStateMachine(typeof(_003CLoadAsync_003Ed__2))]
		public new static Task<JArray> LoadAsync(JsonReader reader, [Nullable(2)] JsonLoadSettings settings, CancellationToken cancellationToken = default(CancellationToken))
		{
			_003CLoadAsync_003Ed__2 stateMachine = default(_003CLoadAsync_003Ed__2);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<JArray>.Create();
			stateMachine.reader = reader;
			stateMachine.settings = settings;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public JArray()
		{
		}

		public JArray(JArray other)
			: base(other)
		{
		}

		public JArray(params object[] content)
			: this((object)content)
		{
		}

		public JArray(object content)
		{
			Add(content);
		}

		internal override bool DeepEquals(JToken node)
		{
			JArray jArray = node as JArray;
			if (jArray != null)
			{
				return ContentsEqual(jArray);
			}
			return false;
		}

		internal override JToken CloneToken()
		{
			return new JArray(this);
		}

		public new static JArray Load(JsonReader reader)
		{
			return Load(reader, null);
		}

		public new static JArray Load(JsonReader reader, [Nullable(2)] JsonLoadSettings settings)
		{
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading JArray from JsonReader.");
			}
			reader.MoveToContent();
			if (reader.TokenType != JsonToken.StartArray)
			{
				throw JsonReaderException.Create(reader, "Error reading JArray from JsonReader. Current JsonReader item is not an array: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JArray jArray = new JArray();
			jArray.SetLineInfo(reader as IJsonLineInfo, settings);
			jArray.ReadTokenFrom(reader, settings);
			return jArray;
		}

		public new static JArray Parse(string json)
		{
			return Parse(json, null);
		}

		public new static JArray Parse(string json, [Nullable(2)] JsonLoadSettings settings)
		{
			using (JsonReader jsonReader = new JsonTextReader(new StringReader(json)))
			{
				JArray result = Load(jsonReader, settings);
				while (jsonReader.Read())
				{
				}
				return result;
			}
		}

		public new static JArray FromObject(object o)
		{
			return FromObject(o, JsonSerializer.CreateDefault());
		}

		public new static JArray FromObject(object o, JsonSerializer jsonSerializer)
		{
			JToken jToken = JToken.FromObjectInternal(o, jsonSerializer);
			if (jToken.Type != JTokenType.Array)
			{
				throw new ArgumentException("Object serialized to {0}. JArray instance expected.".FormatWith(CultureInfo.InvariantCulture, jToken.Type));
			}
			return (JArray)jToken;
		}

		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WriteStartArray();
			for (int i = 0; i < _values.Count; i++)
			{
				_values[i].WriteTo(writer, converters);
			}
			writer.WriteEndArray();
		}

		[NullableContext(2)]
		internal override int IndexOfItem(JToken item)
		{
			if (item == null)
			{
				return -1;
			}
			return _values.IndexOfReference(item);
		}

		internal override void MergeItem(object content, [Nullable(2)] JsonMergeSettings settings)
		{
			IEnumerable enumerable = ((IsMultiContent(content) || content is JArray) ? ((IEnumerable)content) : null);
			if (enumerable != null)
			{
				JContainer.MergeEnumerableContent(this, enumerable, settings);
			}
		}

		public int IndexOf(JToken item)
		{
			return IndexOfItem(item);
		}

		public void Insert(int index, JToken item)
		{
			InsertItem(index, item, false);
		}

		public void RemoveAt(int index)
		{
			RemoveItemAt(index);
		}

		public IEnumerator<JToken> GetEnumerator()
		{
			return Children().GetEnumerator();
		}

		public void Add(JToken item)
		{
			Add((object)item);
		}

		public void Clear()
		{
			ClearItems();
		}

		public bool Contains(JToken item)
		{
			return ContainsItem(item);
		}

		public void CopyTo(JToken[] array, int arrayIndex)
		{
			CopyItemsTo(array, arrayIndex);
		}

		public bool Remove(JToken item)
		{
			return RemoveItem(item);
		}

		internal override int GetDeepHashCode()
		{
			return ContentsHashCode();
		}
	}
}
