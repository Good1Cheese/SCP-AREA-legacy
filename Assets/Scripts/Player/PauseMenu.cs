using System;
using UnityEngine;
using Zenject;


public class PauseMenu : MonoBehaviour
{
    [Inject] readonly InventoryAcviteStateSetter m_inventoryAcviteStateSetter;
    [Inject] readonly PlayerHealth m_playerHealth;

    Action deactivateAction;

    public bool IsGamePaused { get; set; }
    public Action OnPauseMenuButtonPressed { get; set; }

    void Update()
    {
        if (Input.GetButtonDown("PauseMenu"))
        {
            PauseGameOrUnpauseGame();
        }
    }

    public void PauseGameOrUnpauseGame()
    {
        IsGamePaused = !IsGamePaused;
        m_inventoryAcviteStateSetter.OnInventoryButtonPressed?.Invoke();
        OnPauseMenuButtonPressed.Invoke();
    }
}
