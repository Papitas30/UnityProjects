using UnityEngine;

public class FrameLock : MonoBehaviour
{
	private void Start()
	{
		QualitySettings.vSyncCount = 1;
	}
}
