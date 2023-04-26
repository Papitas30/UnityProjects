using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	[NullableContext(1)]
	[Nullable(0)]
	internal class LateBoundReflectionDelegateFactory : ReflectionDelegateFactory
	{
		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass3_0
		{
			[Nullable(0)]
			public ConstructorInfo c;

			[Nullable(0)]
			public MethodBase method;

			internal object _003CCreateParameterizedConstructor_003Eb__0([Nullable(new byte[] { 1, 2 })] object[] a)
			{
				return c.Invoke(a);
			}

			internal object _003CCreateParameterizedConstructor_003Eb__1([Nullable(new byte[] { 1, 2 })] object[] a)
			{
				return method.Invoke(null, a);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass4_0<T>
		{
			[Nullable(0)]
			public ConstructorInfo c;

			[Nullable(0)]
			public MethodBase method;

			[NullableContext(0)]
			[return: Nullable(2)]
			internal object _003CCreateMethodCall_003Eb__0(T o, [Nullable(new byte[] { 1, 2 })] object[] a)
			{
				return c.Invoke(a);
			}

			[NullableContext(0)]
			[return: Nullable(2)]
			internal object _003CCreateMethodCall_003Eb__1(T o, [Nullable(new byte[] { 1, 2 })] object[] a)
			{
				return method.Invoke(o, a);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass5_0<T>
		{
			[Nullable(0)]
			public Type type;

			[Nullable(0)]
			public ConstructorInfo constructorInfo;

			[NullableContext(0)]
			internal T _003CCreateDefaultConstructor_003Eb__0()
			{
				return (T)Activator.CreateInstance(type);
			}

			[NullableContext(0)]
			internal T _003CCreateDefaultConstructor_003Eb__1()
			{
				return (T)constructorInfo.Invoke(null);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass6_0<T>
		{
			[Nullable(0)]
			public PropertyInfo propertyInfo;

			[NullableContext(0)]
			[return: Nullable(2)]
			internal object _003CCreateGet_003Eb__0(T o)
			{
				return propertyInfo.GetValue(o, null);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass7_0<T>
		{
			[Nullable(0)]
			public FieldInfo fieldInfo;

			[NullableContext(0)]
			[return: Nullable(2)]
			internal object _003CCreateGet_003Eb__0(T o)
			{
				return fieldInfo.GetValue(o);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass8_0<T>
		{
			[Nullable(0)]
			public FieldInfo fieldInfo;

			[NullableContext(0)]
			internal void _003CCreateSet_003Eb__0(T o, [Nullable(2)] object v)
			{
				fieldInfo.SetValue(o, v);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass9_0<T>
		{
			[Nullable(0)]
			public PropertyInfo propertyInfo;

			[NullableContext(0)]
			internal void _003CCreateSet_003Eb__0(T o, [Nullable(2)] object v)
			{
				propertyInfo.SetValue(o, v, null);
			}
		}

		private static readonly LateBoundReflectionDelegateFactory _instance = new LateBoundReflectionDelegateFactory();

		internal static ReflectionDelegateFactory Instance
		{
			get
			{
				return _instance;
			}
		}

		public override ObjectConstructor<object> CreateParameterizedConstructor(MethodBase method)
		{
			_003C_003Ec__DisplayClass3_0 _003C_003Ec__DisplayClass3_ = new _003C_003Ec__DisplayClass3_0();
			_003C_003Ec__DisplayClass3_.method = method;
			ValidationUtils.ArgumentNotNull(_003C_003Ec__DisplayClass3_.method, "method");
			_003C_003Ec__DisplayClass3_.c = _003C_003Ec__DisplayClass3_.method as ConstructorInfo;
			if ((object)_003C_003Ec__DisplayClass3_.c != null)
			{
				return _003C_003Ec__DisplayClass3_._003CCreateParameterizedConstructor_003Eb__0;
			}
			return _003C_003Ec__DisplayClass3_._003CCreateParameterizedConstructor_003Eb__1;
		}

		[return: Nullable(new byte[] { 1, 1, 2 })]
		public override MethodCall<T, object> CreateMethodCall<[Nullable(2)] T>(MethodBase method)
		{
			_003C_003Ec__DisplayClass4_0<T> _003C_003Ec__DisplayClass4_ = new _003C_003Ec__DisplayClass4_0<T>();
			_003C_003Ec__DisplayClass4_.method = method;
			ValidationUtils.ArgumentNotNull(_003C_003Ec__DisplayClass4_.method, "method");
			_003C_003Ec__DisplayClass4_.c = _003C_003Ec__DisplayClass4_.method as ConstructorInfo;
			if ((object)_003C_003Ec__DisplayClass4_.c != null)
			{
				return _003C_003Ec__DisplayClass4_._003CCreateMethodCall_003Eb__0;
			}
			return _003C_003Ec__DisplayClass4_._003CCreateMethodCall_003Eb__1;
		}

		public override Func<T> CreateDefaultConstructor<[Nullable(2)] T>(Type type)
		{
			_003C_003Ec__DisplayClass5_0<T> _003C_003Ec__DisplayClass5_ = new _003C_003Ec__DisplayClass5_0<T>();
			_003C_003Ec__DisplayClass5_.type = type;
			ValidationUtils.ArgumentNotNull(_003C_003Ec__DisplayClass5_.type, "type");
			if (_003C_003Ec__DisplayClass5_.type.IsValueType())
			{
				return _003C_003Ec__DisplayClass5_._003CCreateDefaultConstructor_003Eb__0;
			}
			_003C_003Ec__DisplayClass5_.constructorInfo = ReflectionUtils.GetDefaultConstructor(_003C_003Ec__DisplayClass5_.type, true);
			return _003C_003Ec__DisplayClass5_._003CCreateDefaultConstructor_003Eb__1;
		}

		[return: Nullable(new byte[] { 1, 1, 2 })]
		public override Func<T, object> CreateGet<[Nullable(2)] T>(PropertyInfo propertyInfo)
		{
			_003C_003Ec__DisplayClass6_0<T> _003C_003Ec__DisplayClass6_ = new _003C_003Ec__DisplayClass6_0<T>();
			_003C_003Ec__DisplayClass6_.propertyInfo = propertyInfo;
			ValidationUtils.ArgumentNotNull(_003C_003Ec__DisplayClass6_.propertyInfo, "propertyInfo");
			return _003C_003Ec__DisplayClass6_._003CCreateGet_003Eb__0;
		}

		[return: Nullable(new byte[] { 1, 1, 2 })]
		public override Func<T, object> CreateGet<[Nullable(2)] T>(FieldInfo fieldInfo)
		{
			_003C_003Ec__DisplayClass7_0<T> _003C_003Ec__DisplayClass7_ = new _003C_003Ec__DisplayClass7_0<T>();
			_003C_003Ec__DisplayClass7_.fieldInfo = fieldInfo;
			ValidationUtils.ArgumentNotNull(_003C_003Ec__DisplayClass7_.fieldInfo, "fieldInfo");
			return _003C_003Ec__DisplayClass7_._003CCreateGet_003Eb__0;
		}

		[return: Nullable(new byte[] { 1, 1, 2 })]
		public override Action<T, object> CreateSet<[Nullable(2)] T>(FieldInfo fieldInfo)
		{
			_003C_003Ec__DisplayClass8_0<T> _003C_003Ec__DisplayClass8_ = new _003C_003Ec__DisplayClass8_0<T>();
			_003C_003Ec__DisplayClass8_.fieldInfo = fieldInfo;
			ValidationUtils.ArgumentNotNull(_003C_003Ec__DisplayClass8_.fieldInfo, "fieldInfo");
			return _003C_003Ec__DisplayClass8_._003CCreateSet_003Eb__0;
		}

		[return: Nullable(new byte[] { 1, 1, 2 })]
		public override Action<T, object> CreateSet<[Nullable(2)] T>(PropertyInfo propertyInfo)
		{
			_003C_003Ec__DisplayClass9_0<T> _003C_003Ec__DisplayClass9_ = new _003C_003Ec__DisplayClass9_0<T>();
			_003C_003Ec__DisplayClass9_.propertyInfo = propertyInfo;
			ValidationUtils.ArgumentNotNull(_003C_003Ec__DisplayClass9_.propertyInfo, "propertyInfo");
			return _003C_003Ec__DisplayClass9_._003CCreateSet_003Eb__0;
		}
	}
}
