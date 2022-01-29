using Zenject;

public class UtilityHandler : WearableItemHandler
{
    [Inject]
    private void Construct(UtilitySlot utilitySlot)
    {
        _wearableSlot = utilitySlot;
    }

    public override void Equip()
    {
        _wearableSlot.SetItem(this);
    }
}