using Zenject;

public class UtilityActivator : WearableItemActivator
{
    [Inject]
    private void Inject(UtilitySlot utilitySlot)
    {
        _itemSlot = utilitySlot;
    }
}