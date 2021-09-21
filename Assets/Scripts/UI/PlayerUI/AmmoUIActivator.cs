using UnityEngine;
using Zenject;

[RequireComponent(typeof(AmmoUICountUpdater))]
public class AmmoUIActivator : MonoBehaviour
{
    [Inject] readonly WearableItemsInventory m_wearableItemsInventory;

    AmmoUICountUpdater m_ammoUICountUpdater;
    GameObject m_textMeshProGameObject;

    void Awake()
    {
        m_wearableItemsInventory.WeaponSlot.OnWeaponDropped += DeactivateWeaponUI;
        m_wearableItemsInventory.WeaponSlot.IsWeaponActived += ActiveOrDisableUI;
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

    void ActiveOrDisableUI(bool activeState)
    {
        m_ammoUICountUpdater.UpdateUI();
        m_textMeshProGameObject.SetActive(activeState);
    }

    void OnDestroy()
    {
        m_wearableItemsInventory.WeaponSlot.OnWeaponDropped -= DeactivateWeaponUI;
        m_wearableItemsInventory.WeaponSlot.IsWeaponActived -= ActiveOrDisableUI;
    }
}