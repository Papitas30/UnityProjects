using UnityEngine;

public class Quit : MonoBehaviour
{
	private Fader fader;

	public void QuitGame()
	{
		fader = GameObject.FindWithTag("Fader").GetComponent<Fader>();
		fader.FadeIn();
		Invoke("Execute", 2f);
	}

	private void Execute()
	{
		Application.Quit();
	}
}
