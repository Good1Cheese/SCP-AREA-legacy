using UnityEngine;

public class WeaponNoAmmo : WeaponScriptBase
{
    public override WaitForSeconds RequestTimeout => _weaponHandler.Weapon_SO.shotTimeout;
    public override AudioClip RequestClip => _weaponHandler.Weapon_SO.missFireSound;

    public void ShootWithNoAmmo()
    {
        _weaponRequestsHandler.Handle(this);
    }
}