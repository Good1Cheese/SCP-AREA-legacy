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

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }

}