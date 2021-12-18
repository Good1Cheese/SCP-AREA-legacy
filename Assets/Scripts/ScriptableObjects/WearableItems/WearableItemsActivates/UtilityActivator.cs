using Zenject;

public class UtilityActivator : WearableItemActivator
{
    [Inject] private readonly UtilitySlot _utilitySlot;

    public override WearableSlot Slot => _utilitySlot;
}