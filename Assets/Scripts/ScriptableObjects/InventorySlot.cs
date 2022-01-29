using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    private const int CLICK_COUNT_TO_USE = 2;

    [SerializeField] protected Image _image;

    protected ItemsInteraction _inventoryItemsUse;
    protected ItemsInteraction _inventoryItemsDrop;

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
            _inventoryItemsDrop.CallFunction(this);
        }

        if (eventData.clickCount == CLICK_COUNT_TO_USE)
        {
            _inventoryItemsUse.CallFunction(this);
        }
    }

    public abstract void Setted();
    public abstract void Cleared();
}