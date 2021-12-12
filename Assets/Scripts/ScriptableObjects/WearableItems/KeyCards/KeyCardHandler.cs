using Zenject;

public class KeyCardHandler : WearableItemHandler
{
    [Inject] private readonly KeyCardSlot _keyCardSlot;

    public KeyCard_SO KeyCard_SO => (KeyCard_SO)_wearableIte_SO;

    public override void Equip()
    {
        _keyCardSlot.SetItem(this);
    }
}