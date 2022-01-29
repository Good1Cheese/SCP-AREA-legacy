public abstract class ItemsInteraction : Interactable
{
    protected InventorySlot _inventorySlot;

    public void CallFunction(InventorySlot inventorySlot)
    {
        _inventorySlot = inventorySlot;
        Interact();
    }
}