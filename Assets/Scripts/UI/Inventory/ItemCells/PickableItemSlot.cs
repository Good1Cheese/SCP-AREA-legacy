﻿using UnityEngine;
using Zenject;
using UnityEngine.EventSystems;
using TMPro;

public class PickableItemSlot : InventorySlot, IPointerClickHandler
{
    const int CLICK_COUNT_TO_USE = 2;
    [SerializeField] TextMeshProUGUI m_itemDescription;
    [Inject] readonly PickableItemsInventory playerInventory;
    GameObject m_gameObject;

    void Start()
    {
        m_gameObject = gameObject;
        m_gameObject.SetActive(false);
    }

    public override void OnItemSet()
    {
        m_itemDescription.text = Item.description;
        m_gameObject.SetActive(true);
    }

    public override void OnItemDeleted()
    {
        m_gameObject.SetActive(false);
    }

    public override void OnRightClick()
    {
        playerInventory.OnItemRightClicked.Invoke(this);
    }

    public new void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        if (eventData.clickCount == CLICK_COUNT_TO_USE)
        {
            OnLeftClick();
        }
    }

    public void OnLeftClick()
    {
        playerInventory.OnItemLeftClicked.Invoke(this);
    }
}
