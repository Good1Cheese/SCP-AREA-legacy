using System;

public class WearableItemsDrop : ItemsInteraction
{
    public Action ItemRemoved { get; set; }

    private void Drop()
    {
        ItemRemoved?.Invoke();
        _itemSlot.Clear();
    }

    public void Drop(ItemSlot itemSlot)
    {
        ItemRemoved?.Invoke();
        itemSlot.Clear();
    }

    public override void Interact() => Drop();
}