using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public abstract class WearableItemSlot : InventorySlot
{
    [Inject] readonly EquipmentInventory m_equipmentInventory;

    public override void OnItemSetted()
    {
        m_image.enabled = true;
    }

    public override void OnItemDeleted()
    {
        m_image.enabled = false;
    }

    public override Action<Vector2, InventorySlot> GetActionToInvokeOnClick()
    {
        return m_equipmentInventory.OnItemClicked;
    }

    protected abstract void AddToEquipmentInventory(EquipmentInventory m_equipmentInventory);

}
