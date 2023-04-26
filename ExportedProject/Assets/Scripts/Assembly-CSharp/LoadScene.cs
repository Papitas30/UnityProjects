using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
	private string scene;

	private Fader fader;

	public void Load(string sceneName)
	{
		fader = GameObject.FindWithTag("Fader").GetComponent<Fader>();
		fader.FadeIn();
		scene = sceneName;
		Invoke("Execute", 2f);
	}

	private void Execute()
	{
		SceneManager.LoadScene(scene);
	}
}
