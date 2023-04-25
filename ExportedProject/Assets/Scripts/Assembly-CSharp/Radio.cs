using UnityEngine;

public class Radio : MonoBehaviour
{
	public AudioSource audioSource;

	public AudioClip[] soundtrack;

	public AudioClip noise;

	public bool isActive;

	public bool randomPlay;

	private Material material;

	private int clipOrder;

	private void Start()
	{
		material = GetComponent<Renderer>().material;
		audioSource.loop = false;
	}

	private void Update()
	{
		if (!audioSource.isPlaying && isActive)
		{
			if (randomPlay)
			{
				audioSource.clip = GetRandomClip();
				audioSource.Play();
			}
			else
			{
				audioSource.clip = GetNextClip();
				audioSource.Play();
			}
		}
	}

	public void TurnOn()
	{
		audioSource.clip = noise;
		audioSource.Play();
		material.SetFloat("_Emission", 0.25f);
	}

	public void TurnOff()
	{
		audioSource.Stop();
		material.SetFloat("_Emission", 0f);
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

	private AudioClip GetRandomClip()
	{
		return soundtrack[Random.Range(0, soundtrack.Length)];
	}

	private AudioClip GetNextClip()
	{
		if (clipOrder >= soundtrack.Length - 1)
		{
			clipOrder = 0;
		}
		else
		{
			clipOrder++;
		}
		return soundtrack[clipOrder];
	}
}
