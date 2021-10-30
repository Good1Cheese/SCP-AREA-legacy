using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class WearableItemSlot : InventorySlot
{
    [Inject] readonly WearableItemsInteraction m_wearableItemsInteraction;

    public Action<WearableItemHandler> OnItemChanged { get; set; }
    public WearableItemActivator WearableItemActivator { get; set; }
    public Action OnItemRemoved  { get; set; }
    public bool IsItemActionGoing { get; set; }

    IEnumerator StartItemActionCoroutine(WaitForSeconds waitForSeconds) 
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

            m_wearableItemsInteraction.DropItem(this);
        }

        OnItemChanged?.Invoke((WearableItemHandler)item);
        base.SetItem(item);
    }

    public void ClearWearableSlot()
    {
        OnItemDeleted();
        ItemHandler = null;
        m_image.sprite = null;
    }

    public override void OnItemSet()
    {
        m_image.enabled = true;
    }

    public override void OnItemDeleted()
    {
        OnItemRemoved?.Invoke();
        m_image.enabled = false;
    }

    public override void OnRightClick()
    {
        m_wearableItemsInteraction.DropItem(this);
    }
}
