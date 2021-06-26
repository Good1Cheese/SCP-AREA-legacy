using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ItemHandler : MonoBehaviour, IInteractable
{
    [SerializeField] Item_SO m_item_SO;
    [Inject] PlayerInventory m_playerInventory;

    GameObject m_gameObject;

    public Item_SO Item_SO { get => m_item_SO; }

    void Awake()
    {
        m_gameObject = gameObject;
    }

    public void Interact()
    {
        m_item_SO.gameobject = m_gameObject;
        m_playerInventory.AddItem(m_item_SO);
        m_gameObject.SetActive(false);
    }

}
