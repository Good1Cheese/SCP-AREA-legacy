using UnityEngine;

[CreateAssetMenu(fileName = "new HK-USP Ammo", menuName = "ScriptableObjects/Ammo/HK-USP")]
public class Ammo_SO : PickableItem_SO
{
    Weapon_SO m_weapon;
    Weapon_SO m_previousWeapon;
    WeaponSlot m_weaponCell;

    public int ammoCount;
    public int caliber;

    public override void Equip()
    {
        base.Equip();
        if (m_weapon == null) { return; }
        AddAmmoToGun(m_weapon);
    }

    public override void Use()
    {

    }

    public override void GetDependencies(PlayerInstaller playerInstaller)
    {
        base.GetDependencies(playerInstaller);
        m_weaponCell = playerInstaller.EquipmentInventory.WeaponSlot;

        m_weaponCell.OnWeaponChanged += AddAmmoToGun;
        m_weaponCell.OnWeaponDropped += ResetAmmoByGun;
        Inventory.OnInventoryChanged += VerifyAmmoExistance;
    }

    void AddAmmoToGun(Weapon_SO weapon)
    {
        m_weapon = weapon;

        Debug.Log(weapon);
        if (Inventory.HasItem(this) && weapon.caliber == caliber)
        {
            weapon.ammoCount = ammoCount;
            m_previousWeapon = m_weapon;
            m_weaponCell.OnAmmoAdded.Invoke(weapon);
        }
    }

    void ResetAmmoByGun()
    {
        if (m_previousWeapon != null)
        {
            ResetAmmo(m_previousWeapon);
        }
    }

    void VerifyAmmoExistance()
    {
        if (Inventory.HasItem(this)) { return; }

        if (m_weapon != null)
        {
            ResetAmmo(m_weapon);
            m_weaponCell.OnAmmoAdded.Invoke(m_weapon);
        }
    }

    void ResetAmmo(Weapon_SO weapon)
    {
        ammoCount = (weapon.cartridge—lipAmmo != 0) ? weapon.cartridge—lipAmmo : ammoCount;
        weapon.ammoCount = 0;
        weapon.cartridge—lipAmmo = 0;
    }


    public override void OnDestroy()
    {
        m_weaponCell.OnWeaponChanged -= AddAmmoToGun;
        m_weaponCell.OnWeaponDropped -= ResetAmmoByGun;
        Inventory.OnInventoryChanged -= VerifyAmmoExistance;
        m_weapon = null;
        m_previousWeapon = null;
    }
}


