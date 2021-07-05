using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class PickableItemSlot : InventorySlot
{
    [Inject] readonly PlayerInventory playerInventory;
    GameObject m_gameObject;

    void Start()
    {
        m_gameObject = gameObject;
        m_gameObject.SetActive(false);
    }

    public override void OnItemSetted()
    {
        m_gameObject.SetActive(true);
    }

    public override void OnItemDeleted()
    {
        m_gameObject.SetActive(false);
    }

    public override Action<Vector2, InventorySlot> GetActionToInvokeOnClick()
    {
        return playerInventory.OnItemClicked;
    }
}
