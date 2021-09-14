using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponFire), typeof(WeaponMiss), typeof(WeaponReload))]
public class WeaponActivator : MonoBehaviour
{
    [Inject] readonly WeaponSpawnerAndDestroyer m_weaponGameObjectController;
    [Inject] readonly InventoryAcviteStateSetter m_inventoryAcviteStateSetter;
    [Inject] readonly WearableItemsInventory m_equipmentInventory;

    public bool IsWeaponActive { get; set; }

    void Start()
    {
        m_inventoryAcviteStateSetter.OnInventoryButtonPressed += SetActiveState;
        m_equipmentInventory.WeaponSlot.OnWeaponDropped += SetWeaponActiveState;
    }

    void SetWeaponActiveState()
    {
        IsWeaponActive = false;
    }

    void Update()
    {
        if (!Input.GetButtonDown("TakeGun") || m_weaponGameObjectController.CurrentGunGameObject == null) { return; }

        SetWeaponActiveState(!m_weaponGameObjectController.CurrentGunGameObject.activeSelf);
    }

    public void SetWeaponActiveState(bool activeGunState)
    {
        m_equipmentInventory.WeaponSlot.IsWeaponActived?.Invoke(activeGunState);
        IsWeaponActive = activeGunState;
        m_weaponGameObjectController.CurrentGunGameObject.SetActive(activeGunState);
    }

    void SetActiveState()
    {
        enabled = !enabled;
    }

    void OnDestroy()
    {
        m_inventoryAcviteStateSetter.OnInventoryButtonPressed -= SetActiveState;
    }
}
