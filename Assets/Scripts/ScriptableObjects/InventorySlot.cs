using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] protected Image _image;

    public ItemHandler ItemHandler { get; set; }

    public void SetItem(ItemHandler itemHandler)
    {
        ItemHandler = itemHandler;
        _image.sprite = itemHandler.Item_SO.sprite;
        Setted();
    }

    public void Clear()
    {
        ItemHandler.Dropped();
        ClearSlot();
    }

    public void ClearSlot()
    {
        Cleared();
        ItemHandler = null;
        _image.sprite = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (ItemHandler == null) { return; }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            eventData.clickCount = 0;
            Clicked();
        }
    }

    public abstract void Setted();
    public abstract void Cleared();
    public abstract void Clicked();
}