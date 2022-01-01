using Zenject;

public class MaskSaving : WearableItemSaving
{
    [Inject] private readonly MaskSlot _maskSlot;

    protected override WearableSlot SlotToSave => _maskSlot;
}