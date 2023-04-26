using UnityEngine;

[CreateAssetMenu(fileName = "New Ambient Event", menuName = "Events/Ambient Event", order = 2)]
public class AmbientEvent : ScriptableObject
{
	public AudioClip[] audioClips;

	[Header("Volume")]
	[Range(0f, 1f)]
	public float minVolume = 1f;

	[Range(0f, 1f)]
	public float maxVolume = 1f;

	[Header("Pitch")]
	[Range(0f, 1f)]
	public float minPitch = 1f;

	[Range(0f, 1f)]
	public float maxPitch = 1f;

	public void Play(AudioSource audioSource, float volume)
	{
		if (audioClips.Length != 0)
		{
			audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
			audioSource.volume = Random.Range(minVolume * volume, maxVolume * volume);
			audioSource.pitch = Random.Range(minPitch, maxPitch);
			audioSource.Play();
		}
	}
}
