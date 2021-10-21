using UnityEngine;
using Zenject;


[RequireComponent(typeof(AudioSource))]
public abstract class WeaponSoundPlayer : SoundPlayerOnAction
{
    [Inject] protected readonly WearableItemsInventory m_wearableItemsInventory;

    void Awake()
    {
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged += ChangeAudio;
        SubscribeToAction();
    }

    void OnDestroy()
    {
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged -= ChangeAudio;
        UnscribeToAction();
    }

    protected abstract void ChangeAudio(WeaponHandler weapon);

}