using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace GameOldBoy.Rendering
{
	public class TAA : ScriptableRendererFeature
	{
		private class TAACameraPass : ScriptableRenderPass
		{
			private ProfilingSampler m_ProfilingSampler;

			private Matrix4x4 matrix;

			public TAACameraPass(string profilerTag)
			{
				m_ProfilingSampler = new ProfilingSampler(profilerTag);
			}

			public void Setup(Matrix4x4 matrix)
			{
				this.matrix = matrix;
			}

			public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
			{
				CommandBuffer commandBuffer = CommandBufferPool.Get();
				using (new ProfilingScope(commandBuffer, m_ProfilingSampler))
				{
					Camera camera = renderingData.cameraData.camera;
					commandBuffer.SetViewProjectionMatrices(camera.worldToCameraMatrix, matrix);
				}
				context.ExecuteCommandBuffer(commandBuffer);
				CommandBufferPool.Release(commandBuffer);
			}
		}

		private class TAAPass : ScriptableRenderPass
		{
			private class TAATextureSwap
			{
				[CompilerGenerated]
				private readonly RenderTexture _003CTAATextureA_003Ek__BackingField;

				[CompilerGenerated]
				private readonly RenderTexture _003CTAATextureB_003Ek__BackingField;

				[CompilerGenerated]
				private readonly RenderTextureDescriptor _003CDescriptor_003Ek__BackingField;

				private bool swap;

				public RenderTexture TAATextureA
				{
					[CompilerGenerated]
					get
					{
						return _003CTAATextureA_003Ek__BackingField;
					}
				}

				public RenderTexture TAATextureB
				{
					[CompilerGenerated]
					get
					{
						return _003CTAATextureB_003Ek__BackingField;
					}
				}

				public RenderTextureDescriptor Descriptor
				{
					[CompilerGenerated]
					get
					{
						return _003CDescriptor_003Ek__BackingField;
					}
				}

				public TAATextureSwap(RenderTexture taaTextureA, RenderTexture taaTextureB, RenderTextureDescriptor descriptor)
				{
					_003CTAATextureA_003Ek__BackingField = taaTextureA;
					_003CTAATextureB_003Ek__BackingField = taaTextureB;
					_003CDescriptor_003Ek__BackingField = descriptor;
					swap = true;
				}

				public bool Swap()
				{
					swap = !swap;
					return swap;
				}

				public bool CheckTextureNull()
				{
					if (!(TAATextureA == null))
					{
						return TAATextureB == null;
					}
					return true;
				}

				public void Release()
				{
					if (TAATextureA != null)
					{
						TAATextureA.Release();
					}
					if (TAATextureB != null)
					{
						TAATextureB.Release();
					}
				}
			}

			private ProfilingSampler m_ProfilingSampler;

			private ScriptableRenderer renderer;

			private bool use32Bit;

			private Material material;

			private Matrix4x4 prevViewProj;

			private Vector2 offset;

			private float blend;

			private bool antiGhosting;

			private bool useSharpenFilter;

			private bool useBicubicFilter;

			private bool useClipAABB;

			private bool useDilation;

			private bool useTonemap;

			private bool useVarianceClipping;

			private bool useYCoCgSpace;

			private float gamma;

			private float sharp;

			private float prevSharp;

			private bool useMotionVector;

			private bool use4Tap;

			private Dictionary<int, TAATextureSwap> taaTextures = new Dictionary<int, TAATextureSwap>();

			private int taaPrevViewProj = Shader.PropertyToID("_TAA_PrevViewProj");

			private int taaOffset = Shader.PropertyToID("_TAA_Offset");

			private int taaParams0 = Shader.PropertyToID("_TAA_Params0");

			private int taaSourceTex = Shader.PropertyToID("_SourceTex");

			private int taaTexture = Shader.PropertyToID("_TAA_Texture");

			public TAAPass(string profilerTag)
			{
				m_ProfilingSampler = new ProfilingSampler(profilerTag);
			}

			public void Setup(ScriptableRenderer renderer, bool use32Bit, Material material, Matrix4x4 prevViewProj, Vector2 offset, TAAComponent taa, bool useMotionVector, RenderingMode renderingMode)
			{
				this.renderer = renderer;
				this.use32Bit = use32Bit;
				this.material = material;
				this.prevViewProj = prevViewProj;
				this.offset = offset;
				blend = taa.Blend;
				antiGhosting = taa.AntiGhosting;
				useSharpenFilter = taa.UseBlurSharpenFilter;
				useBicubicFilter = taa.UseBicubicFilter;
				useClipAABB = taa.UseClipAABB;
				useDilation = taa.UseDilation;
				useTonemap = taa.UseTonemap;
				useVarianceClipping = taa.UseVarianceClipping;
				useYCoCgSpace = taa.UseYCoCgSpace;
				gamma = taa.Stability;
				sharp = taa.SharpenStrength;
				prevSharp = taa.HistorySharpening;
				this.useMotionVector = useMotionVector;
				use4Tap = taa.Use4Tap;
				switch (renderingMode)
				{
				case RenderingMode.Forward:
					if (useMotionVector)
					{
						ConfigureInput(ScriptableRenderPassInput.Depth | ScriptableRenderPassInput.Motion);
					}
					else
					{
						ConfigureInput(ScriptableRenderPassInput.Depth);
					}
					break;
				case RenderingMode.Deferred:
					if (useMotionVector)
					{
						ConfigureInput(ScriptableRenderPassInput.Motion);
					}
					break;
				}
			}

			private void allocRT(out RenderTexture rt_a, out RenderTexture rt_b, RenderTextureDescriptor descriptor)
			{
				rt_a = new RenderTexture(descriptor);
				rt_a.filterMode = FilterMode.Bilinear;
				rt_b = new RenderTexture(descriptor);
				rt_b.filterMode = FilterMode.Bilinear;
			}

			private void allocRT(out RenderTexture rt_a, out RenderTexture rt_b, CommandBuffer cmd, ScriptableRenderContext context, RenderTargetIdentifier rtid, RenderTextureDescriptor descriptor)
			{
				allocRT(out rt_a, out rt_b, descriptor);
				cmd.SetGlobalTexture(taaSourceTex, rtid);
				cmd.SetRenderTarget(rt_a);
				cmd.DrawMesh(RenderingUtils.fullscreenMesh, Matrix4x4.identity, material, 0, 1);
				cmd.SetRenderTarget(rt_b);
				cmd.DrawMesh(RenderingUtils.fullscreenMesh, Matrix4x4.identity, material, 0, 1);
				context.ExecuteCommandBuffer(cmd);
				cmd.Clear();
			}

			private void genRT(CommandBuffer cmd, ScriptableRenderContext context, RenderTargetIdentifier rtid, RenderTextureDescriptor descriptor, int hash)
			{
				descriptor.useMipMap = false;
				descriptor.autoGenerateMips = false;
				descriptor.depthBufferBits = 0;
				descriptor.msaaSamples = 1;
				if (use32Bit)
				{
					descriptor.colorFormat = RenderTextureFormat.ARGBFloat;
				}
				if (taaTextures.ContainsKey(hash))
				{
					if (taaTextures[hash].CheckTextureNull())
					{
						RenderTexture rt_a;
						RenderTexture rt_b;
						allocRT(out rt_a, out rt_b, cmd, context, rtid, descriptor);
						taaTextures[hash] = new TAATextureSwap(rt_a, rt_b, descriptor);
						return;
					}
					RenderTextureDescriptor descriptor2 = taaTextures[hash].Descriptor;
					if (descriptor2.width != descriptor.width || descriptor2.height != descriptor.height || descriptor2.colorFormat != descriptor.colorFormat)
					{
						taaTextures[hash].Release();
						RenderTexture rt_a2;
						RenderTexture rt_b2;
						allocRT(out rt_a2, out rt_b2, cmd, context, rtid, descriptor);
						taaTextures[hash] = new TAATextureSwap(rt_a2, rt_b2, descriptor);
					}
				}
				else
				{
					RenderTexture rt_a3;
					RenderTexture rt_b3;
					allocRT(out rt_a3, out rt_b3, cmd, context, rtid, descriptor);
					taaTextures[hash] = new TAATextureSwap(rt_a3, rt_b3, descriptor);
				}
			}

			public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
			{
				CommandBuffer commandBuffer = CommandBufferPool.Get();
				using (new ProfilingScope(commandBuffer, m_ProfilingSampler))
				{
					Camera camera = renderingData.cameraData.camera;
					RenderTargetIdentifier cameraColorTarget = renderer.cameraColorTarget;
					RenderTextureDescriptor cameraTargetDescriptor = renderingData.cameraData.cameraTargetDescriptor;
					int hashCode = renderingData.cameraData.camera.GetHashCode();
					commandBuffer.SetViewProjectionMatrices(Matrix4x4.identity, Matrix4x4.identity);
					genRT(commandBuffer, context, cameraColorTarget, cameraTargetDescriptor, hashCode);
					material.SetMatrix(taaPrevViewProj, prevViewProj);
					commandBuffer.SetGlobalVector(taaOffset, offset);
					material.SetVector(taaParams0, new Vector4(blend, gamma, sharp, prevSharp));
					CoreUtils.SetKeyword(material, "_TAA_AntiGhosting", antiGhosting);
					CoreUtils.SetKeyword(material, "_TAA_UseMotionVector", !renderingData.cameraData.isSceneViewCamera && useMotionVector);
					CoreUtils.SetKeyword(material, "_TAA_UseBlurSharpenFilter", useSharpenFilter);
					CoreUtils.SetKeyword(material, "_TAA_UseBicubicFilter", useBicubicFilter);
					CoreUtils.SetKeyword(material, "_TAA_UseClipAABB", useClipAABB);
					CoreUtils.SetKeyword(material, "_TAA_UseDilation", useDilation);
					CoreUtils.SetKeyword(material, "_TAA_UseTonemap", useTonemap);
					CoreUtils.SetKeyword(material, "_TAA_UseVarianceClipping", useVarianceClipping);
					CoreUtils.SetKeyword(material, "_TAA_UseYCoCgSpace", useYCoCgSpace);
					CoreUtils.SetKeyword(material, "_TAA_Use4Tap", use4Tap);
					TAATextureSwap tAATextureSwap = taaTextures[hashCode];
					if (tAATextureSwap.Swap())
					{
						commandBuffer.SetGlobalTexture(taaSourceTex, cameraColorTarget);
						material.SetTexture(taaTexture, tAATextureSwap.TAATextureB);
						commandBuffer.SetRenderTarget(tAATextureSwap.TAATextureA);
						commandBuffer.DrawMesh(RenderingUtils.fullscreenMesh, Matrix4x4.identity, material, 0, 0);
						commandBuffer.SetGlobalTexture(taaSourceTex, tAATextureSwap.TAATextureA);
						commandBuffer.SetRenderTarget(cameraColorTarget);
						commandBuffer.DrawMesh(RenderingUtils.fullscreenMesh, Matrix4x4.identity, material, 0, 1);
					}
					else
					{
						commandBuffer.SetGlobalTexture(taaSourceTex, cameraColorTarget);
						material.SetTexture(taaTexture, tAATextureSwap.TAATextureA);
						commandBuffer.SetRenderTarget(tAATextureSwap.TAATextureB);
						commandBuffer.DrawMesh(RenderingUtils.fullscreenMesh, Matrix4x4.identity, material, 0, 0);
						commandBuffer.SetGlobalTexture(taaSourceTex, tAATextureSwap.TAATextureB);
						commandBuffer.SetRenderTarget(cameraColorTarget);
						commandBuffer.DrawMesh(RenderingUtils.fullscreenMesh, Matrix4x4.identity, material, 0, 1);
					}
					commandBuffer.SetViewProjectionMatrices(camera.worldToCameraMatrix, camera.projectionMatrix);
				}
				context.ExecuteCommandBuffer(commandBuffer);
				CommandBufferPool.Release(commandBuffer);
			}

			public void CleanUp(int hash)
			{
				if (taaTextures.ContainsKey(hash))
				{
					taaTextures[hash].Release();
					taaTextures.Remove(hash);
				}
			}
		}

		private struct HaltonSequence
		{
			private int count;

			private int index;

			private float[] arrX;

			private float[] arrY;

			public Matrix4x4 prevViewProj;

			public int frameCount;

			public HaltonSequence(int count)
			{
				this.count = count;
				index = 0;
				arrX = new float[count];
				arrY = new float[count];
				prevViewProj = Matrix4x4.zero;
				frameCount = 0;
				for (int i = 0; i < arrX.Length; i++)
				{
					arrX[i] = get(i, 2);
				}
				for (int j = 0; j < arrY.Length; j++)
				{
					arrY[j] = get(j, 3);
				}
			}

			private float get(int index, int @base)
			{
				float num = 1f;
				float num2 = 0f;
				while (index > 0)
				{
					num /= (float)@base;
					num2 += num * (float)(index % @base);
					index /= @base;
				}
				return num2;
			}

			public void Get(out float x, out float y)
			{
				if (++index == count)
				{
					index = 1;
				}
				x = arrX[index];
				y = arrY[index];
			}
		}

		public bool PreviewInSceneView = true;

		public bool UseMotionVector;

		public RenderingMode RenderingMode;

		public bool WorkOnPrepass;

		public bool IgonreTransparentObject;

		public bool Use32Bit;

		[HideInInspector]
		public Shader Shader;

		private Material material;

		private TAACameraPass m_TAACameraPrepass;

		private TAACameraPass m_TAACameraPass;

		private TAAPass m_TAAPass;

		private Dictionary<int, HaltonSequence> haltonSequences = new Dictionary<int, HaltonSequence>();

		public override void Create()
		{
			m_TAACameraPrepass = new TAACameraPass(base.name);
			m_TAACameraPrepass.renderPassEvent = RenderPassEvent.BeforeRenderingPrePasses;
			m_TAACameraPass = new TAACameraPass(base.name);
			switch (RenderingMode)
			{
			case RenderingMode.Forward:
				m_TAACameraPass.renderPassEvent = RenderPassEvent.BeforeRenderingOpaques;
				break;
			case RenderingMode.Deferred:
				m_TAACameraPass.renderPassEvent = RenderPassEvent.BeforeRenderingGbuffer;
				break;
			}
			m_TAAPass = new TAAPass(base.name);
			if (IgonreTransparentObject)
			{
				m_TAAPass.renderPassEvent = RenderPassEvent.BeforeRenderingTransparents;
			}
			else
			{
				m_TAAPass.renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
			}
		}

		private bool getMaterial()
		{
			if (material == null)
			{
				material = CoreUtils.CreateEngineMaterial(Shader);
				if (material == null)
				{
					return false;
				}
				return true;
			}
			return true;
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			Camera camera = renderingData.cameraData.camera;
			bool isSceneViewCamera = renderingData.cameraData.isSceneViewCamera;
			TAAComponent tAAComponent = ((!isSceneViewCamera || !(Camera.main != null)) ? camera.GetComponent<TAAComponent>() : Camera.main.GetComponent<TAAComponent>());
			if (tAAComponent != null && getMaterial() && (!isSceneViewCamera || PreviewInSceneView) && tAAComponent.Enabled)
			{
				int hashCode = camera.GetHashCode();
				Matrix4x4 projectionMatrix = camera.projectionMatrix;
				Matrix4x4 worldToCameraMatrix = camera.worldToCameraMatrix;
				Matrix4x4 prevViewProj = projectionMatrix * worldToCameraMatrix;
				HaltonSequence value = ((!haltonSequences.ContainsKey(hashCode)) ? new HaltonSequence(1024) : haltonSequences[hashCode]);
				if (value.prevViewProj == Matrix4x4.zero)
				{
					value.prevViewProj = prevViewProj;
				}
				float x;
				float y;
				value.Get(out x, out y);
				Matrix4x4 projectionMatrix2 = camera.projectionMatrix;
				RenderTextureDescriptor cameraTargetDescriptor = renderingData.cameraData.cameraTargetDescriptor;
				if (camera.orthographic)
				{
					projectionMatrix2[0, 3] -= (x * 2f - 1f) / (float)cameraTargetDescriptor.width;
					projectionMatrix2[1, 3] -= (y * 2f - 1f) / (float)cameraTargetDescriptor.height;
				}
				else
				{
					projectionMatrix2[0, 2] += (x * 2f - 1f) / (float)cameraTargetDescriptor.width;
					projectionMatrix2[1, 2] += (y * 2f - 1f) / (float)cameraTargetDescriptor.height;
				}
				Vector2 offset = new Vector2((x - 0.5f) / (float)cameraTargetDescriptor.width, (y - 0.5f) / (float)cameraTargetDescriptor.height);
				if (WorkOnPrepass)
				{
					m_TAACameraPrepass.Setup(projectionMatrix2);
					renderer.EnqueuePass(m_TAACameraPrepass);
				}
				m_TAACameraPass.Setup(projectionMatrix2);
				renderer.EnqueuePass(m_TAACameraPass);
				m_TAAPass.Setup(renderer, Use32Bit, material, value.prevViewProj, offset, tAAComponent, UseMotionVector, RenderingMode);
				renderer.EnqueuePass(m_TAAPass);
				value.prevViewProj = prevViewProj;
				value.frameCount = Time.frameCount;
				haltonSequences[hashCode] = value;
			}
			if (haltonSequences.Count <= 0)
			{
				return;
			}
			int[] array = new int[8];
			int num = 0;
			foreach (KeyValuePair<int, HaltonSequence> haltonSequence in haltonSequences)
			{
				HaltonSequence value2 = haltonSequence.Value;
				if (Time.frameCount - value2.frameCount > 10)
				{
					array[num] = haltonSequence.Key;
					if (++num == 8)
					{
						break;
					}
				}
			}
			if (num > 0)
			{
				for (int i = 0; i < num; i++)
				{
					haltonSequences.Remove(array[i]);
					m_TAAPass.CleanUp(array[i]);
				}
				if (haltonSequences.Count == 0)
				{
					CoreUtils.Destroy(material);
					material = null;
				}
			}
		}
	}
}
