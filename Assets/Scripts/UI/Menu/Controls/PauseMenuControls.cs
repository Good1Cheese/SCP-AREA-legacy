using UnityEngine;
using Zenject;

public class PauseMenuControls : MonoBehaviour
{
    [Inject] private readonly SceneTransition sceneTransition;
    [Inject] private readonly GameSaving _gameSaver;
    [Inject] private readonly GameLoading _gameLoader;
    [Inject] private readonly PauseMenuEnablerDisabler _pauseMenu;
    private GameObject _gameObject;

    private void Start()
    {
        _gameObject = gameObject;
        _gameObject.SetActive(false);
        _pauseMenu.EnabledDisabled += ActivateOrDeacrivateUI;
    }

    public void ActivateOrDeacrivateUI()
    {
        Time.timeScale = (_gameObject.activeSelf) ? 1 : 0;
        _gameObject.SetActive(!_gameObject.activeSelf);
    }

    public void UnpauseGame()
    {
        _pauseMenu.EnableDisableUI();
    }

    public void SaveGame()
    {
        _pauseMenu.EnableDisableUI();
        _gameSaver.Save();
    }

    public void LoadGame()
    {
        _pauseMenu.EnableDisableUI();
        _gameLoader.Load();
    }

    public void Exit()
    {
        sceneTransition.LoadSceneAsynchronously((int)SceneTransition.Scenes.StartScene);
        Time.timeScale = 1;
    }

    private void OnDestroy()
    {
        _pauseMenu.EnabledDisabled -= ActivateOrDeacrivateUI;
    }
}
