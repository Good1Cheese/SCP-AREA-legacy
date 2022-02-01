public class WearableItemsUse : ItemsInteraction
{
    private void Use()
    {
        var itemSlot = _inventorySlot.ItemHandler as IClickable;
        itemSlot.Clicked(-1);
    }

    public override void Interact() => Use();
}