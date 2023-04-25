using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SRPProfiler : MonoBehaviour
{
	internal class RecorderEntry
	{
		public string name;

		public string oldName;

		public int callCount;

		public float accTime;

		public Recorder recorder;
	}

	private enum SRPBMarkers
	{
		kStdRenderDraw = 0,
		kStdShadowDraw = 1,
		kSRPBRenderDraw = 2,
		kSRPBShadowDraw = 3,
		kRenderThreadIdle = 4,
		kStdRenderApplyShader = 5,
		kStdShadowApplyShader = 6,
		kSRPBRenderApplyShader = 7,
		kSRPBShadowApplyShader = 8,
		kPrepareBatchRendererGroupNodes = 9
	}

	public bool m_Enable = true;

	[Header("Profiler GUI")]
	public Text currentFPS;

	public Text averageFPS;

	private const float profilerCycle = 1f;

	private int frameCount;

	private float m_AccDeltaTime;

	private bool m_oldBatcherEnable;

	private float fps;

	private float frameTime;

	private RecorderEntry[] recordersList = new RecorderEntry[10]
	{
		new RecorderEntry
		{
			name = "RenderLoop.Draw"
		},
		new RecorderEntry
		{
			name = "Shadows.Draw"
		},
		new RecorderEntry
		{
			name = "SRPBatcher.Draw",
			oldName = "RenderLoopNewBatcher.Draw"
		},
		new RecorderEntry
		{
			name = "SRPBatcherShadow.Draw",
			oldName = "ShadowLoopNewBatcher.Draw"
		},
		new RecorderEntry
		{
			name = "RenderLoopDevice.Idle"
		},
		new RecorderEntry
		{
			name = "StdRender.ApplyShader"
		},
		new RecorderEntry
		{
			name = "StdShadow.ApplyShader"
		},
		new RecorderEntry
		{
			name = "SRPBRender.ApplyShader"
		},
		new RecorderEntry
		{
			name = "SRPBShadow.ApplyShader"
		},
		new RecorderEntry
		{
			name = "PrepareBatchRendererGroupNodes"
		}
	};

	private void Awake()
	{
		for (int i = 0; i < recordersList.Length; i++)
		{
			Sampler sampler = Sampler.Get(recordersList[i].name);
			if (sampler.isValid)
			{
				recordersList[i].recorder = sampler.GetRecorder();
			}
			else if (recordersList[i].oldName != null)
			{
				sampler = Sampler.Get(recordersList[i].oldName);
				if (sampler.isValid)
				{
					recordersList[i].recorder = sampler.GetRecorder();
				}
			}
		}
		m_oldBatcherEnable = m_Enable;
		ResetStats();
	}

	private void RazCounters()
	{
		m_AccDeltaTime = 0f;
		frameCount = 0;
		for (int i = 0; i < recordersList.Length; i++)
		{
			recordersList[i].accTime = 0f;
			recordersList[i].callCount = 0;
		}
	}

	private void ResetStats()
	{
		RazCounters();
	}

	private void ToggleStats()
	{
		m_Enable = !m_Enable;
		ResetStats();
	}

	private void Update()
	{
		if (GraphicsSettings.useScriptableRenderPipelineBatching != m_oldBatcherEnable)
		{
			ResetStats();
			m_oldBatcherEnable = GraphicsSettings.useScriptableRenderPipelineBatching;
		}
		if (m_Enable)
		{
			m_AccDeltaTime += Time.unscaledDeltaTime;
			frameCount++;
			for (int i = 0; i < recordersList.Length; i++)
			{
				if (recordersList[i].recorder != null)
				{
					recordersList[i].accTime += (float)recordersList[i].recorder.elapsedNanoseconds / 1000000f;
					recordersList[i].callCount += recordersList[i].recorder.sampleBlockCount;
				}
			}
			if (m_AccDeltaTime >= 1f)
			{
				float num = 1f / (float)frameCount;
				frameTime = m_AccDeltaTime * 1000f * num;
				fps = (float)frameCount / m_AccDeltaTime;
				RazCounters();
			}
		}
		currentFPS.text = fps.ToString("F0");
		averageFPS.text = frameTime.ToString("F1");
	}
}
