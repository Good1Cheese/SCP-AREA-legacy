using UnityEngine;
using Zenject;

public class RayForFireProvider : MonoBehaviour, IRayProvider
{
    [SerializeField] private float _multyplierOfBulletSpawnPointRadious;
    [SerializeField] private Transform _bulletSpawnPoint;

    [Inject] private readonly WeaponSlot _weaponSlot;
    [Inject] private readonly WeaponAim _weaponAim;
    [Inject(Id = "Player")] private readonly Transform _playerTransform;
    private Ray ray;

    private void Awake()
    {
        ray = new Ray();
    }

    private void Start()
    {
        _weaponSlot.Changed += SetWeaponBulletSpawnPoint;
    }

    public Ray ProvideRay()
    {
        if (_weaponAim.IsAiming)
        {
            return ray = ProvideRayForAimedShot();
        }
        else
        {
            return ray = ProvideRayForShot();
        }
    }

    public Ray ProvideRayForAimedShot()
    {
        return ProvideRay(_bulletSpawnPoint.position);
    }

    public Ray ProvideRayForShot()
    {
        Vector3 origin = _playerTransform.position + _playerTransform.up + (Vector3)Random.insideUnitCircle * _multyplierOfBulletSpawnPointRadious;
        return ProvideRay(origin);
    }

    public Ray ProvideRay(Vector3 origin)
    {
        ray.origin = origin;
        ray.direction = transform.forward;
        return ray;
    }

    private void SetWeaponBulletSpawnPoint(WeaponHandler weaponHandler)
    {
        _bulletSpawnPoint.localPosition = weaponHandler.Weapon_SO.bulletSpawnPoint;
    }

    private void OnDestroy()
    {
        _weaponSlot.Changed -= SetWeaponBulletSpawnPoint;
    }

}
