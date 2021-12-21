using UnityEngine;
using Zenject;

public class MainMenuControls : MonoBehaviour
{
    [Inject] private readonly SceneTransition _sceneTransition;
    [Inject] private readonly GameLoading _gameLoading;

    public void Play()
    {
        _gameLoading.PreLoadGame(false);
    }

    public void LoadGame()
    {
        _gameLoading.PreLoadGame(true);
    }

    public void EnterSettings()
    {
        _sceneTransition.LoadScene((int)SceneTransition.Scenes.SettingsScene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}