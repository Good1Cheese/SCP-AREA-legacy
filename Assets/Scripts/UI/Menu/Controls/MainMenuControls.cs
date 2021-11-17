using UnityEngine;
using Zenject;

public class MainMenuControls : MonoBehaviour
{
    [Inject] private readonly SceneTransition _sceneTransition;
    [Inject] private readonly GameLoading _gameLoading;
    [Inject] private readonly GameSaving _gameSaving;

    public void Play()
    {
        _gameSaving.SaveData.Clear();
        _sceneTransition.LoadSceneAsynchronously((int)SceneTransition.Scenes.ScpScene);
    }

    public void LoadGame()
    {
        _gameLoading.PreLoadGame();
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
