using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] protected Image m_image;

    public ItemHandler ItemHandler { get; set; }

    public void SetItem(ItemHandler item)
    {
        ItemHandler = item;
        m_image.sprite = item.GetItem().sprite;
        OnItemSet();
    }

    public void Clear()
    {
        ItemHandler.OnItemDropped();
        OnItemDeleted();
        ItemHandler = null;
        m_image.sprite = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (ItemHandler == null) { return; }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            eventData.clickCount = 0;
            OnRightClick();
        }
    }

    public abstract void OnItemSet();
    public abstract void OnItemDeleted();
    public abstract void OnRightClick();
}