using UnityEngine;
using Zenject;


[RequireComponent(typeof(AudioSource))]
public abstract class WeaponSoundPlayer : SoundOnAction
{
    [Inject] protected readonly WearableItemsInventory _wearableItemsInventory;

    protected WeaponHandler _weaponHandler;

    protected abstract AudioClip Sound { get; }

    private void Awake()
    {
        _wearableItemsInventory.WeaponSlot.OnWeaponChanged += GetWeaponHandler;
        SubscribeToAction();
    }

    protected virtual void GetWeaponHandler(WeaponHandler weaponHandler)
    {
        _weaponHandler = weaponHandler;
    }

    protected override void PlaySound()
    {
        _audioSource.clip = Sound;
        _audioSource.Play();
    }

    private void OnDestroy()
    {
        _wearableItemsInventory.WeaponSlot.OnWeaponChanged -= GetWeaponHandler;
        UnscribeToAction();
    }

}