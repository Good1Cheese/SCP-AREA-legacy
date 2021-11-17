using System;
using UnityEngine;
using Zenject;


public class PauseMenuEnablerDisabler : UIEnablerDisabler
{
    private const KeyCode PAUSE_KEY = KeyCode.Escape;
    [Inject] private readonly InventoryEnablerDisabler _wearableInventoryActivator;

    public Action OnPauseMenuButtonPressed { get; set; }

    private void Update()
    {
        if (!Input.GetKeyDown(PAUSE_KEY)) { return; }

        if (_wearableInventoryActivator.IsUIActivated)
        {
            _wearableInventoryActivator.EnableDisableUI();
        }
        EnableDisableUI();
    }

    public override void EnableDisableUI()
    {
        IsUIActivated = !IsUIActivated;
        _wearableInventoryActivator.OnInventoryEnabledDisabled?.Invoke();
        OnPauseMenuButtonPressed.Invoke();
    }
}
