using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InventorySlotMoving))]
public class InventorySlot : MonoBehaviour
{
    [SerializeField] Image m_image;

    InventorySlotMoving m_InventorySlotInteractions;
    Item_SO m_item;
    public Item_SO Item { get => m_item; }

    void Awake()
    {
        m_InventorySlotInteractions = GetComponent<InventorySlotMoving>();
        PlayerInventoryUI.InventorySlots.Add(this);
    }

    public void SetItem(Item_SO item)
    {
        m_item = item;
        m_InventorySlotInteractions.enabled = true;
        m_image.sprite = item.sprite;
        m_image.enabled = true;
    }

    public void ClearSlot()
    {
        m_item = null;
        m_InventorySlotInteractions.enabled = false;
        m_image.sprite = null;
        m_image.enabled = false;
    }

    public void UseItem()
    {
        if (m_item == null) { return; }
        Item.Use();
    }

}