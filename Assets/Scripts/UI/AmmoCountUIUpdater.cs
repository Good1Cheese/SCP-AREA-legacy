using UnityEngine;
using Zenject;
using TMPro;
using System;

public class AmmoCountUIUpdater : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_textMeshProUGUI;
    [Inject] readonly EquipmentInventory m_equipmentInventory;
    [Inject] readonly WeaponFire m_weaponFire;
    [Inject] readonly WeaponReload m_weaponReload;

    GameObject m_gameObject;
    Weapon_SO m_weapon;

    void Awake()
    {
        m_equipmentInventory.WeaponCell.OnWeaponChanged += SetWeapon;
        m_equipmentInventory.WeaponCell.OnWeaponDropped += DeActivateWeaponUI;
        m_equipmentInventory.WeaponCell.OnWeaponActivated += ActiveOrDisableUI;
        m_equipmentInventory.WeaponCell.OnAmmoAdded += UpdateUIProperly;
        m_weaponFire.OnPlayerShooted += UpdateUI;
        m_weaponReload.OnPlayerReloaded += UpdateUI;
    }

    void Start()
    {
        m_gameObject = gameObject;
        m_gameObject.SetActive(false);
    }

    void SetWeapon(Weapon_SO weapon)
    {
        m_weapon = weapon;
    }

    void DeActivateWeaponUI()
    {
        m_gameObject.SetActive(false);
    }

    void ActiveOrDisableUI()
    {
        UpdateUI();
        m_gameObject.SetActive(!gameObject.activeSelf);
    }

    void UpdateUI()
    {
        m_textMeshProUGUI.text = string.Format($"{m_weapon.cartridge—lipAmmo}/{m_weapon.ammoCount}");
    }

    void UpdateUIProperly(Weapon_SO weapon)
    {
        m_textMeshProUGUI.text = string.Format($"{weapon.cartridge—lipAmmo}/{weapon.ammoCount}");
    }

    void OnDestroy()
    {
        m_equipmentInventory.WeaponCell.OnWeaponChanged -= SetWeapon;
        m_equipmentInventory.WeaponCell.OnWeaponDropped -= DeActivateWeaponUI;
        m_equipmentInventory.WeaponCell.OnWeaponActivated -= ActiveOrDisableUI;
        m_equipmentInventory.WeaponCell.OnAmmoAdded -= UpdateUIProperly;
        m_weaponFire.OnPlayerShooted -= UpdateUI;
        m_weaponReload.OnPlayerReloaded -= UpdateUI;
    }

}