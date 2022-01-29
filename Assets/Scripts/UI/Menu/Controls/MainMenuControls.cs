using UnityEngine;
using Zenject;

public class MainMenuControls : MonoBehaviour
{
    private SceneTransition _sceneTransition;
    private GameLoading _gameLoading;

    [Inject]
    private void Construct(SceneTransition sceneTransition, GameLoading gameLoading)
    {
        _sceneTransition = sceneTransition;
        _gameLoading = gameLoading;
    }

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