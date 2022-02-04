using System;
using Zenject;

public class WeaponSlot : WearableSlot
{
    private AmmoPackage _ammoPackage;

    public Action<WeaponHandler> Changed { get; set; }

    [Inject]
    private void Construct(AmmoPackage ammoPackage)
    {
        _ammoPackage = ammoPackage;
    }

    public override void Setted()
    {
        base.Setted();

        Changed.Invoke(ItemHandler as WeaponHandler);
        Toggled?.Invoke(false);
    }

    protected override void ReplaceOldItem(ItemHandler newItem)
    {
        var newWeapon = (WeaponHandler)newItem;
        var oldWeapon = (WeaponHandler)ItemHandler;

        if (newWeapon.Weapon_SO.ammoType != oldWeapon.Weapon_SO.ammoType) 
        {
            _ammoPackage.DropAll(oldWeapon.Weapon_SO.ammoType);
        }

        base.ReplaceOldItem(newItem);
    }
}