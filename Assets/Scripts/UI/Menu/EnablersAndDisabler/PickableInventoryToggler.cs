using UnityEngine;

public class PickableInventoryToggler : UIToggler
{
    private const KeyCode INVENTORY_KEY = KeyCode.Tab;

    [SerializeField] private PickableItemsInventoryUIUpdater _pickableItemssInventoryUIUpdater;

    private void Update()
    {
        if (!Input.GetKeyDown(INVENTORY_KEY)) { return; }

        TryInteract();
    }

    private void Toggle()
    {
        IsToggled = !IsToggled;
        Toggled?.Invoke();
        _pickableItemssInventoryUIUpdater.ActivateOrClose();
    }

    public override void Interact() => Toggle();
}