using System;
using UnityEngine;
using Zenject;


public class PauseMenu : MonoBehaviour
{
    [Inject] readonly WearableInventoryActivator m_wearableInventoryActivator;

    public bool IsGamePaused { get; set; }
    public Action OnPauseMenuButtonPressed { get; set; }

    void Update()
    {
        if (Input.GetButtonDown("PauseMenu"))
        {
            if (m_wearableInventoryActivator.IsInventoryActivated)
            {
                m_wearableInventoryActivator.ActivateOrDeactivateMenu();
            }
            PauseGameOrUnpauseGame();
        }
    }

    public void PauseGameOrUnpauseGame()
    {
        IsGamePaused = !IsGamePaused;
        m_wearableInventoryActivator.OnInventoryButtonPressed?.Invoke();
        OnPauseMenuButtonPressed.Invoke();
    }
}
