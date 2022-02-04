using System;
using Zenject;

public abstract class WearableSlot : InventorySlot
{
    private static WearableItemActivator _currentItemActivator;
    private WearableItemsDrop _wearableItemsDrop;

    public Action<WearableItemHandler> ItemChanged { get; set; }
    public Action<bool> Toggled { get; set; }
    public Action ItemRemoved { get; set; }
    public Action ActionStarted { get; set; }
    public Action Used { get; set; }
    public WearableItemActivator Activator { get; set; }

    public static WearableItemActivator CurrentItemActivator
    {
        get => _currentItemActivator;
        set
        {
            if (value == _currentItemActivator) { return; }

            if (_currentItemActivator != null)
            {
                _currentItemActivator.SetItemActiveState(false);
            }

            _currentItemActivator = value;
        }
    }

    [Inject]
    private void Construct(WearableItemsUse wearableItemsUse, WearableItemsDrop wearableItemsDrop)
    {
        _inventoryItemsUse = wearableItemsUse;
        _inventoryItemsDrop = wearableItemsDrop;
        _wearableItemsDrop = wearableItemsDrop;
    }

    public new void SetItem(ItemHandler item)
    {
        if (ItemHandler != null)
        {
            ReplaceOldItem(item);
        }

        ItemChanged?.Invoke((WearableItemHandler)item);
        base.SetItem(item);
    }

    protected virtual void ReplaceOldItem(ItemHandler newItem)
    {
        _wearableItemsDrop.Drop(this);
    }

    public override void Setted()
    {
        _image.enabled = true;
    }

    public override void Cleared()
    {
        ItemRemoved?.Invoke();
        _image.enabled = false;

        if (_currentItemActivator == null || _currentItemActivator.ItemSlot != this) { return; }

        _currentItemActivator = null;
    }
}