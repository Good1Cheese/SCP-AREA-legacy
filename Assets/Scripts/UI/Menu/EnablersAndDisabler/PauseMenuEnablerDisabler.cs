using System;
using UnityEngine;
using Zenject;


public class PauseMenuEnablerDisabler : MonoBehaviour
{
    [Inject] readonly InventoryEnablerDisabler m_wearableInventoryActivator;
    [Inject] readonly PlayerHealth m_playerHealth;
    [Inject] readonly GameLoader m_gameLoader;

    public bool IsGamePaused { get; set; }
    public Action OnPauseMenuButtonPressed { get; set; }

    void Awake()
    {
        m_playerHealth.OnPlayerDies += DisablePauseMenu;
        m_gameLoader.OnGameLoading += SetScriptActiveState;
    }

    void DisablePauseMenu()
    {
        if (IsGamePaused)
        {
            PauseOrUnpauseGame();
        }
        enabled = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("PauseMenu"))
        {
            if (m_wearableInventoryActivator.IsInventoryActivated)
            {
                m_wearableInventoryActivator.ActivateOrDeactivateMenu();
            }
            PauseOrUnpauseGame();
        }
    }

    public void PauseOrUnpauseGame()
    {
        IsGamePaused = !IsGamePaused;
        m_wearableInventoryActivator.OnInventoryButtonPressed?.Invoke();
        OnPauseMenuButtonPressed.Invoke();
    }

    public void SetScriptActiveState(bool activeState)
    {
        enabled = activeState;
    }

    void OnDestroy()
    {
        m_playerHealth.OnPlayerDies -= DisablePauseMenu;
        m_gameLoader.OnGameLoading -= SetScriptActiveState;
    }
}
