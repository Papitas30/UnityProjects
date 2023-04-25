using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

namespace OccaSoftware.Buto
{
	public class ButoVolumetricLightingAndFog : ScriptableRendererFeature
	{
		private class RenderFogPass : ScriptableRenderPass
		{
			private class TAACameraData
			{
				private uint lastFrameUsed;

				private RenderTexture renderTexture;

				private string cameraName;

				public uint LastFrameUsed
				{
					get
					{
						return lastFrameUsed;
					}
					set
					{
						lastFrameUsed = value;
					}
				}

				public RenderTexture RenderTexture
				{
					get
					{
						return renderTexture;
					}
					set
					{
						renderTexture = value;
					}
				}

				public string CameraName
				{
					get
					{
						return cameraName;
					}
					set
					{
						cameraName = value;
					}
				}

				public TAACameraData(uint lastFrameUsed, RenderTexture renderTexture, string cameraName)
				{
					LastFrameUsed = lastFrameUsed;
					RenderTexture = renderTexture;
					CameraName = cameraName;
				}
			}

			[StructLayout(LayoutKind.Auto)]
			[CompilerGenerated]
			private struct _003C_003Ec__DisplayClass35_0
			{
				public Camera camera;

				public RenderFogPass _003C_003E4__this;

				public Color worldcolor;
			}

			private RenderTargetIdentifier source;

			private RenderTextureDescriptor fogTargetDescriptor;

			private RenderTargetHandle fogTarget;

			private RenderTargetHandle finalMergeTarget;

			private RenderTargetHandle lowResDepthTarget;

			private VolumetricFog volumetricFog;

			private Material fogMaterial;

			private Material mergeMaterial;

			private Material depthMaterial;

			private const string fogShaderPath = "Shader Graphs/VolumetricFogShader";

			private const string mergeShaderPath = "Shader Graphs/VolumetricFogMergeShader";

			private const string depthDownscaleShaderPath = "Shader Graphs/DepthDownscaleShader";

			private const string mergeInputTexId = "_FOG_MERGE_INPUT_TEX";

			private const string lowResDepthTexId = "_DOWNSCALED_DEPTH_TEX";

			private bool isFirst = true;

			private const string taaInputTexId = "_BUTO_HISTORY_TEX";

			private Material taaMaterial;

			private const string taaShaderPath = "Shader Graphs/ButoTemporalAA";

			private RenderTargetHandle taaTarget;

			private Dictionary<Camera, TAACameraData> renderTextures;

			private float managedTime;

			private uint frameCount;

			private void CalculateTime()
			{
				float realtimeSinceStartup = Time.realtimeSinceStartup;
				uint num = (uint)Time.frameCount;
				bool flag;
				if (Application.isPlaying)
				{
					flag = frameCount != num;
					frameCount = num;
				}
				else
				{
					flag = realtimeSinceStartup - managedTime > 0.0166f;
					if (flag)
					{
						frameCount++;
					}
				}
				if (flag)
				{
					managedTime = realtimeSinceStartup;
				}
			}

			private void GetTemporalAARenderTexture(Camera camera, RenderTextureDescriptor descriptor)
			{
				TAACameraData value;
				if (renderTextures.TryGetValue(camera, out value))
				{
					CheckRenderTextureScale(camera, descriptor, value.RenderTexture);
				}
				else
				{
					CreateTAARenderTextureAndAddToDictionary(camera, descriptor);
				}
			}

			private void CheckRenderTextureScale(Camera camera, RenderTextureDescriptor descriptor, RenderTexture renderTexture)
			{
				if (renderTexture == null)
				{
					CreateTAARenderTextureAndAddToDictionary(camera, descriptor);
				}
				else if ((renderTexture.width != descriptor.width || renderTexture.height != descriptor.height) ? true : false)
				{
					CreateTAARenderTextureAndAddToDictionary(camera, descriptor);
				}
			}

