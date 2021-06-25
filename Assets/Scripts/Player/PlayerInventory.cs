using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] int m_maxClotsCount;

    int currentItemIndex;
    Transform m_transform;
    Item_SO[] inventory;
    public Item_SO[] Inventory { get => inventory; }
    public Action OnItemAdded { get; set; }

    void Start()
    {
        inventory = new Item_SO[m_maxClotsCount];
        m_transform = transform;
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
                ActivateItem(item);
                OnItemAdded.Invoke();
                if (i > currentItemIndex ) { return; }
                currentItemIndex = i;
                    
                return;
            }
        }
    }

    void ActivateItem(Item_SO item)
    {
        GameObject gameobject = item.gameobject;
        gameobject.SetActive(true);
        gameobject.transform.position = m_transform.position;
    }
}
