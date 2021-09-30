using UnityEngine;

[RequireComponent(typeof(WeaponSaving))]
public class WeaponHandler : WearableItemHandler
{
    [SerializeField] Weapon_SO m_weapon_SO;

    public int AmmoCount { get; set; }
    public int ClipAmmo { get; set; }
    public AudioClip CurrentShotSound { get; set; }
    public SilencerHandler SilencerHandler { get; set; }
    public Weapon_SO Weapon_SO { get => m_weapon_SO; }
    public GameObject PlayerWeapon { get; set; }

    void Awake()
    {
        Weapon_SO.timeoutAfterShot = new WaitForSeconds(Weapon_SO.delayAfterShot);
        CurrentShotSound = Weapon_SO.shotSoundPrefab;
    }

    public override void Equip()
    {
        m_wearableItemsInventory.WeaponSlot.SetItem(this);
    }

    public override Item_SO GetItem() => m_weapon_SO;
}
