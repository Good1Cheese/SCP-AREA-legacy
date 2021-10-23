using System;
using UnityEngine;
using Zenject;

public class InventoryEnablerDisabler : UIEnablerDisabler
{
    const KeyCode INVENTORY_KEY = KeyCode.Tab;
    [SerializeField] PlayerInventoryUIUpdater m_playerInventoryUI;

    [Inject] readonly PauseMenuEnablerDisabler m_pauseMenu;

    public Action OnInventoryEnabledDisabled { get; set; }

    void Update()
    {
        if (Input.GetKeyDown(INVENTORY_KEY))
        {
            EnableDisableUI();
        }
    }

    public override void EnableDisableUI()
    {
        if (m_pauseMenu.IsUIActivated) { return; }

        IsUIActivated = !IsUIActivated;
        OnInventoryEnabledDisabled?.Invoke();
        m_playerInventoryUI.ActivateOrClose();
    }
}