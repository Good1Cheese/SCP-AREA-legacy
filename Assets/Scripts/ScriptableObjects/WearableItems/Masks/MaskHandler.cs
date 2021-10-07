using UnityEngine;
using Zenject;

public class MaskHandler : ItemHandler
{
    [SerializeField] Mask_SO m_mask_SO;

    [Inject] protected readonly WearableItemsInventory m_wearableItemsInventory;

    public override void Equip()
    {
        m_wearableItemsInventory.MaskSlot.SetItem(this);
    }

    public override Item_SO GetItem() => m_mask_SO;
}
