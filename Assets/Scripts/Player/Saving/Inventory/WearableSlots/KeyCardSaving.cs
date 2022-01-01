using Zenject;

public class KeyCardSaving : WearableItemSaving
{
    [Inject] private readonly KeyCardSlot _keyCardSlot;

    protected override WearableSlot SlotToSave => _keyCardSlot;
}