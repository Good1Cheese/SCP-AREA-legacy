using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class PickableSlot : ItemSlot, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI _itemDescription;

    private GameObject _gameObject;

    public int SlotIndex { get; set; }

    [Inject]
    private void Construct(PickableItemsUse pickableItemsUse, PickabeItemsDrop pickableItemsDrop)
    {
        _inventoryItemsUse = pickableItemsUse;
        _inventoryItemsDrop = pickableItemsDrop;
    }

    private void Awake()
    {
        _gameObject = gameObject;
    }

    public override void Setted()
    {
        _itemDescription.text = ItemHandler.Item_SO.description;
        _gameObject.SetActive(true);
    }

    public override void Cleared()
    {
        _gameObject.SetActive(false);
    }
}