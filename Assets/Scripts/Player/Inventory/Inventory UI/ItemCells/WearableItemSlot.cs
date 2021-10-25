using System;
using Zenject;

public class WearableItemSlot : InventorySlot
{
    [Inject] readonly WearableItemsInteraction m_wearableItemsInteraction;

    public Action<WearableItemHandler> OnItemChanged { get; set; }
    public Action OnItemRemoved  { get; set; }

    public new void SetItem(ItemHandler item)
    {
        if (ItemHandler != null)
        {
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
