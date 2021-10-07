using System;
using UnityEngine;
using Zenject;

public class InventoryEnablerDisabler : MonoBehaviour
{
    [SerializeField] PlayerInventoryUIUpdater m_playerInventoryUI;

    [Inject] readonly PauseMenuEnablerDisabler m_pauseMenu;
    [Inject] readonly GameLoader m_gameLoader;

    public bool IsInventoryActivated { get; set; }
    public Action OnInventoryButtonPressed { get; set; }

    void Awake()
    {
        m_gameLoader.OnGameLoading += SetScriptActiveState;
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            ActivateOrDeactivateMenu();
        }
    }

    public void SetScriptActiveState(bool activeState)
    {
        enabled = activeState;
    }

    public void ActivateOrDeactivateMenu()
    {
        if (m_pauseMenu.IsGamePaused) { return; }

        IsInventoryActivated = !IsInventoryActivated;
        OnInventoryButtonPressed?.Invoke();
        m_playerInventoryUI.ActivateOrClose();
    }

    void OnDestroy()
    {
        m_gameLoader.OnGameLoading -= SetScriptActiveState;
    }
}