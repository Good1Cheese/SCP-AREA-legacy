using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class WearableItemSlot : InventorySlot
{
    [Inject] private readonly WearableItemsInteraction _wearableItemsInteraction;

    public Action<WearableItemHandler> OnItemChanged { get; set; }
    public WearableItemActivator WearableItemActivator { get; set; }
    public Action OnItemRemoved { get; set; }
    public bool IsItemActionGoing { get; set; }

    private IEnumerator StartItemActionCoroutine(WaitForSeconds waitForSeconds)
    {
        IsItemActionGoing = true;

        yield return waitForSeconds;

        IsItemActionGoing = false;
    }

    public void StartItemAction(WaitForSeconds timeout)
    {
        WearableItemActivator.StartCoroutine(StartItemActionCoroutine(timeout));
    }

    public new void SetItem(ItemHandler item)
    {
        if (ItemHandler != null)
        {
            if (IsItemActionGoing) { return; }

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
        OnItemRemoved?.Invoke();
        _image.enabled = false;
    }

    public override void OnRightClick()
    {
        _wearableItemsInteraction.DropItem(this);
    }
}
