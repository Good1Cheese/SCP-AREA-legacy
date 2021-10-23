using UnityEngine;
using Zenject;

public class RayForShootingProvider : MonoBehaviour, IRayProvider
{
    [SerializeField] float m_multyplierOfBulletSpawnPointRadious;
    [SerializeField] Transform m_bulletSpawnPoint;

    [Inject] readonly WearableItemsInventory m_wearableItemsInventory;
    [Inject(Id = "Player")] readonly Transform m_playerTransform;
    [Inject] readonly WeaponAim m_weaponAim;

    public System.Action<RaycastHit> OnRayLaunched { get; set; }

    bool IsPlayerAiming;
    Ray ray;

    void Awake()
    {
        ray = new Ray();
    }

    void Start()
    {
        m_weaponAim.OnPlayerAimed += SetPlayerAimState;
        m_weaponAim.OnPlayerInTakedAim += SetPlayerAimState;
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged += SetWeaponBulletSpawnPoint;
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

    void SetWeaponBulletSpawnPoint(WeaponHandler weaponHandler)
    {
        m_bulletSpawnPoint.localPosition = weaponHandler.Weapon_SO.bulletSpawnPoint;
    }

    void OnDestroy()
    {
        m_weaponAim.OnPlayerAimed -= SetPlayerAimState;
        m_weaponAim.OnPlayerInTakedAim -= SetPlayerAimState;
        m_wearableItemsInventory.WeaponSlot.OnWeaponChanged -= SetWeaponBulletSpawnPoint;
    }
}
