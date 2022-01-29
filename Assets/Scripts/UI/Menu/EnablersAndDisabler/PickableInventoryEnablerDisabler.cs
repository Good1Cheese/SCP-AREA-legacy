using UnityEngine;
using Zenject;

public class PickableInventoryEnablerDisabler : UIEnablerDisabler
{
    private const KeyCode INVENTORY_KEY = KeyCode.Tab;

    [SerializeField] private PickableItemsInventoryUIUpdater _pickableItemssInventoryUIUpdater;

    private PauseMenuEnablerDisabler _pauseMenuEnablerDisabler;

    [Inject]
    private void Construct(PauseMenuEnablerDisabler pauseMenuEnablerDisabler)
    {
        _pauseMenuEnablerDisabler = pauseMenuEnablerDisabler;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(INVENTORY_KEY)) { return; }

        EnableDisableUI();
    }

    public override void EnableDisableUI()
    {
        if (_pauseMenuEnablerDisabler.IsActivated) { return; }

        IsActivated = !IsActivated;
        EnabledDisabled?.Invoke();
        _pickableItemssInventoryUIUpdater.ActivateOrClose();
    }
}