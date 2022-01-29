using UnityEngine;
using Zenject;

public class PauseMenuEnablerDisabler : UIEnablerDisabler
{
    private const KeyCode PAUSE_KEY = KeyCode.Escape;
    private PickableInventoryEnablerDisabler _pickableInventoryEnablerDisabler;

    [Inject]
    private void Construct(PickableInventoryEnablerDisabler pickableInventoryEnablerDisabler)
    {
        _pickableInventoryEnablerDisabler = pickableInventoryEnablerDisabler;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(PAUSE_KEY)) { return; }

        if (_pickableInventoryEnablerDisabler.IsActivated)
        {
            _pickableInventoryEnablerDisabler.EnableDisableUI();
        }

        EnableDisableUI();
    }

    public override void EnableDisableUI()
    {
        IsActivated = !IsActivated;
        EnabledDisabled?.Invoke();
        _pickableInventoryEnablerDisabler.EnabledDisabled?.Invoke();
    }
}