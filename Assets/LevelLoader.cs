using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {

    public GameObject LoadingCanvas;
    public Slider LoadingBar;

    public void LoadLevel(int scene)
    {
        StartCoroutine(LoadAsync(scene));
    }

    public void LoadLevel(string scene)
    {
        StartCoroutine(LoadAsync(SceneManager.GetSceneByName(scene).buildIndex));
    }

    IEnumerator LoadAsync(int scene)
    {
        AsyncOperation progress = SceneManager.LoadSceneAsync(scene);

        LoadingCanvas.SetActive(true);

        while (!progress.isDone)
        {
            float progressVal = Mathf.Clamp01(progress.progress / .9f);

            LoadingBar.value = progressVal;

            yield return null;
        }

        LoadingCanvas.SetActive(false);
        LoadingBar.value = 0;
    }

}
