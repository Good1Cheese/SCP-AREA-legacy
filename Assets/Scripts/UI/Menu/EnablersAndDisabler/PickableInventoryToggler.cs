using UnityEngine;
using Zenject;

public class PickableInventoryToggler : UIToggler
{
    private const KeyCode INVENTORY_KEY = KeyCode.Tab;

    [SerializeField] private PickableItemsInventoryUIUpdater _pickableItemssInventoryUIUpdater;

    private PauseMenuToggler _pauseMenuToggler;

    [Inject]
    private void Construct(PauseMenuToggler pauseMenuToggler)
    {
        _pauseMenuToggler = pauseMenuToggler;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(INVENTORY_KEY)) { return; }

        IsToggled = !IsToggled;
        TryInteract();
    }

    private void Toggle()
    {
        if (_pauseMenuToggler.IsToggled)
        {
            IsToggled = !IsToggled;
            return;
        }

        Toggled?.Invoke();
        _pickableItemssInventoryUIUpdater.ActivateOrClose();
    }

    public override void Interact() => Toggle();
}