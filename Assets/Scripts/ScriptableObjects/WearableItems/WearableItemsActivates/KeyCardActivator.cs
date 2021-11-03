using UnityEngine;

public class KeyCardActivator : WearableItemActivator
{
    protected override WearableItemSlot WearableItemSlot => m_wearableItemsInventory.KeyCardSlot;
}
