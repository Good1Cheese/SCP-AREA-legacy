using Zenject;

public class WeaponShotSound : WeaponSoundPlayer
{
    [Inject] readonly WeaponFire m_weaponFire;

    protected override void SubscribeToAction()
    {
        m_weaponFire.OnPlayerShooted += PlaySound;
        Weapon_SO.OnSilencerEquiped += ChangeAudioWithSilencer;
    }

    protected override void UnscribeToAction()
    {
        Weapon_SO.OnSilencerEquiped -= ChangeAudioWithSilencer;
        m_weaponFire.OnPlayerShooted -= PlaySound;
    }

    protected override void ChangeAudio(Weapon_SO weapon)
    {
        audioSource.clip = weapon.shotSound;
    }

    void ChangeAudioWithSilencer(Weapon_SO weapon)
    {
        audioSource.clip = weapon.shotSoundWithSilencer;
    }


}