			private void CreateTAARenderTextureAndAddToDictionary(Camera camera, RenderTextureDescriptor descriptor)
			{
				RenderTexture renderTexture;
				SetupTAARenderTexture(camera, descriptor, out renderTexture);
				if (renderTextures.ContainsKey(camera))
				{
					if (renderTextures[camera].RenderTexture != null)
					{
						renderTextures[camera].RenderTexture.Release();
					}
					renderTextures[camera].RenderTexture = renderTexture;
				}
				else
				{
					renderTextures.Add(camera, new TAACameraData(frameCount, renderTexture, camera.name));
				}
			}

			private void SetupTAARenderTexture(Camera camera, RenderTextureDescriptor descriptor, out RenderTexture renderTexture)
			{
				descriptor.colorFormat = RenderTextureFormat.DefaultHDR;
				renderTexture = new RenderTexture(descriptor);
				RenderTexture active = RenderTexture.active;
				RenderTexture.active = renderTexture;
				GL.Clear(false, true, new Color(0f, 0f, 0f, 1f));
				RenderTexture.active = active;
				renderTexture.name = camera.name + " Buto TAA History";
				renderTexture.filterMode = FilterMode.Point;
				renderTexture.wrapMode = TextureWrapMode.Clamp;
				renderTexture.Create();
			}

			private void CleanupDictionary()
			{
				List<Camera> list = new List<Camera>();
				foreach (KeyValuePair<Camera, TAACameraData> renderTexture in renderTextures)
				{
					if (renderTexture.Value.LastFrameUsed < frameCount - 10)
					{
						if (renderTexture.Value.RenderTexture != null)
						{
							renderTexture.Value.RenderTexture.Release();
						}
						list.Add(renderTexture.Key);
					}
				}
				for (int i = 0; i < list.Count; i++)
				{
					renderTextures.Remove(list[i]);
				}
			}

			internal void SetupMaterials()
			{
				GetShaderAndSetupMaterial("Shader Graphs/VolumetricFogShader", ref fogMaterial);
				GetShaderAndSetupMaterial("Shader Graphs/VolumetricFogMergeShader", ref mergeMaterial);
				GetShaderAndSetupMaterial("Shader Graphs/DepthDownscaleShader", ref depthMaterial);
				GetShaderAndSetupMaterial("Shader Graphs/ButoTemporalAA", ref taaMaterial);
			}

			private void GetShaderAndSetupMaterial(string path, ref Material target)
			{
				if (!(target != null))
				{
					Shader shader = Shader.Find(path);
					if (shader != null)
					{
						target = CoreUtils.CreateEngineMaterial(shader);
					}
					else
					{
						Debug.Log("Buto missing shader reference at " + path);
					}
				}
			}

			public RenderFogPass()
			{
				renderTextures = new Dictionary<Camera, TAACameraData>();
				fogTarget.Init("Buto Fog Render Target");
				taaTarget.Init("Buto TAA Target");
				lowResDepthTarget.Init("Buto Low Res Depth Target");
				finalMergeTarget.Init("Buto Final Merge Target");
			}

			public bool RegisterStackComponent()
			{
				volumetricFog = VolumeManager.instance.stack.GetComponent<VolumetricFog>();
				if (volumetricFog == null)
				{
					return false;
				}
				return volumetricFog.IsActive();
			}

			public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
			{
				RenderTextureDescriptor cameraTargetDescriptor = renderingData.cameraData.cameraTargetDescriptor;
				cameraTargetDescriptor.colorFormat = RenderTextureFormat.DefaultHDR;
				fogTargetDescriptor = cameraTargetDescriptor;
				fogTargetDescriptor.width /= 2;
				fogTargetDescriptor.height /= 2;
				cmd.GetTemporaryRT(fogTarget.id, fogTargetDescriptor);
				cmd.GetTemporaryRT(lowResDepthTarget.id, fogTargetDescriptor);
				cmd.GetTemporaryRT(taaTarget.id, fogTargetDescriptor);
				cmd.GetTemporaryRT(finalMergeTarget.id, cameraTargetDescriptor);
			}

