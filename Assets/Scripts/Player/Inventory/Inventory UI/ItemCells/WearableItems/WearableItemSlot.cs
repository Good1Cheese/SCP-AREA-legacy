using UnityEngine;
using Zenject;

public abstract class WearableItemSlot : InventorySlot
{
    [Inject] protected readonly WearableItemsInventory m_wearableItemsInventory;
    [Inject(Id = "Player")] readonly Transform m_playerTransform;

    public new void Clear()
    {
        DespawnWeapon();
        ItemHandler.IsInInventory = false;
        base.Clear();
    }

    public override void OnItemSet()
    {
        m_image.enabled = true;
    }

    public override void OnItemDeleted()
    {
        m_image.enabled = false;
    }

    public virtual void ClearWearableSlot()
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

    public void DespawnWeapon()
    {
        ItemHandler.gameObject.transform.position = m_playerTransform.position + m_playerTransform.forward;
        ItemHandler.gameObject.SetActive(true);
    }


}
