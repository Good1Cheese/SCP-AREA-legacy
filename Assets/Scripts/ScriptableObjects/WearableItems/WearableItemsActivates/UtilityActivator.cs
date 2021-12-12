using Zenject;

public class UtilityActivator : WearableItemActivator
{
    [Inject] private readonly UtilitySlot _utilitySlot;

    protected override WearableSlot WearableItemSlot => _utilitySlot;
}