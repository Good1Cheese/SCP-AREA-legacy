using UnityEngine;
using Zenject;

public class MainMenuUIManager : MonoBehaviour
{
    [Inject] readonly SceneTransition m_sceneTransition;
    [Inject] readonly GameLoading m_gameLoading;

    public void Play()
    {
        m_sceneTransition.LoadScene((int)SceneTransition.Scenes.ScpScene);
    }

    public void LoadGame()
    {
        m_sceneTransition.LoadScene((int)SceneTransition.Scenes.ScpScene);
        m_gameLoading.WasGameLoadedFromMenu = true;
    }

    public void EnterSettings()
    {
        m_sceneTransition.LoadScene((int)SceneTransition.Scenes.SettingsScene);
    }

    public void Exit() => Application.Quit();
}
