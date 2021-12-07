using Zenject;

public class UtilityHandler : WearableItemHandler
{
    [Inject(Id = "UtilitySlot")] private readonly WearableSlot _utilitySlot;

    public override void Equip()
    {
        _utilitySlot.SetItem(this);
    }
}