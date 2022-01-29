public class PickableItemsUse : ItemsInteraction
{
    public override void Interact()
    {
        var itemSlot = _inventorySlot.ItemHandler as IClickable;

        var pickableSlot = (PickableSlot)_inventorySlot;
        itemSlot.Clicked(pickableSlot.SlotIndex);
    }
}