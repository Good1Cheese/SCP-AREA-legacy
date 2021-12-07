using Zenject;

public class MaskSaving : WearableItemSaving
{
    [Inject(Id = "MaskSlot")] private readonly WeaponSlot _maskSlot;

    protected override WearableSlot SlotToSave => _maskSlot;
}
