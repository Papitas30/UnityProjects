using UnityEngine;

public class Fader : MonoBehaviour
{
	private Animator fader;

	private void Start()
	{
		fader = GetComponent<Animator>();
		fader.Play("Fade Out");
	}

	public void FadeOut()
	{
		fader.Play("Fade Out");
	}

	public void FadeIn()
	{
		fader.Play("Fade In");
	}
}
