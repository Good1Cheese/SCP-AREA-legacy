using UnityEngine;
using Zenject;

public class InjectorSaving : WearableItemSaving
{
    [Inject] private readonly InjectorSlot _injectorSlot;

    protected override WearableSlot SlotToSave => _injectorSlot;

    public override void Load(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);

        if (string.IsNullOrEmpty(itemName)) { return; }

        SlotToSave.Activator.TrySetItemActiveState(isActive);
    }
}