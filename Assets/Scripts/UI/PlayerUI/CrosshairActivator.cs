using UnityEngine;
using Zenject;

public class CrosshairActivator : MonoBehaviour
{
    [Inject] readonly WeaponAim m_weaponAim;
    [Inject] readonly WearableItemsInventory m_wearableItemsInventory;

    GameObject m_gameObject;

    void Start()
    {
        m_gameObject = gameObject;
        m_weaponAim.OnPlayerAimed += Deactivate;
        m_weaponAim.OnPlayerInTakedAim += Activate;
        m_wearableItemsInventory.WeaponSlot.OnWeaponDropped += Activate;
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
    }
}
