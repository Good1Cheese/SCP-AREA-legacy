using UnityEngine;
using Zenject;

[RequireComponent(typeof(AudioSource))]
public abstract class WeaponSoundPlayer : SoundOnAction
{
    [Inject] protected readonly WeaponSlot _weaponSlot;

    protected WeaponHandler _weaponHandler;

    protected abstract AudioClip Sound { get; }

    private void Awake()
    {
        _weaponSlot.OnWeaponChanged += GetWeaponHandler;
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

    private new void OnDestroy()
    {
        base.OnDestroy();
        _weaponSlot.OnWeaponChanged -= GetWeaponHandler;
    }
}