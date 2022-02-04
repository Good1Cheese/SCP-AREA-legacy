using System;

public class WearableItemsDrop : ItemsInteraction
{
    public Action ItemRemoved { get; set; }

    private void Drop()
    {
        ItemRemoved?.Invoke();
        _inventorySlot.Clear();
    }

    public void Drop(InventorySlot inventorySlot)
    {
        ItemRemoved?.Invoke();
        inventorySlot.Clear();
    }

    public override void Interact() => Drop();
}