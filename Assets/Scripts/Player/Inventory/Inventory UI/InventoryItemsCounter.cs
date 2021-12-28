using TMPro;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TextMeshProUGUI))]
public class InventoryItemsCounter : MonoBehaviour
{
    [Inject] private readonly PickableItemsInventory _pickableItemsInventory;

    private TextMeshProUGUI _textMesh;

    private void Awake()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
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