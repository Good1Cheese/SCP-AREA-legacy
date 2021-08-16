using UnityEngine;
using Zenject;

public class WeaponMissFireSound : WeaponSoundPlayer
{
    [Inject] readonly WeaponMiss m_weaponAmmoController;

    protected override void SubscribeToAction()
    {
        m_weaponAmmoController.OnAmmoRunOut += PlaySound;
    }

    protected override void UnscribeToAction()
    {
        m_weaponAmmoController.OnAmmoRunOut -= PlaySound;
    }
    protected override void ChangeAudio(Weapon_SO weapon)
    {
        if (weapon == null) { return; }
        audioSource.clip = weapon.missFireSound;
    }
}
