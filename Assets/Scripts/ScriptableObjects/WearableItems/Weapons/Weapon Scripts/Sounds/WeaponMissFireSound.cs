using UnityEngine;
using Zenject;

public class WeaponMissFireSound : WeaponSoundPlayer
{
    [Inject] readonly WeaponMiss m_weaponAmmoController;

    protected override AudioClip Sound => m_weaponHandler.Weapon_SO.missFireSound;

    protected override void SubscribeToAction()
    {
        m_weaponAmmoController.OnAmmoRunOut += PlaySound;
    }

    protected override void UnscribeToAction()
    {
        m_weaponAmmoController.OnAmmoRunOut -= PlaySound;
    }
}
