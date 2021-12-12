using Zenject;

public class DeviceSaving : WearableItemSaving
{
    [Inject] private readonly UtilitySlot _utilitySlot;

    protected override WearableSlot SlotToSave => _utilitySlot;
}