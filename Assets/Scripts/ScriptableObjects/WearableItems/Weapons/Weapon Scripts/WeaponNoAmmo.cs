using UnityEngine;

public class WeaponNoAmmo : WeaponScriptBase
{
    public override WaitForSeconds InteractionTimeout => _weaponHandler.Weapon_SO.shotTimeout;
    public override AudioClip Sound => _weaponHandler.Weapon_SO.missFireSound;

    public void ShootWithNoAmmo()
    {
        _weaponRequestsHandler.Handle(this);
    }
}