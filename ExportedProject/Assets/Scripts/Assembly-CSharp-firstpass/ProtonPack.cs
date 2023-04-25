using System.Collections;
using UnityEngine;

public class ProtonPack : MonoBehaviour
{
	public GameObject ProtonMainFX;

	public GameObject ProtonExtraFX;

	public AudioSource beamMainAudio;

	public AudioSource beamStartAudio;

	public AudioSource beamStopAudio;

	public ParticleSystem lightningBoltParticles;

	public ParticleSystem protonBeamParticles;

	private int protonBeamFlag;

	private void Start()
	{
		ProtonMainFX.SetActive(false);
		ProtonExtraFX.SetActive(false);
	}

	private void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			StartCoroutine("ProtonPackFire");
		}
		if (Input.GetButtonUp("Fire1"))
		{
			ProtonPackStop();
		}
	}

	private IEnumerator ProtonPackFire()
	{
		ProtonExtraFX.SetActive(true);
		beamStartAudio.Play();
		protonBeamFlag = 0;
		yield return new WaitForSeconds(0.5f);
		if (protonBeamFlag == 0)
		{
			ProtonMainFX.SetActive(true);
			lightningBoltParticles.Play();
			protonBeamParticles.Play();
			beamMainAudio.Play();
		}
	}

	private void ProtonPackStop()
	{
		protonBeamFlag = 1;
		ProtonMainFX.SetActive(false);
		lightningBoltParticles.Stop();
		protonBeamParticles.Stop();
		beamMainAudio.Stop();
		beamStartAudio.Stop();
		beamStopAudio.Play();
	}
}
