using System;
using UnityEngine;
using Zenject;


public class PauseMenu : MonoBehaviour
{
    [Inject] readonly PlayerInventory m_playerInventory;
    [Inject] readonly PlayerHealth m_playerHealth;

    Action deactivateAction;

    public bool IsGamePaused { get; set; }
    public Action OnPauseMenuButtonPressed { get; set; }

    void Start()
    {
        deactivateAction = delegate { gameObject.SetActive(false); };
        m_playerHealth.OnPlayerDie += deactivateAction;
    }

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

    void OnDestroy()
    {
        m_playerHealth.OnPlayerDie -= deactivateAction;
    }
}
