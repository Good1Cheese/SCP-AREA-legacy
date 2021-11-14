using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class GameLoader : MonoBehaviour
{
    [Inject] readonly GameLoading m_gameLoading;
    [Inject] readonly SceneTransition m_sceneTransition;

    public Action<bool> OnGameLoadingUI { get; set; }
    public Action OnGameLoaded { get; set; }

    IEnumerator Start()
    {
        if (!m_gameLoading.WasGameLoadedFromMenu) { yield break; }

        m_sceneTransition.LoadingSceneUIController.IsActiveStateConstant = true;

        OnGameLoadingUI?.Invoke(false);

        yield return new WaitForSeconds(1);

        m_gameLoading.LoadGame();

        OnGameLoadingUI?.Invoke(true);

        m_sceneTransition.LoadingSceneUIController.IsActiveStateConstant = false;
        m_sceneTransition.LoadingSceneUIController.SetActiveState(false);

        OnGameLoaded?.Invoke();
    }
}
