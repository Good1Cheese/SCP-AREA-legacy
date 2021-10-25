using UnityEngine;
using Zenject;
using UnityEngine.EventSystems;
using TMPro;

public class PickableItemSlot : InventorySlot, IPointerClickHandler
{
    const int CLICK_COUNT_TO_USE = 2;

    [SerializeField] TextMeshProUGUI m_itemDescription;

    [Inject] readonly PickableItemsInteraction m_pickableItemsInteraction;

    GameObject m_gameObject;

    public int SlotIndex { get; set; }

    void Awake()
    {
        m_gameObject = gameObject;
    }

    public new void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if (eventData.clickCount == CLICK_COUNT_TO_USE)
        {
            OnDoubleLeftClick();
        }
    }

    public void OnDoubleLeftClick()
    {
        m_pickableItemsInteraction.UseItem(this);
    }

    public override void OnRightClick()
    {
        m_pickableItemsInteraction.DropItem(this);
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
}
