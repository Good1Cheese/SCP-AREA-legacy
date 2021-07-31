using System;
using UnityEngine;

[RequireComponent(typeof(EquipmentInventory))]
public class PlayerInventory : MonoBehaviour
{
    [SerializeField] int m_maxSlotsAmount;
    [SerializeField] Vector3 m_itemsOffsetForSpawn;

    public bool IsUIActivte { get; set; }
    Transform m_transform;

    public PickableItem_SO[] Inventory { get; set; }
    public Action OnInventoryChanged { get; set; }
    public Action<bool> OnInventoryButtonPressed { get; set; }

    public bool HasItem(PickableItem_SO item)
    {
        for (int i = 0; i < Inventory.Length; i++)
        {
            if (Inventory[i] == item)
            {
                return true;
            }
        }
        return false;
    }

    public int CurrentItemIndex { get; set; }

    public Action<PickableItemSlot> OnItemRightClicked { get; set; }
    public Action<PickableItemSlot> OnItemLeftClicked { get; set; }

    void Start()
    {
        Inventory = new PickableItem_SO[m_maxSlotsAmount];
        m_transform = transform;
    }

    public PickableItem_SO GetItem(PickableItem_SO item)
    {
        for (int i = 0; i < Inventory.Length; i++)
        {
            if (Inventory[i] == item)
            {
                return Inventory[i];
            }
        }
        return null;
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            IsUIActivte = !IsUIActivte;
            OnInventoryButtonPressed.Invoke(IsUIActivte);
        }
    }

    public void AddItem(PickableItem_SO item)
    {
        if (CurrentItemIndex >= m_maxSlotsAmount) { return; }

        Inventory[CurrentItemIndex] = item;
        CurrentItemIndex++;

        OnInventoryChanged.Invoke();
    }

    public void RemoveItem(Item_SO item)
    {
        int indexOfLastEnterance = 0;
        for (int i = 0; i < Inventory.Length; i++)
        {
            if (Inventory[i] == item)
            {
                indexOfLastEnterance = i;
            }
        }

        Inventory[indexOfLastEnterance] = null;
        CurrentItemIndex = indexOfLastEnterance;

        OnInventoryChanged.Invoke();
    }

    public void SpawnItem(PickableItemSlot slot)
    {
        GameObject gameobjectOfItem = slot.Item.gameObject;
        Instantiate(gameobjectOfItem, m_transform.position + m_transform.forward, gameobjectOfItem.transform.rotation);
        RemoveItem(slot.Item);
    }
}
