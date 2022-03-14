using Zenject;

public class PickabeItemsDrop : ItemsInteraction
{
    private PickableItemsInventory _pickableItemsInventory;

    [Inject]
    private void Inject(PickableItemsInventory pickableItemsInventory)
    {
        _pickableItemsInventory = pickableItemsInventory;
    }

    private void Drop()
    {
        _itemSlot.Clear();

        var pickableSlot = (PickableSlot)_itemSlot;
        _pickableItemsInventory.Remove(pickableSlot.SlotIndex);
    }

    public override void Interact() => Drop();
}