using System;
using System.Collections;
using System.Linq;
using Zenject;

public class WeaponReload : CoroutineUser
{
    private AmmoHandler[] _inventoryAmmo;
    private int _calculatedClipAmmo;
    private WeaponHandler _weaponHandler;
    protected WeaponSlot _weaponSlot;
    private PickableItemsInventory _pickableItemsInventory;

    protected override IEnumerator Method => Reload();
    protected override IEnumerator Coroutine() { yield break; }

    [Inject]
    private void Inject(WeaponSlot weaponSlot, PickableItemsInventory pickableItemsInventory)
    {
        _weaponSlot = weaponSlot;
        _pickableItemsInventory = pickableItemsInventory;
    }

    private new void Start()
    {
        base.Start();
        _weaponSlot.Changed += SetWeaponHandler;
    }

    public void ActivateReload()
    {
        if (_weaponHandler.ClipAmmo == _weaponHandler.Weapon_SO.clipMaxAmmo
            || _weaponHandler.Ammo == 0) { return; }

        StartWithoutInterrupt();
    }

    public IEnumerator Reload()
    {
        yield return _weaponHandler.Weapon_SO.reloadTimeout;

        GetAllWeaponAmmo();
        CalculateCurrentClipAmmo();

        _weaponHandler.ClipAmmo = _calculatedClipAmmo;
        IsCoroutineGoing = false;
        print("da");
    }

    private void CalculateCurrentClipAmmo()
    {
        int clipAmmo = _weaponHandler.Weapon_SO.clipMaxAmmo;
        clipAmmo -= _weaponHandler.ClipAmmo;

        for (int i = 0; i < _inventoryAmmo.Length; i++)
        {
            var currentAmmo = _inventoryAmmo[i].Ammo;
            _inventoryAmmo[i].Ammo -= clipAmmo;

            if (currentAmmo > clipAmmo) { clipAmmo = 0; break; }

            clipAmmo -= currentAmmo;

            if (clipAmmo < 0) { break; }
        }

        _calculatedClipAmmo = _weaponHandler.Weapon_SO.clipMaxAmmo - clipAmmo;
        _weaponHandler.UpdateAmmo();
    }

    private void GetAllWeaponAmmo()
    {
        var itemHandlers = _pickableItemsInventory.Inventory.Where(item =>
        {
            return item is AmmoHandler ammo
                   && ammo.Ammo > 0
                   && ammo.Ammo_SO.ammoType == _weaponHandler.Weapon_SO.ammoType;
        });

        _inventoryAmmo = itemHandlers.Select(ammo => (AmmoHandler)ammo).ToArray();
    }

    protected void SetWeaponHandler(WeaponHandler weaponHandler)
    {
        _weaponHandler = weaponHandler;
    }
}