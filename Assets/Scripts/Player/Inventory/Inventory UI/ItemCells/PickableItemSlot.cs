using UnityEngine;
using Zenject;
using UnityEngine.EventSystems;
using TMPro;

public class PickableItemSlot : InventorySlot, IPointerClickHandler
{
    const int CLICK_COUNT_TO_USE = 2;
    [SerializeField] TextMeshProUGUI m_itemDescription;
    [Inject] readonly PickableItemsInventory playerInventory;
    GameObject m_gameObject;

    public int SlotIndex { get; set; }

    void Awake()
    {
        m_gameObject = gameObject;
    }

    public override void OnItemSet()
    {
        m_itemDescription.text = ItemHandler.GetItem().description;
        m_gameObject.SetActive(true);
    }

    public override void OnItemDeleted()
    {
        m_gameObject.SetActive(false);
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
        playerInventory.OnItemLeftClicked.Invoke(this, SlotIndex);
    }

    public override void OnRightClick()
    {
        playerInventory.OnItemRightClicked.Invoke(this, SlotIndex);
    }


}
