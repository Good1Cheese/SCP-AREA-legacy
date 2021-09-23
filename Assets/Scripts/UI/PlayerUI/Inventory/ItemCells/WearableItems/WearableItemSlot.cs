using Zenject;

public abstract class WearableItemSlot : InventorySlot
{
    [Inject] protected readonly WearableItemsInventory m_wearableItemsInventory;

    public override void OnItemSet()
    {
        m_image.enabled = true;
    }

    public override void OnItemDeleted()
    {
        m_image.enabled = false;
    }

    public virtual void ClearSlot()
    {
        ItemHandler.IsInInventory = false;
        ItemHandler = null;
        m_image.sprite = null;
        m_image.enabled = false;
    }

    public override void OnRightClick()
    {
        m_wearableItemsInventory.OnItemClicked.Invoke(this);
    }


}
