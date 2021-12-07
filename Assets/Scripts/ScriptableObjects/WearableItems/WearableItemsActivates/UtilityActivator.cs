using Zenject;

public class UtilityActivator : WearableItemActivator
{
    [Inject(Id = "KeyCardSlot")] private readonly WearableSlot _utilitySlot;

    protected override WearableSlot WearableItemSlot => _utilitySlot;

}
