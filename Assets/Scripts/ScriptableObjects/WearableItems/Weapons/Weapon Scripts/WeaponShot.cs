using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponRecoilAnimation))]
public class WeaponShot : MonoBehaviour
{
    [Inject] private readonly GameObject _playerGameObject;
    [Inject] private readonly WearableItemsInventory _wearableItemsInventory;
    private IDamagable _damagable;
    private Weapon_SO _weapon;

    private void Start()
    {
        _wearableItemsInventory.WeaponSlot.OnWeaponChanged += SetWeapon;
    }

    public void AttendShot(RaycastHit raycastHit)
    {
        bool isHitObjectInteractable = raycastHit.collider.gameObject.TryGetComponent(out _damagable);

        if (isHitObjectInteractable && raycastHit.collider.gameObject != _playerGameObject)
        {
            _damagable.Damage(_weapon.damagePerShot);
            return;
        }

        Vector3 highestPointOfCollider = raycastHit.point + raycastHit.normal * 0.001f;
        GameObject bulletHole = Instantiate(_weapon.bulletHolePrefab, highestPointOfCollider, Quaternion.identity);
        bulletHole.transform.LookAt(raycastHit.point + raycastHit.normal);
    }

    private void SetWeapon(WeaponHandler weaponHandler)
    {
        _weapon = weaponHandler.Weapon_SO;
    }

    private void OnDestroy()
    {
        _wearableItemsInventory.WeaponSlot.OnWeaponChanged -= SetWeapon;
    }
}