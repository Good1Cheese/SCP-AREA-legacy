using Zenject;

public abstract class WeaponAction : ItemAction
{
    [Inject] protected readonly WeaponSlot _weaponSlot;

    protected WeaponHandler _weaponHandler;

    protected override WearableSlot ItemSlot => _weaponSlot;
    protected override WearableItemHandler WearableItemHandler => _weaponHandler;

    protected new void Start()
    {
        base.Start();

        _weaponSlot.OnWeaponChanged += SetWeapon;
    }

    protected virtual void SetWeapon(WeaponHandler weaponHandler)
    {
        _weaponHandler = weaponHandler;
    }

    protected new void OnDestroy()
    {
        base.OnDestroy();

        _weaponSlot.OnWeaponChanged -= SetWeapon;
    }
}