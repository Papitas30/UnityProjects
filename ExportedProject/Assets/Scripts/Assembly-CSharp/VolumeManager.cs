using UnityEngine;
using UnityEngine.Rendering;

public class VolumeManager : MonoBehaviour
{
	[Header("Volumes")]
	public Volume mainVolume;

	public Volume UIVolume;

	public void ShowUIVolume()
	{
		UIVolume.enabled = true;
	}

	public void HideUIVolume()
	{
		UIVolume.enabled = false;
	}
}
