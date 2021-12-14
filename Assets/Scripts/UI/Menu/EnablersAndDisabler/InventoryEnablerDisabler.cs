using System;
using UnityEngine;
using Zenject;

public class InventoryEnablerDisabler : UIEnablerDisabler
{
    private const KeyCode INVENTORY_KEY = KeyCode.Tab;

    [SerializeField] private PlayerInventoryUIUpdater _playerInventoryUI;

    [Inject] private readonly PauseMenuEnablerDisabler _pauseMenu;

    public Action ActiveStateChanged { get; set; }

    private void Update()
    {
        if (!Input.GetKeyDown(INVENTORY_KEY)) { return; }

        EnableDisableUI();
    }

    public override void EnableDisableUI()
    {
        if (_pauseMenu.IsUIActivated) { return; }

        IsUIActivated = !IsUIActivated;
        ActiveStateChanged?.Invoke();
        _playerInventoryUI.ActivateOrClose();
    }
}