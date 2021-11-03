using UnityEngine;
using Zenject;

public class WeaponRecoilEffect : MonoBehaviour
{
    [SerializeField] float m_smooth;

    [Inject] readonly WeaponFire m_weaponFire;
    [Inject] readonly WeaponAim m_weaponAim;
    [Inject] readonly WearableItemsInventory m_wearableItemsInventory;

    Weapon_SO m_weapon_SO;

    Vector3 m_currentRotation;
    Vector3 m_targetRotation;

    Vector3 m_recoilRotation = new Vector3();

    void Start()
    {
        m_weaponAim.OnPlayerFiredWithAim += ActivateRecoilInAim;
        m_weaponAim.OnPlayerFiredWithoutAim += ActivateRecoilWithoutAim;
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged += SetWeapon;
    }

    void Update()
    {
        if (m_weapon_SO == null) { return; }

        m_targetRotation = Vector3.Lerp(m_targetRotation, Vector3.zero, m_weapon_SO.recoilReturnSpeed * Time.deltaTime);
        m_currentRotation = Vector3.Slerp(m_targetRotation, m_targetRotation, m_weapon_SO.snappiness * Time.fixedDeltaTime);

        transform.localRotation = Quaternion.Euler(m_currentRotation);
    }

    void ActivateRecoilInAim()
    {
        SetRecoil(m_weapon_SO.recoil);
    }

    void ActivateRecoilWithoutAim()
    {
        SetRecoil(m_weapon_SO.aimRecoil);
    }

    void SetRecoil(Vector3 recoilRotation)
    {
        m_recoilRotation.x = recoilRotation.x;
        m_recoilRotation.y = Random.Range(-recoilRotation.y, recoilRotation.y);
        m_recoilRotation.z = Random.Range(-recoilRotation.z, recoilRotation.z);

        m_targetRotation += m_recoilRotation;
    }

    void SetWeapon(WeaponHandler weaponHandler)
    {
        m_weapon_SO = weaponHandler.Weapon_SO;
    }

    void OnDestroy()
    {
        m_weaponAim.OnPlayerFiredWithAim += ActivateRecoilInAim;
        m_weaponAim.OnPlayerFiredWithoutAim += ActivateRecoilWithoutAim;
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged -= SetWeapon;
    }
}
