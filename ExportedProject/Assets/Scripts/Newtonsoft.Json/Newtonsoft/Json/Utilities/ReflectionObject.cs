using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	[NullableContext(1)]
	[Nullable(0)]
	internal class ReflectionObject
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass11_0
		{
			[Nullable(new byte[] { 0, 1 })]
			public Func<object> ctor;

			internal object _003CCreate_003Eb__0([Nullable(new byte[] { 1, 2 })] object[] args)
			{
				return ctor();
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass11_1
		{
			[Nullable(new byte[] { 0, 1, 2 })]
			public MethodCall<object, object> call;

			[return: Nullable(2)]
			internal object _003CCreate_003Eb__1(object target)
			{
				return call(target);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass11_2
		{
			[Nullable(new byte[] { 0, 1, 2 })]
			public MethodCall<object, object> call;

			internal void _003CCreate_003Eb__2(object target, [Nullable(2)] object arg)
			{
				call(target, arg);
			}
		}

		[Nullable(new byte[] { 2, 1 })]
		[CompilerGenerated]
		private readonly ObjectConstructor<object> _003CCreator_003Ek__BackingField;

		[CompilerGenerated]
		private readonly IDictionary<string, ReflectionMember> _003CMembers_003Ek__BackingField;

		[Nullable(new byte[] { 2, 1 })]
		public ObjectConstructor<object> Creator
		{
			[CompilerGenerated]
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return _003CCreator_003Ek__BackingField;
			}
		}

		public IDictionary<string, ReflectionMember> Members
		{
			[CompilerGenerated]
			get
			{
				return _003CMembers_003Ek__BackingField;
			}
		}

		private ReflectionObject([Nullable(new byte[] { 2, 1 })] ObjectConstructor<object> creator)
		{
			_003CMembers_003Ek__BackingField = new Dictionary<string, ReflectionMember>();
			_003CCreator_003Ek__BackingField = creator;
		}

		[return: Nullable(2)]
		public object GetValue(object target, string member)
		{
			return Members[member].Getter(target);
		}

		public void SetValue(object target, string member, [Nullable(2)] object value)
		{
			Members[member].Setter(target, value);
		}

		public Type GetType(string member)
		{
			return Members[member].MemberType;
		}

		public static ReflectionObject Create(Type t, params string[] memberNames)
		{
			return Create(t, null, memberNames);
		}

		public static ReflectionObject Create(Type t, [Nullable(2)] MethodBase creator, params string[] memberNames)
		{
			ReflectionDelegateFactory reflectionDelegateFactory = JsonTypeReflector.ReflectionDelegateFactory;
			ObjectConstructor<object> creator2 = null;
			if (creator != null)
			{
				creator2 = reflectionDelegateFactory.CreateParameterizedConstructor(creator);
			}
			else if (ReflectionUtils.HasDefaultConstructor(t, false))
			{
				_003C_003Ec__DisplayClass11_0 _003C_003Ec__DisplayClass11_ = new _003C_003Ec__DisplayClass11_0();
				_003C_003Ec__DisplayClass11_.ctor = reflectionDelegateFactory.CreateDefaultConstructor<object>(t);
				creator2 = _003C_003Ec__DisplayClass11_._003CCreate_003Eb__0;
			}
			ReflectionObject reflectionObject = new ReflectionObject(creator2);
			foreach (string text in memberNames)
			{
				MemberInfo[] member = t.GetMember(text, BindingFlags.Instance | BindingFlags.Public);
				if (member.Length != 1)
				{
					throw new ArgumentException("Expected a single member with the name '{0}'.".FormatWith(CultureInfo.InvariantCulture, text));
				}
				MemberInfo memberInfo = member.Single();
				ReflectionMember reflectionMember = new ReflectionMember();
				switch (memberInfo.MemberType())
				{
				case MemberTypes.Field:
				case MemberTypes.Property:
					if (ReflectionUtils.CanReadMemberValue(memberInfo, false))
					{
						reflectionMember.Getter = reflectionDelegateFactory.CreateGet<object>(memberInfo);
					}
					if (ReflectionUtils.CanSetMemberValue(memberInfo, false, false))
					{
						reflectionMember.Setter = reflectionDelegateFactory.CreateSet<object>(memberInfo);
					}
					break;
				case MemberTypes.Method:
				{
					MethodInfo methodInfo = (MethodInfo)memberInfo;
					if (methodInfo.IsPublic)
					{
						ParameterInfo[] parameters = methodInfo.GetParameters();
						if (parameters.Length == 0 && methodInfo.ReturnType != typeof(void))
						{
							_003C_003Ec__DisplayClass11_1 _003C_003Ec__DisplayClass11_2 = new _003C_003Ec__DisplayClass11_1();
							_003C_003Ec__DisplayClass11_2.call = reflectionDelegateFactory.CreateMethodCall<object>(methodInfo);
							reflectionMember.Getter = _003C_003Ec__DisplayClass11_2._003CCreate_003Eb__1;
						}
						else if (parameters.Length == 1 && methodInfo.ReturnType == typeof(void))
						{
							_003C_003Ec__DisplayClass11_2 _003C_003Ec__DisplayClass11_3 = new _003C_003Ec__DisplayClass11_2();
							_003C_003Ec__DisplayClass11_3.call = reflectionDelegateFactory.CreateMethodCall<object>(methodInfo);
							reflectionMember.Setter = _003C_003Ec__DisplayClass11_3._003CCreate_003Eb__2;
						}
					}
					break;
				}
				default:
					throw new ArgumentException("Unexpected member type '{0}' for member '{1}'.".FormatWith(CultureInfo.InvariantCulture, memberInfo.MemberType(), memberInfo.Name));
				}
				reflectionMember.MemberType = ReflectionUtils.GetMemberUnderlyingType(memberInfo);
				reflectionObject.Members[text] = reflectionMember;
			}
			return reflectionObject;
		}
	}
}
