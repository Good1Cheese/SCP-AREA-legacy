public abstract class ItemsInteraction : InteractableWithDelay
{
    protected InventorySlot _inventorySlot;

    public void CallFunction(InventorySlot inventorySlot)
    {
        _inventorySlot = inventorySlot;
        TryInteract();
    }
}