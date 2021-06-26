using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInventoryUI : MonoBehaviour
{
    [Inject] PlayerInventory m_playerInventory;
    [Inject] SettingsPresetInstaller m_settingsPresetInstaller;
    GameObject m_gameObject;

    public static List<InventoryCell> InventorySlots { get; set; } = new List<InventoryCell>();

    void Start()
    {
        m_gameObject = gameObject;
        m_gameObject.SetActive(false);
        m_playerInventory.OnItemAdded += UpdateUI;
        m_playerInventory.OnInventoryButtonPressed += ActivateOrCloseUI;
    }

    void UpdateUI()
    {
        int inventoryLength = m_playerInventory.Inventory.Length;
        for (int i = 0, b = inventoryLength - 1; i < inventoryLength; i++, b--)
        {
            Item_SO item = m_playerInventory.Inventory[i];
            if (item != null)
            {
                InventorySlots[b].SetItem(item);
            }
            else
            {
                InventorySlots[b].ClearSlot();
            }
        }
    }

    void ActivateOrCloseUI(bool isUIActivated)
    {
        gameObject.SetActive(isUIActivated);
    }

    void OnDestroy()
    {
        m_playerInventory.OnItemAdded -= UpdateUI;
        m_playerInventory.OnInventoryButtonPressed -= ActivateOrCloseUI;
    }

}
