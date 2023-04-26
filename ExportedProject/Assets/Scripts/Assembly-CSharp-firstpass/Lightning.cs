using System.Collections;
using UnityEngine;

public class Lightning : MonoBehaviour
{
	public float offMin = 10f;

	public float offMax = 60f;

	public AudioSource ThunderAudioA;

	public AudioSource ThunderAudioB;

	public AudioSource ThunderAudioC;

	public AudioSource ThunderAudioD;

	public GameObject LightningBolt;

	private float onMin = 0.25f;

	private float onMax = 2f;

	private int ThunderRND = 1;

	private float ThunderVol;

	private float ThunderWait;

	private void Start()
	{
		StartCoroutine("Storm");
	}

	private IEnumerator Storm()
	{
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(offMin, offMax));
			LightningBolt.SetActive(true);
			LightningBolt.transform.Rotate(0f, Random.Range(1, 360), 0f);
			StartCoroutine("Soundfx");
			yield return new WaitForSeconds(Random.Range(onMin, onMax));
			LightningBolt.SetActive(false);
		}
	}

	private IEnumerator Soundfx()
	{
		ThunderRND = Random.Range(1, 5);
		ThunderVol = Random.Range(0.2f, 1f);
		for (ThunderWait = 9f - ThunderVol * 3f * 3f - 2f; ThunderRND == 1; ThunderRND = 0)
		{
			yield return new WaitForSeconds(ThunderWait);
			ThunderAudioA.volume = ThunderVol;
			ThunderAudioA.Play();
		}
		while (ThunderRND == 2)
		{
			yield return new WaitForSeconds(ThunderWait);
			ThunderAudioB.volume = ThunderVol;
			ThunderAudioB.Play();
			ThunderRND = 0;
		}
		while (ThunderRND == 3)
		{
			yield return new WaitForSeconds(ThunderWait);
			ThunderAudioC.volume = ThunderVol;
			ThunderAudioC.Play();
			ThunderRND = 0;
		}
		while (ThunderRND == 4)
		{
			yield return new WaitForSeconds(ThunderWait);
			ThunderAudioD.volume = ThunderVol;
			ThunderAudioD.Play();
			ThunderRND = 0;
		}
	}
}
