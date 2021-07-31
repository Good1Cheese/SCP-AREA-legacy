using UnityEngine;
using Zenject;

public class RayForShootingProvider : WeaponAction, IRayProvider
{
    [SerializeField] float multyplierOfBulletSpawnPointRadious;
    [SerializeField] Transform m_bulletSpawnPoint;
    [Inject] readonly WeaponFire m_weaponFire;

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
            m_weaponFire.OnPlayerShootedWithAim.Invoke();
        }
        else
        {
            ray.origin = m_bulletSpawnPoint.position + (Vector3)Random.insideUnitCircle * multyplierOfBulletSpawnPointRadious;
            m_weaponFire.OnPlayerShootedWithoutAim.Invoke();
        }

        ray.direction = transform.forward;

        return ray;
    }

    protected override void SetWeapon(Weapon_SO weapon)
    {
        m_bulletSpawnPoint.localPosition = weapon.bulletSpawnPoint;
    }
}
