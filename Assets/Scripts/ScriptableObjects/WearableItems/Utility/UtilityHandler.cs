using Zenject;

public class UtilityHandler : WearableItemHandler
{
    [Inject] private readonly UtilitySlot _utilitySlot;

    public override void Equip()
    {
        _utilitySlot.SetItem(this);
    }
}