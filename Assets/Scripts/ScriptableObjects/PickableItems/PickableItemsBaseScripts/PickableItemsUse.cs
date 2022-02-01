public class PickableItemsUse : ItemsInteraction
{
    private void Use()
    {
        var itemSlot = _inventorySlot.ItemHandler as IClickable;

        var pickableSlot = (PickableSlot)_inventorySlot;
        itemSlot.Clicked(pickableSlot.SlotIndex);
    }
    public override void Interact() => Use();
}