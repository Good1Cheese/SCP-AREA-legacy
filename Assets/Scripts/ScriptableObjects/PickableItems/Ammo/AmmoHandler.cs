using System.Linq;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(AmmoSaving))]
public class AmmoHandler : PickableItemHandler
{
    private const int MAX_SLOT_AMMO = 50;

    [SerializeField] private int ammoCount;

    [Inject] private readonly WeaponReload _weaponReload;
    [Inject] private readonly WearableItemsInventory _wearableItemsInventory;
    private WeaponHandler _weaponHandler;

    public Ammo_SO Ammo_SO => (Ammo_SO)_pickableIte_SO;
    public bool IsAmmoAdded { get; set; }
    public bool WasAmmoMixed { get; set; }
    public int AmmoCount { get => ammoCount; set => ammoCount = value; }

    private void Awake()
    {
        _wearableItemsInventory.WeaponSlot.OnWeaponChanged += AddAmmo;
        _wearableItemsInventory.WeaponSlot.OnWeaponDropped += TakeAmmoFromGun;
    }

    public override void Equip()
    {
        EquipAmmo();

        if (_weaponHandler == null) { return; }

        AddAmmo(_weaponHandler);
    }

    public override void OnItemDropped()
    {
        _weaponReload.UpdateWeaponAmmoCount(AmmoCount);
        IsAmmoAdded = false;
    }

    private void EquipAmmo()
    {
        AmmoHandler ammoHandler = (AmmoHandler)_pickableItemsInventory.Inventory.LastOrDefault(item => item as AmmoHandler != null);

        if (ammoHandler == null || ammoHandler.AmmoCount + AmmoCount > MAX_SLOT_AMMO)
        {
            base.Equip();
            return;
        }

        ammoHandler.AmmoCount += AmmoCount;
        IsAmmoAdded = true;
        WasAmmoMixed = true;

        if (_weaponHandler == null) { return; }

        _weaponHandler.AmmoCount = ammoHandler.AmmoCount;
        _wearableItemsInventory.WeaponSlot.OnAmmoAdded.Invoke(_weaponHandler);
    }

    private void TakeAmmoFromGun()
    {
        _weaponHandler.AmmoCount = 0;
        IsAmmoAdded = false;
        _weaponHandler = null;
    }

    private void AddAmmo(WeaponHandler weaponHandler)
    {
        _weaponHandler = weaponHandler;

        if (!IsInInventory || IsAmmoAdded || WasAmmoMixed) { return; }

        if (_weaponHandler.Weapon_SO.ammoType == Ammo_SO.ammoType)
        {
            weaponHandler.AmmoCount += AmmoCount;
            _wearableItemsInventory.WeaponSlot.OnAmmoAdded.Invoke(_weaponHandler);

            IsAmmoAdded = true;
        }
    }

    private void OnDestroy()
    {
        _wearableItemsInventory.WeaponSlot.OnWeaponChanged -= AddAmmo;
        _wearableItemsInventory.WeaponSlot.OnWeaponDropped -= TakeAmmoFromGun;
    }
}
