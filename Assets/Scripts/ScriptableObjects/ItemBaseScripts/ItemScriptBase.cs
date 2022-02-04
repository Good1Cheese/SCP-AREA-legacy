using Zenject;

public abstract class ItemScriptBase : InteractableWithDelay
{
    protected PickableInventoryToggler _pickableInventoryToggler;
    protected WearableSlot _itemSlot;

    [Inject]
    private void Inject(PickableInventoryToggler pickableInventoryToggler)
    {
        _pickableInventoryToggler = pickableInventoryToggler;
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