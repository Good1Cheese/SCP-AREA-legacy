using Zenject;

public class UtilitySaving : WearableItemSaving
{
    [Inject] private readonly UtilitySlot _utilitySlot;

    protected override WearableSlot SlotToSave => _utilitySlot;
}