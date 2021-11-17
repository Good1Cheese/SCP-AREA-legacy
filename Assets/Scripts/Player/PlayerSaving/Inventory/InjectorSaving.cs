using UnityEngine;
using Zenject;

public class InjectorSaving : WearableItemSaving
{
    [Inject] private readonly WearableItemsInventory _wearableItemsInventory;

    protected override WearableItemSlot SlotToSave => _wearableItemsInventory.InjectorSlot;

    public override void Load(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);

        if (string.IsNullOrEmpty(itemName)) { return; }

        SlotToSave.WearableItemActivator.SetItemActiveState(isActive);
    }
}
