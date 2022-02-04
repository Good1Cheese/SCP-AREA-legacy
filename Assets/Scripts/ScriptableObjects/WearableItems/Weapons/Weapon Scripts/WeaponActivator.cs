using Zenject;

public class WeaponActivator : WearableItemActivator
{
    private WeaponRequestsHandler _weaponRequestsHandler;

    public override bool CanItemActivateDeactivate => base.CanItemActivateDeactivate && !_weaponRequestsHandler.IsCoroutineGoing;

    [Inject]
    private void Inject(WeaponSlot weaponSlot, WeaponRequestsHandler weaponRequestsHandler)
    {
        _itemSlot = weaponSlot;
        _weaponRequestsHandler = weaponRequestsHandler;
    }
}