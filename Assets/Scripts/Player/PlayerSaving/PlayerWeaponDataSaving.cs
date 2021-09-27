using System;
using UnityEngine;
using Zenject;

public class PlayerWeaponDataSaving : DataSaving
{
    [Inject] readonly WearableItemsInventory m_wearableItemsInventory;
    [Inject] readonly WeaponActivator m_weaponActivator;

    WeaponHandler m_currentWeaponHandler;

    public WeaponHandler savedWeaponHandler;
    public SilencerHandler savedSilencerHandler;
    public bool isActive;
    public int ammoCount;
    public int clipAmmo;

    public string weaponName;
    public string silencerName;

    public Transform PropsHandler { get; set; }

    void Start()
    {
        m_wearableItemsInventory.WeaponSlot.OnSilencerEquiped += SetSilencerName;
        m_wearableItemsInventory.WeaponSlot.OnSilencerUnequiped += SetSilencerNameToNull;
    }

    void SetSilencerNameToNull()
    {
        silencerName = string.Empty;
    }

    void SetSilencerName()
    {
        WeaponHandler currentWeapon = m_wearableItemsInventory.WeaponSlot.ItemHandler as WeaponHandler;
        silencerName = currentWeapon.SilencerHandler.GameObject.name;
    }

    public override void Save()
    {
        isActive = m_weaponActivator.IsWeaponActive;

        savedWeaponHandler = m_wearableItemsInventory.WeaponSlot.ItemHandler as WeaponHandler;

        if (savedWeaponHandler == null) { return; }

        savedSilencerHandler = savedWeaponHandler.SilencerHandler;
        ammoCount = savedWeaponHandler.AmmoCount;
        clipAmmo = savedWeaponHandler.ClipAmmo;

        weaponName = savedWeaponHandler.name;

        if (savedSilencerHandler == null) { return; }

        silencerName = savedSilencerHandler.name;
    }

    public override void Load()
    {
        m_currentWeaponHandler = m_wearableItemsInventory.WeaponSlot.ItemHandler as WeaponHandler;

        if (m_currentWeaponHandler == null && savedWeaponHandler == null) { return; }

        if (m_currentWeaponHandler == savedWeaponHandler)
        {
            LoadWeaponData();
            return;
        }

        if (isActive != m_weaponActivator.IsWeaponActive)
        {
            m_weaponActivator.SetWeaponActiveState(isActive);
        }

        if (m_currentWeaponHandler == null && savedWeaponHandler != null)
        {
            LoadWeaponData();
            m_wearableItemsInventory.WeaponSlot.SetItem(savedWeaponHandler);

            return;
        }

        m_wearableItemsInventory.WeaponSlot.ClearSlot();
    }

    void LoadWeaponData()
    {
        savedWeaponHandler.AmmoCount = ammoCount;
        savedWeaponHandler.ClipAmmo = clipAmmo;
        m_weaponActivator.SetWeaponActiveState(isActive);

        if (savedSilencerHandler == null && savedWeaponHandler.SilencerHandler != null)
        {
            m_currentWeaponHandler.SilencerHandler.Unequip();
            silencerName = string.Empty;
        }
    }

    public override void LoadFromMenu(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);

        if (string.IsNullOrEmpty(weaponName)) { return; }

        GameObject weaponGameObject = PropsHandler.Find(weaponName).gameObject;
        WeaponHandler weaponHandler = weaponGameObject.GetComponent<ItemHandler>() as WeaponHandler;

        savedWeaponHandler = weaponHandler;
        weaponHandler.Interact();

        if (!string.IsNullOrEmpty(silencerName)) 
        {
            GameObject silencerGameObject = PropsHandler.Find(weaponName).gameObject;
            SilencerHandler silencerHandler = silencerGameObject.GetComponent<ItemHandler>() as SilencerHandler;

            savedSilencerHandler = silencerHandler;
            silencerHandler.Interact();
        }

        LoadWeaponData();
    }

    void OnDestroy()
    {
        m_wearableItemsInventory.WeaponSlot.OnSilencerEquiped -= SetSilencerName;
        m_wearableItemsInventory.WeaponSlot.OnSilencerUnequiped -= SetSilencerNameToNull;
    }
}
