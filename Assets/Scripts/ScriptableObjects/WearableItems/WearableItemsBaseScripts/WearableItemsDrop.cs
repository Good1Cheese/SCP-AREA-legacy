using System;
using Zenject;

public class WearableItemsDrop : ItemsInteraction
{
    public Action ItemRemoved { get; set; }

    private void Drop()
    {
        ItemRemoved?.Invoke();
        _inventorySlot.Clear();
    }

    public override void Interact() => Drop();
}