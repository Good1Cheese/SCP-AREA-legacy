using System;
using UnityEngine;
using Zenject;

public class InventoryAcviteStateSetter : MonoBehaviour
{
    [Inject] readonly PauseMenu m_pauseMenu;
    [SerializeField] PlayerInventoryUI m_playerInventoryUI;

    public Action OnInventoryButtonPressed { get; set; }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            if (m_pauseMenu.IsGamePaused) { return; }
            OnInventoryButtonPressed?.Invoke();
            m_playerInventoryUI.ActivateOrClose();
        }
    }
}