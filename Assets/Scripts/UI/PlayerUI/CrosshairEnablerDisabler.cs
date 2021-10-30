using UnityEngine;
using Zenject;

public class CrosshairEnablerDisabler : MonoBehaviour
{
    [Inject] readonly WeaponAim m_weaponAim;
    [Inject] readonly m_wearableItemsInventory m_wearableItemsInventory;
    [Inject] readonly GameLoader m_gameLoader;

    GameObject m_gameObject;

    void Awake()
    {
        m_gameObject = gameObject;

        m_weaponAim.OnPlayerAimed += Deactivate;
        m_weaponAim.OnPlayerInTakedAim += Activate;
        m_wearableItemsInventory.WeaponSlot.OnWeaponDropped += Activate;
        m_gameLoader.OnGameLoading += SetActiveState;
    }

    void Activate() => SetActiveState(true);
    void Deactivate() => SetActiveState(false);

    void SetActiveState(bool active)
    {
        m_gameObject.SetActive(active);
    }

    void OnDestroy()
    {
        m_weaponAim.OnPlayerAimed -= Deactivate;
        m_weaponAim.OnPlayerInTakedAim -= Activate;
        m_wearableItemsInventory.WeaponSlot.OnWeaponDropped -= Activate;
        m_gameLoader.OnGameLoading -= SetActiveState;
    }
}
