using UnityEngine;

public class MaskHandler : WearableItemHandler
{
    [SerializeField] Mask_SO m_mask_SO;

    public override void Equip()
    {
        m_wearableItemsInventory.MaskSlot.SetItem(this);
    }

    public override Item_SO GetItem() => m_mask_SO;
}
