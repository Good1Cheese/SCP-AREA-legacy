using UnityEngine;
using Zenject;
using TMPro;
using System;

public class AmmoUICountUpdater : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_textMeshProUGUI;
    [Inject] readonly EquipmentInventory m_equipmentInventory;
    [Inject] readonly WeaponFire m_weaponFire;
    [Inject] readonly WeaponReload m_weaponReload;

    Weapon_SO m_weapon;

    public TextMeshProUGUI TextMeshProUGUI { get => m_textMeshProUGUI; }

    void Awake()
    {
        m_equipmentInventory.WeaponSlot.OnWeaponChanged += SetWeapon;
        m_equipmentInventory.WeaponSlot.OnAmmoAdded += UpdateUIProperly;
        m_weaponFire.OnPlayerShooted += UpdateUI;
        m_weaponReload.OnPlayerReloaded += UpdateUI;
    }

    void SetWeapon(Weapon_SO weapon)
    {
        m_weapon = weapon;
    }

    public void UpdateUI()
    {
        TextMeshProUGUI.text = string.Format($"{m_weapon.cartridge—lipAmmo}/{m_weapon.ammoCount}");
    }

    void UpdateUIProperly(Weapon_SO weapon)
    {
        TextMeshProUGUI.text = string.Format($"{weapon.cartridge—lipAmmo}/{weapon.ammoCount}");
    }

    void OnDestroy()
    {
        m_equipmentInventory.WeaponSlot.OnWeaponChanged -= SetWeapon;
        m_equipmentInventory.WeaponSlot.OnAmmoAdded -= UpdateUIProperly;
        m_weaponFire.OnPlayerShooted -= UpdateUI;
        m_weaponReload.OnPlayerReloaded -= UpdateUI;
    }

}
