using Zenject;

public class KeyCardHandler : WearableItemHandler
{
    public KeyCard_SO KeyCard_SO => (KeyCard_SO)_wearableIte_SO;

    [Inject]
    private void Construct(KeyCardSlot keyCardSlot)
    {
        _wearableSlot = keyCardSlot;
    }

    public override void Equip()
    {
        _wearableSlot.SetItem(this);
    }
}