using System;
using System.Linq;
using System.Collections;
using Zenject;

public class WeaponReload : CoroutineUser
{
    private WeaponHandler _weaponHandler;
    protected WeaponSlot _weaponSlot;
    private AmmoPackage _ammoPackage;

    public Action Reloaded { get; set; }
    public ItemSlot<AmmoHandler> CurrentClip { get; set; }
    public ItemSlot<AmmoHandler> NextClipAmmo
    {
        get
        {
            return CurrentClip = _ammoPackage.Сlips.Slots
                .Where(slot => slot.HasItem)
                .OrderByDescending(slot => slot.Item.Ammo)
                .FirstOrDefault();
        }
    }

    [Inject]
    private void Inject(WeaponSlot weaponSlot, AmmoPackage ammoPackage)
    {
        _weaponSlot = weaponSlot;
        _ammoPackage = ammoPackage;
    }

    private new void Start()
    {
        base.Start();
        _weaponSlot.Changed += SetWeaponHandler;
    }

    protected override IEnumerator Coroutine()
    {
        yield return _weaponHandler.Weapon_SO.reloadTimeout;

        CurrentClip = NextClipAmmo;
        _weaponHandler.ClipAmmo = CurrentClip.Item.Ammo;

        IsCoroutineGoing = false;
    }

    protected void SetWeaponHandler(WeaponHandler weaponHandler)
    {
        _weaponHandler = weaponHandler;
    }
}