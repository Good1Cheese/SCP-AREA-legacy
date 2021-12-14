using System;
using UnityEngine;
using Zenject;

public abstract class WearableSlot : InventorySlot
{
    [Inject] private readonly WearableItemsInteraction _wearableItemsInteraction;
    [Inject(Id = "ItemsAudio")] private readonly AudioSource _slotAudio;

    private static WearableItemActivator _currentItemActivator;

    public WearableItemActivator WearableItemActivator { get; set; }
    public ItemActionMaker ItemActionMaker { get; set; }
    public Action<WearableItemHandler> ItemChanged { get; set; }
    public Action<bool> Toggled { get; set; }
    public Action ItemRemoved { get; set; }

    public static Action NewItemActivated { get; set; }

    public static WearableItemActivator CurrentItemActivator
    {
        get => _currentItemActivator;
        set
        {
            if (_currentItemActivator != null && _currentItemActivator != value)
            {
                _currentItemActivator.SetItemActiveState(false);
                NewItemActivated?.Invoke();
            }

            _currentItemActivator = value;
        }
    }

    protected void Awake()
    {
        ItemActionMaker = new ItemActionMaker(WearableItemActivator, _slotAudio);
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
        ItemActionMaker.StartEmptyItemActionWithAudioStop();

        ItemRemoved?.Invoke();
        _image.enabled = false;
    }

    public override void Clicked()
    {
        _wearableItemsInteraction.DropItem(this);
    }
}