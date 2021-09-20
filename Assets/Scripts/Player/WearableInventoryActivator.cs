using System;
using UnityEngine;
using Zenject;

public class WearableInventoryActivator : MonoBehaviour
{
    [SerializeField] PlayerInventoryUI m_playerInventoryUI;
    [Inject] readonly PauseMenu m_pauseMenu;

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