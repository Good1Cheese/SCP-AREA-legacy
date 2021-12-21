using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class GameLoader : MonoBehaviour
{
    [Inject] private readonly GameLoading _gameLoading;
    [Inject] private readonly SceneTransition _sceneTransition;

    public Action<bool> UILoading { get; set; }
    public Action Loaded { get; set; }

    private IEnumerator Start()
    {
        if (!_gameLoading.WasGameLoadedFromMenu) { yield break; }

        _sceneTransition.LoadingSceneUIController.IsActiveStateConstant = true;

        UILoading?.Invoke(false);

        yield return new WaitForSeconds(1);

        _gameLoading.LoadGame();

        UILoading?.Invoke(true);

        _sceneTransition.LoadingSceneUIController.IsActiveStateConstant = false;
        _sceneTransition.LoadingSceneUIController.SetActiveState(false);

        Loaded?.Invoke();
    }
}