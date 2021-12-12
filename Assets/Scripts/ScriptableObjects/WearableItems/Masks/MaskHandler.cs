using Zenject;

public class MaskHandler : WearableItemHandler
{
    [Inject] private readonly MaskSlot _maskSlot;

    public override void Equip()
    {
        _maskSlot.SetItem(this);
    }
}