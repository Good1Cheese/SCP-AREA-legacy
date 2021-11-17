using UnityEngine;
using Zenject;

public class WeaponMissFireSound : WeaponSoundPlayer
{
    [Inject] private readonly WeaponMiss _weaponAmmoController;

    protected override AudioClip Sound => _weaponHandler.Weapon_SO.missFireSound;

    protected override void SubscribeToAction()
    {
        _weaponAmmoController.OnAmmoRunOut += PlaySound;
    }

    protected override void UnscribeToAction()
    {
        _weaponAmmoController.OnAmmoRunOut -= PlaySound;
    }
}
