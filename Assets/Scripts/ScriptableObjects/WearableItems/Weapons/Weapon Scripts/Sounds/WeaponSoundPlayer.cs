using UnityEngine;
using Zenject;


[RequireComponent(typeof(AudioSource))]
public abstract class WeaponSoundPlayer : SoundOnAction
{
    [Inject] protected readonly WearableItemsInventory m_wearableItemsInventory;

    protected WeaponHandler m_weaponHandler;

    protected abstract AudioClip Sound { get; }

    void Awake()
    {
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged += GetWeaponHandler;
        SubscribeToAction();
    }

    protected virtual void GetWeaponHandler(WeaponHandler weaponHandler)
    {
        m_weaponHandler = weaponHandler;
    }

    protected override void PlaySound()
    {
        m_audioSource.clip = Sound;
        m_audioSource.Play();
    }

    void OnDestroy()
    {
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged -= GetWeaponHandler;
        UnscribeToAction();
    }

}