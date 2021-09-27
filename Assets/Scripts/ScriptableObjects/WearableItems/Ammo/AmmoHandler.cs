using UnityEngine;
using Zenject;

public class AmmoHandler : WearableItemHandler
{
    [SerializeField] Ammo_SO m_ammo_SO;

    WeaponHandler m_weaponHandler;

    void Awake()
    {
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged += AddAmmo;
        m_wearableItemsInventory.WeaponSlot.OnWeaponDropped += SetWeaponToNull;
    }

    public override void Equip()
    {
        if (m_weaponHandler == null) { return; }
        AddAmmo(m_weaponHandler);
    }

    void SetWeaponToNull()
    {
        m_weaponHandler = null;
    }

    void AddAmmo(WeaponHandler weaponHandler)
    {
        m_weaponHandler = weaponHandler;

        if (!IsInInventory) { return; }

        if (m_weaponHandler.Weapon_SO.caliber == m_ammo_SO.caliber)
        {
            weaponHandler.AmmoCount += m_ammo_SO.ammoCount;
            m_wearableItemsInventory.WeaponSlot.OnAmmoAdded.Invoke(weaponHandler);
            m_wearableItemsInventory.WeaponSlot.OnWeaponChanged -= AddAmmo;
        }
    }

    public override Item_SO GetItem() => m_ammo_SO;


    protected override void OnDestroy()
    {
        base.OnDestroy();
        m_weaponHandler = null;
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged -= AddAmmo;
    }
}
