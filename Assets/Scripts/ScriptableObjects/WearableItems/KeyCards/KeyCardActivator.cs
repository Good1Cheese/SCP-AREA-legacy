using Zenject;

public class KeyCardActivator : WearableItemActivator
{
    [Inject] private readonly KeyCardSlot _keyCardSlot;

    public override WearableSlot Slot => _keyCardSlot;
}