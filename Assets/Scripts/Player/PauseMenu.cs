using System;
using UnityEngine;
using Zenject;


public class PauseMenu : MonoBehaviour
{
    [Inject] readonly PlayerInventory m_playerInventory;
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
        m_playerInventory.OnInventoryButtonPressed.Invoke();
        OnPauseMenuButtonPressed.Invoke();
    }
}
