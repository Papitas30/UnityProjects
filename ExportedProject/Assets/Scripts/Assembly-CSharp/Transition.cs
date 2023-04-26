using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
	public PlayerData playerData;

	public string scene;

	private Fader fader;

	private AudioManager audioManager;

	private bool hasTransitioned;

	private void Start()
	{
		fader = GameObject.FindWithTag("Fader").GetComponent<Fader>();
		audioManager = GameObject.FindWithTag("Audio Manager").GetComponent<AudioManager>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && !hasTransitioned)
		{
			Load();
			hasTransitioned = true;
		}
	}

	public void Load()
	{
		audioManager.PlayTransition();
		Invoke("Execute", 2f);
		fader.FadeIn();
	}

	private void Execute()
	{
		SceneManager.LoadScene(scene);
	}
}
