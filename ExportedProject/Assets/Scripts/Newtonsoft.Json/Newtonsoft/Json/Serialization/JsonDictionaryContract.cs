using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	[NullableContext(2)]
	[Nullable(0)]
	public class JsonDictionaryContract : JsonContainerContract
	{
		[CompilerGenerated]
		private readonly Type _003CDictionaryKeyType_003Ek__BackingField;

		[CompilerGenerated]
		private readonly Type _003CDictionaryValueType_003Ek__BackingField;

		private readonly Type _genericCollectionDefinitionType;

		private Type _genericWrapperType;

		[Nullable(new byte[] { 2, 1 })]
		private ObjectConstructor<object> _genericWrapperCreator;

		[Nullable(new byte[] { 2, 1 })]
		private Func<object> _genericTemporaryDictionaryCreator;

		[CompilerGenerated]
		private readonly bool _003CShouldCreateWrapper_003Ek__BackingField;

		private readonly ConstructorInfo _parameterizedConstructor;

		[Nullable(new byte[] { 2, 1 })]
		private ObjectConstructor<object> _overrideCreator;

		[Nullable(new byte[] { 2, 1 })]
		private ObjectConstructor<object> _parameterizedCreator;

		[Nullable(new byte[] { 2, 1, 1 })]
		[field: Nullable(new byte[] { 2, 1, 1 })]
		public Func<string, string> DictionaryKeyResolver
		{
			[return: Nullable(new byte[] { 2, 1, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1, 1 })]
			set;
		}

		public Type DictionaryKeyType
		{
			[CompilerGenerated]
			get
			{
				return _003CDictionaryKeyType_003Ek__BackingField;
			}
		}

		public Type DictionaryValueType
		{
			[CompilerGenerated]
			get
			{
				return _003CDictionaryValueType_003Ek__BackingField;
			}
		}

		internal JsonContract KeyContract { get; set; }

		internal bool ShouldCreateWrapper
		{
			[CompilerGenerated]
			get
			{
				return _003CShouldCreateWrapper_003Ek__BackingField;
			}
		}

		[Nullable(new byte[] { 2, 1 })]
		internal ObjectConstructor<object> ParameterizedCreator
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				if (_parameterizedCreator == null && _parameterizedConstructor != null)
				{
					_parameterizedCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(_parameterizedConstructor);
				}
				return _parameterizedCreator;
			}
		}

		[Nullable(new byte[] { 2, 1 })]
		public ObjectConstructor<object> OverrideCreator
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return _overrideCreator;
			}
			[param: Nullable(new byte[] { 2, 1 })]
			set
			{
				_overrideCreator = value;
			}
		}

		public bool HasParameterizedCreator { get; set; }

		internal bool HasParameterizedCreatorInternal
		{
			get
			{
				if (!HasParameterizedCreator && _parameterizedCreator == null)
				{
					return _parameterizedConstructor != null;
				}
				return true;
			}
		}

		[NullableContext(1)]
		public JsonDictionaryContract(Type underlyingType)
			: base(underlyingType)
		{
			ContractType = JsonContractType.Dictionary;
			Type keyType;
			Type valueType;
			if (ReflectionUtils.ImplementsGenericDefinition(NonNullableUnderlyingType, typeof(IDictionary<, >), out _genericCollectionDefinitionType))
			{
				keyType = _genericCollectionDefinitionType.GetGenericArguments()[0];
				valueType = _genericCollectionDefinitionType.GetGenericArguments()[1];
				if (ReflectionUtils.IsGenericDefinition(NonNullableUnderlyingType, typeof(IDictionary<, >)))
				{
					base.CreatedType = typeof(Dictionary<, >).MakeGenericType(keyType, valueType);
				}
				else if (NonNullableUnderlyingType.IsGenericType() && NonNullableUnderlyingType.GetGenericTypeDefinition().FullName == "System.Collections.Concurrent.ConcurrentDictionary`2")
				{
					_003CShouldCreateWrapper_003Ek__BackingField = true;
				}
				IsReadOnlyOrFixedSize = ReflectionUtils.InheritsGenericDefinition(NonNullableUnderlyingType, typeof(ReadOnlyDictionary<, >));
			}
			else if (ReflectionUtils.ImplementsGenericDefinition(NonNullableUnderlyingType, typeof(IReadOnlyDictionary<, >), out _genericCollectionDefinitionType))
			{
				keyType = _genericCollectionDefinitionType.GetGenericArguments()[0];
				valueType = _genericCollectionDefinitionType.GetGenericArguments()[1];
				if (ReflectionUtils.IsGenericDefinition(NonNullableUnderlyingType, typeof(IReadOnlyDictionary<, >)))
				{
					base.CreatedType = typeof(ReadOnlyDictionary<, >).MakeGenericType(keyType, valueType);
				}
				IsReadOnlyOrFixedSize = true;
			}
			else
			{
				ReflectionUtils.GetDictionaryKeyValueTypes(NonNullableUnderlyingType, out keyType, out valueType);
				if (NonNullableUnderlyingType == typeof(IDictionary))
				{
					base.CreatedType = typeof(Dictionary<object, object>);
				}
			}
			if (keyType != null && valueType != null)
			{
				_parameterizedConstructor = CollectionUtils.ResolveEnumerableCollectionConstructor(base.CreatedType, typeof(KeyValuePair<, >).MakeGenericType(keyType, valueType), typeof(IDictionary<, >).MakeGenericType(keyType, valueType));
				if (!HasParameterizedCreatorInternal && NonNullableUnderlyingType.Name == "FSharpMap`2")
				{
					FSharpUtils.EnsureInitialized(NonNullableUnderlyingType.Assembly());
					_parameterizedCreator = FSharpUtils.Instance.CreateMap(keyType, valueType);
				}
			}
			if (!typeof(IDictionary).IsAssignableFrom(base.CreatedType))
			{
				_003CShouldCreateWrapper_003Ek__BackingField = true;
			}
			_003CDictionaryKeyType_003Ek__BackingField = keyType;
			_003CDictionaryValueType_003Ek__BackingField = valueType;
			Type createdType;
			ObjectConstructor<object> parameterizedCreator;
			if (DictionaryKeyType != null && DictionaryValueType != null && ImmutableCollectionsUtils.TryBuildImmutableForDictionaryContract(NonNullableUnderlyingType, DictionaryKeyType, DictionaryValueType, out createdType, out parameterizedCreator))
			{
				base.CreatedType = createdType;
				_parameterizedCreator = parameterizedCreator;
				IsReadOnlyOrFixedSize = true;
			}
		}

		[NullableContext(1)]
		internal IWrappedDictionary CreateWrapper(object dictionary)
		{
			if (_genericWrapperCreator == null)
			{
				_genericWrapperType = typeof(DictionaryWrapper<, >).MakeGenericType(DictionaryKeyType, DictionaryValueType);
				ConstructorInfo constructor = _genericWrapperType.GetConstructor(new Type[1] { _genericCollectionDefinitionType });
				_genericWrapperCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(constructor);
			}
			return (IWrappedDictionary)_genericWrapperCreator(dictionary);
		}

		[NullableContext(1)]
		internal IDictionary CreateTemporaryDictionary()
		{
			if (_genericTemporaryDictionaryCreator == null)
			{
				Type type = typeof(Dictionary<, >).MakeGenericType(DictionaryKeyType ?? typeof(object), DictionaryValueType ?? typeof(object));
				_genericTemporaryDictionaryCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateDefaultConstructor<object>(type);
			}
			return (IDictionary)_genericTemporaryDictionaryCreator();
		}
	}
}
