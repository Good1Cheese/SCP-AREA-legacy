using System.Collections;
using System.Collections.Generic;   
using UnityEngine;
using Zenject;

public class ItemHandler : MonoBehaviour, IInteractable
{
    [SerializeField] Item_SO item_SO;
    [Inject] PlayerInventory m_playerInventory;

    public Item_SO Item_SO { get => item_SO; }

    public void Interact()
    {
        m_playerInventory.AddItem(item_SO);
        //Destroy(this);
    }

}
