using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	[NullableContext(1)]
	[Nullable(0)]
	internal static class JsonTypeReflector
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass22_0
		{
			[Nullable(0)]
			public Type type;

			[Nullable(new byte[] { 0, 1 })]
			public Func<object> defaultConstructor;

			internal object _003CGetCreator_003Eb__0([Nullable(new byte[] { 2, 1 })] object[] parameters)
			{
				try
				{
					if (parameters != null)
					{
						Type[] types = parameters.Select(_003C_003Ec._003C_003E9__22_1 ?? (_003C_003Ec._003C_003E9__22_1 = _003C_003Ec._003C_003E9._003CGetCreator_003Eb__22_1)).ToArray();
						ConstructorInfo constructor = type.GetConstructor(types);
						if (constructor != null)
						{
							return ReflectionDelegateFactory.CreateParameterizedConstructor(constructor)(parameters);
						}
						throw new JsonException("No matching parameterized constructor found for '{0}'.".FormatWith(CultureInfo.InvariantCulture, type));
					}
					if (defaultConstructor == null)
					{
						throw new JsonException("No parameterless constructor defined for '{0}'.".FormatWith(CultureInfo.InvariantCulture, type));
					}
					return defaultConstructor();
				}
				catch (Exception innerException)
				{
					throw new JsonException("Error creating '{0}'.".FormatWith(CultureInfo.InvariantCulture, type), innerException);
				}
			}
		}

		[Serializable]
		[CompilerGenerated]
		private sealed class _003C_003Ec
		{
			[Nullable(0)]
			public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

			[Nullable(0)]
			public static Func<object, Type> _003C_003E9__22_1;

			[NullableContext(0)]
			internal Type _003CGetCreator_003Eb__22_1(object param)
			{
				if (param == null)
				{
					throw new InvalidOperationException("Cannot pass a null parameter to the constructor.");
				}
				return param.GetType();
			}
		}

		private static bool? _dynamicCodeGeneration;

		private static bool? _fullyTrusted;

		public const string IdPropertyName = "$id";

		public const string RefPropertyName = "$ref";

		public const string TypePropertyName = "$type";

		public const string ValuePropertyName = "$value";

		public const string ArrayValuesPropertyName = "$values";

		public const string ShouldSerializePrefix = "ShouldSerialize";

		public const string SpecifiedPostfix = "Specified";

		public const string ConcurrentDictionaryTypeName = "System.Collections.Concurrent.ConcurrentDictionary`2";

		[Nullable(new byte[] { 1, 1, 1, 2, 1, 1 })]
		private static readonly ThreadSafeStore<Type, Func<object[], object>> CreatorCache = new ThreadSafeStore<Type, Func<object[], object>>(GetCreator);

		[Nullable(new byte[] { 1, 1, 2 })]
		private static readonly ThreadSafeStore<Type, Type> AssociatedMetadataTypesCache = new ThreadSafeStore<Type, Type>(GetAssociateMetadataTypeFromAttribute);

		[Nullable(2)]
		private static ReflectionObject _metadataTypeAttributeReflectionObject;

		public static bool DynamicCodeGeneration
		{
			[SecuritySafeCritical]
			get
			{
				if (!_dynamicCodeGeneration.HasValue)
				{
					_dynamicCodeGeneration = false;
				}
				return _dynamicCodeGeneration.GetValueOrDefault();
			}
		}

		public static bool FullyTrusted
		{
			get
			{
				if (!_fullyTrusted.HasValue)
				{
					AppDomain currentDomain = AppDomain.CurrentDomain;
					_fullyTrusted = currentDomain.IsHomogenous && currentDomain.IsFullyTrusted;
				}
				return _fullyTrusted.GetValueOrDefault();
			}
		}

		public static ReflectionDelegateFactory ReflectionDelegateFactory
		{
			get
			{
				return LateBoundReflectionDelegateFactory.Instance;
			}
		}

		[return: Nullable(2)]
		public static T GetCachedAttribute<[Nullable(0)] T>(object attributeProvider) where T : Attribute
		{
			return CachedAttributeGetter<T>.GetAttribute(attributeProvider);
		}

		public static bool CanTypeDescriptorConvertString(Type type, out TypeConverter typeConverter)
		{
			typeConverter = TypeDescriptor.GetConverter(type);
			if (typeConverter != null)
			{
				Type type2 = typeConverter.GetType();
				if (!string.Equals(type2.FullName, "System.ComponentModel.ComponentConverter", StringComparison.Ordinal) && !string.Equals(type2.FullName, "System.ComponentModel.ReferenceConverter", StringComparison.Ordinal) && !string.Equals(type2.FullName, "System.Windows.Forms.Design.DataSourceConverter", StringComparison.Ordinal) && type2 != typeof(TypeConverter))
				{
					return typeConverter.CanConvertTo(typeof(string));
				}
			}
			return false;
		}

		[return: Nullable(2)]
		public static DataContractAttribute GetDataContractAttribute(Type type)
		{
			Type type2 = type;
			while (type2 != null)
			{
				DataContractAttribute attribute = CachedAttributeGetter<DataContractAttribute>.GetAttribute(type2);
				if (attribute != null)
				{
					return attribute;
				}
				type2 = type2.BaseType();
			}
			return null;
		}

		[return: Nullable(2)]
		public static DataMemberAttribute GetDataMemberAttribute(MemberInfo memberInfo)
		{
			if (memberInfo.MemberType() == MemberTypes.Field)
			{
				return CachedAttributeGetter<DataMemberAttribute>.GetAttribute(memberInfo);
			}
			PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
			DataMemberAttribute attribute = CachedAttributeGetter<DataMemberAttribute>.GetAttribute(propertyInfo);
			if (attribute == null && propertyInfo.IsVirtual())
			{
				Type type = propertyInfo.DeclaringType;
				while (attribute == null && type != null)
				{
					PropertyInfo propertyInfo2 = (PropertyInfo)ReflectionUtils.GetMemberInfoFromType(type, propertyInfo);
					if (propertyInfo2 != null && propertyInfo2.IsVirtual())
					{
						attribute = CachedAttributeGetter<DataMemberAttribute>.GetAttribute(propertyInfo2);
					}
					type = type.BaseType();
				}
			}
			return attribute;
		}

		public static MemberSerialization GetObjectMemberSerialization(Type objectType, bool ignoreSerializableAttribute)
		{
			JsonObjectAttribute cachedAttribute = GetCachedAttribute<JsonObjectAttribute>(objectType);
			if (cachedAttribute != null)
			{
				return cachedAttribute.MemberSerialization;
			}
			if (GetDataContractAttribute(objectType) != null)
			{
				return MemberSerialization.OptIn;
			}
			if (!ignoreSerializableAttribute && IsSerializable(objectType))
			{
				return MemberSerialization.Fields;
			}
			return MemberSerialization.OptOut;
		}

		[return: Nullable(2)]
		public static JsonConverter GetJsonConverter(object attributeProvider)
		{
			JsonConverterAttribute cachedAttribute = GetCachedAttribute<JsonConverterAttribute>(attributeProvider);
			if (cachedAttribute != null)
			{
				Func<object[], object> func = CreatorCache.Get(cachedAttribute.ConverterType);
				if (func != null)
				{
					return (JsonConverter)func(cachedAttribute.ConverterParameters);
				}
			}
			return null;
		}

		public static JsonConverter CreateJsonConverterInstance(Type converterType, [Nullable(new byte[] { 2, 1 })] object[] args)
		{
			return (JsonConverter)CreatorCache.Get(converterType)(args);
		}

		public static NamingStrategy CreateNamingStrategyInstance(Type namingStrategyType, [Nullable(new byte[] { 2, 1 })] object[] args)
		{
			return (NamingStrategy)CreatorCache.Get(namingStrategyType)(args);
		}

		[return: Nullable(2)]
		public static NamingStrategy GetContainerNamingStrategy(JsonContainerAttribute containerAttribute)
		{
			if (containerAttribute.NamingStrategyInstance == null)
			{
				if (containerAttribute.NamingStrategyType == null)
				{
					return null;
				}
				containerAttribute.NamingStrategyInstance = CreateNamingStrategyInstance(containerAttribute.NamingStrategyType, containerAttribute.NamingStrategyParameters);
			}
			return containerAttribute.NamingStrategyInstance;
		}

		[return: Nullable(new byte[] { 1, 2, 1, 1 })]
		private static Func<object[], object> GetCreator(Type type)
		{
			_003C_003Ec__DisplayClass22_0 _003C_003Ec__DisplayClass22_ = new _003C_003Ec__DisplayClass22_0();
			_003C_003Ec__DisplayClass22_.type = type;
			_003C_003Ec__DisplayClass22_.defaultConstructor = (ReflectionUtils.HasDefaultConstructor(_003C_003Ec__DisplayClass22_.type, false) ? ReflectionDelegateFactory.CreateDefaultConstructor<object>(_003C_003Ec__DisplayClass22_.type) : null);
			return _003C_003Ec__DisplayClass22_._003CGetCreator_003Eb__0;
		}

		[return: Nullable(2)]
		private static Type GetAssociatedMetadataType(Type type)
		{
			return AssociatedMetadataTypesCache.Get(type);
		}

		[return: Nullable(2)]
		private static Type GetAssociateMetadataTypeFromAttribute(Type type)
		{
			Attribute[] attributes = ReflectionUtils.GetAttributes(type, null, true);
			foreach (Attribute attribute in attributes)
			{
				Type type2 = attribute.GetType();
				if (string.Equals(type2.FullName, "System.ComponentModel.DataAnnotations.MetadataTypeAttribute", StringComparison.Ordinal))
				{
					if (_metadataTypeAttributeReflectionObject == null)
					{
						_metadataTypeAttributeReflectionObject = ReflectionObject.Create(type2, "MetadataClassType");
					}
					return (Type)_metadataTypeAttributeReflectionObject.GetValue(attribute, "MetadataClassType");
				}
			}
			return null;
		}

		[return: Nullable(2)]
		private static T GetAttribute<[Nullable(0)] T>(Type type) where T : Attribute
		{
			Type associatedMetadataType = GetAssociatedMetadataType(type);
			T attribute;
			if (associatedMetadataType != null)
			{
				attribute = ReflectionUtils.GetAttribute<T>(associatedMetadataType, true);
				if (attribute != null)
				{
					return attribute;
				}
			}
			attribute = ReflectionUtils.GetAttribute<T>(type, true);
			if (attribute != null)
			{
				return attribute;
			}
			Type[] interfaces = type.GetInterfaces();
			for (int i = 0; i < interfaces.Length; i++)
			{
				attribute = ReflectionUtils.GetAttribute<T>(interfaces[i], true);
				if (attribute != null)
				{
					return attribute;
				}
			}
			return null;
		}

		[return: Nullable(2)]
		private static T GetAttribute<[Nullable(0)] T>(MemberInfo memberInfo) where T : Attribute
		{
			Type associatedMetadataType = GetAssociatedMetadataType(memberInfo.DeclaringType);
			T attribute;
			if (associatedMetadataType != null)
			{
				MemberInfo memberInfoFromType = ReflectionUtils.GetMemberInfoFromType(associatedMetadataType, memberInfo);
				if (memberInfoFromType != null)
				{
					attribute = ReflectionUtils.GetAttribute<T>(memberInfoFromType, true);
					if (attribute != null)
					{
						return attribute;
					}
				}
			}
			attribute = ReflectionUtils.GetAttribute<T>(memberInfo, true);
			if (attribute != null)
			{
				return attribute;
			}
			if (memberInfo.DeclaringType != null)
			{
				Type[] interfaces = memberInfo.DeclaringType.GetInterfaces();
				for (int i = 0; i < interfaces.Length; i++)
				{
					MemberInfo memberInfoFromType2 = ReflectionUtils.GetMemberInfoFromType(interfaces[i], memberInfo);
					if (memberInfoFromType2 != null)
					{
						attribute = ReflectionUtils.GetAttribute<T>(memberInfoFromType2, true);
						if (attribute != null)
						{
							return attribute;
						}
					}
				}
			}
			return null;
		}

		public static bool IsNonSerializable(object provider)
		{
			return ReflectionUtils.GetAttribute<NonSerializedAttribute>(provider, false) != null;
		}

		public static bool IsSerializable(object provider)
		{
			return ReflectionUtils.GetAttribute<SerializableAttribute>(provider, false) != null;
		}

		[return: Nullable(2)]
		public static T GetAttribute<[Nullable(0)] T>(object provider) where T : Attribute
		{
			Type type = provider as Type;
			if ((object)type != null)
			{
				return GetAttribute<T>(type);
			}
			MemberInfo memberInfo = provider as MemberInfo;
			if ((object)memberInfo != null)
			{
				return GetAttribute<T>(memberInfo);
			}
			return ReflectionUtils.GetAttribute<T>(provider, true);
		}
	}
}
