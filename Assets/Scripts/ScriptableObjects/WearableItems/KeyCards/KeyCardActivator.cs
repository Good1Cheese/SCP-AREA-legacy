using Zenject;

public class KeyCardActivator : WearableItemActivator
{
    [Inject]
    private void Inject(KeyCardSlot keyCardSlot)
    {
        _itemSlot = keyCardSlot;
    }
}