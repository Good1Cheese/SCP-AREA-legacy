using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class PickableItemSlot : InventorySlot, IPointerClickHandler
{
    private const int CLICK_COUNT_TO_USE = 2;

    [SerializeField] private TextMeshProUGUI _itemDescription;

    [Inject] private readonly PickableItemsInteraction _pickableItemsInteraction;
    private GameObject _gameObject;

    public int SlotIndex { get; set; }

    private void Awake()
    {
        _gameObject = gameObject;
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
        _pickableItemsInteraction.UseItem(this);
    }

    public override void OnRightClick()
    {
        _pickableItemsInteraction.DropItem(this);
    }

    public override void OnItemSet()
    {
        _itemDescription.text = ItemHandler.Item.description;
        _gameObject.SetActive(true);
    }

    public override void OnItemDeleted()
    {
        _gameObject.SetActive(false);
    }
}
