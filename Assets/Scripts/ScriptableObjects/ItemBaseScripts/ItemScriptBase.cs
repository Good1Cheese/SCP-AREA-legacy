public abstract class ItemScriptBase : InteractableWithDelay
{
    protected WearableSlot _itemSlot;

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