			internal bool HasAllMaterials()
			{
				if (fogMaterial != null && depthMaterial != null && mergeMaterial != null && taaMaterial != null)
				{
					return true;
				}
				return false;
			}

			public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
			{
				_003C_003Ec__DisplayClass35_0 _003C_003Ec__DisplayClass35_ = default(_003C_003Ec__DisplayClass35_0);
				_003C_003Ec__DisplayClass35_._003C_003E4__this = this;
				CalculateTime();
				CleanupDictionary();
				source = renderingData.cameraData.renderer.cameraColorTarget;
				_003C_003Ec__DisplayClass35_.camera = renderingData.cameraData.camera;
				CommandBuffer commandBuffer = CommandBufferPool.Get("ButoRenderPass");
				_003CExecute_003Eg__SetMaterialParameters_007C35_1(ref _003C_003Ec__DisplayClass35_);
				CoreUtils.SetKeyword(commandBuffer, "_DBUFFER_MRT1", false);
				CoreUtils.SetKeyword(commandBuffer, "_DBUFFER_MRT2", false);
				CoreUtils.SetKeyword(commandBuffer, "_DBUFFER_MRT3", false);
				Blit(commandBuffer, source, fogTarget.Identifier(), fogMaterial);
				RenderTargetHandle renderTargetHandle = fogTarget;
				if (_003CExecute_003Eg__IsTaaEnabled_007C35_0(ref _003C_003Ec__DisplayClass35_))
				{
					GetTemporalAARenderTexture(renderingData.cameraData.camera, fogTargetDescriptor);
					if (renderTextures[_003C_003Ec__DisplayClass35_.camera].RenderTexture != null)
					{
						taaMaterial.SetTexture("_BUTO_HISTORY_TEX", renderTextures[_003C_003Ec__DisplayClass35_.camera].RenderTexture);
					}
					Blit(commandBuffer, fogTarget.Identifier(), taaTarget.Identifier(), taaMaterial);
					if (renderTextures[_003C_003Ec__DisplayClass35_.camera].RenderTexture == null)
					{
						Debug.Log("Buto Temporal AA Render Texture is missing. Please submit bug report. Missing Texture: " + renderTextures[_003C_003Ec__DisplayClass35_.camera].CameraName + " RT");
					}
					else
					{
						Blit(commandBuffer, taaTarget.Identifier(), renderTextures[_003C_003Ec__DisplayClass35_.camera].RenderTexture);
						renderTextures[_003C_003Ec__DisplayClass35_.camera].LastFrameUsed = frameCount;
					}
					renderTargetHandle = taaTarget;
				}
				commandBuffer.SetGlobalTexture("_FOG_MERGE_INPUT_TEX", renderTargetHandle.Identifier());
				Blit(commandBuffer, source, lowResDepthTarget.Identifier(), depthMaterial);
				commandBuffer.SetGlobalTexture("_DOWNSCALED_DEPTH_TEX", lowResDepthTarget.Identifier());
				Blit(commandBuffer, source, finalMergeTarget.Identifier(), mergeMaterial);
				Blit(commandBuffer, finalMergeTarget.Identifier(), source);
				context.ExecuteCommandBuffer(commandBuffer);
				commandBuffer.Clear();
				CommandBufferPool.Release(commandBuffer);
			}

			public override void OnCameraCleanup(CommandBuffer cmd)
			{
				cmd.ReleaseTemporaryRT(fogTarget.id);
				cmd.ReleaseTemporaryRT(taaTarget.id);
				cmd.ReleaseTemporaryRT(lowResDepthTarget.id);
				cmd.ReleaseTemporaryRT(finalMergeTarget.id);
			}

			[CompilerGenerated]
			private bool _003CExecute_003Eg__IsTaaEnabled_007C35_0(ref _003C_003Ec__DisplayClass35_0 P_0)
			{
				if (P_0.camera.cameraType != CameraType.Game)
				{
					return false;
				}
				if (!volumetricFog.temporalAntiAliasingEnabled.value)
				{
					return false;
				}
				return true;
			}

