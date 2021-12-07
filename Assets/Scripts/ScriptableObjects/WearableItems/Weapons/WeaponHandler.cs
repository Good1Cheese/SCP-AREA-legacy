using UnityEngine;
using Zenject;
using System.Linq;

[RequireComponent(typeof(WeaponSaving))]
public class WeaponHandler : WearableItemHandler
{
    [Inject] private readonly PickableItemsInventory _pickableItemsInventory;
    [Inject] private readonly WeaponSlot _weaponSlot;

    private int _ammoCount;

    public int Ammo => _ammoCount;
    public Weapon_SO Weapon_SO { get; private set; }
    public SilencerHandler SilencerHandler { get; set; }
    public int ClipAmmo { get; set; }
    public AudioClip CurrentShotSound { get; set; }

    private new void Start()
    {
        base.Start();

        if (Weapon_SO.reloadTimeout != null) { return; }

        Weapon_SO.reloadTimeout = new WaitForSeconds(Weapon_SO.reloadDelay);
        Weapon_SO.shotTimeout = new WaitForSeconds(Weapon_SO.shotDelay);
    }

    private new void Awake()
    {
        base.Awake();

        Weapon_SO = (Weapon_SO)_wearableIte_SO;
        CurrentShotSound = Weapon_SO.shotSound;
    }

    public override void Equip()
    {
        _weaponSlot.SetItem(this);
    }

    public void UpdateAmmo()
    {
        _ammoCount = _pickableItemsInventory.Inventory.TakeWhile(item => item != null && item as AmmoHandler)
                        .Sum(item => (item as AmmoHandler).Ammo);
    }
}