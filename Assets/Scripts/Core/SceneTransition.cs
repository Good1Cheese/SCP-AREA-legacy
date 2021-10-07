using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public LoadingSceneUIController LoadingSceneUIController { get; set; }

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

    public void LoadSceneAsynchronously(int index)
    {
        LoadingSceneUIController.SetActiveState(true);

        StartCoroutine(LoadSceneAsynchronouslyCoroutine(index));
    }

    public IEnumerator LoadSceneAsynchronouslyCoroutine(int index)
    {
        var loadingSceneProcess = SceneManager.LoadSceneAsync(index);

        while (!loadingSceneProcess.isDone)
        {
            float progress = Mathf.Clamp01(loadingSceneProcess.progress / .9f);
            LoadingSceneUIController.UpdateUI(progress);

            yield return null;
        }

        LoadingSceneUIController.SetActiveState(false);
    }
}
