using System.Linq;
using UnityEngine;
using Zenject;

public class WeaponReload : WeaponScriptBase
{
    private AmmoPackage _ammoPackage;
    private ItemSlot<AmmoHandler> _currentClip;

    public int CurrentClipAmmo
    {
        get => _currentClip == null ? 0 : _currentClip.Item.Ammo;
        set => _currentClip.Item.Ammo = value;
    }

    public ItemSlot<AmmoHandler> NextClipAmmo
    {
        get
        {
            return _currentClip = _ammoPackage.Сlips.Slots
                .Where(slot => slot.HasItem)
                .OrderByDescending(slot => slot.Item.Ammo)
                .FirstOrDefault();
        }
    }

    public bool HasAmmo
    {
        get
        {
            return _ammoPackage.Сlips.Slots
                .TakeWhile(slot => slot.Item != null)
                .Sum(slot => slot.Item.Ammo) > 0;
        }
    }

    public override WaitForSeconds RequestTimeout => _weaponHandler.Weapon_SO.reloadTimeout;
    public override AudioClip RequestClip => _weaponHandler.Weapon_SO.reloadSound;
    public override bool Interuppable => true;

    [Inject]
    private void Inject(AmmoPackage ammoPackage)
    {
        _ammoPackage = ammoPackage;
    }

    public override void Interact()
    {
        _currentClip = null;
    }

    public override void OnSuccesRequest()
    {
        _currentClip = NextClipAmmo;
    }
}