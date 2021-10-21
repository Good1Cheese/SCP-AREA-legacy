using UnityEngine;

public class KeyCardActivator : WearableItemActivator
{
    void Awake()
    {
        m_wearableItemSlot = m_wearableItemsInventory.KeyCardSlot;
    }
}
