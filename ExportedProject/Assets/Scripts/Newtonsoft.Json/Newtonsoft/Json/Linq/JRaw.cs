using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Newtonsoft.Json.Linq
{
	[NullableContext(1)]
	[Nullable(0)]
	public class JRaw : JValue
	{
		[StructLayout(LayoutKind.Auto)]
		[CompilerGenerated]
		private struct _003CCreateAsync_003Ed__0 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			[Nullable(0)]
			public AsyncTaskMethodBuilder<JRaw> _003C_003Et__builder;

			[Nullable(0)]
			public JsonReader reader;

			public CancellationToken cancellationToken;

			[Nullable(0)]
			private StringWriter _003Csw_003E5__2;

			[Nullable(0)]
			private JsonTextWriter _003CjsonWriter_003E5__3;

			private ConfiguredTaskAwaitable.ConfiguredTaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
				int num = _003C_003E1__state;
				JRaw result;
				try
				{
					if (num != 0)
					{
						_003Csw_003E5__2 = new StringWriter(CultureInfo.InvariantCulture);
					}
					try
					{
						if (num != 0)
						{
							_003CjsonWriter_003E5__3 = new JsonTextWriter(_003Csw_003E5__2);
						}
						try
						{
							ConfiguredTaskAwaitable.ConfiguredTaskAwaiter awaiter;
							if (num != 0)
							{
								awaiter = _003CjsonWriter_003E5__3.WriteTokenSyncReadingAsync(reader, cancellationToken).ConfigureAwait(false).GetAwaiter();
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
							result = new JRaw(_003Csw_003E5__2.ToString());
						}
						finally
						{
							if (num < 0 && _003CjsonWriter_003E5__3 != null)
							{
								((IDisposable)_003CjsonWriter_003E5__3).Dispose();
							}
						}
					}
					finally
					{
						if (num < 0 && _003Csw_003E5__2 != null)
						{
							((IDisposable)_003Csw_003E5__2).Dispose();
						}
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

		[AsyncStateMachine(typeof(_003CCreateAsync_003Ed__0))]
		public static Task<JRaw> CreateAsync(JsonReader reader, CancellationToken cancellationToken = default(CancellationToken))
		{
			_003CCreateAsync_003Ed__0 stateMachine = default(_003CCreateAsync_003Ed__0);
			stateMachine._003C_003Et__builder = AsyncTaskMethodBuilder<JRaw>.Create();
			stateMachine.reader = reader;
			stateMachine.cancellationToken = cancellationToken;
			stateMachine._003C_003E1__state = -1;
			stateMachine._003C_003Et__builder.Start(ref stateMachine);
			return stateMachine._003C_003Et__builder.Task;
		}

		public JRaw(JRaw other)
			: base(other)
		{
		}

		[NullableContext(2)]
		public JRaw(object rawJson)
			: base(rawJson, JTokenType.Raw)
		{
		}

		public static JRaw Create(JsonReader reader)
		{
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				using (JsonTextWriter jsonTextWriter = new JsonTextWriter(stringWriter))
				{
					jsonTextWriter.WriteToken(reader);
					return new JRaw(stringWriter.ToString());
				}
			}
		}

		internal override JToken CloneToken()
		{
			return new JRaw(this);
		}
	}
}
