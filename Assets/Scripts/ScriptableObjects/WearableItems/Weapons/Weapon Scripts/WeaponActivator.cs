using Zenject;

public class WeaponActivator : WearableItemActivator
{
    [Inject]
    private void Inject(WeaponSlot weaponSlot)
    {
        _itemSlot = weaponSlot;
    }
}