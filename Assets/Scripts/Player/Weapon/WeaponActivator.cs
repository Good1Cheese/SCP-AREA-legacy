using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponFire), typeof(WeaponSpawnerAndDestroyer), typeof(WeaponReload))]
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

        m_equipmentInventory.WeaponCell.OnWeaponActivated.Invoke();

        GameObject m_currentGun = m_weaponGameObjectController.CurrentGunGameObject;
        IsWeaponActive = !m_currentGun.activeSelf;
        m_currentGun.SetActive(!m_currentGun.activeSelf);
    }

    void SetActiveState(bool isUIActive)
    {
        enabled = !isUIActive;
    }

    void OnDestroy()
    {
        m_playerInventory.OnInventoryButtonPressed -= SetActiveState;
    }
}
