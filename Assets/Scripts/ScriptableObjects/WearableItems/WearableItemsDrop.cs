using System;
using Zenject;

public class WearableItemsDrop : ItemsInteraction
{
    private ItemActionCreator _itemActionCreator;

    public Action ItemRemoved { get; set; }

    [Inject]
    private void Inject(ItemActionCreator itemActionCreator)
    {
        _itemActionCreator = itemActionCreator;
    }

    public override void Interact()
    {
        if (_itemActionCreator.IsGoing) { return; }

        ItemRemoved?.Invoke();
        _inventorySlot.Clear();
    }
}