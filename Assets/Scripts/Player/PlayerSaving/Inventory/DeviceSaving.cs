using Zenject;

public class DeviceSaving : WearableItemSaving
{
    [Inject(Id = "UtilitySlot")] private readonly WearableSlot _utilitySlot;

    protected override WearableSlot SlotToSave => _utilitySlot;
}
