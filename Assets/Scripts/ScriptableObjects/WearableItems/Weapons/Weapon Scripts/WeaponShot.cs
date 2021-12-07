using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponRecoilAnimation))]
public class WeaponShot : MonoBehaviour
{
    private const float NORMAL_MULTYPLIER = 0.001f;

    [Inject] private readonly GameObject _playerGameObject;
    [Inject] private readonly WeaponSlot _weaponSlot;

    private IDamagable _damagable;
    private Weapon_SO _weapon;

    private void Start()
    {
        _weaponSlot.OnWeaponChanged += SetWeapon;
    }

    public void Shoot(RaycastHit raycastHit)
    {
        bool isHitObjectInteractable = raycastHit.collider.gameObject.TryGetComponent(out _damagable);

        if (isHitObjectInteractable && raycastHit.collider.gameObject != _playerGameObject)
        {
            _damagable.Damage(_weapon.damagePerShot);
            return;
        }

        Vector3 highestPointOfCollider = raycastHit.point + raycastHit.normal * NORMAL_MULTYPLIER;
        SpawnBulletHole(raycastHit, highestPointOfCollider);
    }

    private void SpawnBulletHole(RaycastHit raycastHit, Vector3 highestPointOfCollider)
    {
        GameObject bulletHole = Instantiate(_weapon.bulletHolePrefab, highestPointOfCollider, Quaternion.identity);
        bulletHole.transform.LookAt(raycastHit.point + raycastHit.normal);
    }

    private void SetWeapon(WeaponHandler weaponHandler)
    {
        _weapon = weaponHandler.Weapon_SO;
    }

    private void OnDestroy()
    {
        _weaponSlot.OnWeaponChanged -= SetWeapon;
    }
}