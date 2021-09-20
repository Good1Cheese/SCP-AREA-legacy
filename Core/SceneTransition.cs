using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public enum Scenes
    {
        StartScene = 0,
        SettingsScene = 1,
        RespawnScene = 2,
        ScpScene = 3
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadSceneAsynchronously(int index) => StartCoroutine(LoadSceneAsynchronouslyCoroutine(index));

    public IEnumerator LoadSceneAsynchronouslyCoroutine(int index)
    {
        var loadingSceneProcess = SceneManager.LoadSceneAsync(index);

        while (!loadingSceneProcess.isDone)
        {
            float progress = Mathf.Clamp01(loadingSceneProcess.progress / .9f);
            print(progress);

            yield return null;
        }
    }

}