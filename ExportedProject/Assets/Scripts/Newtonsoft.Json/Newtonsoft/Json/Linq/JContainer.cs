using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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
	public abstract class JContainer : JToken, IList<JToken>, ICollection<JToken>, IEnumerable<JToken>, IEnumerable, ITypedList, IBindingList, ICollection, IList, INotifyCollectionChanged
	{
		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CReadTokenFromAsync_003Ed__0 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public JsonReader reader;

			public CancellationToken cancellationToken;

			[Nullable(0)]
			public JContainer _003C_003E4__this;

			[Nullable(0)]
			public JsonLoadSettings options;

			private int _003CstartDepth_003E5__2;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter _003C_003Eu__1;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__2;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JContainer jContainer = _003C_003E4__this;
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
							goto IL_0143;
						}
						ValidationUtils.ArgumentNotNull(reader, "reader");
						_003CstartDepth_003E5__2 = reader.Depth;
						awaiter2 = reader.ReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
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
					if (!awaiter2.GetResult())
					{
						throw JsonReaderException.Create(reader, "Error reading {0} from JsonReader.".FormatWith(CultureInfo.InvariantCulture, jContainer.GetType().Name));
					}
					awaiter = jContainer.ReadContentFromAsync(reader, options, cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_003C_003E1__state = 1);
						_003C_003Eu__2 = awaiter;
						_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_0143;
					IL_0143:
					awaiter.GetResult();
					if (reader.Depth > _003CstartDepth_003E5__2)
					{
						throw JsonReaderException.Create(reader, "Unexpected end of content while loading {0}.".FormatWith(CultureInfo.InvariantCulture, jContainer.GetType().Name));
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
		private struct _003CReadContentFromAsync_003Ed__1 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			[Nullable(0)]
			public JsonReader reader;

			[Nullable(0)]
			public JContainer _003C_003E4__this;

			[Nullable(0)]
			public JsonLoadSettings settings;

			public CancellationToken cancellationToken;

			[Nullable(0)]
			private IJsonLineInfo _003ClineInfo_003E5__2;

			[Nullable(0)]
			private JContainer _003Cparent_003E5__3;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__1;

			[Nullable(0)]
			private ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter _003C_003Eu__2;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JContainer jContainer = _003C_003E4__this;
				try
				{
					ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter awaiter;
					if (num != 0)
					{
						if (num != 1)
						{
							_003ClineInfo_003E5__2 = reader as IJsonLineInfo;
							_003Cparent_003E5__3 = jContainer;
							goto IL_0033;
						}
						awaiter = _003C_003Eu__2;
						_003C_003Eu__2 = default(ConfiguredTaskAwaitable<bool>.ConfiguredTaskAwaiter);
						num = (_003C_003E1__state = -1);
						goto IL_03fc;
					}
					ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter2 = _003C_003Eu__1;
					_003C_003Eu__1 = default(ConfiguredTaskAwaitable.ConfiguredTaskAwaiter);
					num = (_003C_003E1__state = -1);
					goto IL_0365;
					IL_0393:
					awaiter = reader.ReadAsync(cancellationToken).ConfigureAwait(false).GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						num = (_003C_003E1__state = 1);
						_003C_003Eu__2 = awaiter;
						_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter, ref this);
						return;
					}
					goto IL_03fc;
					IL_03fc:
					if (awaiter.GetResult())
					{
						goto IL_0033;
					}
					goto end_IL_000e;
					IL_0033:
					JProperty jProperty = _003Cparent_003E5__3 as JProperty;
					if (jProperty == null || jProperty.Value == null)
					{
						goto IL_0069;
					}
					if (_003Cparent_003E5__3 != jContainer)
					{
						_003Cparent_003E5__3 = _003Cparent_003E5__3.Parent;
						goto IL_0069;
					}
					goto end_IL_000e;
					IL_0069:
					switch (reader.TokenType)
					{
					case JsonToken.StartArray:
					{
						JArray jArray = new JArray();
						jArray.SetLineInfo(_003ClineInfo_003E5__2, settings);
						_003Cparent_003E5__3.Add(jArray);
						_003Cparent_003E5__3 = jArray;
						goto IL_0393;
					}
					case JsonToken.EndArray:
						if (_003Cparent_003E5__3 != jContainer)
						{
							_003Cparent_003E5__3 = _003Cparent_003E5__3.Parent;
							goto IL_0393;
						}
						goto end_IL_000e;
					case JsonToken.StartObject:
					{
						JObject jObject = new JObject();
						jObject.SetLineInfo(_003ClineInfo_003E5__2, settings);
						_003Cparent_003E5__3.Add(jObject);
						_003Cparent_003E5__3 = jObject;
						goto IL_0393;
					}
					case JsonToken.EndObject:
						if (_003Cparent_003E5__3 != jContainer)
						{
							_003Cparent_003E5__3 = _003Cparent_003E5__3.Parent;
							goto IL_0393;
						}
						goto end_IL_000e;
					case JsonToken.StartConstructor:
					{
						JConstructor jConstructor = new JConstructor(reader.Value.ToString());
						jConstructor.SetLineInfo(_003ClineInfo_003E5__2, settings);
						_003Cparent_003E5__3.Add(jConstructor);
						_003Cparent_003E5__3 = jConstructor;
						goto IL_0393;
					}
					case JsonToken.EndConstructor:
						if (_003Cparent_003E5__3 != jContainer)
						{
							_003Cparent_003E5__3 = _003Cparent_003E5__3.Parent;
							goto IL_0393;
						}
						goto end_IL_000e;
					case JsonToken.Integer:
					case JsonToken.Float:
					case JsonToken.String:
					case JsonToken.Boolean:
					case JsonToken.Date:
					case JsonToken.Bytes:
					{
						JValue jValue = new JValue(reader.Value);
						jValue.SetLineInfo(_003ClineInfo_003E5__2, settings);
						_003Cparent_003E5__3.Add(jValue);
						goto IL_0393;
					}
					case JsonToken.Comment:
						if (settings != null && settings.CommentHandling == CommentHandling.Load)
						{
							JValue jValue = JValue.CreateComment(reader.Value.ToString());
							jValue.SetLineInfo(_003ClineInfo_003E5__2, settings);
							_003Cparent_003E5__3.Add(jValue);
						}
						goto IL_0393;
					case JsonToken.Null:
					{
						JValue jValue = JValue.CreateNull();
						jValue.SetLineInfo(_003ClineInfo_003E5__2, settings);
						_003Cparent_003E5__3.Add(jValue);
						goto IL_0393;
					}
					case JsonToken.Undefined:
					{
						JValue jValue = JValue.CreateUndefined();
						jValue.SetLineInfo(_003ClineInfo_003E5__2, settings);
						_003Cparent_003E5__3.Add(jValue);
						goto IL_0393;
					}
					case JsonToken.PropertyName:
						break;
					default:
						throw new InvalidOperationException("The JsonReader should not be on a token of type {0}.".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
					case JsonToken.None:
						goto IL_0393;
					}
					JProperty jProperty2 = ReadProperty(reader, settings, _003ClineInfo_003E5__2, _003Cparent_003E5__3);
					if (jProperty2 != null)
					{
						_003Cparent_003E5__3 = jProperty2;
						goto IL_0393;
					}
					awaiter2 = reader.SkipAsync().ConfigureAwait(false).GetAwaiter();
					if (!awaiter2.IsCompleted)
					{
						num = (_003C_003E1__state = 0);
						_003C_003Eu__1 = awaiter2;
						_003C_003Et__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref this);
						return;
					}
					goto IL_0365;
					IL_0365:
					awaiter2.GetResult();
					goto IL_0393;
					end_IL_000e:;
				}
				catch (Exception exception)
				{
					_003C_003E1__state = -2;
					_003ClineInfo_003E5__2 = null;
					_003Cparent_003E5__3 = null;
					_003C_003Et__builder.SetException(exception);
					return;
				}
				_003C_003E1__state = -2;
				_003ClineInfo_003E5__2 = null;
				_003Cparent_003E5__3 = null;
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

		[Nullable(2)]
		internal ListChangedEventHandler _listChanged;

		[Nullable(2)]
		internal AddingNewEventHandler _addingNew;

		[Nullable(2)]
		internal NotifyCollectionChangedEventHandler _collectionChanged;

		[Nullable(2)]
		private object _syncRoot;

		private bool _busy;

		protected abstract IList<JToken> ChildrenTokens { get; }

		public override bool HasValues
		{
			get
			{
				return ChildrenTokens.Count > 0;
			}
		}

		[Nullable(2)]
		public override JToken First
		{
			[NullableContext(2)]
			get
			{
				IList<JToken> childrenTokens = ChildrenTokens;
				if (childrenTokens.Count <= 0)
				{
					return null;
				}
				return childrenTokens[0];
			}
		}

		[Nullable(2)]
		public override JToken Last
		{
			[NullableContext(2)]
			get
			{
				IList<JToken> childrenTokens = ChildrenTokens;
				int count = childrenTokens.Count;
				if (count <= 0)
				{
					return null;
				}
				return childrenTokens[count - 1];
			}
		}

		JToken IList<JToken>.this[int index]
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

		bool ICollection<JToken>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		object IList.this[int index]
		{
			get
			{
				return GetItem(index);
			}
			set
			{
				SetItem(index, EnsureValue(value));
			}
		}

		public int Count
		{
			get
			{
				return ChildrenTokens.Count;
			}
		}

		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		object ICollection.SyncRoot
		{
			get
			{
				if (_syncRoot == null)
				{
					Interlocked.CompareExchange(ref _syncRoot, new object(), null);
				}
				return _syncRoot;
			}
		}

		bool IBindingList.AllowEdit
		{
			get
			{
				return true;
			}
		}

		bool IBindingList.AllowNew
		{
			get
			{
				return true;
			}
		}

		bool IBindingList.AllowRemove
		{
			get
			{
				return true;
			}
		}

		bool IBindingList.IsSorted
		{
			get
			{
				return false;
			}
		}

		ListSortDirection IBindingList.SortDirection
		{
			get
			{
				return ListSortDirection.Ascending;
			}
		}

		[Nullable(2)]
		PropertyDescriptor IBindingList.SortProperty
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		bool IBindingList.SupportsChangeNotification
		{
			get
			{
				return true;
			}
		}

		bool IBindingList.SupportsSearching
		{
			get
			{
				return false;
			}
		}

		bool IBindingList.SupportsSorting
		{
			get
			{
				return false;
			}
		}

		public event ListChangedEventHandler ListChanged
		{
			add
			{
				_listChanged = (ListChangedEventHandler)Delegate.Combine(_listChanged, value);
			}
			remove
			{
				_listChanged = (ListChangedEventHandler)Delegate.Remove(_listChanged, value);
			}
		}

		public event AddingNewEventHandler AddingNew
		{
			add
			{
				_addingNew = (AddingNewEventHandler)Delegate.Combine(_addingNew, value);
			}
			remove
			{
				_addingNew = (AddingNewEventHandler)Delegate.Remove(_addingNew, value);
			}
		}

		public event NotifyCollectionChangedEventHandler CollectionChanged
		{
			add
			{
				_collectionChanged = (NotifyCollectionChangedEventHandler)Delegate.Combine(_collectionChanged, value);
			}
			remove
			{
				_collectionChanged = (NotifyCollectionChangedEventHandler)Delegate.Remove(_collectionChanged, value);
			}
		}

		[AsyncStateMachine(typeof(_003CReadTokenFromAsync_003Ed__0))]
		internal Task ReadTokenFromAsync(JsonReader reader, [Nullable(2)] JsonLoadSettings options, CancellationToken cancellationToken = default(CancellationToken))
		{
			_003CReadTokenFromAsync_003Ed__0 stateMachine = default(_003CReadTokenFromAsync_003Ed__0);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.reader = reader;
			stateMachine.options = options;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		[AsyncStateMachine(typeof(_003CReadContentFromAsync_003Ed__1))]
		private Task ReadContentFromAsync(JsonReader reader, [Nullable(2)] JsonLoadSettings settings, CancellationToken cancellationToken = default(CancellationToken))
		{
			_003CReadContentFromAsync_003Ed__1 stateMachine = default(_003CReadContentFromAsync_003Ed__1);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder.Create();
			stateMachine._003C_003E4__this = this;
			stateMachine.reader = reader;
			stateMachine.settings = settings;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		internal JContainer()
		{
		}

		internal JContainer(JContainer other)
			: this()
		{
			ValidationUtils.ArgumentNotNull(other, "other");
			int num = 0;
			foreach (JToken item in (IEnumerable<JToken>)other)
			{
				TryAddInternal(num, item, false);
				num++;
			}
			CopyAnnotations(this, other);
		}

		internal void CheckReentrancy()
		{
			if (_busy)
			{
				throw new InvalidOperationException("Cannot change {0} during a collection change event.".FormatWith(CultureInfo.InvariantCulture, GetType()));
			}
		}

		internal virtual IList<JToken> CreateChildrenCollection()
		{
			return new List<JToken>();
		}

		protected virtual void OnAddingNew(AddingNewEventArgs e)
		{
			AddingNewEventHandler addingNew = _addingNew;
			if (addingNew != null)
			{
				addingNew(this, e);
			}
		}

		protected virtual void OnListChanged(ListChangedEventArgs e)
		{
			ListChangedEventHandler listChanged = _listChanged;
			if (listChanged != null)
			{
				_busy = true;
				try
				{
					listChanged(this, e);
				}
				finally
				{
					_busy = false;
				}
			}
		}

		protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			NotifyCollectionChangedEventHandler collectionChanged = _collectionChanged;
			if (collectionChanged != null)
			{
				_busy = true;
				try
				{
					collectionChanged(this, e);
				}
				finally
				{
					_busy = false;
				}
			}
		}

		internal bool ContentsEqual(JContainer container)
		{
			if (container == this)
			{
				return true;
			}
			IList<JToken> childrenTokens = ChildrenTokens;
			IList<JToken> childrenTokens2 = container.ChildrenTokens;
			if (childrenTokens.Count != childrenTokens2.Count)
			{
				return false;
			}
			for (int i = 0; i < childrenTokens.Count; i++)
			{
				if (!childrenTokens[i].DeepEquals(childrenTokens2[i]))
				{
					return false;
				}
			}
			return true;
		}

		[return: Nullable(new byte[] { 0, 1 })]
		public override JEnumerable<JToken> Children()
		{
			return new JEnumerable<JToken>(ChildrenTokens);
		}

		[NullableContext(2)]
		[return: Nullable(new byte[] { 1, 2 })]
		public override IEnumerable<T> Values<T>()
		{
			return ChildrenTokens.Convert<JToken, T>();
		}

		public IEnumerable<JToken> Descendants()
		{
			return GetDescendants(false);
		}

		public IEnumerable<JToken> DescendantsAndSelf()
		{
			return GetDescendants(true);
		}

		internal IEnumerable<JToken> GetDescendants(bool self)
		{
			if (self)
			{
				yield return this;
			}
			foreach (JToken o in ChildrenTokens)
			{
				yield return o;
				JContainer jContainer = o as JContainer;
				if (jContainer == null)
				{
					continue;
				}
				foreach (JToken item in jContainer.Descendants())
				{
					yield return item;
				}
			}
		}

		[NullableContext(2)]
		internal bool IsMultiContent([NotNullWhen(true)] object content)
		{
			if (content is IEnumerable && !(content is string) && !(content is JToken))
			{
				return !(content is byte[]);
			}
			return false;
		}

		internal JToken EnsureParentToken([Nullable(2)] JToken item, bool skipParentCheck)
		{
			if (item == null)
			{
				return JValue.CreateNull();
			}
			if (skipParentCheck)
			{
				return item;
			}
			if (item.Parent != null || item == this || (item.HasValues && base.Root == item))
			{
				item = item.CloneToken();
			}
			return item;
		}

		[NullableContext(2)]
		internal abstract int IndexOfItem(JToken item);

		[NullableContext(2)]
		internal virtual bool InsertItem(int index, JToken item, bool skipParentCheck)
		{
			IList<JToken> childrenTokens = ChildrenTokens;
			if (index > childrenTokens.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index must be within the bounds of the List.");
			}
			CheckReentrancy();
			item = EnsureParentToken(item, skipParentCheck);
			JToken jToken = ((index == 0) ? null : childrenTokens[index - 1]);
			JToken jToken2 = ((index == childrenTokens.Count) ? null : childrenTokens[index]);
			ValidateToken(item, null);
			item.Parent = this;
			item.Previous = jToken;
			if (jToken != null)
			{
				jToken.Next = item;
			}
			item.Next = jToken2;
			if (jToken2 != null)
			{
				jToken2.Previous = item;
			}
			childrenTokens.Insert(index, item);
			if (_listChanged != null)
			{
				OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
			}
			if (_collectionChanged != null)
			{
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
			}
			return true;
		}

		internal virtual void RemoveItemAt(int index)
		{
			IList<JToken> childrenTokens = ChildrenTokens;
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Index is less than 0.");
			}
			if (index >= childrenTokens.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index is equal to or greater than Count.");
			}
			CheckReentrancy();
			JToken jToken = childrenTokens[index];
			JToken jToken2 = ((index == 0) ? null : childrenTokens[index - 1]);
			JToken jToken3 = ((index == childrenTokens.Count - 1) ? null : childrenTokens[index + 1]);
			if (jToken2 != null)
			{
				jToken2.Next = jToken3;
			}
			if (jToken3 != null)
			{
				jToken3.Previous = jToken2;
			}
			jToken.Parent = null;
			jToken.Previous = null;
			jToken.Next = null;
			childrenTokens.RemoveAt(index);
			if (_listChanged != null)
			{
				OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
			}
			if (_collectionChanged != null)
			{
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, jToken, index));
			}
		}

		[NullableContext(2)]
		internal virtual bool RemoveItem(JToken item)
		{
			if (item != null)
			{
				int num = IndexOfItem(item);
				if (num >= 0)
				{
					RemoveItemAt(num);
					return true;
				}
			}
			return false;
		}

		internal virtual JToken GetItem(int index)
		{
			return ChildrenTokens[index];
		}

		[NullableContext(2)]
		internal virtual void SetItem(int index, JToken item)
		{
			IList<JToken> childrenTokens = ChildrenTokens;
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Index is less than 0.");
			}
			if (index >= childrenTokens.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index is equal to or greater than Count.");
			}
			JToken jToken = childrenTokens[index];
			if (!IsTokenUnchanged(jToken, item))
			{
				CheckReentrancy();
				item = EnsureParentToken(item, false);
				ValidateToken(item, jToken);
				JToken jToken2 = ((index == 0) ? null : childrenTokens[index - 1]);
				JToken jToken3 = ((index == childrenTokens.Count - 1) ? null : childrenTokens[index + 1]);
				item.Parent = this;
				item.Previous = jToken2;
				if (jToken2 != null)
				{
					jToken2.Next = item;
				}
				item.Next = jToken3;
				if (jToken3 != null)
				{
					jToken3.Previous = item;
				}
				childrenTokens[index] = item;
				jToken.Parent = null;
				jToken.Previous = null;
				jToken.Next = null;
				if (_listChanged != null)
				{
					OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, index));
				}
				if (_collectionChanged != null)
				{
					OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, item, jToken, index));
				}
			}
		}

		internal virtual void ClearItems()
		{
			CheckReentrancy();
			IList<JToken> childrenTokens = ChildrenTokens;
			foreach (JToken item in childrenTokens)
			{
				item.Parent = null;
				item.Previous = null;
				item.Next = null;
			}
			childrenTokens.Clear();
			if (_listChanged != null)
			{
				OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
			}
			if (_collectionChanged != null)
			{
				OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
			}
		}

		internal virtual void ReplaceItem(JToken existing, JToken replacement)
		{
			if (existing != null && existing.Parent == this)
			{
				int index = IndexOfItem(existing);
				SetItem(index, replacement);
			}
		}

		[NullableContext(2)]
		internal virtual bool ContainsItem(JToken item)
		{
			return IndexOfItem(item) != -1;
		}

		internal virtual void CopyItemsTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", "arrayIndex is less than 0.");
			}
			if (arrayIndex >= array.Length && arrayIndex != 0)
			{
				throw new ArgumentException("arrayIndex is equal to or greater than the length of array.");
			}
			if (Count > array.Length - arrayIndex)
			{
				throw new ArgumentException("The number of elements in the source JObject is greater than the available space from arrayIndex to the end of the destination array.");
			}
			int num = 0;
			foreach (JToken childrenToken in ChildrenTokens)
			{
				array.SetValue(childrenToken, arrayIndex + num);
				num++;
			}
		}

		internal static bool IsTokenUnchanged(JToken currentValue, [Nullable(2)] JToken newValue)
		{
			JValue jValue = currentValue as JValue;
			if (jValue != null)
			{
				if (newValue == null)
				{
					return jValue.Type == JTokenType.Null;
				}
				return jValue.Equals(newValue);
			}
			return false;
		}

		internal virtual void ValidateToken(JToken o, [Nullable(2)] JToken existing)
		{
			ValidationUtils.ArgumentNotNull(o, "o");
			if (o.Type == JTokenType.Property)
			{
				throw new ArgumentException("Can not add {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, o.GetType(), GetType()));
			}
		}

		[NullableContext(2)]
		public virtual void Add(object content)
		{
			TryAddInternal(ChildrenTokens.Count, content, false);
		}

		[NullableContext(2)]
		internal bool TryAdd(object content)
		{
			return TryAddInternal(ChildrenTokens.Count, content, false);
		}

		internal void AddAndSkipParentCheck(JToken token)
		{
			TryAddInternal(ChildrenTokens.Count, token, true);
		}

		[NullableContext(2)]
		public void AddFirst(object content)
		{
			TryAddInternal(0, content, false);
		}

		[NullableContext(2)]
		internal bool TryAddInternal(int index, object content, bool skipParentCheck)
		{
			if (IsMultiContent(content))
			{
				IEnumerable obj = (IEnumerable)content;
				int num = index;
				foreach (object item2 in obj)
				{
					TryAddInternal(num, item2, skipParentCheck);
					num++;
				}
				return true;
			}
			JToken item = CreateFromContent(content);
			return InsertItem(index, item, skipParentCheck);
		}

		internal static JToken CreateFromContent([Nullable(2)] object content)
		{
			JToken jToken = content as JToken;
			if (jToken != null)
			{
				return jToken;
			}
			return new JValue(content);
		}

		public JsonWriter CreateWriter()
		{
			return new JTokenWriter(this);
		}

		public void ReplaceAll(object content)
		{
			ClearItems();
			Add(content);
		}

		public void RemoveAll()
		{
			ClearItems();
		}

		internal abstract void MergeItem(object content, [Nullable(2)] JsonMergeSettings settings);

		public void Merge(object content)
		{
			MergeItem(content, null);
		}

		public void Merge(object content, [Nullable(2)] JsonMergeSettings settings)
		{
			MergeItem(content, settings);
		}

		internal void ReadTokenFrom(JsonReader reader, [Nullable(2)] JsonLoadSettings options)
		{
			int depth = reader.Depth;
			if (!reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading {0} from JsonReader.".FormatWith(CultureInfo.InvariantCulture, GetType().Name));
			}
			ReadContentFrom(reader, options);
			if (reader.Depth > depth)
			{
				throw JsonReaderException.Create(reader, "Unexpected end of content while loading {0}.".FormatWith(CultureInfo.InvariantCulture, GetType().Name));
			}
		}

		internal void ReadContentFrom(JsonReader r, [Nullable(2)] JsonLoadSettings settings)
		{
			ValidationUtils.ArgumentNotNull(r, "r");
			IJsonLineInfo lineInfo = r as IJsonLineInfo;
			JContainer jContainer = this;
			do
			{
				JProperty jProperty = jContainer as JProperty;
				if (jProperty != null && jProperty.Value != null)
				{
					if (jContainer == this)
					{
						break;
					}
					jContainer = jContainer.Parent;
				}
				switch (r.TokenType)
				{
				case JsonToken.StartArray:
				{
					JArray jArray = new JArray();
					jArray.SetLineInfo(lineInfo, settings);
					jContainer.Add(jArray);
					jContainer = jArray;
					break;
				}
				case JsonToken.EndArray:
					if (jContainer == this)
					{
						return;
					}
					jContainer = jContainer.Parent;
					break;
				case JsonToken.StartObject:
				{
					JObject jObject = new JObject();
					jObject.SetLineInfo(lineInfo, settings);
					jContainer.Add(jObject);
					jContainer = jObject;
					break;
				}
				case JsonToken.EndObject:
					if (jContainer == this)
					{
						return;
					}
					jContainer = jContainer.Parent;
					break;
				case JsonToken.StartConstructor:
				{
					JConstructor jConstructor = new JConstructor(r.Value.ToString());
					jConstructor.SetLineInfo(lineInfo, settings);
					jContainer.Add(jConstructor);
					jContainer = jConstructor;
					break;
				}
				case JsonToken.EndConstructor:
					if (jContainer == this)
					{
						return;
					}
					jContainer = jContainer.Parent;
					break;
				case JsonToken.Integer:
				case JsonToken.Float:
				case JsonToken.String:
				case JsonToken.Boolean:
				case JsonToken.Date:
				case JsonToken.Bytes:
				{
					JValue jValue = new JValue(r.Value);
					jValue.SetLineInfo(lineInfo, settings);
					jContainer.Add(jValue);
					break;
				}
				case JsonToken.Comment:
					if (settings != null && settings.CommentHandling == CommentHandling.Load)
					{
						JValue jValue = JValue.CreateComment(r.Value.ToString());
						jValue.SetLineInfo(lineInfo, settings);
						jContainer.Add(jValue);
					}
					break;
				case JsonToken.Null:
				{
					JValue jValue = JValue.CreateNull();
					jValue.SetLineInfo(lineInfo, settings);
					jContainer.Add(jValue);
					break;
				}
				case JsonToken.Undefined:
				{
					JValue jValue = JValue.CreateUndefined();
					jValue.SetLineInfo(lineInfo, settings);
					jContainer.Add(jValue);
					break;
				}
				case JsonToken.PropertyName:
				{
					JProperty jProperty2 = ReadProperty(r, settings, lineInfo, jContainer);
					if (jProperty2 != null)
					{
						jContainer = jProperty2;
					}
					else
					{
						r.Skip();
					}
					break;
				}
				default:
					throw new InvalidOperationException("The JsonReader should not be on a token of type {0}.".FormatWith(CultureInfo.InvariantCulture, r.TokenType));
				case JsonToken.None:
					break;
				}
			}
			while (r.Read());
		}

		[NullableContext(2)]
		private static JProperty ReadProperty([Nullable(1)] JsonReader r, JsonLoadSettings settings, IJsonLineInfo lineInfo, [Nullable(1)] JContainer parent)
		{
			DuplicatePropertyNameHandling duplicatePropertyNameHandling = ((settings != null) ? settings.DuplicatePropertyNameHandling : DuplicatePropertyNameHandling.Replace);
			JObject obj = (JObject)parent;
			string text = r.Value.ToString();
			JProperty jProperty = obj.Property(text, StringComparison.Ordinal);
			if (jProperty != null)
			{
				switch (duplicatePropertyNameHandling)
				{
				case DuplicatePropertyNameHandling.Ignore:
					return null;
				case DuplicatePropertyNameHandling.Error:
					throw JsonReaderException.Create(r, "Property with the name '{0}' already exists in the current JSON object.".FormatWith(CultureInfo.InvariantCulture, text));
				}
			}
			JProperty jProperty2 = new JProperty(text);
			jProperty2.SetLineInfo(lineInfo, settings);
			if (jProperty == null)
			{
				parent.Add(jProperty2);
			}
			else
			{
				jProperty.Replace(jProperty2);
			}
			return jProperty2;
		}

		internal int ContentsHashCode()
		{
			int num = 0;
			foreach (JToken childrenToken in ChildrenTokens)
			{
				num ^= childrenToken.GetDeepHashCode();
			}
			return num;
		}

		string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
		{
			return string.Empty;
		}

		[return: Nullable(2)]
		PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			ICustomTypeDescriptor obj = First as ICustomTypeDescriptor;
			if (obj == null)
			{
				return null;
			}
			return obj.GetProperties();
		}

		int IList<JToken>.IndexOf(JToken item)
		{
			return IndexOfItem(item);
		}

		void IList<JToken>.Insert(int index, JToken item)
		{
			InsertItem(index, item, false);
		}

		void IList<JToken>.RemoveAt(int index)
		{
			RemoveItemAt(index);
		}

		void ICollection<JToken>.Add(JToken item)
		{
			Add(item);
		}

		void ICollection<JToken>.Clear()
		{
			ClearItems();
		}

		bool ICollection<JToken>.Contains(JToken item)
		{
			return ContainsItem(item);
		}

		void ICollection<JToken>.CopyTo(JToken[] array, int arrayIndex)
		{
			CopyItemsTo(array, arrayIndex);
		}

		bool ICollection<JToken>.Remove(JToken item)
		{
			return RemoveItem(item);
		}

		[return: Nullable(2)]
		private JToken EnsureValue(object value)
		{
			if (value == null)
			{
				return null;
			}
			JToken jToken = value as JToken;
			if (jToken != null)
			{
				return jToken;
			}
			throw new ArgumentException("Argument is not a JToken.");
		}

		int IList.Add(object value)
		{
			Add(EnsureValue(value));
			return Count - 1;
		}

		void IList.Clear()
		{
			ClearItems();
		}

		bool IList.Contains(object value)
		{
			return ContainsItem(EnsureValue(value));
		}

		int IList.IndexOf(object value)
		{
			return IndexOfItem(EnsureValue(value));
		}

		void IList.Insert(int index, object value)
		{
			InsertItem(index, EnsureValue(value), false);
		}

		void IList.Remove(object value)
		{
			RemoveItem(EnsureValue(value));
		}

		void IList.RemoveAt(int index)
		{
			RemoveItemAt(index);
		}

		void ICollection.CopyTo(Array array, int index)
		{
			CopyItemsTo(array, index);
		}

		void IBindingList.AddIndex(PropertyDescriptor property)
		{
		}

		object IBindingList.AddNew()
		{
			AddingNewEventArgs addingNewEventArgs = new AddingNewEventArgs();
			OnAddingNew(addingNewEventArgs);
			if (addingNewEventArgs.NewObject == null)
			{
				throw new JsonException("Could not determine new value to add to '{0}'.".FormatWith(CultureInfo.InvariantCulture, GetType()));
			}
			JToken jToken = addingNewEventArgs.NewObject as JToken;
			if (jToken == null)
			{
				throw new JsonException("New item to be added to collection must be compatible with {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JToken)));
			}
			Add(jToken);
			return jToken;
		}

		void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
		{
			throw new NotSupportedException();
		}

		int IBindingList.Find(PropertyDescriptor property, object key)
		{
			throw new NotSupportedException();
		}

		void IBindingList.RemoveIndex(PropertyDescriptor property)
		{
		}

		void IBindingList.RemoveSort()
		{
			throw new NotSupportedException();
		}

		internal static void MergeEnumerableContent(JContainer target, IEnumerable content, [Nullable(2)] JsonMergeSettings settings)
		{
			switch ((settings != null) ? settings.MergeArrayHandling : MergeArrayHandling.Concat)
			{
			case MergeArrayHandling.Concat:
			{
				foreach (JToken item in content)
				{
					target.Add(item);
				}
				break;
			}
			case MergeArrayHandling.Union:
			{
				HashSet<JToken> hashSet = new HashSet<JToken>(target, JToken.EqualityComparer);
				{
					foreach (JToken item2 in content)
					{
						if (hashSet.Add(item2))
						{
							target.Add(item2);
						}
					}
					break;
				}
			}
			case MergeArrayHandling.Replace:
				if (target == content)
				{
					break;
				}
				target.ClearItems();
				{
					foreach (JToken item3 in content)
					{
						target.Add(item3);
					}
					break;
				}
			case MergeArrayHandling.Merge:
			{
				int num = 0;
				{
					foreach (object item4 in content)
					{
						if (num < target.Count)
						{
							JContainer jContainer = target[num] as JContainer;
							if (jContainer != null)
							{
								jContainer.Merge(item4, settings);
							}
							else if (item4 != null)
							{
								JToken jToken = CreateFromContent(item4);
								if (jToken.Type != JTokenType.Null)
								{
									target[num] = jToken;
								}
							}
						}
						else
						{
							target.Add(item4);
						}
						num++;
					}
					break;
				}
			}
			default:
				throw new ArgumentOutOfRangeException("settings", "Unexpected merge array handling when merging JSON.");
			}
		}
	}
}
