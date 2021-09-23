using UnityEngine;
using Zenject;

public class WeaponShotSound : WeaponSoundPlayer
{
    [Inject] readonly WeaponFire m_weaponFire;
    WeaponHandler m_weaponHandler;

    protected override void SubscribeToAction()
    {
        m_weaponFire.OnPlayerShooted += PlaySound;
        m_wearableItemsInventory.WeaponSlot.OnSilencerEquiped += ChangeAudioWithSilencer;
        m_wearableItemsInventory.WeaponSlot.OnSilencerUnequiped += ChangeAudioWithSilencer;
    }

    protected override void UnscribeToAction()
    {
        m_weaponFire.OnPlayerShooted -= PlaySound;
        m_wearableItemsInventory.WeaponSlot.OnSilencerEquiped -= ChangeAudioWithSilencer;
        m_wearableItemsInventory.WeaponSlot.OnSilencerUnequiped -= ChangeAudioWithSilencer;
    }

    protected override void ChangeAudio(WeaponHandler weaponHandler)
    {
        m_weaponHandler = weaponHandler;
        audioSource.clip = weaponHandler.CurrentShotSound;
    }

    void ChangeAudioWithSilencer()
    {
        AudioClip shotSound = m_weaponHandler.Weapon_SO.shotSoundPrefab;
        if (m_weaponHandler.SilencerHandler != null)
        {
            shotSound = m_weaponHandler.Weapon_SO.shotSoundWithSilencerPrefab;
        }

        m_weaponHandler.CurrentShotSound = shotSound;
        audioSource.clip = m_weaponHandler.CurrentShotSound;
    }
}
