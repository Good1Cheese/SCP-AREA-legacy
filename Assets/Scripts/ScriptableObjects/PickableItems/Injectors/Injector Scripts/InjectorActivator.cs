using Zenject;

public class InjectorActivator : WearableItemActivator
{
    [Inject]
    private void Inject(InjectorSlot injectorSlot)
    {
        _itemSlot = injectorSlot;
    }
}