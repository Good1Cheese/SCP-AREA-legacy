public class PickableItemsUse : ItemsInteraction
{
    private void Use()
    {
        var itemSlot = _itemSlot.ItemHandler as IClickable;

        var pickableSlot = (PickableSlot)_itemSlot;
        itemSlot.Clicked(pickableSlot.SlotIndex);
    }
    public override void Interact() => Use();
}