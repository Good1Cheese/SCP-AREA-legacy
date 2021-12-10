using Zenject;

public class UtilityActivator : WearableItemActivator
{
    [Inject(Id = "UtilitySlot")] private readonly WearableSlot _utilitySlot;

    protected override WearableSlot WearableItemSlot => _utilitySlot;

}