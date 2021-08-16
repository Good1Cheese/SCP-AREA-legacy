using UnityEngine;
using Zenject;

[RequireComponent(typeof(ItemDataHandler))]
public class ItemDataSaving : MonoBehaviour//, IDataHandler
{
    [Inject] readonly PlayerInventory m_playerInventory;

    ItemHandler m_itemHandler;
    ItemDataHandler m_itemDataHandler;

    void Awake()
    {
        m_itemHandler = GetComponent<ItemHandler>();
        m_itemDataHandler = GetComponent<ItemDataHandler>();
    }

    void Start()
    {
        m_playerInventory.OnInventoryChanged += SetSavableState;
    }

    void SetSavableState()
    {
        if (m_itemHandler.Item_SO.HasPlayerThisItem())
        {
            m_itemDataHandler.BecomeUnsaveable();
            return;
        }
        if (m_itemDataHandler.IsSubscribed) { return; }
        m_itemDataHandler.BecomeSaveable();
    }

    void OnDestroy()
    {
        m_playerInventory.OnInventoryChanged -= SetSavableState;
    }
}
