using UnityEngine;
using Zenject;

public class WeaponMiss : MonoBehaviour
{
    private WeaponSlot _weaponSlot;
    private ItemActionCreator _itemActionCreator;
    private WaitForSeconds _timeoutAfterShot;
    private WeaponHandler _weaponHandler;

    [Inject]
    private void Inject(WeaponSlot weaponSlot, ItemActionCreator itemActionCreator)
    {
        _weaponSlot = weaponSlot;
        _itemActionCreator = itemActionCreator;
    }

    private void Start()
    {
        _weaponSlot.Changed += SetWeaponTimeoutAfterShot;
    }

    public void ActivateMissSound()
    {
        _itemActionCreator.StartItemAction(_timeoutAfterShot, _weaponHandler.Weapon_SO.missFireSound, false);
    }

    private void SetWeaponTimeoutAfterShot(WeaponHandler weaponHandler)
    {
        _weaponHandler = weaponHandler;
        _timeoutAfterShot = new WaitForSeconds(weaponHandler.Weapon_SO.shotDelay);
    }

    private void OnDestroy()
    {
        _weaponSlot.Changed -= SetWeaponTimeoutAfterShot;
    }
}