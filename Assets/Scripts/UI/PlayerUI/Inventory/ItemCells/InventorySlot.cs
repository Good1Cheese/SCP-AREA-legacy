using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] protected Image m_image;

    public Item_SO Item { get; set; }

    public void SetItem(Item_SO item)
    {
        Item = item;
        m_image.sprite = item.sprite;
        OnItemSet();
    }

    public void Clear()
    {
        Item.IsInInventory = false;
        Item = null;
        m_image.sprite = null;
        OnItemDeleted();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (Item == null) { return; }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public abstract void OnItemSet();
    public abstract void OnItemDeleted();
    public abstract void OnRightClick();
}