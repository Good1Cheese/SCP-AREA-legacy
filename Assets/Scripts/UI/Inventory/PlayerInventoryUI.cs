using UnityEngine;
using Zenject;

public class PlayerInventoryUI : MonoBehaviour
{
    [Inject] readonly PlayerInventory m_playerInventory;
    GameObject m_gameObject;

    public PickableItemSlot[] InventoryCells { get; set; }

    void Awake()
    {
        InventoryCells = transform.GetComponentsInChildren<PickableItemSlot>();
        m_playerInventory.OnInventoryChanged += UpdateUI;
        m_playerInventory.OnInventoryRemaded += UpdateUI;
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
            PickableItem_SO item = m_playerInventory.Inventory[i];
            if (item != null)
            {
                InventoryCells[i].SetItem(item);
                continue;
            }

            if (InventoryCells[i].Item != null)
            {
                print(InventoryCells[i].Item);
                InventoryCells[i].ClearSlot();
            }
        }
    }

    public void ActivateOrCloseUI()
    {
        m_gameObject.SetActive(!m_gameObject.activeSelf);
    }

    void OnDestroy()
    {
        m_playerInventory.OnInventoryChanged -= UpdateUI;
        m_playerInventory.OnInventoryRemaded -= UpdateUI;
    }

}
