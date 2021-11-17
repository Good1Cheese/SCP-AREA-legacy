using UnityEngine;
using Zenject;

public class WeaponFireSound : WeaponSoundPlayer
{
    [Inject] private readonly WeaponFire _weaponFire;

    protected override AudioClip Sound => _weaponHandler.CurrentShotSound;

    protected override void SubscribeToAction()
    {
        _weaponFire.OnPlayerFired += PlaySound;
    }

    protected override void UnscribeToAction()
    {
        _weaponFire.OnPlayerFired -= PlaySound;
    }
}
