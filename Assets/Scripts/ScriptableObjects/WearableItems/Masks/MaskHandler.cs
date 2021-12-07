using Zenject;

public class MaskHandler : WearableItemHandler
{
    [Inject(Id = "MaskSlot")] private readonly WearableSlot _maskSlot;

    public override void Equip()
    {
        _maskSlot.SetItem(this);
    }
}
