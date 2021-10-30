using UnityEngine;

[RequireComponent(typeof(WeaponSaving))]
public class WeaponHandler : WearableItemHandler
{
    int m_ammoCount;
    SilencerHandler m_silencerHandler;

    public int AmmoCount
    {
        get => m_ammoCount;
        set
        {
            m_ammoCount = value;
        }
    }

    public int ClipAmmo { get; set; }
    public AudioClip CurrentShotSound { get; set; }
    public SilencerHandler SilencerHandler
    {
        get => m_silencerHandler;
        set
        {
            CurrentShotSound = Weapon_SO.shotSoundWithSilencer;
            m_silencerHandler = value;
        }
    }

    public Weapon_SO Weapon_SO { get => (Weapon_SO)m_wearableItem_SO; }

    new void Awake()
    {
        base.Awake();
        CurrentShotSound = Weapon_SO.shotSound;

        if (Weapon_SO.reloadTimeout != null) { return; }

        Weapon_SO.reloadTimeout = new WaitForSeconds(Weapon_SO.reloadDelay);
        Weapon_SO.shotTimeout = new WaitForSeconds(Weapon_SO.shotDelay);
    }

    public override void Equip()
    {
        m_wearableItemsInventory.WeaponSlot.SetItem(this);
    }
}