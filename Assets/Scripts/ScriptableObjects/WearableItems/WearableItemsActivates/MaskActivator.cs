using Zenject;

public class MaskActivator : WearableItemActivator
{
    [Inject] private readonly MaskSlot _maskSlot;

    protected override WearableSlot WearableItemSlot => _maskSlot;
}