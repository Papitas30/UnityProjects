using UnityEngine;
using UnityEngine.Video;

public class Television : MonoBehaviour
{
	public bool isActive;

	public Material material;

	public VideoPlayer videoPlayer;

	public Light televisionLight;

	private void Start()
	{
		TurnOff();
	}

	public void TurnOn()
	{
		videoPlayer.Play();
		material.SetInt("_PlayVideo", 1);
		material.SetFloat("_Emission", 4f);
		televisionLight.enabled = true;
	}

	public void TurnOff()
	{
		videoPlayer.Pause();
		material.SetInt("_PlayVideo", 0);
		material.SetFloat("_Emission", 0f);
		televisionLight.enabled = false;
	}

	public void Selected()
	{
		isActive = !isActive;
		if (isActive)
		{
			TurnOn();
		}
		else
		{
			TurnOff();
		}
	}
}
