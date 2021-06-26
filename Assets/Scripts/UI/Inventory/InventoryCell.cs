using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image m_image;

    Item_SO m_item;

    public Item_SO Item { get => m_item; }
    public static Action<PointerEventData> OnItemClicked { get; set; }

    void Awake()
    {
        PlayerInventoryUI.InventorySlots.Add(this);
    }

    public void SetItem(Item_SO item)
    {
        m_item = item;
        m_image.sprite = item.sprite;
        m_image.enabled = true;
    }

    public void ClearSlot()
    {
        m_item = null;
        m_image.sprite = null;
        m_image.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (m_item == null) { return; }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnItemClicked.Invoke(eventData);
        }
    }

}