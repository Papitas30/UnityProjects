using UnityEngine;

[CreateAssetMenu(fileName = "New Audio Event", menuName = "Events/Audio Event", order = 2)]
public class AudioEvent : ScriptableObject
{
	public AudioClip[] audioClips;

	[Header("Volume")]
	[Range(0f, 1f)]
	public float minVolume;

	[Range(0f, 1f)]
	public float maxVolume;

	[Header("Pitch")]
	[Range(0f, 1f)]
	public float minPitch;

	[Range(0f, 1f)]
	public float maxPitch;

	public void Play(AudioSource audioSource, float volume)
	{
		if (audioClips.Length != 0)
		{
			audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
			audioSource.volume = Random.Range(minVolume * volume, maxVolume * volume);
			audioSource.pitch = Random.Range(minPitch, maxPitch);
			audioSource.PlayOneShot(audioSource.clip, 1f);
		}
	}
}
