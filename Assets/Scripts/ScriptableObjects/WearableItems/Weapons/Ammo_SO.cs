using UnityEngine;

[CreateAssetMenu(fileName = "new HK-USP Ammo", menuName = "ScriptableObjects/Ammo/HK-USP")]
public class Ammo_SO : WearableItem_SO
{
    Weapon_SO m_weapon;
    WeaponSlot m_weaponCell;

    public int ammoCount;
    public int caliber;

    public override void Equip()
    {
        if (m_weapon == null) { return; }
        AddAmmo(m_weapon);
    }

    public override void GetDependencies(PlayerInstaller playerInstaller)
    {
        m_weaponCell = playerInstaller.EquipmentInventory.WeaponSlot;

        m_weaponCell.OnWeaponChanged += AddAmmo;
    }

    void AddAmmo(Weapon_SO weapon)
    {
        m_weapon = weapon;

        if (!IsInInventory) { return; }

        if (weapon.caliber == caliber)
        {
            weapon.ammoCount = ammoCount;
            m_weaponCell.OnAmmoAdded.Invoke(weapon);
            m_weaponCell.OnWeaponChanged -= AddAmmo;
        }
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        m_weaponCell.OnWeaponChanged -= AddAmmo;
        m_weapon = null;
    }
}


