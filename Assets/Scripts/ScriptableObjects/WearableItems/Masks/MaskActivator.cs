using Zenject;

public class MaskActivator : WearableItemActivator
{
    [Inject]
    private void Inject(MaskSlot maskSlot)
    {
        _itemSlot = maskSlot;
    }
}