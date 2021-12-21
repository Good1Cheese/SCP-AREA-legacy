using Zenject;

public abstract class WeaponScriptBase : ItemScriptBase
{
    [Inject] protected readonly WeaponSlot _weaponSlot;

    protected WeaponHandler _weaponHandler;

    protected override WearableSlot ItemSlot => _weaponSlot;
    protected override WearableItemHandler WearableItemHandler => _weaponHandler;

    protected new void Start()
    {
        base.Start();

        _weaponSlot.Changed += SetWeaponHandler;
    }

    protected virtual void SetWeaponHandler(WeaponHandler weaponHandler)
    {
        _weaponHandler = weaponHandler;
    }

    protected new void OnDestroy()
    {
        base.OnDestroy();

        _weaponSlot.Changed -= SetWeaponHandler;
    }
}