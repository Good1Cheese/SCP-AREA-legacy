using System;
using Zenject;

public abstract class WearableSlot : InventorySlot
{
    [Inject] private readonly WearableItemsInteraction _wearableItemsInteraction;

    private static WearableItemActivator _currentItemActivator;

    public Action<WearableItemHandler> ItemChanged { get; set; }
    public Action<bool> Toggled { get; set; }
    public Action ItemRemoved { get; set; }
    public Action ActionStarted { get; set; }
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

    public new void SetItem(ItemHandler item)
    {
        if (ItemHandler != null)
        {
            _wearableItemsInteraction.DropItem(this);
        }

        ItemChanged?.Invoke((WearableItemHandler)item);
        base.SetItem(item);
    }

    public override void Setted()
    {
        _image.enabled = true;
    }

    public override void Cleared()
    {
        ItemRemoved?.Invoke();
        _image.enabled = false;

        if (_currentItemActivator == null || _currentItemActivator.Slot != this) { return; }

        _currentItemActivator = null;
    }

    public override void Clicked()
    {
        _wearableItemsInteraction.DropItem(this);
    }
}