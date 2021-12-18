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
            if (_currentItemActivator != null && _currentItemActivator != value)
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

    public void ClearWearableSlot()
    {
        Cleared();
        ItemHandler = null;
        _image.sprite = null;
    }

    public override void Setted()
    {
        _image.enabled = true;
    }

    public override void Cleared()
    {
        ItemRemoved?.Invoke();
        _image.enabled = false;

        if (CurrentItemActivator.Slot != this) { return; }

        CurrentItemActivator = null;
    }

    public override void Clicked()
    {
        _wearableItemsInteraction.DropItem(this);
    }
}