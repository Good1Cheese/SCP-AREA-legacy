using Zenject;

public class MaskHandler : WearableItemHandler
{
    [Inject]
    private void Construct(MaskSlot maskSlot)
    {
        _wearableSlot = maskSlot;
    }

    public override void Equip()
    {
        _wearableSlot.SetItem(this);
    }
}