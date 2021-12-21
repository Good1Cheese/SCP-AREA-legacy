using System;
using UnityEngine;
using Zenject;


public class PauseMenuEnablerDisabler : UIEnablerDisabler
{
    private const KeyCode PAUSE_KEY = KeyCode.Escape;

    [Inject] private readonly InventoryEnablerDisabler _wearableInventoryActivator;

    private void Update()
    {
        if (!Input.GetKeyDown(PAUSE_KEY)) { return; }

        if (_wearableInventoryActivator.IsActivated)
        {
            _wearableInventoryActivator.EnableDisableUI();
        }
        EnableDisableUI();
    }

    public override void EnableDisableUI()
    {
        IsActivated = !IsActivated;
        EnabledDisabled.Invoke();
        _wearableInventoryActivator.EnabledDisabled?.Invoke();
    }
}