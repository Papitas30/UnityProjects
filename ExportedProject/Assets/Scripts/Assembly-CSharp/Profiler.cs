using UnityEngine;
using UnityEngine.UI;

public class Profiler : MonoBehaviour
{
	[Header("Profiling Period")]
	public float period = 1f;

	[Header("Profiler GUI")]
	public Text currentFPS;

	public Text averageFPS;

	private float fps;

	private int framesPassed;

	private float fpsTotal;

	private float time;

	public SRPProfiler SRPProfiler;

	private void Start()
	{
		SRPProfiler = GetComponent<SRPProfiler>();
	}

	private void Update()
	{
		Profile();
	}

	private void Profile()
	{
	}
}
