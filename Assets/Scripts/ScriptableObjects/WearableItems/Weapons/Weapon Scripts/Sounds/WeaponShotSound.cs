using UnityEngine;
using Zenject;

public class WeaponShotSound : WeaponSoundPlayer
{
    [Inject] readonly WeaponFire m_weaponFire;
    WeaponHandler m_weaponHandler;

    protected override void SubscribeToAction()
    {
        m_weaponFire.OnPlayerShooted += PlaySound;
        m_wearableItemsInventory.WeaponSlot.OnSilencerEquiped += SetSilencerShotSound;
        m_wearableItemsInventory.WeaponSlot.OnSilencerUnequiped += SetRegularShotSound;
    }

    protected override void UnscribeToAction()
    {
        m_weaponFire.OnPlayerShooted -= PlaySound;
        m_wearableItemsInventory.WeaponSlot.OnSilencerEquiped -= SetSilencerShotSound;
        m_wearableItemsInventory.WeaponSlot.OnSilencerUnequiped -= SetRegularShotSound;
    }

    protected override void ChangeAudio(WeaponHandler weaponHandler)
    {
        m_weaponHandler = weaponHandler;
        audioSource.clip = weaponHandler.CurrentShotSound;
    }

    private void SetSilencerShotSound()
    {
        audioSource.clip = m_weaponHandler.CurrentShotSound;
    }

    void SetRegularShotSound()
    {
        m_weaponHandler.CurrentShotSound = m_weaponHandler.Weapon_SO.shotSound;
        audioSource.clip = m_weaponHandler.CurrentShotSound;
    }
}
