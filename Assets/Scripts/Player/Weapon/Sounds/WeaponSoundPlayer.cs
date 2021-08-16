using UnityEngine;
using Zenject;


[RequireComponent(typeof(AudioSource))]
public abstract class WeaponSoundPlayer : SoundPlayerOnAction
{
    [Inject] protected readonly EquipmentInventory m_equipmentInventory;

    void Awake()
    {
        m_equipmentInventory.WeaponSlot.OnWeaponChanged += ChangeAudio;
        m_equipmentInventory.WeaponSlot.OnWeaponDropped += SetAudioToNull;
        SubscribeToAction();
    }

    void OnDestroy()
    {
        m_equipmentInventory.WeaponSlot.OnWeaponChanged -= ChangeAudio;
        m_equipmentInventory.WeaponSlot.OnWeaponDropped -= SetAudioToNull;
        UnscribeToAction();
    }

    protected abstract void ChangeAudio(Weapon_SO weapon);

    protected void SetAudioToNull()
    {
        audioSource.clip = null;
    }


}