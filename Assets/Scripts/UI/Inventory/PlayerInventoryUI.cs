using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerInventoryUI : MonoBehaviour
{
    [Inject] PlayerInventory m_playerInventory;

    public static List<InventoryCell> InventorySlots { get; set; } = new List<InventoryCell>();

    void Start()
    {
        m_playerInventory.OnItemAdded += UpdateUI;
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

    void OnDisable()
    {
        m_playerInventory.OnItemAdded -= UpdateUI;
    }

}
