﻿using UnityEngine;

public class SilencerHandler : WearableItemHandler
{
    [SerializeField] Silencer_SO m_silencer_SO;

    public Silencer_SO Silencer_SO { get => m_silencer_SO; }
    public GameObject SilencerForPlayerWeapon { get; set; }
    public GameObject SilencerForWorldWeapon { get; set; }


    void Awake()
    {
        SilencerForPlayerWeapon = Instantiate(Silencer_SO.silencerForPlayerPrefab);
        SilencerForPlayerWeapon.SetActive(false);

        SilencerForWorldWeapon = Instantiate(SilencerForPlayerWeapon);
        SilencerForWorldWeapon.SetActive(false);
    }

    public override void Equip()
    {
        WeaponHandler weaponHandler = m_wearableItemsInventory.WeaponSlot.ItemHandler as WeaponHandler;

        if (weaponHandler == null)
        {
            GameObject.SetActive(true);
            return;
        }

        SpawnSilencer(SilencerForPlayerWeapon, weaponHandler.PlayerWeapon);
        SpawnSilencer(SilencerForWorldWeapon, weaponHandler.GameObject);

        m_wearableItemsInventory.WeaponSlot.OnSilencerEquiped.Invoke();
        weaponHandler.SilencerHandler = this;
    }

    public void Unequip()
    {
        WeaponHandler weaponHandler = m_wearableItemsInventory.WeaponSlot.ItemHandler as WeaponHandler;

        DespawnSilencer(SilencerForPlayerWeapon);
        DespawnSilencer(SilencerForWorldWeapon);

        m_wearableItemsInventory.WeaponSlot.OnSilencerUnequiped.Invoke();
        weaponHandler.SilencerHandler = null;
    }

    void SpawnSilencer(GameObject silencer, GameObject spawnObject)
    {
        silencer.transform.SetParent(spawnObject.transform, false);
        silencer.transform.localPosition = Silencer_SO.positionForSilencer;
        silencer.SetActive(true);
    }

    void DespawnSilencer(GameObject silencer)
    {
        silencer.transform.SetParent(null, false);
        silencer.SetActive(false);
    }

    public override Item_SO GetItem() => m_silencer_SO;
}