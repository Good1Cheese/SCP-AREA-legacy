using TMPro;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TextMeshProUGUI))]
public class InventoryItemsCounter : MonoBehaviour
{
    private PickableItemsInventory _pickableItemsInventory;
    private TextMeshProUGUI _textMesh;

    [Inject]
    private void Construct(PickableItemsInventory pickableItemsInventory)
    {
        _pickableItemsInventory = pickableItemsInventory;
    }

    private void Awake()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _pickableItemsInventory.Changed += UpdateItemsCount;
    }

    private void UpdateItemsCount()
    {
        _textMesh.text = string.Format($"{_pickableItemsInventory.CurrentItemIndex.ToString()}/{_pickableItemsInventory.Inventory.Length.ToString()}");
    }

    private void OnDestroy()
    {
        _pickableItemsInventory.Changed -= UpdateItemsCount;
    }
}