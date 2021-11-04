using UnityEngine;
using Zenject;

public class WeaponFireSound : WeaponSoundPlayer
{
    [Inject] readonly WeaponFire m_weaponFire;

    protected override AudioClip Sound => m_weaponHandler.CurrentShotSound;

    protected override void SubscribeToAction()
    {
        m_weaponFire.OnPlayerFired += PlaySound;
    }

    protected override void UnscribeToAction()
    {
        m_weaponFire.OnPlayerFired -= PlaySound;
    }
}
