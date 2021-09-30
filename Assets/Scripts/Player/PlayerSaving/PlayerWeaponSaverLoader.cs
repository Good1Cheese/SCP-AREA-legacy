using UnityEngine;
using Zenject;

[RequireComponent(typeof(PlayerSilencerSaving))]
public class PlayerWeaponSaverLoader : MonoBehaviour
{
    [Inject] readonly WearableItemsInventory m_wearableItemsInventory;
    [Inject] readonly WeaponActivator m_weaponActivator;

    public WeaponHandler savedWeaponHandler;
    public bool isActive;
    public int ammoCount;
    public int clipAmmo;
    public bool isInInventory;


    public string weaponName;

    public void Save()
    {
        isActive = m_weaponActivator.IsWeaponActive;

        savedWeaponHandler = m_wearableItemsInventory.WeaponSlot.ItemHandler as WeaponHandler;

        if (savedWeaponHandler == null) { return; }

        ammoCount = savedWeaponHandler.AmmoCount;
        isInInventory = savedWeaponHandler.IsInInventory;
        clipAmmo = savedWeaponHandler.ClipAmmo;

        weaponName = savedWeaponHandler.name;
    }

    public void LoadWeaponData()
    {
        savedWeaponHandler.AmmoCount = ammoCount;
        savedWeaponHandler.IsInInventory = isInInventory;
        savedWeaponHandler.ClipAmmo = clipAmmo;
        m_weaponActivator.SetWeaponActiveState(isActive);
    }
}
