using UnityEngine;
using System.Linq;
using Zenject;

[RequireComponent(typeof(AmmoSaving))]
public class AmmoHandler : PickableItemHandler
{
    const int MAX_SLOT_AMMO = 50;
    [SerializeField] int ammoCount;

    [Inject] readonly WeaponReload m_weaponReload;

    WeaponHandler m_weaponHandler;

    public Ammo_SO Ammo_SO { get; set; }
    public bool IsAmmoAdded { get; set; }
    public int AmmoCount { get => ammoCount; set => ammoCount = value; }

    void Awake()
    {
        Ammo_SO = m_pickableItem_SO as Ammo_SO;
        m_playerInstaller.WearableItemsInventory.WeaponSlot.OnWeaponChanged += AddAmmo;
        m_playerInstaller.WearableItemsInventory.WeaponSlot.OnWeaponDropped += TakeAmmoFromGun;
    }

    public override void Equip()
    {
        EquipAmmo();

        if (m_weaponHandler == null) { return; }

        AddAmmo(m_weaponHandler);
    }

    void EquipAmmo()
    {
        AmmoHandler ammoHandler = (AmmoHandler)m_playerInstaller.PickableItemsInventory.Inventory.FirstOrDefault(item => item as AmmoHandler != null);

        if (ammoHandler == null || ammoHandler.AmmoCount + AmmoCount > MAX_SLOT_AMMO)
        {
            base.Equip();
            return;
        }

        ammoHandler.AmmoCount += AmmoCount;

        IsAmmoAdded = true;

    }

    void TakeAmmoFromGun()
    {
        m_weaponHandler.AmmoCount = 0;
        IsAmmoAdded = false;
        m_weaponHandler = null;
    }

    void AddAmmo(WeaponHandler weaponHandler)
    {
        m_weaponHandler = weaponHandler;

        if (!IsInInventory || IsAmmoAdded) { return; }

        if (m_weaponHandler.Weapon_SO.ammoType == Ammo_SO.ammoType)
        {
            weaponHandler.AmmoCount += AmmoCount;
            m_playerInstaller.WearableItemsInventory.WeaponSlot.OnAmmoAdded.Invoke(m_weaponHandler);

            IsAmmoAdded = true;
        }
    }

    public override void OnInventoryStateChanged(bool isItemInInventory)
    {
        if (isItemInInventory) { return; }
        m_weaponReload.UpdateWeaponAmmoCount(AmmoCount);
        IsAmmoAdded = false;
    }

    void OnDestroy()
    {
        m_playerInstaller.WearableItemsInventory.WeaponSlot.OnWeaponChanged -= AddAmmo;
        m_playerInstaller.WearableItemsInventory.WeaponSlot.OnWeaponDropped -= TakeAmmoFromGun;
    }
}
