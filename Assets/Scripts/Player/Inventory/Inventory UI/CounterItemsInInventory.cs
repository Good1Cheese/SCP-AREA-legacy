using TMPro;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CounterItemsInInventory : MonoBehaviour
{
    [Inject] private readonly PickableItemsInventory _playerInventory;
    private TextMeshProUGUI _textMesh;

    private void Awake()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
        _playerInventory.OnInventoryChanged += UpdateItemsCount;
        _playerInventory.OnInventoryRemaded += UpdateItemsCount;
    }

    private void UpdateItemsCount()
    {
        _textMesh.text = string.Format($"{_playerInventory.CurrentItemIndex}/{_playerInventory.Inventory.Length}");
    }

    private void OnDestroy()
    {
        _playerInventory.OnInventoryChanged -= UpdateItemsCount;
        _playerInventory.OnInventoryRemaded -= UpdateItemsCount;
    }
}
