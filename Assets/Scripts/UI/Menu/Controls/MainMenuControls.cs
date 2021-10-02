using UnityEngine;
using Zenject;

public class MainMenuControls : MonoBehaviour
{
    [Inject] readonly SceneTransition m_sceneTransition;
    [Inject] readonly GameLoading m_gameLoading;
    [Inject] readonly GameSaving m_gameSaving;

    public void Play()
    {
        m_gameSaving.SaveData.Clear();
        m_sceneTransition.LoadScene((int)SceneTransition.Scenes.ScpScene);
    }

    public void LoadGame()
    {
        m_gameSaving.SaveData.Clear();
        m_sceneTransition.LoadScene((int)SceneTransition.Scenes.ScpScene);
        m_gameLoading.WasGameLoadedFromMenu = true;
    }

    public void EnterSettings()
    {
        m_sceneTransition.LoadScene((int)SceneTransition.Scenes.SettingsScene);
    }

    public void Exit() => Application.Quit();
}
