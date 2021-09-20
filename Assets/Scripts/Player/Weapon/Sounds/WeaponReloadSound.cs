using Zenject;

public class WeaponReloadSound : WeaponSoundPlayer
{
    [Inject] readonly WeaponReload m_weaponReload;

    protected override void SubscribeToAction()
    {
        m_weaponReload.OnPlayerReloaded += PlaySound;
    }

    protected override void UnscribeToAction()
    {
        m_weaponReload.OnPlayerReloaded -= PlaySound;
    }

    protected override void ChangeAudio(Weapon_SO weapon)
    {
        audioSource.clip = weapon.reloadSoundPrefab;
    }

}
