using UnityEngine;
using Zenject;

public class CrosshairEnablerDisabler : MonoBehaviour
{
    [Inject] readonly WeaponAim m_weaponAim;
    [Inject] readonly WearableItemsInventory m_wearableItemsInventory;
    [Inject] readonly GameLoader m_gameLoader;

    GameObject m_gameObject;

    void Awake()
    {
        m_gameObject = gameObject;

        m_weaponAim.OnPlayerAimed += Deactivate;
        m_weaponAim.OnPlayerInTakedAim += Activate;
        m_wearableItemsInventory.WeaponSlot.OnWeaponDropped += Activate;
        m_gameLoader.OnGameLoadingUI += m_gameObject.SetActive;
    }

    void Activate() => m_gameObject.SetActive(true);
    void Deactivate() => m_gameObject.SetActive(false);

    void OnDestroy()
    {
        m_weaponAim.OnPlayerAimed -= Deactivate;
        m_weaponAim.OnPlayerInTakedAim -= Activate;
        m_wearableItemsInventory.WeaponSlot.OnWeaponDropped -= Activate;
        m_gameLoader.OnGameLoadingUI -= m_gameObject.SetActive;
    }
}
