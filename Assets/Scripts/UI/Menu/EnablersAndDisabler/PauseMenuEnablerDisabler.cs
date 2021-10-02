using System;
using UnityEngine;
using Zenject;


public class PauseMenuEnablerDisabler : MonoBehaviour
{
    [Inject] readonly InventoryEnablerDisabler m_wearableInventoryActivator;
    [Inject] readonly PlayerHealth m_playerHealth;

    public bool IsGamePaused { get; set; }
    public Action OnPauseMenuButtonPressed { get; set; }

    void Start()
    {
        m_playerHealth.OnPlayerDies += DisablePauseMenu;
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

    void OnDestroy()
    {
        m_playerHealth.OnPlayerDies += DisablePauseMenu;
    }
}
