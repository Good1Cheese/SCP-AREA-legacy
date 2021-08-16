using Zenject;

public class WeaponShotSound : WeaponSoundPlayer
{
    [Inject] readonly WeaponFire m_weaponFire;
    Weapon_SO m_weapon;

    protected override void SubscribeToAction()
    {
        m_weaponFire.OnPlayerShooted += PlaySound;
        m_equipmentInventory.WeaponSlot.OnSilencerEquiped += ChangeAudioWithSilencer;
    }

    protected override void UnscribeToAction()
    {
        m_weaponFire.OnPlayerShooted -= PlaySound;
        m_equipmentInventory.WeaponSlot.OnSilencerEquiped -= ChangeAudioWithSilencer;
    }

    protected override void ChangeAudio(Weapon_SO weapon)
    {
        audioSource.clip = weapon.shotSound;
        m_weapon = weapon;
    }

    void ChangeAudioWithSilencer()
    {
        audioSource.clip = m_weapon.shotSoundWithSilencer;
    }


}
