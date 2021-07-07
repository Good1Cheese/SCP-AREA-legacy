using System;
using UnityEngine;
using Zenject;

public abstract class WearableItemSlot : InventorySlot
{
    [Inject] protected readonly EquipmentInventory m_equipmentInventory;

    void Awake()
    {
        AddToEquipmentInventory();
    }

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

    protected abstract void AddToEquipmentInventory();

}
