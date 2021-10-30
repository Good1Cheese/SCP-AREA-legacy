using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(m_wearableItemsInventory), typeof(InventoryEnablerDisabler))]
public class PickableItemsInventory : MonoBehaviour
{
    [SerializeField, Range(0, 8)] int m_maxSlotsAmount;
    [SerializeField] Vector3 m_itemsOffsetForSpawn;

    [Inject(Id = "Player")] readonly Transform m_playerTransform;

    public ItemHandler[] Inventory { get; set; }
    public Action OnInventoryChanged { get; set; }
    public Action OnInventoryRemaded { get; set; }

    public int CurrentItemIndex { get; set; }

    void Awake()
    {
        Inventory = new ItemHandler[m_maxSlotsAmount];
    }

    public void AddItem(ItemHandler item)
    {
        if (CurrentItemIndex >= m_maxSlotsAmount) { return; }

        Inventory[CurrentItemIndex] = item;
        CurrentItemIndex++;

        OnInventoryChanged?.Invoke();
    }

    public void RemoveItem(int index)
    {
        Inventory[index].IsInInventory = false;
        Inventory[index] = null;

        bool isIndexItemLast = index > CurrentItemIndex;
        if (!isIndexItemLast)
        {
            for (int i = index + 1; Inventory[i] != null; i++)
            {
                Inventory[i - 1] = Inventory[i];
                Inventory[i].SetIsInventoty(false);
                Inventory[i] = null;
            }
        }

        CurrentItemIndex = !isIndexItemLast ? CurrentItemIndex - 1 : index - 1;
        OnInventoryChanged?.Invoke();
    }

    public void RemoveItem(ItemHandler itemHandler)
    {
        int index = Array.IndexOf(Inventory, itemHandler);

        if (index == -1) 
        {
            Debug.LogError("Item is null!");
        }
        
        RemoveItem(index);
    }

    public ItemHandler GetIem(Predicate<ItemHandler> condition)
    {
        return Inventory.TakeWhile(item => item != null).LastOrDefault(item => condition.Invoke(item));
    }
}