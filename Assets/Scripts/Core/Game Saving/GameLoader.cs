using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class GameLoader : MonoBehaviour
{
    [Inject] readonly GameLoading m_gameLoading;
    [Inject] readonly SceneTransition m_sceneTransition;

    public Action<bool> OnGameLoading { get; set; }

    IEnumerator Start()
    {
        if (!m_gameLoading.WasGameLoadedFromMenu) { yield break; }

        m_sceneTransition.LoadingSceneUIController.IsActiveStateConstant = true;

        OnGameLoading?.Invoke(false);

        yield return new WaitForSeconds(1);

        m_gameLoading.LoadGame();

        OnGameLoading?.Invoke(true);
        m_sceneTransition.LoadingSceneUIController.IsActiveStateConstant = false;
        m_sceneTransition.LoadingSceneUIController.SetActiveState(false);
    }
}
