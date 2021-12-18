using Zenject;

public class MaskActivator : WearableItemActivator
{
    [Inject] private readonly MaskSlot _maskSlot;

    public override WearableSlot Slot => _maskSlot;
}