using System;
using UnityEngine;
using Zenject;


public class PauseMenuEnablerDisabler : UIEnablerDisabler
{
    const KeyCode PAUSE_KEY = KeyCode.Escape;
    [Inject] readonly InventoryEnablerDisabler m_wearableInventoryActivator;

    public Action OnPauseMenuButtonPressed { get; set; }

    void Update()
    {
        if (Input.GetKeyDown(PAUSE_KEY))
        {
            if (m_wearableInventoryActivator.IsUIActivated)
            {
                m_wearableInventoryActivator.EnableDisableUI();
            }
            EnableDisableUI();
        }
    }

    public override void EnableDisableUI()
    {
        IsUIActivated = !IsUIActivated;
        m_wearableInventoryActivator.OnInventoryEnabledDisabled?.Invoke();
        OnPauseMenuButtonPressed.Invoke();
    }
}
