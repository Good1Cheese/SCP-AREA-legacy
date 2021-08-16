using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponFire), typeof(WeaponMiss), typeof(WeaponReload))]
public class WeaponActivator : MonoBehaviour
{
    [Inject] readonly WeaponSpawnerAndDestroyer m_weaponGameObjectController;
    [Inject] readonly PlayerInventory m_playerInventory;
    [Inject] readonly EquipmentInventory m_equipmentInventory;

    public bool IsWeaponActive { get; set; }

    void Start()
    {
        m_playerInventory.OnInventoryButtonPressed += SetActiveState; 
    }

    void Update()
    {
        if (!Input.GetButtonDown("TakeGun") || m_weaponGameObjectController.CurrentGunGameObject == null) { return; }

        GameObject m_currentGun = m_weaponGameObjectController.CurrentGunGameObject;
        ActivateOrDeactivateWeapon(m_currentGun, !m_currentGun.activeSelf);
    }

    public void ActivateOrDeactivateWeapon(GameObject m_currentGun, bool activateGun)
    {
        m_equipmentInventory.WeaponSlot.OnWeaponActivatedOrDeactivated.Invoke();
        IsWeaponActive = activateGun;
        m_currentGun.SetActive(activateGun);
    }

    void SetActiveState()
    {
        enabled = !enabled;
    }

    void OnDestroy()
    {
        m_playerInventory.OnInventoryButtonPressed -= SetActiveState;
    }
}
