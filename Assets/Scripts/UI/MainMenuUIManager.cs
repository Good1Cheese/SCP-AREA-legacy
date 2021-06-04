using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    public void Play()
    {
        MainLinks.Instance.SceneChanger.ChangeScene((int)SceneTransition.Scenes.ScpScene);
    }

    public void EnterSettings()
    {
        MainLinks.Instance.SceneChanger.ChangeScene((int)SceneTransition.Scenes.SettingsScene);
    }

    public void Exit() => Application.Quit();
}