			[CompilerGenerated]
			private void _003CExecute_003Eg__SetMaterialParameters_007C35_1(ref _003C_003Ec__DisplayClass35_0 P_0)
			{
				P_0.worldcolor = _003CExecute_003Eg__SetSphericalHarmonics_007C35_3(ref P_0);
				_003CExecute_003Eg__SetFogMaterialData_007C35_2(ref P_0);
				_003CExecute_003Eg__SetAdditionalLightData_007C35_5(ref P_0);
				_003CExecute_003Eg__SetVolumeData_007C35_6(ref P_0);
				_003CExecute_003Eg__SetTaaIntegrationRate_007C35_4(ref P_0);
			}

			[CompilerGenerated]
			private void _003CExecute_003Eg__SetFogMaterialData_007C35_2(ref _003C_003Ec__DisplayClass35_0 P_0)
			{
				fogMaterial.SetInt(Params.SampleCount.Id, volumetricFog.sampleCount.value);
				fogMaterial.SetInt(Params.AnimateSamplePosition.Id, _003CExecute_003Eg__BoolToInt_007C35_8(volumetricFog.animateSamplePosition.value));
				_003CExecute_003Eg__SetKeyword_007C35_7(fogMaterial, Params.EnableSelfShadowing.Property, volumetricFog.selfShadowingEnabled.value);
				fogMaterial.SetInt(Params.MaximumSelfShadowingOctaves.Id, volumetricFog.maximumSelfShadowingOctaves.value);
				_003CExecute_003Eg__SetKeyword_007C35_7(fogMaterial, Params.EnableHorizonShadowing.Property, volumetricFog.horizonShadowingEnabled.value);
				_003CExecute_003Eg__SetKeyword_007C35_7(fogMaterial, Params.AnalyticFogEnabled.Property, volumetricFog.analyticFogEnabled.value);
				fogMaterial.SetInt(Params.FogMaskBlendMode.Id, (int)volumetricFog.fogMaskBlendMode.value);
				fogMaterial.SetFloat(Params.MaxDistanceVolumetric.Id, volumetricFog.maxDistanceVolumetric.value);
				fogMaterial.SetFloat(Params.MaxDistanceNonVolumetric.Id, volumetricFog.maxDistanceAnalytic.value);
				fogMaterial.SetFloat(Params.Anisotropy.Id, volumetricFog.anisotropy.value);
				fogMaterial.SetFloat(Params.BaseHeight.Id, volumetricFog.baseHeight.value);
				fogMaterial.SetFloat(Params.AttenuationBoundarySize.Id, volumetricFog.attenuationBoundarySize.value);
				fogMaterial.SetFloat(Params.FogDensity.Id, volumetricFog.fogDensity.value);
				fogMaterial.SetFloat(Params.LightIntensity.Id, volumetricFog.lightIntensity.value);
				fogMaterial.SetFloat(Params.ShadowIntensity.Id, volumetricFog.shadowIntensity.value);
				fogMaterial.SetTexture(Params.ColorRamp.Id, volumetricFog.colorRamp.value);
				fogMaterial.SetFloat(Params.ColorRampInfluence.Id, volumetricFog.colorRampInfluence.value);
				fogMaterial.SetTexture(Params.NoiseTexture.Id, volumetricFog.noiseTexure.value);
				fogMaterial.SetInt(Params.Octaves.Id, volumetricFog.octaves.value);
				fogMaterial.SetFloat(Params.NoiseTiling.Id, volumetricFog.noiseTiling.value);
				fogMaterial.SetVector(Params.NoiseWindSpeed.Id, volumetricFog.noiseWindSpeed.value);
				fogMaterial.SetFloat(Params.NoiseIntensityMin.Id, volumetricFog.noiseMap.value.x);
				fogMaterial.SetFloat(Params.NoiseIntensityMax.Id, volumetricFog.noiseMap.value.y);
				fogMaterial.SetFloat(Params.Lacunarity.Id, volumetricFog.lacunarity.value);
				fogMaterial.SetFloat(Params.Gain.Id, volumetricFog.gain.value);
			}

