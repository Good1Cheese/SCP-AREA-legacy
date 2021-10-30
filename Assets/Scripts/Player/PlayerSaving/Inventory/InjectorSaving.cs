using UnityEngine;
using Zenject;

public class InjectorSaving : WearableItemSaving
{
    [Inject] readonly m_wearableItemsInventory m_wearableItemsInventory;

    protected override WearableItemSlot SlotToSave => m_wearableItemsInventory.InjectorSlot;

    public override void Load(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);

        if (string.IsNullOrEmpty(itemName)) { return; }

        SlotToSave.WearableItemActivator.SetItemActiveState(isActive);
    }
}
