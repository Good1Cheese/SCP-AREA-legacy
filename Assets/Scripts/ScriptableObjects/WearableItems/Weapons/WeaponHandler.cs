using UnityEngine;
using Zenject;
using System.Linq;

[RequireComponent(typeof(WeaponSaving))]
public class WeaponHandler : WearableItemHandler
{
    [Inject(Id = "Player")] private readonly Transform _playerTransform;
    [Inject] private readonly PickableItemsInventory _pickableItemsInventory;

    public Weapon_SO Weapon_SO { get; private set; }
    public int AmmoCount => _pickableItemsInventory.Inventory.TakeWhile(item => item != null && item as AmmoHandler)
                                                    .Sum(item => (item as AmmoHandler).AmmoCount);
    public SilencerHandler SilencerHandler { get; set; }
    public int ClipAmmo { get; set; }
    public AudioClip CurrentShotSound { get; set; }

    private new void Awake()
    {
        base.Awake();

        Weapon_SO = (Weapon_SO)_wearableIte_SO;
        CurrentShotSound = Weapon_SO.shotSound;

        if (Weapon_SO.reloadTimeout != null) { return; }

        Weapon_SO.reloadTimeout = new WaitForSeconds(Weapon_SO.reloadDelay);
        Weapon_SO.shotTimeout = new WaitForSeconds(Weapon_SO.shotDelay);
    }

    public override void Equip()
    {
        _wearableItemsInventory.WeaponSlot.SetItem(this);
    }
}