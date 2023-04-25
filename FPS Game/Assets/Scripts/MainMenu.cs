using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartGame()
    {
        LoadingData.sceneToLoad = "Game1";
        SceneManager.LoadScene("Loading");
    }
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        System.GC.Collect();
        Application.Quit();
    }
    public void SettingsMenu()
    { }
}
