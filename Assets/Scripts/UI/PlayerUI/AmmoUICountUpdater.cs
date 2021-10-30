using UnityEngine;
using Zenject;
using TMPro;
using System;

public class AmmoUICountUpdater : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_textMeshProUGUI;
    [Inject] readonly m_wearableItemsInventory m_wearableItemsInventory;
    [Inject] readonly WeaponFire m_weaponFire;
    [Inject] readonly WeaponReload m_weaponReload;

    WeaponHandler m_weaponHandler;

    public TextMeshProUGUI TextMeshProUGUI { get => m_textMeshProUGUI; }

    void Awake()
    {
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged += SetWeapon;
        m_wearableItemsInventory.WeaponSlot.OnAmmoAdded += UpdateUIProperly;
        m_weaponFire.OnPlayerShooted += UpdateUI;
        m_weaponReload.OnWeaponAmmoChanged += UpdateUI;
    }

    void SetWeapon(WeaponHandler weaponHandler)
    {
        m_weaponHandler = weaponHandler;
    }

    public void UpdateUI()
    {
        TextMeshProUGUI.text = string.Format($"{m_weaponHandler.ClipAmmo}/{m_weaponHandler.AmmoCount}");
    }

    public void UpdateUIProperly(WeaponHandler weaponHandler)
    {
        TextMeshProUGUI.text = string.Format($"{weaponHandler.ClipAmmo}/{weaponHandler.AmmoCount}");
    }

    void OnDestroy()
    {
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged -= SetWeapon;
        m_wearableItemsInventory.WeaponSlot.OnAmmoAdded -= UpdateUIProperly;
        m_weaponFire.OnPlayerShooted -= UpdateUI;
        m_weaponReload.OnPlayerReloaded -= UpdateUI;
    }

}
