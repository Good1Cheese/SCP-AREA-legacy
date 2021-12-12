using Zenject;

public class KeyCardActivator : WearableItemActivator
{
    [Inject] private readonly KeyCardSlot _keyCardSlot;

    protected override WearableSlot WearableItemSlot => _keyCardSlot;
}