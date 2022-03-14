using UnityEngine;
using Zenject;

public class WeaponUser : MonoBehaviour
{
    protected WeaponSlot _weaponSlot;
    protected WeaponHandler _weaponHandler;

    [Inject]
    private void Inject(WeaponSlot weaponSlot)
    {
        _weaponSlot = weaponSlot;
    }

    protected void Start()
    {
        _weaponSlot.Changed += SetWeaponHandler;
    }

    protected virtual void SetWeaponHandler(WeaponHandler weaponHandler) => _weaponHandler = weaponHandler;

    protected void OnDestroy()
    {
        _weaponSlot.Changed -= SetWeaponHandler;
    }
}