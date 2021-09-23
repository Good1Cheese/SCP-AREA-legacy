using UnityEngine;

[RequireComponent(typeof(WeaponFire), typeof(WeaponMiss), typeof(WeaponReload))]
public class WeaponActivator : WeaponAction
{
    public bool IsWeaponActive { get; set; }

    new void Start()
    {
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged += SetWeapon;
        m_wearableItemsInventory.WeaponSlot.OnWeaponDropped += SetWeaponToNull;
        m_inventoryAcviteStateSetter.OnInventoryButtonPressed += SetActiveState;
    }


    void Update()
    {
        if (!Input.GetButtonDown("TakeGun") || m_weaponHandler == null) { return; }

        SetWeaponActiveState(!m_weaponHandler.PlayerWeapon.activeSelf);
    }

    public void SetWeaponActiveState(bool activeGunState)
    {
        m_wearableItemsInventory.WeaponSlot.IsWeaponActived?.Invoke(activeGunState);
        IsWeaponActive = activeGunState;
        m_weaponHandler.PlayerWeapon.SetActive(activeGunState);
    }

    void SetActiveState()
    {
        enabled = !enabled;
    }

    private void SetWeaponToNull()
    {
        m_weaponHandler = null;
    }

    new void OnDestroy()
    {
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged -= SetWeapon;
        m_wearableItemsInventory.WeaponSlot.OnWeaponDropped -= SetWeaponToNull;
        m_inventoryAcviteStateSetter.OnInventoryButtonPressed -= SetActiveState;
    }
}
