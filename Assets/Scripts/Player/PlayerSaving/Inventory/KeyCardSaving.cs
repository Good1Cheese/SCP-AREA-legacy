using Zenject;

public class KeyCardSaving : WearableItemSaving
{
    [Inject(Id = "KeyCardSlot")] private readonly WearableSlot _keyCardSlot;

    protected override WearableSlot SlotToSave => _keyCardSlot;
}
