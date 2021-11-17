using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class GameLoader : MonoBehaviour
{
    [Inject] private readonly GameLoading _gameLoading;
    [Inject] private readonly SceneTransition _sceneTransition;

    public Action<bool> OnGameLoadingUI { get; set; }
    public Action OnGameLoaded { get; set; }

    private IEnumerator Start()
    {
        if (!_gameLoading.WasGameLoadedFromMenu) { yield break; }

        _sceneTransition.LoadingSceneUIController.IsActiveStateConstant = true;

        OnGameLoadingUI?.Invoke(false);

        yield return new WaitForSeconds(1);

        _gameLoading.LoadGame();

        OnGameLoadingUI?.Invoke(true);

        _sceneTransition.LoadingSceneUIController.IsActiveStateConstant = false;
        _sceneTransition.LoadingSceneUIController.SetActiveState(false);

        OnGameLoaded?.Invoke();
    }
}
