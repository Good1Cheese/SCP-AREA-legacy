using Zenject;

public class KeyCardHandler : WearableItemHandler
{
    [Inject(Id = "KeyCardSlot")] private readonly WearableSlot _keyCardSlot;
    public KeyCard_SO KeyCard_SO => (KeyCard_SO)_wearableIte_SO;

    public override void Equip()
    {
        _keyCardSlot.SetItem(this);
    }
}
