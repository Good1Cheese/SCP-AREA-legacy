using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(WearableItemsInventory))]
public class PickableItemsInventory : MonoBehaviour
{
    [SerializeField, Range(0, 8)] int m_maxSlotsAmount;
    [SerializeField] Vector3 m_itemsOffsetForSpawn;

    [Inject] readonly Transform m_playerTransform;

    public PickableItem_SO[] Inventory { get; set; }
    public Action OnInventoryChanged { get; set; }
    public Action OnInventoryRemaded { get; set; }
    public Action<PickableItemSlot> OnItemRightClicked { get; set; }
    public Action<PickableItemSlot> OnItemLeftClicked { get; set; } 

    public int CurrentItemIndex { get; set; }

    void Awake()
    {
        Inventory = new PickableItem_SO[m_maxSlotsAmount];
    }

    public void AddItem(PickableItem_SO item)
    {
        if (CurrentItemIndex >= m_maxSlotsAmount) { return; }

        Inventory[CurrentItemIndex] = item;
        CurrentItemIndex++;

        OnInventoryChanged?.Invoke();
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

        OnInventoryChanged?.Invoke();
    }

    public void SpawnItem(PickableItemSlot slot)
    {
        GameObject gameobjectOfItem = slot.Item.gameObject;
        gameobjectOfItem.SetActive(true);
        gameobjectOfItem.transform.position = m_playerTransform.position + m_playerTransform.forward;
        RemoveItem(slot.Item);
    }
}
