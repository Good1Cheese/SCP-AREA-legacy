using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class WearableSlot : InventorySlot
{
    [Inject] private readonly WearableItemsInteraction _wearableItemsInteraction;

    public Action<WearableItemHandler> OnItemChanged { get; set; }
    public WearableItemActivator WearableItemActivator { get; set; }
    public Action OnItemRemoved { get; set; }
    public Action OnNewAcitionStarted { get; set; }
    public bool IsItemActionGoing { get; set; }

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
        OnItemRemoved?.Invoke();
        _image.enabled = false;
    }

    public override void OnRightClick()
    {
        _wearableItemsInteraction.DropItem(this);
    }

    public void StartItemAction(WaitForSeconds timeout)
    {
        WearableItemActivator.StartCoroutine(StartItemActionCoroutine(timeout));
    }

    private IEnumerator StartItemActionCoroutine(WaitForSeconds timeout)
    {
        IsItemActionGoing = true;

        OnNewAcitionStarted?.Invoke();
        yield return timeout;

        IsItemActionGoing = false;
    }
}