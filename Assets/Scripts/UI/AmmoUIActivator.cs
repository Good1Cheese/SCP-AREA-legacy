using UnityEngine;
using Zenject;

[RequireComponent(typeof(AmmoUICountUpdater))]
public class AmmoUIActivator : MonoBehaviour
{
    [Inject] readonly EquipmentInventory m_equipmentInventory;

    AmmoUICountUpdater m_ammoUICountUpdater;
    GameObject m_textMeshProGameObject;

    void Awake()
    {
        m_equipmentInventory.WeaponSlot.OnWeaponDropped += DeactivateWeaponUI;
        m_equipmentInventory.WeaponSlot.OnWeaponActivatedOrDeactivated += ActiveOrDisableUI;
    }

    void Start()
    {
        m_ammoUICountUpdater = GetComponent<AmmoUICountUpdater>();
        m_textMeshProGameObject = m_ammoUICountUpdater.TextMeshProUGUI.gameObject;
        DeactivateWeaponUI();
    }

    void DeactivateWeaponUI()
    {
        m_textMeshProGameObject.SetActive(false);
    }

    void ActiveOrDisableUI()
    {
        m_ammoUICountUpdater.UpdateUI();
        m_textMeshProGameObject.SetActive(!m_textMeshProGameObject.activeSelf);
    }

    void OnDestroy()
    {
        m_equipmentInventory.WeaponSlot.OnWeaponDropped -= DeactivateWeaponUI;
        m_equipmentInventory.WeaponSlot.OnWeaponActivatedOrDeactivated -= ActiveOrDisableUI;
    }
}