			[CompilerGenerated]
			private Color _003CExecute_003Eg__SetSphericalHarmonics_007C35_3(ref _003C_003Ec__DisplayClass35_0 P_0)
			{
				SphericalHarmonicsL2 probe;
				LightProbes.GetInterpolatedProbe(P_0.camera.transform.position, null, out probe);
				Vector3[] directions = new Vector3[1]
				{
					new Vector3(0f, 1f, 0f)
				};
				Color[] array = new Color[1];
				probe.Evaluate(directions, array);
				fogMaterial.SetColor("_WorldColor", array[0]);
				return array[0];
			}

			[CompilerGenerated]
			private void _003CExecute_003Eg__SetTaaIntegrationRate_007C35_4(ref _003C_003Ec__DisplayClass35_0 P_0)
			{
				float value = volumetricFog.temporalAntiAliasingIntegrationRate.value;
				if (isFirst)
				{
					isFirst = false;
					value = 1f;
				}
				taaMaterial.SetFloat(Params.TemporalAaIntegrationRate.Id, value);
			}

			[CompilerGenerated]
			private void _003CExecute_003Eg__SetAdditionalLightData_007C35_5(ref _003C_003Ec__DisplayClass35_0 P_0)
			{
				Vector4[] array = new Vector4[ButoCommon._MAXLIGHTCOUNT];
				float[] array2 = new float[ButoCommon._MAXLIGHTCOUNT];
				Vector4[] array3 = new Vector4[ButoCommon._MAXLIGHTCOUNT];
				if (ButoLight.Lights.Count > ButoCommon._MAXLIGHTCOUNT)
				{
					ButoLight.SortByDistance(P_0.camera.transform.position);
				}
				int num = Mathf.Min(ButoLight.Lights.Count, ButoCommon._MAXLIGHTCOUNT);
				for (int i = 0; i < num; i++)
				{
					array[i] = ButoLight.Lights[i].transform.position;
					array2[i] = ButoLight.Lights[i].LightIntensity;
					array3[i] = ButoLight.Lights[i].LightColor;
				}
				fogMaterial.SetInt(Params.LightCountButo.Id, num);
				if (num > 0)
				{
					fogMaterial.SetVectorArray(Params.LightPosButo.Id, array);
					fogMaterial.SetFloatArray(Params.LightIntensityButo.Id, array2);
					fogMaterial.SetVectorArray(Params.LightColorButo.Id, array3);
				}
			}

			[CompilerGenerated]
			private void _003CExecute_003Eg__SetVolumeData_007C35_6(ref _003C_003Ec__DisplayClass35_0 P_0)
			{
				Vector4[] array = new Vector4[ButoCommon._MAXVOLUMECOUNT];
				float[] array2 = new float[ButoCommon._MAXVOLUMECOUNT];
				if (FogDensityMask.FogVolumes.Count > ButoCommon._MAXVOLUMECOUNT)
				{
					FogDensityMask.SortByDistance(P_0.camera.transform.position);
				}
				int num = Mathf.Min(FogDensityMask.FogVolumes.Count, ButoCommon._MAXVOLUMECOUNT);
				for (int i = 0; i < num; i++)
				{
					array[i] = FogDensityMask.FogVolumes[i].transform.position;
					array[i].w = FogDensityMask.FogVolumes[i].Radius;
					array2[i] = FogDensityMask.FogVolumes[i].DensityMultiplier;
				}
				fogMaterial.SetInt(Params.VolumeCountButo.Id, num);
				if (num > 0)
				{
					fogMaterial.SetVectorArray(Params.VolumeData.Id, array);
					fogMaterial.SetFloatArray(Params.VolumeIntensity.Id, array2);
				}
			}

			[CompilerGenerated]
			internal static void _003CExecute_003Eg__SetKeyword_007C35_7(Material m, string keyword, bool value)
			{
				if (value)
				{
					m.EnableKeyword(keyword);
				}
				else
				{
					m.DisableKeyword(keyword);
				}
			}

