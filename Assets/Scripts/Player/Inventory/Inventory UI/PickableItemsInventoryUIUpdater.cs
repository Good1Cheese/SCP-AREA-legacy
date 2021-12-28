using UnityEngine;
using Zenject;

public class PickableItemsInventoryUIUpdater : MonoBehaviour
{
    [Inject] private readonly PickableItemsInventory _playerInventory;
    [Inject] private readonly PlayerHealth _playerHealth;

    private GameObject _gameObject;

    public PickableSlot[] InventoryCells { get; set; }

    private void Awake()
    {
        _gameObject = gameObject;
        _playerInventory.Changed += Renew;
        _playerHealth.Died += DisableUI;
    }

    private void Start()
    {
        InventoryCells = transform.GetComponentsInChildren<PickableSlot>();

        for (int i = 0; i < InventoryCells.Length; i++)
        {
            InventoryCells[i].SlotIndex = i;
            InventoryCells[i].gameObject.SetActive(false);
        }

        _gameObject.SetActive(false);
    }

    private void Renew()
    {
        for (int i = 0; i < _playerInventory.Inventory.Length; i++)
        {
            ItemHandler item = _playerInventory.Inventory[i];

            if (item != null)
            {
                InventoryCells[i].SetItem(item);
                continue;
            }

            if (InventoryCells[i].ItemHandler != null)
            {
                InventoryCells[i].Clear();
            }
        }
    }

    public void ActivateOrClose()
    {
        _gameObject.SetActive(!_gameObject.activeSelf);
    }

    public void DisableUI()
    {
        if (_gameObject.activeSelf)
        {
            ActivateOrClose();
        }
    }

    private void OnDestroy()
    {
        _playerInventory.Changed -= Renew;
        _playerHealth.Died -= DisableUI;
    }
}