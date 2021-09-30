using UnityEngine;
using Zenject;

public class PlayerSilencerSaving : DataSaving
{
    [Inject] readonly WearableItemsInventory m_wearableItemsInventory;

    PlayerWeaponSaving m_playerWeaponDataSaving;

    public SilencerHandler savedSilencerHandler;
    public string silencerName;

    void Start()
    {
        m_playerWeaponDataSaving = GetComponent<PlayerWeaponSaving>();
        m_wearableItemsInventory.WeaponSlot.OnSilencerEquiped += SetSilencerName;
        m_wearableItemsInventory.WeaponSlot.OnSilencerUnequiped += SetSilencerNameToNull;
    }

    void SetSilencerNameToNull() => silencerName = string.Empty;

    void SetSilencerName()
    {
        WeaponHandler currentWeaponHandler = m_wearableItemsInventory.WeaponSlot.ItemHandler as WeaponHandler;
        silencerName = currentWeaponHandler.SilencerHandler.GameObject.name;
    }

    public override void Save()
    {
        WeaponHandler currentWeaponHandler = m_playerWeaponDataSaving.CurrentWeaponHandler;
        if (currentWeaponHandler == null) { return; }

        savedSilencerHandler = currentWeaponHandler.SilencerHandler;

        if (savedSilencerHandler == null) { return; }

        silencerName = savedSilencerHandler.name;
    }

    public override void Load()
    {
        WeaponHandler weaponHandler = m_playerWeaponDataSaving.WeaponData.savedWeaponHandler;
        if (weaponHandler == null) { return; }

        if (savedSilencerHandler == null && weaponHandler.SilencerHandler != null)
        {
            m_playerWeaponDataSaving.CurrentWeaponHandler.SilencerHandler.Unequip();
            silencerName = string.Empty;
        }
    }

    public override void LoadFromMenu(string json)
    {
        if (!string.IsNullOrEmpty(silencerName))
        {
            GameObject silencerGameObject = m_playerWeaponDataSaving.PropsHandler.Find(silencerName).gameObject;
            SilencerHandler silencerHandler = silencerGameObject.GetComponent<ItemHandler>() as SilencerHandler;

            savedSilencerHandler = silencerHandler;
            silencerHandler.Interact();
        }
    }

    void OnDestroy()
    {
        m_wearableItemsInventory.WeaponSlot.OnSilencerEquiped -= SetSilencerName;
        m_wearableItemsInventory.WeaponSlot.OnSilencerUnequiped -= SetSilencerNameToNull;
    }

}