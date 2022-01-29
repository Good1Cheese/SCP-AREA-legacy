public class WearableItemsUse : ItemsInteraction
{
    public override void Interact()
    {
        var itemSlot = _inventorySlot.ItemHandler as IClickable;
        itemSlot.Clicked(-1);
    }
}