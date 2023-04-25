using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ReflectionUtils
	{
		[Serializable]
		[CompilerGenerated]
		private sealed class _003C_003Ec
		{
			[Nullable(0)]
			public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

			[Nullable(0)]
			public static Func<ConstructorInfo, bool> _003C_003E9__11_0;

			[Nullable(0)]
			public static Func<MemberInfo, string> _003C_003E9__31_0;

			[Nullable(0)]
			public static Func<ParameterInfo, Type> _003C_003E9__39_0;

			[Nullable(0)]
			public static Func<FieldInfo, bool> _003C_003E9__41_0;

			[NullableContext(0)]
			internal bool _003CGetDefaultConstructor_003Eb__11_0(ConstructorInfo c)
			{
				return !c.GetParameters().Any();
			}

			[NullableContext(0)]
			internal string _003CGetFieldsAndProperties_003Eb__31_0(MemberInfo m)
			{
				return m.Name;
			}

			[NullableContext(0)]
			internal Type _003CGetMemberInfoFromType_003Eb__39_0(ParameterInfo p)
			{
				return p.ParameterType;
			}

			[NullableContext(0)]
			internal bool _003CGetChildPrivateFields_003Eb__41_0(FieldInfo f)
			{
				return f.IsPrivate;
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass31_0
		{
			[Nullable(0)]
			public MemberInfo memberInfo;

			[NullableContext(0)]
			internal bool _003CGetFieldsAndProperties_003Eb__1(MemberInfo m)
			{
				return m.DeclaringType == memberInfo.DeclaringType;
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass44_0
		{
			[Nullable(0)]
			public PropertyInfo subTypeProperty;

			[NullableContext(0)]
			internal bool _003CGetChildPrivateProperties_003Eb__0(PropertyInfo p)
			{
				return p.Name == subTypeProperty.Name;
			}

			[NullableContext(0)]
			internal bool _003CGetChildPrivateProperties_003Eb__1(PropertyInfo p)
			{
				if (p.Name == subTypeProperty.Name)
				{
					return p.DeclaringType == subTypeProperty.DeclaringType;
				}
				return false;
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass44_1
		{
			[Nullable(0)]
			public Type subTypePropertyDeclaringType;

			[Nullable(0)]
			public _003C_003Ec__DisplayClass44_0 CS_0024_003C_003E8__locals1;

			[NullableContext(0)]
			internal bool _003CGetChildPrivateProperties_003Eb__2(PropertyInfo p)
			{
				if (p.Name == CS_0024_003C_003E8__locals1.subTypeProperty.Name && p.IsVirtual())
				{
					MethodInfo baseDefinition = p.GetBaseDefinition();
					return ((((object)baseDefinition != null) ? baseDefinition.DeclaringType : null) ?? p.DeclaringType).IsAssignableFrom(subTypePropertyDeclaringType);
				}
				return false;
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass45_0
		{
			[Nullable(0)]
			public string method;

			[Nullable(0)]
			public Type methodDeclaringType;

			[NullableContext(0)]
			internal bool _003CIsMethodOverridden_003Eb__0(MethodInfo info)
			{
				if (info.Name == method && info.DeclaringType != methodDeclaringType)
				{
					return info.GetBaseDefinition().DeclaringType == methodDeclaringType;
				}
				return false;
			}
		}

		public static readonly Type[] EmptyTypes;

		static ReflectionUtils()
		{
			EmptyTypes = Type.EmptyTypes;
		}

		public static bool IsVirtual(this PropertyInfo propertyInfo)
		{
			ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");
			MethodInfo getMethod = propertyInfo.GetGetMethod(true);
			if (getMethod != null && getMethod.IsVirtual)
			{
				return true;
			}
			getMethod = propertyInfo.GetSetMethod(true);
			if (getMethod != null && getMethod.IsVirtual)
			{
				return true;
			}
			return false;
		}

		[return: Nullable(2)]
		public static MethodInfo GetBaseDefinition(this PropertyInfo propertyInfo)
		{
			ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");
			MethodInfo getMethod = propertyInfo.GetGetMethod(true);
			if (getMethod != null)
			{
				return getMethod.GetBaseDefinition();
			}
			MethodInfo setMethod = propertyInfo.GetSetMethod(true);
			if ((object)setMethod == null)
			{
				return null;
			}
			return setMethod.GetBaseDefinition();
		}

		public static bool IsPublic(PropertyInfo property)
		{
			MethodInfo getMethod = property.GetGetMethod();
			if (getMethod != null && getMethod.IsPublic)
			{
				return true;
			}
			MethodInfo setMethod = property.GetSetMethod();
			if (setMethod != null && setMethod.IsPublic)
			{
				return true;
			}
			return false;
		}

		[NullableContext(2)]
		public static Type GetObjectType(object v)
		{
			if (v == null)
			{
				return null;
			}
			return v.GetType();
		}

		public static string GetTypeName(Type t, TypeNameAssemblyFormatHandling assemblyFormat, [Nullable(2)] ISerializationBinder binder)
		{
			string fullyQualifiedTypeName = GetFullyQualifiedTypeName(t, binder);
			switch (assemblyFormat)
			{
			case TypeNameAssemblyFormatHandling.Simple:
				return RemoveAssemblyDetails(fullyQualifiedTypeName);
			case TypeNameAssemblyFormatHandling.Full:
				return fullyQualifiedTypeName;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		private static string GetFullyQualifiedTypeName(Type t, [Nullable(2)] ISerializationBinder binder)
		{
			if (binder != null)
			{
				string assemblyName;
				string typeName;
				binder.BindToName(t, out assemblyName, out typeName);
				return typeName + ((assemblyName == null) ? "" : (", " + assemblyName));
			}
			return t.AssemblyQualifiedName;
		}

		private static string RemoveAssemblyDetails(string fullyQualifiedTypeName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			bool flag2 = false;
			foreach (char c in fullyQualifiedTypeName)
			{
				switch (c)
				{
				case '[':
				case ']':
					flag = false;
					flag2 = false;
					stringBuilder.Append(c);
					break;
				case ',':
					if (!flag)
					{
						flag = true;
						stringBuilder.Append(c);
					}
					else
					{
						flag2 = true;
					}
					break;
				default:
					if (!flag2)
					{
						stringBuilder.Append(c);
					}
					break;
				}
			}
			return stringBuilder.ToString();
		}

		public static bool HasDefaultConstructor(Type t, bool nonPublic)
		{
			ValidationUtils.ArgumentNotNull(t, "t");
			if (t.IsValueType())
			{
				return true;
			}
			return GetDefaultConstructor(t, nonPublic) != null;
		}

		public static ConstructorInfo GetDefaultConstructor(Type t)
		{
			return GetDefaultConstructor(t, false);
		}

		public static ConstructorInfo GetDefaultConstructor(Type t, bool nonPublic)
		{
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;
			if (nonPublic)
			{
				bindingFlags |= BindingFlags.NonPublic;
			}
			return t.GetConstructors(bindingFlags).SingleOrDefault(_003C_003Ec._003C_003E9__11_0 ?? (_003C_003Ec._003C_003E9__11_0 = _003C_003Ec._003C_003E9._003CGetDefaultConstructor_003Eb__11_0));
		}

		public static bool IsNullable(Type t)
		{
			ValidationUtils.ArgumentNotNull(t, "t");
			if (t.IsValueType())
			{
				return IsNullableType(t);
			}
			return true;
		}

		public static bool IsNullableType(Type t)
		{
			ValidationUtils.ArgumentNotNull(t, "t");
			if (t.IsGenericType())
			{
				return t.GetGenericTypeDefinition() == typeof(Nullable<>);
			}
			return false;
		}

		public static Type EnsureNotNullableType(Type t)
		{
			if (!IsNullableType(t))
			{
				return t;
			}
			return Nullable.GetUnderlyingType(t);
		}

		public static Type EnsureNotByRefType(Type t)
		{
			if (!t.IsByRef || !t.HasElementType)
			{
				return t;
			}
			return t.GetElementType();
		}

		public static bool IsGenericDefinition(Type type, Type genericInterfaceDefinition)
		{
			if (!type.IsGenericType())
			{
				return false;
			}
			return type.GetGenericTypeDefinition() == genericInterfaceDefinition;
		}

		public static bool ImplementsGenericDefinition(Type type, Type genericInterfaceDefinition)
		{
			Type implementingType;
			return ImplementsGenericDefinition(type, genericInterfaceDefinition, out implementingType);
		}

		public static bool ImplementsGenericDefinition(Type type, Type genericInterfaceDefinition, [Nullable(2)][NotNullWhen(true)] out Type implementingType)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			ValidationUtils.ArgumentNotNull(genericInterfaceDefinition, "genericInterfaceDefinition");
			if (!genericInterfaceDefinition.IsInterface() || !genericInterfaceDefinition.IsGenericTypeDefinition())
			{
				throw new ArgumentNullException("'{0}' is not a generic interface definition.".FormatWith(CultureInfo.InvariantCulture, genericInterfaceDefinition));
			}
			if (type.IsInterface() && type.IsGenericType())
			{
				Type genericTypeDefinition = type.GetGenericTypeDefinition();
				if (genericInterfaceDefinition == genericTypeDefinition)
				{
					implementingType = type;
					return true;
				}
			}
			Type[] interfaces = type.GetInterfaces();
			foreach (Type type2 in interfaces)
			{
				if (type2.IsGenericType())
				{
					Type genericTypeDefinition2 = type2.GetGenericTypeDefinition();
					if (genericInterfaceDefinition == genericTypeDefinition2)
					{
						implementingType = type2;
						return true;
					}
				}
			}
			implementingType = null;
			return false;
		}

		public static bool InheritsGenericDefinition(Type type, Type genericClassDefinition)
		{
			Type implementingType;
			return InheritsGenericDefinition(type, genericClassDefinition, out implementingType);
		}

		public static bool InheritsGenericDefinition(Type type, Type genericClassDefinition, [Nullable(2)] out Type implementingType)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			ValidationUtils.ArgumentNotNull(genericClassDefinition, "genericClassDefinition");
			if (!genericClassDefinition.IsClass() || !genericClassDefinition.IsGenericTypeDefinition())
			{
				throw new ArgumentNullException("'{0}' is not a generic class definition.".FormatWith(CultureInfo.InvariantCulture, genericClassDefinition));
			}
			return InheritsGenericDefinitionInternal(type, genericClassDefinition, out implementingType);
		}

		private static bool InheritsGenericDefinitionInternal(Type currentType, Type genericClassDefinition, [Nullable(2)] out Type implementingType)
		{
			do
			{
				if (currentType.IsGenericType() && genericClassDefinition == currentType.GetGenericTypeDefinition())
				{
					implementingType = currentType;
					return true;
				}
				currentType = currentType.BaseType();
			}
			while (currentType != null);
			implementingType = null;
			return false;
		}

		[return: Nullable(2)]
		public static Type GetCollectionItemType(Type type)
		{
			ValidationUtils.ArgumentNotNull(type, "type");
			if (type.IsArray)
			{
				return type.GetElementType();
			}
			Type implementingType;
			if (ImplementsGenericDefinition(type, typeof(IEnumerable<>), out implementingType))
			{
				if (implementingType.IsGenericTypeDefinition())
				{
					throw new Exception("Type {0} is not a collection.".FormatWith(CultureInfo.InvariantCulture, type));
				}
				return implementingType.GetGenericArguments()[0];
			}
			if (typeof(IEnumerable).IsAssignableFrom(type))
			{
				return null;
			}
			throw new Exception("Type {0} is not a collection.".FormatWith(CultureInfo.InvariantCulture, type));
		}

		[NullableContext(2)]
		public static void GetDictionaryKeyValueTypes([Nullable(1)] Type dictionaryType, out Type keyType, out Type valueType)
		{
			ValidationUtils.ArgumentNotNull(dictionaryType, "dictionaryType");
			Type implementingType;
			if (ImplementsGenericDefinition(dictionaryType, typeof(IDictionary<, >), out implementingType))
			{
				if (implementingType.IsGenericTypeDefinition())
				{
					throw new Exception("Type {0} is not a dictionary.".FormatWith(CultureInfo.InvariantCulture, dictionaryType));
				}
				Type[] genericArguments = implementingType.GetGenericArguments();
				keyType = genericArguments[0];
				valueType = genericArguments[1];
			}
			else
			{
				if (!typeof(IDictionary).IsAssignableFrom(dictionaryType))
				{
					throw new Exception("Type {0} is not a dictionary.".FormatWith(CultureInfo.InvariantCulture, dictionaryType));
				}
				keyType = null;
				valueType = null;
			}
		}

		public static Type GetMemberUnderlyingType(MemberInfo member)
		{
			ValidationUtils.ArgumentNotNull(member, "member");
			switch (member.MemberType())
			{
			case MemberTypes.Field:
				return ((FieldInfo)member).FieldType;
			case MemberTypes.Property:
				return ((PropertyInfo)member).PropertyType;
			case MemberTypes.Event:
				return ((EventInfo)member).EventHandlerType;
			case MemberTypes.Method:
				return ((MethodInfo)member).ReturnType;
			default:
				throw new ArgumentException("MemberInfo must be of type FieldInfo, PropertyInfo, EventInfo or MethodInfo", "member");
			}
		}

		public static bool IsByRefLikeType(Type type)
		{
			if (!type.IsValueType())
			{
				return false;
			}
			Attribute[] attributes = GetAttributes(type, null, false);
			for (int i = 0; i < attributes.Length; i++)
			{
				if (string.Equals(attributes[i].GetType().FullName, "System.Runtime.CompilerServices.IsByRefLikeAttribute", StringComparison.Ordinal))
				{
					return true;
				}
			}
			return false;
		}

		public static bool IsIndexedProperty(PropertyInfo property)
		{
			ValidationUtils.ArgumentNotNull(property, "property");
			return property.GetIndexParameters().Length != 0;
		}

		public static object GetMemberValue(MemberInfo member, object target)
		{
			ValidationUtils.ArgumentNotNull(member, "member");
			ValidationUtils.ArgumentNotNull(target, "target");
			switch (member.MemberType())
			{
			case MemberTypes.Field:
				return ((FieldInfo)member).GetValue(target);
			case MemberTypes.Property:
				try
				{
					return ((PropertyInfo)member).GetValue(target, null);
				}
				catch (TargetParameterCountException innerException)
				{
					throw new ArgumentException("MemberInfo '{0}' has index parameters".FormatWith(CultureInfo.InvariantCulture, member.Name), innerException);
				}
			default:
				throw new ArgumentException("MemberInfo '{0}' is not of type FieldInfo or PropertyInfo".FormatWith(CultureInfo.InvariantCulture, member.Name), "member");
			}
		}

		public static void SetMemberValue(MemberInfo member, object target, [Nullable(2)] object value)
		{
			ValidationUtils.ArgumentNotNull(member, "member");
			ValidationUtils.ArgumentNotNull(target, "target");
			switch (member.MemberType())
			{
			case MemberTypes.Field:
				((FieldInfo)member).SetValue(target, value);
				break;
			case MemberTypes.Property:
				((PropertyInfo)member).SetValue(target, value, null);
				break;
			default:
				throw new ArgumentException("MemberInfo '{0}' must be of type FieldInfo or PropertyInfo".FormatWith(CultureInfo.InvariantCulture, member.Name), "member");
			}
		}

		public static bool CanReadMemberValue(MemberInfo member, bool nonPublic)
		{
			switch (member.MemberType())
			{
			case MemberTypes.Field:
			{
				FieldInfo fieldInfo = (FieldInfo)member;
				if (nonPublic)
				{
					return true;
				}
				if (fieldInfo.IsPublic)
				{
					return true;
				}
				return false;
			}
			case MemberTypes.Property:
			{
				PropertyInfo propertyInfo = (PropertyInfo)member;
				if (!propertyInfo.CanRead)
				{
					return false;
				}
				if (nonPublic)
				{
					return true;
				}
				return propertyInfo.GetGetMethod(nonPublic) != null;
			}
			default:
				return false;
			}
		}

		public static bool CanSetMemberValue(MemberInfo member, bool nonPublic, bool canSetReadOnly)
		{
			switch (member.MemberType())
			{
			case MemberTypes.Field:
			{
				FieldInfo fieldInfo = (FieldInfo)member;
				if (fieldInfo.IsLiteral)
				{
					return false;
				}
				if (fieldInfo.IsInitOnly && !canSetReadOnly)
				{
					return false;
				}
				if (nonPublic)
				{
					return true;
				}
				if (fieldInfo.IsPublic)
				{
					return true;
				}
				return false;
			}
			case MemberTypes.Property:
			{
				PropertyInfo propertyInfo = (PropertyInfo)member;
				if (!propertyInfo.CanWrite)
				{
					return false;
				}
				if (nonPublic)
				{
					return true;
				}
				return propertyInfo.GetSetMethod(nonPublic) != null;
			}
			default:
				return false;
			}
		}

		public static List<MemberInfo> GetFieldsAndProperties(Type type, BindingFlags bindingAttr)
		{
			List<MemberInfo> list = new List<MemberInfo>();
			list.AddRange(GetFields(type, bindingAttr));
			list.AddRange(GetProperties(type, bindingAttr));
			List<MemberInfo> list2 = new List<MemberInfo>(list.Count);
			foreach (IGrouping<string, MemberInfo> item in list.GroupBy(_003C_003Ec._003C_003E9__31_0 ?? (_003C_003Ec._003C_003E9__31_0 = _003C_003Ec._003C_003E9._003CGetFieldsAndProperties_003Eb__31_0)))
			{
				if (item.Count() == 1)
				{
					list2.Add(item.First());
					continue;
				}
				List<MemberInfo> list3 = new List<MemberInfo>();
				using (IEnumerator<MemberInfo> enumerator2 = item.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						_003C_003Ec__DisplayClass31_0 _003C_003Ec__DisplayClass31_ = new _003C_003Ec__DisplayClass31_0();
						_003C_003Ec__DisplayClass31_.memberInfo = enumerator2.Current;
						if (list3.Count == 0)
						{
							list3.Add(_003C_003Ec__DisplayClass31_.memberInfo);
						}
						else if ((!IsOverridenGenericMember(_003C_003Ec__DisplayClass31_.memberInfo, bindingAttr) || _003C_003Ec__DisplayClass31_.memberInfo.Name == "Item") && !list3.Any(_003C_003Ec__DisplayClass31_._003CGetFieldsAndProperties_003Eb__1))
						{
							list3.Add(_003C_003Ec__DisplayClass31_.memberInfo);
						}
					}
				}
				list2.AddRange(list3);
			}
			return list2;
		}

		private static bool IsOverridenGenericMember(MemberInfo memberInfo, BindingFlags bindingAttr)
		{
			if (memberInfo.MemberType() != MemberTypes.Property)
			{
				return false;
			}
			PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
			if (!propertyInfo.IsVirtual())
			{
				return false;
			}
			Type declaringType = propertyInfo.DeclaringType;
			if (!declaringType.IsGenericType())
			{
				return false;
			}
			Type genericTypeDefinition = declaringType.GetGenericTypeDefinition();
			if (genericTypeDefinition == null)
			{
				return false;
			}
			MemberInfo[] member = genericTypeDefinition.GetMember(propertyInfo.Name, bindingAttr);
			if (member.Length == 0)
			{
				return false;
			}
			if (!GetMemberUnderlyingType(member[0]).IsGenericParameter)
			{
				return false;
			}
			return true;
		}

		[return: Nullable(2)]
		public static T GetAttribute<[Nullable(0)] T>(object attributeProvider) where T : Attribute
		{
			return GetAttribute<T>(attributeProvider, true);
		}

		[return: Nullable(2)]
		public static T GetAttribute<[Nullable(0)] T>(object attributeProvider, bool inherit) where T : Attribute
		{
			T[] attributes = GetAttributes<T>(attributeProvider, inherit);
			if (attributes == null)
			{
				return null;
			}
			return attributes.FirstOrDefault();
		}

		public static T[] GetAttributes<[Nullable(0)] T>(object attributeProvider, bool inherit) where T : Attribute
		{
			Attribute[] attributes = GetAttributes(attributeProvider, typeof(T), inherit);
			T[] array = attributes as T[];
			if (array != null)
			{
				return array;
			}
			return attributes.Cast<T>().ToArray();
		}

		public static Attribute[] GetAttributes(object attributeProvider, [Nullable(2)] Type attributeType, bool inherit)
		{
			ValidationUtils.ArgumentNotNull(attributeProvider, "attributeProvider");
			Type type = attributeProvider as Type;
			if ((object)type == null)
			{
				Assembly assembly = attributeProvider as Assembly;
				if ((object)assembly == null)
				{
					MemberInfo memberInfo = attributeProvider as MemberInfo;
					if ((object)memberInfo == null)
					{
						Module module = attributeProvider as Module;
						if ((object)module == null)
						{
							ParameterInfo parameterInfo = attributeProvider as ParameterInfo;
							if (parameterInfo != null)
							{
								if (!(attributeType != null))
								{
									return Attribute.GetCustomAttributes(parameterInfo, inherit);
								}
								return Attribute.GetCustomAttributes(parameterInfo, attributeType, inherit);
							}
							ICustomAttributeProvider customAttributeProvider = (ICustomAttributeProvider)attributeProvider;
							return (Attribute[])((attributeType != null) ? customAttributeProvider.GetCustomAttributes(attributeType, inherit) : customAttributeProvider.GetCustomAttributes(inherit));
						}
						if (!(attributeType != null))
						{
							return Attribute.GetCustomAttributes(module, inherit);
						}
						return Attribute.GetCustomAttributes(module, attributeType, inherit);
					}
					if (!(attributeType != null))
					{
						return Attribute.GetCustomAttributes(memberInfo, inherit);
					}
					return Attribute.GetCustomAttributes(memberInfo, attributeType, inherit);
				}
				if (!(attributeType != null))
				{
					return Attribute.GetCustomAttributes(assembly);
				}
				return Attribute.GetCustomAttributes(assembly, attributeType);
			}
			return ((attributeType != null) ? type.GetCustomAttributes(attributeType, inherit) : type.GetCustomAttributes(inherit)).Cast<Attribute>().ToArray();
		}

		[return: Nullable(new byte[] { 0, 2, 1 })]
		public static StructMultiKey<string, string> SplitFullyQualifiedTypeName(string fullyQualifiedTypeName)
		{
			int? assemblyDelimiterIndex = GetAssemblyDelimiterIndex(fullyQualifiedTypeName);
			string v;
			string v2;
			if (assemblyDelimiterIndex.HasValue)
			{
				v = fullyQualifiedTypeName.Trim(0, assemblyDelimiterIndex.GetValueOrDefault());
				v2 = fullyQualifiedTypeName.Trim(assemblyDelimiterIndex.GetValueOrDefault() + 1, fullyQualifiedTypeName.Length - assemblyDelimiterIndex.GetValueOrDefault() - 1);
			}
			else
			{
				v = fullyQualifiedTypeName;
				v2 = null;
			}
			return new StructMultiKey<string, string>(v2, v);
		}

		private static int? GetAssemblyDelimiterIndex(string fullyQualifiedTypeName)
		{
			int num = 0;
			for (int i = 0; i < fullyQualifiedTypeName.Length; i++)
			{
				switch (fullyQualifiedTypeName[i])
				{
				case '[':
					num++;
					break;
				case ']':
					num--;
					break;
				case ',':
					if (num == 0)
					{
						return i;
					}
					break;
				}
			}
			return null;
		}

		public static MemberInfo GetMemberInfoFromType(Type targetType, MemberInfo memberInfo)
		{
			if (memberInfo.MemberType() == MemberTypes.Property)
			{
				PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
				Type[] types = propertyInfo.GetIndexParameters().Select(_003C_003Ec._003C_003E9__39_0 ?? (_003C_003Ec._003C_003E9__39_0 = _003C_003Ec._003C_003E9._003CGetMemberInfoFromType_003Eb__39_0)).ToArray();
				return targetType.GetProperty(propertyInfo.Name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, propertyInfo.PropertyType, types, null);
			}
			return targetType.GetMember(memberInfo.Name, memberInfo.MemberType(), BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).SingleOrDefault();
		}

		public static IEnumerable<FieldInfo> GetFields(Type targetType, BindingFlags bindingAttr)
		{
			ValidationUtils.ArgumentNotNull(targetType, "targetType");
			List<MemberInfo> list = new List<MemberInfo>(targetType.GetFields(bindingAttr));
			GetChildPrivateFields(list, targetType, bindingAttr);
			return list.Cast<FieldInfo>();
		}

		private static void GetChildPrivateFields(IList<MemberInfo> initialFields, Type targetType, BindingFlags bindingAttr)
		{
			if ((bindingAttr & BindingFlags.NonPublic) != 0)
			{
				BindingFlags bindingAttr2 = bindingAttr.RemoveFlag(BindingFlags.Public);
				while ((targetType = targetType.BaseType()) != null)
				{
					IEnumerable<FieldInfo> collection = targetType.GetFields(bindingAttr2).Where(_003C_003Ec._003C_003E9__41_0 ?? (_003C_003Ec._003C_003E9__41_0 = _003C_003Ec._003C_003E9._003CGetChildPrivateFields_003Eb__41_0));
					initialFields.AddRange(collection);
				}
			}
		}

		public static IEnumerable<PropertyInfo> GetProperties(Type targetType, BindingFlags bindingAttr)
		{
			ValidationUtils.ArgumentNotNull(targetType, "targetType");
			List<PropertyInfo> list = new List<PropertyInfo>(targetType.GetProperties(bindingAttr));
			if (targetType.IsInterface())
			{
				Type[] interfaces = targetType.GetInterfaces();
				foreach (Type type in interfaces)
				{
					list.AddRange(type.GetProperties(bindingAttr));
				}
			}
			GetChildPrivateProperties(list, targetType, bindingAttr);
			for (int j = 0; j < list.Count; j++)
			{
				PropertyInfo propertyInfo = list[j];
				if (propertyInfo.DeclaringType != targetType)
				{
					PropertyInfo value = (PropertyInfo)GetMemberInfoFromType(propertyInfo.DeclaringType, propertyInfo);
					list[j] = value;
				}
			}
			return list;
		}

		public static BindingFlags RemoveFlag(this BindingFlags bindingAttr, BindingFlags flag)
		{
			if ((bindingAttr & flag) != flag)
			{
				return bindingAttr;
			}
			return bindingAttr ^ flag;
		}

		private static void GetChildPrivateProperties(IList<PropertyInfo> initialProperties, Type targetType, BindingFlags bindingAttr)
		{
			while ((targetType = targetType.BaseType()) != null)
			{
				PropertyInfo[] properties = targetType.GetProperties(bindingAttr);
				foreach (PropertyInfo subTypeProperty in properties)
				{
					_003C_003Ec__DisplayClass44_0 _003C_003Ec__DisplayClass44_ = new _003C_003Ec__DisplayClass44_0();
					_003C_003Ec__DisplayClass44_.subTypeProperty = subTypeProperty;
					if (!_003C_003Ec__DisplayClass44_.subTypeProperty.IsVirtual())
					{
						if (!IsPublic(_003C_003Ec__DisplayClass44_.subTypeProperty))
						{
							int num = initialProperties.IndexOf(_003C_003Ec__DisplayClass44_._003CGetChildPrivateProperties_003Eb__0);
							if (num == -1)
							{
								initialProperties.Add(_003C_003Ec__DisplayClass44_.subTypeProperty);
							}
							else if (!IsPublic(initialProperties[num]))
							{
								initialProperties[num] = _003C_003Ec__DisplayClass44_.subTypeProperty;
							}
						}
						else if (initialProperties.IndexOf(_003C_003Ec__DisplayClass44_._003CGetChildPrivateProperties_003Eb__1) == -1)
						{
							initialProperties.Add(_003C_003Ec__DisplayClass44_.subTypeProperty);
						}
					}
					else
					{
						_003C_003Ec__DisplayClass44_1 _003C_003Ec__DisplayClass44_2 = new _003C_003Ec__DisplayClass44_1();
						_003C_003Ec__DisplayClass44_2.CS_0024_003C_003E8__locals1 = _003C_003Ec__DisplayClass44_;
						MethodInfo baseDefinition = _003C_003Ec__DisplayClass44_2.CS_0024_003C_003E8__locals1.subTypeProperty.GetBaseDefinition();
						_003C_003Ec__DisplayClass44_2.subTypePropertyDeclaringType = (((object)baseDefinition != null) ? baseDefinition.DeclaringType : null) ?? _003C_003Ec__DisplayClass44_2.CS_0024_003C_003E8__locals1.subTypeProperty.DeclaringType;
						if (initialProperties.IndexOf(_003C_003Ec__DisplayClass44_2._003CGetChildPrivateProperties_003Eb__2) == -1)
						{
							initialProperties.Add(_003C_003Ec__DisplayClass44_2.CS_0024_003C_003E8__locals1.subTypeProperty);
						}
					}
				}
			}
		}

		public static bool IsMethodOverridden(Type currentType, Type methodDeclaringType, string method)
		{
			_003C_003Ec__DisplayClass45_0 _003C_003Ec__DisplayClass45_ = new _003C_003Ec__DisplayClass45_0();
			_003C_003Ec__DisplayClass45_.method = method;
			_003C_003Ec__DisplayClass45_.methodDeclaringType = methodDeclaringType;
			return currentType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Any(_003C_003Ec__DisplayClass45_._003CIsMethodOverridden_003Eb__0);
		}

		[return: Nullable(2)]
		public static object GetDefaultValue(Type type)
		{
			if (!type.IsValueType())
			{
				return null;
			}
			switch (ConvertUtils.GetTypeCode(type))
			{
			case PrimitiveTypeCode.Boolean:
				return false;
			case PrimitiveTypeCode.Char:
			case PrimitiveTypeCode.SByte:
			case PrimitiveTypeCode.Int16:
			case PrimitiveTypeCode.UInt16:
			case PrimitiveTypeCode.Int32:
			case PrimitiveTypeCode.Byte:
			case PrimitiveTypeCode.UInt32:
				return 0;
			case PrimitiveTypeCode.Int64:
			case PrimitiveTypeCode.UInt64:
				return 0L;
			case PrimitiveTypeCode.Single:
				return 0f;
			case PrimitiveTypeCode.Double:
				return 0.0;
			case PrimitiveTypeCode.Decimal:
				return 0m;
			case PrimitiveTypeCode.DateTime:
				return default(DateTime);
			case PrimitiveTypeCode.BigInteger:
				return default(BigInteger);
			case PrimitiveTypeCode.Guid:
				return default(Guid);
			case PrimitiveTypeCode.DateTimeOffset:
				return default(DateTimeOffset);
			default:
				if (IsNullable(type))
				{
					return null;
				}
				return Activator.CreateInstance(type);
			}
		}
	}
}
