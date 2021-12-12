using System;
using UnityEngine;
using Zenject;

public abstract class WearableSlot : InventorySlot
{
    [Inject] private readonly WearableItemsInteraction _wearableItemsInteraction;
    [Inject(Id = "ItemsAudio")] private readonly AudioSource _slotAudio;

    public WearableItemActivator WearableItemActivator { get; set; }
    public ItemActionMaker ItemActionMaker { get; set; }
    public Action<WearableItemHandler> OnItemChanged { get; set; }
    public Action<bool> OnItemToggled { get; set; }
    public Action OnItemRemoved { get; set; }

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

        OnItemChanged?.Invoke((WearableItemHandler)item);
        base.SetItem(item);
    }

    public void ClearWearableSlot()
    {
        OnItemDeleted();
        ItemHandler = null;
        _image.sprite = null;
    }

    public override void OnItemSet()
    {
        _image.enabled = true;
    }

    public override void OnItemDeleted()
    {
        ItemActionMaker.StartEmptyItemAction2();

        OnItemRemoved?.Invoke();
        _image.enabled = false;
    }

    public override void OnRightClick()
    {
        _wearableItemsInteraction.DropItem(this);
    }
}