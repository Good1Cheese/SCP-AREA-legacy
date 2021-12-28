using UnityEngine;
using Zenject;

public class InventoryEnablerDisabler : UIEnablerDisabler
{
    private const KeyCode INVENTORY_KEY = KeyCode.Tab;

    [SerializeField] private PickableItemsInventoryUIUpdater _pickableItemssInventoryUIUpdater;

    [Inject] private readonly PauseMenuEnablerDisabler _pauseMenu;

    private void Update()
    {
        if (!Input.GetKeyDown(INVENTORY_KEY)) { return; }

        EnableDisableUI();
    }

    public override void EnableDisableUI()
    {
        if (_pauseMenu.IsActivated) { return; }

        IsActivated = !IsActivated;
        EnabledDisabled?.Invoke();
        _pickableItemssInventoryUIUpdater.ActivateOrClose();
    }
}