using UnityEngine;
using Zenject;

public class RayForShootingProvider : MonoBehaviour, IRayProvider
{
    [SerializeField] float m_multyplierOfBulletSpawnPointRadious;
    [SerializeField] Transform m_bulletSpawnPoint;

    [Inject] WearableItemsInventory m_wearableItemsInventory;
    [Inject] readonly Transform m_playerTransform;
    [Inject] readonly WeaponAim m_weaponAim;

    public System.Action<RaycastHit> OnRayLaunched { get; set; }

    bool IsPlayerAiming;
    Ray ray;

    [Inject]
    void Construct(WeaponAim weaponAim)
    {
        weaponAim.OnPlayerAimed += SetPlayerAimState;
        weaponAim.OnPlayerInTakedAim += SetPlayerAimState;
    }

    void Awake()
    {
        ray = new Ray();
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged += SetWeapon;
    }

    void SetPlayerAimState()
    {
        IsPlayerAiming = !IsPlayerAiming;
    }   

    public Ray ProvideRay()
    {
        if (IsPlayerAiming)
        {
            ray.origin = m_bulletSpawnPoint.position;
            m_weaponAim.OnPlayerShootedWithAim?.Invoke();
        }
        else
        {
            ray.origin = m_playerTransform.position + m_playerTransform.up + (Vector3)Random.insideUnitCircle * m_multyplierOfBulletSpawnPointRadious;
            m_weaponAim.OnPlayerShootedWithoutAim?.Invoke();
        }

        ray.direction = transform.forward;

        return ray;
    }

    void SetWeapon(Weapon_SO weapon_SO)
    {
        m_bulletSpawnPoint.localPosition = weapon_SO.bulletSpawnPoint;
    }
}
