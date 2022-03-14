using UnityEngine;

public class PauseMenuToggler : UIToggler
{
    private const KeyCode PAUSE_KEY = KeyCode.Escape;

    private void Update()
    {
        if (!Input.GetKeyDown(PAUSE_KEY)) { return; }

        Toggle();

        if (_pickableInventoryToggler.IsToggled)
        {
            _pickableInventoryToggler.Interact();
        }
    }

    private void Toggle()
    {
        IsToggled = !IsToggled;
        Toggled?.Invoke();
    }

    public override void Interact() => Toggle();
}