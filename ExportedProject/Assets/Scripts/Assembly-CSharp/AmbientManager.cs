using UnityEngine;

public class AmbientManager : MonoBehaviour
{
	[Header("Audio Sources")]
	public AudioSource ambientAudio;

	[Header("Ambient Audio")]
	public AudioClip dayNeutral;

	public AudioClip dayRain;

	public void DayNeutralAmbient()
	{
		ambientAudio.clip = dayNeutral;
	}

	public void DayRainAmbient()
	{
		ambientAudio.clip = dayRain;
	}
}
