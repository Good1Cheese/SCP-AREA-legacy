using Zenject;

public class KeyCardActivator : WearableItemActivator
{
    [Inject(Id = "KeyCardSlot")] private readonly WearableSlot _keyCardSlot;

    protected override WearableSlot WearableItemSlot => _keyCardSlot;
}
