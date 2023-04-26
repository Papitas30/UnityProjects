using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class DynamicProxyMetaObject<[Nullable(2)] T> : DynamicMetaObject
	{
		[NullableContext(0)]
		private delegate DynamicMetaObject Fallback([Nullable(2)] DynamicMetaObject errorSuggestion);

		[Nullable(0)]
		private sealed class GetBinderAdapter : GetMemberBinder
		{
			internal GetBinderAdapter(InvokeMemberBinder binder)
				: base(binder.Name, binder.IgnoreCase)
			{
			}

			public override DynamicMetaObject FallbackGetMember(DynamicMetaObject target, DynamicMetaObject errorSuggestion)
			{
				throw new NotSupportedException();
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass3_0
		{
			[Nullable(0)]
			public GetMemberBinder binder;

			[Nullable(0)]
			public DynamicProxyMetaObject<T> _003C_003E4__this;

			internal DynamicMetaObject _003CBindGetMember_003Eb__0([Nullable(2)] DynamicMetaObject e)
			{
				return binder.FallbackGetMember(_003C_003E4__this, e);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass4_0
		{
			[Nullable(0)]
			public SetMemberBinder binder;

			[Nullable(0)]
			public DynamicProxyMetaObject<T> _003C_003E4__this;

			[Nullable(0)]
			public DynamicMetaObject value;

			internal DynamicMetaObject _003CBindSetMember_003Eb__0([Nullable(2)] DynamicMetaObject e)
			{
				return binder.FallbackSetMember(_003C_003E4__this, value, e);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass5_0
		{
			[Nullable(0)]
			public DeleteMemberBinder binder;

			[Nullable(0)]
			public DynamicProxyMetaObject<T> _003C_003E4__this;

			internal DynamicMetaObject _003CBindDeleteMember_003Eb__0([Nullable(2)] DynamicMetaObject e)
			{
				return binder.FallbackDeleteMember(_003C_003E4__this, e);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass6_0
		{
			[Nullable(0)]
			public ConvertBinder binder;

			[Nullable(0)]
			public DynamicProxyMetaObject<T> _003C_003E4__this;

			internal DynamicMetaObject _003CBindConvert_003Eb__0([Nullable(2)] DynamicMetaObject e)
			{
				return binder.FallbackConvert(_003C_003E4__this, e);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass7_0
		{
			[Nullable(0)]
			public InvokeMemberBinder binder;

			[Nullable(0)]
			public DynamicProxyMetaObject<T> _003C_003E4__this;

			[Nullable(new byte[] { 0, 1 })]
			public DynamicMetaObject[] args;

			internal DynamicMetaObject _003CBindInvokeMember_003Eb__0([Nullable(2)] DynamicMetaObject e)
			{
				return binder.FallbackInvokeMember(_003C_003E4__this, args, e);
			}

			internal DynamicMetaObject _003CBindInvokeMember_003Eb__1([Nullable(2)] DynamicMetaObject e)
			{
				return binder.FallbackInvoke(e, args, null);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass8_0
		{
			[Nullable(0)]
			public CreateInstanceBinder binder;

			[Nullable(0)]
			public DynamicProxyMetaObject<T> _003C_003E4__this;

			[Nullable(new byte[] { 0, 1 })]
			public DynamicMetaObject[] args;

			internal DynamicMetaObject _003CBindCreateInstance_003Eb__0([Nullable(2)] DynamicMetaObject e)
			{
				return binder.FallbackCreateInstance(_003C_003E4__this, args, e);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass9_0
		{
			[Nullable(0)]
			public InvokeBinder binder;

			[Nullable(0)]
			public DynamicProxyMetaObject<T> _003C_003E4__this;

			[Nullable(new byte[] { 0, 1 })]
			public DynamicMetaObject[] args;

			internal DynamicMetaObject _003CBindInvoke_003Eb__0([Nullable(2)] DynamicMetaObject e)
			{
				return binder.FallbackInvoke(_003C_003E4__this, args, e);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass10_0
		{
			[Nullable(0)]
			public BinaryOperationBinder binder;

			[Nullable(0)]
			public DynamicProxyMetaObject<T> _003C_003E4__this;

			[Nullable(0)]
			public DynamicMetaObject arg;

			internal DynamicMetaObject _003CBindBinaryOperation_003Eb__0([Nullable(2)] DynamicMetaObject e)
			{
				return binder.FallbackBinaryOperation(_003C_003E4__this, arg, e);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass11_0
		{
			[Nullable(0)]
			public UnaryOperationBinder binder;

			[Nullable(0)]
			public DynamicProxyMetaObject<T> _003C_003E4__this;

			internal DynamicMetaObject _003CBindUnaryOperation_003Eb__0([Nullable(2)] DynamicMetaObject e)
			{
				return binder.FallbackUnaryOperation(_003C_003E4__this, e);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass12_0
		{
			[Nullable(0)]
			public GetIndexBinder binder;

			[Nullable(0)]
			public DynamicProxyMetaObject<T> _003C_003E4__this;

			[Nullable(new byte[] { 0, 1 })]
			public DynamicMetaObject[] indexes;

			internal DynamicMetaObject _003CBindGetIndex_003Eb__0([Nullable(2)] DynamicMetaObject e)
			{
				return binder.FallbackGetIndex(_003C_003E4__this, indexes, e);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass13_0
		{
			[Nullable(0)]
			public SetIndexBinder binder;

			[Nullable(0)]
			public DynamicProxyMetaObject<T> _003C_003E4__this;

			[Nullable(new byte[] { 0, 1 })]
			public DynamicMetaObject[] indexes;

			[Nullable(0)]
			public DynamicMetaObject value;

			internal DynamicMetaObject _003CBindSetIndex_003Eb__0([Nullable(2)] DynamicMetaObject e)
			{
				return binder.FallbackSetIndex(_003C_003E4__this, indexes, value, e);
			}
		}

		[CompilerGenerated]
		private sealed class _003C_003Ec__DisplayClass14_0
		{
			[Nullable(0)]
			public DeleteIndexBinder binder;

			[Nullable(0)]
			public DynamicProxyMetaObject<T> _003C_003E4__this;

			[Nullable(new byte[] { 0, 1 })]
			public DynamicMetaObject[] indexes;

			internal DynamicMetaObject _003CBindDeleteIndex_003Eb__0([Nullable(2)] DynamicMetaObject e)
			{
				return binder.FallbackDeleteIndex(_003C_003E4__this, indexes, e);
			}
		}

		[Serializable]
		[CompilerGenerated]
		private sealed class _003C_003Ec
		{
			[Nullable(0)]
			public static readonly _003C_003Ec _003C_003E9 = new _003C_003Ec();

			[Nullable(0)]
			public static Func<DynamicMetaObject, Expression> _003C_003E9__18_0;

			[NullableContext(0)]
			internal Expression _003CGetArgs_003Eb__18_0(DynamicMetaObject arg)
			{
				Expression expression = arg.Expression;
				if (!expression.Type.IsValueType())
				{
					return expression;
				}
				return Expression.Convert(expression, typeof(object));
			}
		}

		private readonly DynamicProxy<T> _proxy;

		private static Expression[] NoArgs
		{
			get
			{
				return CollectionUtils.ArrayEmpty<Expression>();
			}
		}

		internal DynamicProxyMetaObject(Expression expression, T value, DynamicProxy<T> proxy)
			: base(expression, BindingRestrictions.Empty, value)
		{
			_proxy = proxy;
		}

		private bool IsOverridden(string method)
		{
			return ReflectionUtils.IsMethodOverridden(_proxy.GetType(), typeof(DynamicProxy<T>), method);
		}

		public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
		{
			_003C_003Ec__DisplayClass3_0 _003C_003Ec__DisplayClass3_ = new _003C_003Ec__DisplayClass3_0();
			_003C_003Ec__DisplayClass3_.binder = binder;
			_003C_003Ec__DisplayClass3_._003C_003E4__this = this;
			if (!IsOverridden("TryGetMember"))
			{
				return base.BindGetMember(_003C_003Ec__DisplayClass3_.binder);
			}
			return CallMethodWithResult("TryGetMember", _003C_003Ec__DisplayClass3_.binder, NoArgs, _003C_003Ec__DisplayClass3_._003CBindGetMember_003Eb__0);
		}

		public override DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
		{
			_003C_003Ec__DisplayClass4_0 _003C_003Ec__DisplayClass4_ = new _003C_003Ec__DisplayClass4_0();
			_003C_003Ec__DisplayClass4_.binder = binder;
			_003C_003Ec__DisplayClass4_._003C_003E4__this = this;
			_003C_003Ec__DisplayClass4_.value = value;
			if (!IsOverridden("TrySetMember"))
			{
				return base.BindSetMember(_003C_003Ec__DisplayClass4_.binder, _003C_003Ec__DisplayClass4_.value);
			}
			return CallMethodReturnLast("TrySetMember", _003C_003Ec__DisplayClass4_.binder, GetArgs(_003C_003Ec__DisplayClass4_.value), _003C_003Ec__DisplayClass4_._003CBindSetMember_003Eb__0);
		}

		public override DynamicMetaObject BindDeleteMember(DeleteMemberBinder binder)
		{
			_003C_003Ec__DisplayClass5_0 _003C_003Ec__DisplayClass5_ = new _003C_003Ec__DisplayClass5_0();
			_003C_003Ec__DisplayClass5_.binder = binder;
			_003C_003Ec__DisplayClass5_._003C_003E4__this = this;
			if (!IsOverridden("TryDeleteMember"))
			{
				return base.BindDeleteMember(_003C_003Ec__DisplayClass5_.binder);
			}
			return CallMethodNoResult("TryDeleteMember", _003C_003Ec__DisplayClass5_.binder, NoArgs, _003C_003Ec__DisplayClass5_._003CBindDeleteMember_003Eb__0);
		}

		public override DynamicMetaObject BindConvert(ConvertBinder binder)
		{
			_003C_003Ec__DisplayClass6_0 _003C_003Ec__DisplayClass6_ = new _003C_003Ec__DisplayClass6_0();
			_003C_003Ec__DisplayClass6_.binder = binder;
			_003C_003Ec__DisplayClass6_._003C_003E4__this = this;
			if (!IsOverridden("TryConvert"))
			{
				return base.BindConvert(_003C_003Ec__DisplayClass6_.binder);
			}
			return CallMethodWithResult("TryConvert", _003C_003Ec__DisplayClass6_.binder, NoArgs, _003C_003Ec__DisplayClass6_._003CBindConvert_003Eb__0);
		}

		public override DynamicMetaObject BindInvokeMember(InvokeMemberBinder binder, DynamicMetaObject[] args)
		{
			_003C_003Ec__DisplayClass7_0 _003C_003Ec__DisplayClass7_ = new _003C_003Ec__DisplayClass7_0();
			_003C_003Ec__DisplayClass7_.binder = binder;
			_003C_003Ec__DisplayClass7_._003C_003E4__this = this;
			_003C_003Ec__DisplayClass7_.args = args;
			if (!IsOverridden("TryInvokeMember"))
			{
				return base.BindInvokeMember(_003C_003Ec__DisplayClass7_.binder, _003C_003Ec__DisplayClass7_.args);
			}
			Fallback fallback = _003C_003Ec__DisplayClass7_._003CBindInvokeMember_003Eb__0;
			return BuildCallMethodWithResult("TryInvokeMember", _003C_003Ec__DisplayClass7_.binder, GetArgArray(_003C_003Ec__DisplayClass7_.args), BuildCallMethodWithResult("TryGetMember", new GetBinderAdapter(_003C_003Ec__DisplayClass7_.binder), NoArgs, fallback(null), _003C_003Ec__DisplayClass7_._003CBindInvokeMember_003Eb__1), null);
		}

		public override DynamicMetaObject BindCreateInstance(CreateInstanceBinder binder, DynamicMetaObject[] args)
		{
			_003C_003Ec__DisplayClass8_0 _003C_003Ec__DisplayClass8_ = new _003C_003Ec__DisplayClass8_0();
			_003C_003Ec__DisplayClass8_.binder = binder;
			_003C_003Ec__DisplayClass8_._003C_003E4__this = this;
			_003C_003Ec__DisplayClass8_.args = args;
			if (!IsOverridden("TryCreateInstance"))
			{
				return base.BindCreateInstance(_003C_003Ec__DisplayClass8_.binder, _003C_003Ec__DisplayClass8_.args);
			}
			return CallMethodWithResult("TryCreateInstance", _003C_003Ec__DisplayClass8_.binder, GetArgArray(_003C_003Ec__DisplayClass8_.args), _003C_003Ec__DisplayClass8_._003CBindCreateInstance_003Eb__0);
		}

		public override DynamicMetaObject BindInvoke(InvokeBinder binder, DynamicMetaObject[] args)
		{
			_003C_003Ec__DisplayClass9_0 _003C_003Ec__DisplayClass9_ = new _003C_003Ec__DisplayClass9_0();
			_003C_003Ec__DisplayClass9_.binder = binder;
			_003C_003Ec__DisplayClass9_._003C_003E4__this = this;
			_003C_003Ec__DisplayClass9_.args = args;
			if (!IsOverridden("TryInvoke"))
			{
				return base.BindInvoke(_003C_003Ec__DisplayClass9_.binder, _003C_003Ec__DisplayClass9_.args);
			}
			return CallMethodWithResult("TryInvoke", _003C_003Ec__DisplayClass9_.binder, GetArgArray(_003C_003Ec__DisplayClass9_.args), _003C_003Ec__DisplayClass9_._003CBindInvoke_003Eb__0);
		}

		public override DynamicMetaObject BindBinaryOperation(BinaryOperationBinder binder, DynamicMetaObject arg)
		{
			_003C_003Ec__DisplayClass10_0 _003C_003Ec__DisplayClass10_ = new _003C_003Ec__DisplayClass10_0();
			_003C_003Ec__DisplayClass10_.binder = binder;
			_003C_003Ec__DisplayClass10_._003C_003E4__this = this;
			_003C_003Ec__DisplayClass10_.arg = arg;
			if (!IsOverridden("TryBinaryOperation"))
			{
				return base.BindBinaryOperation(_003C_003Ec__DisplayClass10_.binder, _003C_003Ec__DisplayClass10_.arg);
			}
			return CallMethodWithResult("TryBinaryOperation", _003C_003Ec__DisplayClass10_.binder, GetArgs(_003C_003Ec__DisplayClass10_.arg), _003C_003Ec__DisplayClass10_._003CBindBinaryOperation_003Eb__0);
		}

		public override DynamicMetaObject BindUnaryOperation(UnaryOperationBinder binder)
		{
			_003C_003Ec__DisplayClass11_0 _003C_003Ec__DisplayClass11_ = new _003C_003Ec__DisplayClass11_0();
			_003C_003Ec__DisplayClass11_.binder = binder;
			_003C_003Ec__DisplayClass11_._003C_003E4__this = this;
			if (!IsOverridden("TryUnaryOperation"))
			{
				return base.BindUnaryOperation(_003C_003Ec__DisplayClass11_.binder);
			}
			return CallMethodWithResult("TryUnaryOperation", _003C_003Ec__DisplayClass11_.binder, NoArgs, _003C_003Ec__DisplayClass11_._003CBindUnaryOperation_003Eb__0);
		}

		public override DynamicMetaObject BindGetIndex(GetIndexBinder binder, DynamicMetaObject[] indexes)
		{
			_003C_003Ec__DisplayClass12_0 _003C_003Ec__DisplayClass12_ = new _003C_003Ec__DisplayClass12_0();
			_003C_003Ec__DisplayClass12_.binder = binder;
			_003C_003Ec__DisplayClass12_._003C_003E4__this = this;
			_003C_003Ec__DisplayClass12_.indexes = indexes;
			if (!IsOverridden("TryGetIndex"))
			{
				return base.BindGetIndex(_003C_003Ec__DisplayClass12_.binder, _003C_003Ec__DisplayClass12_.indexes);
			}
			return CallMethodWithResult("TryGetIndex", _003C_003Ec__DisplayClass12_.binder, GetArgArray(_003C_003Ec__DisplayClass12_.indexes), _003C_003Ec__DisplayClass12_._003CBindGetIndex_003Eb__0);
		}

		public override DynamicMetaObject BindSetIndex(SetIndexBinder binder, DynamicMetaObject[] indexes, DynamicMetaObject value)
		{
			_003C_003Ec__DisplayClass13_0 _003C_003Ec__DisplayClass13_ = new _003C_003Ec__DisplayClass13_0();
			_003C_003Ec__DisplayClass13_.binder = binder;
			_003C_003Ec__DisplayClass13_._003C_003E4__this = this;
			_003C_003Ec__DisplayClass13_.indexes = indexes;
			_003C_003Ec__DisplayClass13_.value = value;
			if (!IsOverridden("TrySetIndex"))
			{
				return base.BindSetIndex(_003C_003Ec__DisplayClass13_.binder, _003C_003Ec__DisplayClass13_.indexes, _003C_003Ec__DisplayClass13_.value);
			}
			return CallMethodReturnLast("TrySetIndex", _003C_003Ec__DisplayClass13_.binder, GetArgArray(_003C_003Ec__DisplayClass13_.indexes, _003C_003Ec__DisplayClass13_.value), _003C_003Ec__DisplayClass13_._003CBindSetIndex_003Eb__0);
		}

		public override DynamicMetaObject BindDeleteIndex(DeleteIndexBinder binder, DynamicMetaObject[] indexes)
		{
			_003C_003Ec__DisplayClass14_0 _003C_003Ec__DisplayClass14_ = new _003C_003Ec__DisplayClass14_0();
			_003C_003Ec__DisplayClass14_.binder = binder;
			_003C_003Ec__DisplayClass14_._003C_003E4__this = this;
			_003C_003Ec__DisplayClass14_.indexes = indexes;
			if (!IsOverridden("TryDeleteIndex"))
			{
				return base.BindDeleteIndex(_003C_003Ec__DisplayClass14_.binder, _003C_003Ec__DisplayClass14_.indexes);
			}
			return CallMethodNoResult("TryDeleteIndex", _003C_003Ec__DisplayClass14_.binder, GetArgArray(_003C_003Ec__DisplayClass14_.indexes), _003C_003Ec__DisplayClass14_._003CBindDeleteIndex_003Eb__0);
		}

		private static IEnumerable<Expression> GetArgs(params DynamicMetaObject[] args)
		{
			return args.Select(_003C_003Ec._003C_003E9__18_0 ?? (_003C_003Ec._003C_003E9__18_0 = _003C_003Ec._003C_003E9._003CGetArgs_003Eb__18_0));
		}

		private static Expression[] GetArgArray(DynamicMetaObject[] args)
		{
			return new NewArrayExpression[1] { Expression.NewArrayInit(typeof(object), GetArgs(args)) };
		}

		private static Expression[] GetArgArray(DynamicMetaObject[] args, DynamicMetaObject value)
		{
			Expression expression = value.Expression;
			return new Expression[2]
			{
				Expression.NewArrayInit(typeof(object), GetArgs(args)),
				expression.Type.IsValueType() ? Expression.Convert(expression, typeof(object)) : expression
			};
		}

		private static ConstantExpression Constant(DynamicMetaObjectBinder binder)
		{
			Type type = binder.GetType();
			while (!type.IsVisible())
			{
				type = type.BaseType();
			}
			return Expression.Constant(binder, type);
		}

		private DynamicMetaObject CallMethodWithResult(string methodName, DynamicMetaObjectBinder binder, IEnumerable<Expression> args, [Nullable(new byte[] { 1, 0 })] Fallback fallback, [Nullable(new byte[] { 2, 0 })] Fallback fallbackInvoke = null)
		{
			DynamicMetaObject fallbackResult = fallback(null);
			return BuildCallMethodWithResult(methodName, binder, args, fallbackResult, fallbackInvoke);
		}

		private DynamicMetaObject BuildCallMethodWithResult(string methodName, DynamicMetaObjectBinder binder, IEnumerable<Expression> args, DynamicMetaObject fallbackResult, [Nullable(new byte[] { 2, 0 })] Fallback fallbackInvoke)
		{
			ParameterExpression parameterExpression = Expression.Parameter(typeof(object), null);
			IList<Expression> list = new List<Expression>();
			list.Add(Expression.Convert(base.Expression, typeof(T)));
			list.Add(Constant(binder));
			list.AddRange(args);
			list.Add(parameterExpression);
			DynamicMetaObject dynamicMetaObject = new DynamicMetaObject(parameterExpression, BindingRestrictions.Empty);
			if (binder.ReturnType != typeof(object))
			{
				dynamicMetaObject = new DynamicMetaObject(Expression.Convert(dynamicMetaObject.Expression, binder.ReturnType), dynamicMetaObject.Restrictions);
			}
			if (fallbackInvoke != null)
			{
				dynamicMetaObject = fallbackInvoke(dynamicMetaObject);
			}
			return new DynamicMetaObject(Expression.Block(new ParameterExpression[1] { parameterExpression }, Expression.Condition(Expression.Call(Expression.Constant(_proxy), typeof(DynamicProxy<T>).GetMethod(methodName), list), dynamicMetaObject.Expression, fallbackResult.Expression, binder.ReturnType)), GetRestrictions().Merge(dynamicMetaObject.Restrictions).Merge(fallbackResult.Restrictions));
		}

		private DynamicMetaObject CallMethodReturnLast(string methodName, DynamicMetaObjectBinder binder, IEnumerable<Expression> args, [Nullable(new byte[] { 1, 0 })] Fallback fallback)
		{
			DynamicMetaObject dynamicMetaObject = fallback(null);
			ParameterExpression parameterExpression = Expression.Parameter(typeof(object), null);
			IList<Expression> list = new List<Expression>();
			list.Add(Expression.Convert(base.Expression, typeof(T)));
			list.Add(Constant(binder));
			list.AddRange(args);
			list[list.Count - 1] = Expression.Assign(parameterExpression, list[list.Count - 1]);
			return new DynamicMetaObject(Expression.Block(new ParameterExpression[1] { parameterExpression }, Expression.Condition(Expression.Call(Expression.Constant(_proxy), typeof(DynamicProxy<T>).GetMethod(methodName), list), parameterExpression, dynamicMetaObject.Expression, typeof(object))), GetRestrictions().Merge(dynamicMetaObject.Restrictions));
		}

		private DynamicMetaObject CallMethodNoResult(string methodName, DynamicMetaObjectBinder binder, Expression[] args, [Nullable(new byte[] { 1, 0 })] Fallback fallback)
		{
			DynamicMetaObject dynamicMetaObject = fallback(null);
			IList<Expression> list = new List<Expression>();
			list.Add(Expression.Convert(base.Expression, typeof(T)));
			list.Add(Constant(binder));
			list.AddRange(args);
			return new DynamicMetaObject(Expression.Condition(Expression.Call(Expression.Constant(_proxy), typeof(DynamicProxy<T>).GetMethod(methodName), list), Expression.Empty(), dynamicMetaObject.Expression, typeof(void)), GetRestrictions().Merge(dynamicMetaObject.Restrictions));
		}

		private BindingRestrictions GetRestrictions()
		{
			if (base.Value != null || !base.HasValue)
			{
				return BindingRestrictions.GetTypeRestriction(base.Expression, base.LimitType);
			}
			return BindingRestrictions.GetInstanceRestriction(base.Expression, null);
		}

		public override IEnumerable<string> GetDynamicMemberNames()
		{
			return _proxy.GetDynamicMemberNames((T)base.Value);
		}
	}
}
