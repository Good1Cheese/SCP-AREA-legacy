using System;
using UnityEngine;
using Zenject;

public class InventoryEnablerDisabler : MonoBehaviour
{
    [SerializeField] PlayerInventoryUIUpdater m_playerInventoryUI;
    [Inject] readonly PauseMenuEnablerDisabler m_pauseMenu;

    public bool IsInventoryActivated { get; set; }
    public Action OnInventoryButtonPressed { get; set; }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            ActivateOrDeactivateMenu();
        }
    }

    public void ActivateOrDeactivateMenu()
    {
        if (m_pauseMenu.IsGamePaused) { return; }

        IsInventoryActivated = !IsInventoryActivated;
        OnInventoryButtonPressed?.Invoke();
        m_playerInventoryUI.ActivateOrClose();
    }
}