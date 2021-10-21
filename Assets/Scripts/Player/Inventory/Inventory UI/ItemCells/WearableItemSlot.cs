using System;
using UnityEngine;
using Zenject;

public class WearableItemSlot : InventorySlot
{
    [Inject] protected readonly WearableItemsInventory m_wearableItemsInventory;
    [Inject(Id = "Player")] readonly Transform m_playerTransform;

    public Action<WearableItemHandler> OnItemChanged { get; set; }

    public new void SetItem(ItemHandler item)
    {
        if (ItemHandler != null)
        {
            OnItemReplaced();
            Clear();
        }

        OnItemChanged?.Invoke((WearableItemHandler)item);
        base.SetItem(item);
    }

    public new void Clear()
    {
        DespawnItem();
        ItemHandler.IsInInventory = false;
        base.Clear();
    }

    public virtual void OnItemReplaced() { }

    public override void OnItemSet()
    {
        m_image.enabled = true;
    }

    public override void OnItemDeleted()
    {
        m_image.enabled = false;
    }

    public override void OnRightClick()
    {
        m_wearableItemsInventory.OnItemClicked.Invoke(this);
    }

    public void DespawnItem()
    {
        ItemHandler.gameObject.transform.position = m_playerTransform.position + m_playerTransform.forward;
        ItemHandler.gameObject.SetActive(true);
    }
}
