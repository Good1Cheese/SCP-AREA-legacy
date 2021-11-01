using UnityEngine;
using Zenject;

public class RecoilEffect : MonoBehaviour
{
    [SerializeField] float m_smooth;

    [Inject] readonly WeaponFire m_weaponFire;
    [Inject] readonly WearableItemsInventory m_wearableItemsInventory;

    Weapon_SO m_weapon_SO;
    Quaternion m_targetRotation = new Quaternion();

    void Start()
    {
        m_weaponFire.OnPlayerShooted += ActivateJerk;
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged += SetWeapon;
    }

    void ActivateJerk()
    {
        m_targetRotation = transform.localRotation;
        m_targetRotation.y += m_weapon_SO.recoilRotation.y * GetPositiveOrNegativeOne();
        m_targetRotation.x -= m_weapon_SO.recoilRotation.x;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, m_targetRotation, m_smooth * Time.deltaTime);
    }

    int GetPositiveOrNegativeOne()
    {
        // Random.Range(0,2)       ==  0 or 1
        // Random.Range(0,2)*2     ==  0 or 2
        // Random.Range(0,2)*2-1   == -1 or 1

        return Random.Range(0, 2) * 2 - 1;
    }

    void SetWeapon(WeaponHandler weaponHandler)
    {
        m_weapon_SO = weaponHandler.Weapon_SO;
    }

    void OnDestroy()
    {
        m_weaponFire.OnPlayerShooted -= ActivateJerk;
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged -= SetWeapon;
    }
}
