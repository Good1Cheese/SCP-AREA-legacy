using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] int m_maxClotsCount;

    int currentItemIndex;
    Item_SO[] inventory;
    public Item_SO[] Inventory { get => inventory; }
    public Action OnItemAdded { get; set; }

    void Start()
    {
        inventory = new Item_SO[m_maxClotsCount];
    }

    public bool AddItem(Item_SO item)
    {
        if (currentItemIndex >= m_maxClotsCount) { return false; }

        Inventory[currentItemIndex] = item;
        OnItemAdded.Invoke();

        currentItemIndex++;
        return true;
    }

    public void RemoveItem(Item_SO item)    
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == item)
            {
                inventory[i] = null;
                OnItemAdded.Invoke();
                currentItemIndex = i;
                return;
            }
        }
    }
}
