using Zenject;

public class PickabeItemsDrop : ItemsInteraction
{
    private PickableItemsInventory _pickableItemsInventory;

    [Inject]
    private void Inject(PickableItemsInventory pickableItemsInventory)
    {
        _pickableItemsInventory = pickableItemsInventory;
    }

    public override void Interact()
    {
        _inventorySlot.Clear();

        var pickableSlot = (PickableSlot)_inventorySlot;
        _pickableItemsInventory.Remove(pickableSlot.SlotIndex);
    }
}