			[CompilerGenerated]
			internal static int _003CExecute_003Eg__BoolToInt_007C35_8(bool a)
			{
				if (a)
				{
					return 1;
				}
				return 0;
			}
		}

		private static class Params
		{
			[IsReadOnly]
			public struct Param
			{
				public readonly string Property;

				public readonly int Id;

				public Param(string property)
				{
					Property = property;
					Id = Shader.PropertyToID(property);
				}
			}

			public static Param SampleCount = new Param("_SampleCount");

			public static Param AnimateSamplePosition = new Param("_AnimateSamplePosition");

			public static Param EnableSelfShadowing = new Param("_BUTO_SELF_ATTENUATION_ENABLED");

			public static Param EnableHorizonShadowing = new Param("_BUTO_HORIZON_SHADOWING_ENABLED");

			public static Param MaxDistanceVolumetric = new Param("_MaxDistanceVolumetric");

			public static Param AnalyticFogEnabled = new Param("_BUTO_ANALYTIC_FOG_ENABLED");

			public static Param MaxDistanceNonVolumetric = new Param("_MaxDistanceNonVolumetric");

			public static Param MaximumSelfShadowingOctaves = new Param("_MaximumSelfShadowingOctaves");

			public static Param FogMaskBlendMode = new Param("_FogMaskBlendMode");

			public static Param TemporalAaIntegrationRate = new Param("_IntegrationRate");

			public static Param FogDensity = new Param("_FogDensity");

			public static Param Anisotropy = new Param("_Anisotropy");

			public static Param LightIntensity = new Param("_LightIntensity");

			public static Param ShadowIntensity = new Param("_ShadowIntensity");

			public static Param BaseHeight = new Param("_BaseHeight");

			public static Param AttenuationBoundarySize = new Param("_AttenuationBoundarySize");

			public static Param ColorRamp = new Param("_ColorRamp");

			public static Param ColorRampInfluence = new Param("_ColorRampInfluence");

			public static Param NoiseTexture = new Param("_NoiseTexture");

			public static Param Octaves = new Param("_Octaves");

			public static Param Lacunarity = new Param("_Lacunarity");

			public static Param Gain = new Param("_Gain");

			public static Param NoiseTiling = new Param("_NoiseTiling");

			public static Param NoiseWindSpeed = new Param("_NoiseWindSpeed");

			public static Param NoiseIntensityMin = new Param("_NoiseIntensityMin");

			public static Param NoiseIntensityMax = new Param("_NoiseIntensityMax");

			public static Param LightCountButo = new Param("_LightCountButo");

			public static Param LightPosButo = new Param("_LightPosButo");

			public static Param LightIntensityButo = new Param("_LightIntensityButo");

			public static Param LightColorButo = new Param("_LightColorButo");

			public static Param VolumeCountButo = new Param("_VolumeCountButo");

			public static Param VolumeData = new Param("_VolumeDataButo");

			public static Param VolumeIntensity = new Param("_VolumeIntensityButo");
		}

		private RenderFogPass renderFogPass;

		private void OnEnable()
		{
			SceneManager.activeSceneChanged += Recreate;
		}

		private void OnDisable()
		{
			SceneManager.activeSceneChanged -= Recreate;
		}

		private void Recreate(Scene current, Scene next)
		{
			Create();
		}

		public override void Create()
		{
			renderFogPass = new RenderFogPass();
			renderFogPass.renderPassEvent = RenderPassEvent.BeforeRenderingTransparents;
		}

		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			if (renderingData.cameraData.camera.cameraType != CameraType.Reflection && renderFogPass.RegisterStackComponent())
			{
				renderFogPass.SetupMaterials();
				if (renderFogPass.HasAllMaterials())
				{
					renderFogPass.ConfigureInput(ScriptableRenderPassInput.Motion);
					renderer.EnqueuePass(renderFogPass);
				}
			}
		}
	}
}
