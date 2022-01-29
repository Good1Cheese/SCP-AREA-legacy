using System;
using UnityEngine;
using Zenject;

public class PauseMenuControls : MonoBehaviour
{
    private SceneTransition _sceneTransition;
    private GameSaving _gameSaving;
    private GameLoading _gameLoading;
    private PauseMenuEnablerDisabler _pauseMenuEnablerDisabler;

    private GameObject _gameObject;

    public Action Exited { get; set; }

    [Inject]
    private void Construct(SceneTransition sceneTransition,
                           GameSaving gameSaving,
                           GameLoading gameLoading,
                           PauseMenuEnablerDisabler pauseMenuEnablerDisabler)
    {
        _sceneTransition = sceneTransition;
        _gameSaving = gameSaving;
        _gameLoading = gameLoading;
        _pauseMenuEnablerDisabler = pauseMenuEnablerDisabler;
    }

    private void Start()
    {
        _gameObject = gameObject;
        _gameObject.SetActive(false);
    }

    public void PauseUnpauseGame()
    {
        _pauseMenuEnablerDisabler.EnableDisableUI();
    }

    public void SaveGame()
    {
        PauseUnpauseGame();
        _gameSaving.Save();
    }

    public void LoadGame()
    {
        PauseUnpauseGame();
        _gameLoading.Load();
    }

    public void Exit()
    {
        Exited?.Invoke();
        _sceneTransition.LoadSceneAsynchronously((int)SceneTransition.Scenes.StartScene);
    }
}