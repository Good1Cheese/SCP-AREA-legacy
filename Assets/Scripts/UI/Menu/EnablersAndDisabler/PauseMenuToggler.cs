using UnityEngine;
using Zenject;

public class PauseMenuToggler : UIToggler
{
    private const KeyCode PAUSE_KEY = KeyCode.Escape;
    private PickableInventoryToggler _pickableInventoryToggler;

    [Inject]
    private void Construct(PickableInventoryToggler pickableInventoryToggler)
    {
        _pickableInventoryToggler = pickableInventoryToggler;
    }

    private void Update()
    {
        if (!Input.GetKeyDown(PAUSE_KEY)) { return; }

        if (_pickableInventoryToggler.IsToggled)
        {
            _pickableInventoryToggler.Interact();
        }

        Interact();
    }

    private void Toggle()
    {
        IsToggled = !IsToggled;
        Toggled?.Invoke();
        _pickableInventoryToggler.Toggled?.Invoke();
    }

    public override void Interact() => Toggle();
}