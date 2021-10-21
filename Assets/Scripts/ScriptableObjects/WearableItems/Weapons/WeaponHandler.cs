using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponSaving))]
public class WeaponHandler : WearableItemHandler
{
    [SerializeField] Weapon_SO m_weapon_SO;

    [Inject] protected readonly WearableItemsInventory m_wearableItemsInventory;

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
            CurrentShotSound = m_weapon_SO.shotSoundWithSilencer;
            m_silencerHandler = value;
        }
    }

    public Weapon_SO Weapon_SO { get => m_weapon_SO; }

    void Awake()
    {
        WearableItemForPlayer = Instantiate(m_weapon_SO.playerWeaponPrefab);
        WearableItemForPlayer.SetActive(false);

        Weapon_SO.timeoutAfterShot = new WaitForSeconds(Weapon_SO.delayAfterShot);
        CurrentShotSound = Weapon_SO.shotSound;
    }

    public override void Equip()
    {
        m_wearableItemsInventory.WeaponSlot.SetItem(this);
    }

    public override Item_SO GetItem() => m_weapon_SO;
}