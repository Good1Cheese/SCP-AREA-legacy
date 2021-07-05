using UnityEngine;
using Zenject;

public class MainMenuUIManager : MonoBehaviour
{
    [Inject] readonly SceneTransition m_sceneTransition;

    public void Play()
    {
        m_sceneTransition.LoadSceneAsynchronously((int)SceneTransition.Scenes.ScpScene);
    }

    public void EnterSettings()
    {
        m_sceneTransition.LoadSceneAsynchronously((int)SceneTransition.Scenes.SettingsScene);
    }

    public void Exit() => Application.Quit();
}
