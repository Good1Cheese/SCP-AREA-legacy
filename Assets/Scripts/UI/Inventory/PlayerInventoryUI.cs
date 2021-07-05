using System;
using UnityEngine;
using Zenject;

public class PlayerInventoryUI : MonoBehaviour
{
    [Inject] readonly PlayerInventory m_playerInventory;
    [Inject] readonly SettingsPresetInstaller m_settingsPresetInstaller;
    GameObject m_gameObject;

    public PickableItemSlot[] InventoryCells { get; set; }

    void Awake()
    {
        InventoryCells = transform.GetComponentsInChildren<PickableItemSlot>();
        m_playerInventory.OnInventoryChanged += UpdateUI;
        m_playerInventory.OnInventoryButtonPressed += ActivateOrCloseUI;
    }

    void Start()
    {
        m_gameObject = gameObject;
        m_gameObject.SetActive(false);
    }

    void UpdateUI()
    {
        int inventoryLength = m_playerInventory.Inventory.Length;
        for (int i = 0; i < inventoryLength; i++)
        {
            Item_SO item = m_playerInventory.Inventory[i];
            if (item != null)
            {
                InventoryCells[i].SetItem(item);
                continue;
            }
            InventoryCells[i].ClearSlot();
        }
    }

    void ActivateOrCloseUI(bool isUIActivated)
    {
        gameObject.SetActive(isUIActivated);
    }

    void OnDestroy()
    {
        m_playerInventory.OnInventoryChanged -= UpdateUI;
        m_playerInventory.OnInventoryButtonPressed -= ActivateOrCloseUI;
    }

}
