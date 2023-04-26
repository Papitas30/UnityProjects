using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public GameObject rotateIcon;
    private Vector3 localAngles;
    public float rotatingSpeed;
    public float loadingProgress;
    public Slider progressBar;
    public float minLoadingTime;
    private AsyncOperation loadingOperation;
    // Start is called before the first frame update
    private IEnumerator Start()
    {
        float startTime = Time.time;
        loadingOperation = SceneManager.LoadSceneAsync(LoadingData.sceneToLoad, LoadSceneMode.Additive);
        loadingOperation.allowSceneActivation = false;

        while (!loadingOperation.isDone)
        {
            loadingProgress = loadingOperation.progress;
            if (loadingProgress == .9f)
            {
                break;
            }
            yield return null;
        }

        if (minLoadingTime > 0)
        {
            float waitTime = minLoadingTime - (Time.time - startTime);
            float totalWaitTime = waitTime;

            while (waitTime > 0f)
            {
                waitTime -= Time.deltaTime;
                loadingProgress = 1 - waitTime / totalWaitTime;
                yield return null;
            }
        }

        loadingOperation.allowSceneActivation = true;
        yield return null;

        if (loadingOperation.isDone)
        {
            AsyncOperation unused1 = SceneManager.UnloadSceneAsync("Loading");
            AsyncOperation unused = Resources.UnloadUnusedAssets();
            Debug.Log($"Finished loading operation for '{LoadingData.sceneToLoad}'");
        }
    }

    // Update is called once per frame
    private void Update()
    {
        localAngles += rotatingSpeed * Time.deltaTime * new Vector3(0, 0, 1);
        rotateIcon.transform.eulerAngles = -localAngles;
        progressBar.value = Mathf.Clamp01(loadingOperation.progress / 0.9f);

    }
}