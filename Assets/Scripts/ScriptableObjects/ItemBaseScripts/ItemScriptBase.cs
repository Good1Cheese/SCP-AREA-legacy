using Zenject;

public abstract class ItemScriptBase : InteractableWithDelay
{
    protected PickableInventoryEnablerDisabler _pickableInventoryEnablerDisabler;
    protected WearableSlot _itemSlot;

    [Inject]
    private void Inject(PickableInventoryEnablerDisabler pickableInventoryEnablerDisabler)
    {
        _pickableInventoryEnablerDisabler = pickableInventoryEnablerDisabler;
    }

    protected void Start()
    {
        _itemSlot.Toggled += SetActiveState;
        enabled = false;
    }

    private void SetActiveState(bool activeState)
    {
        enabled = activeState;
    }

    protected void OnDestroy()
    {
        _itemSlot.Toggled -= SetActiveState;
    }
}