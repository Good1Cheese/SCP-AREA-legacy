using System;
using System.Collections;
using Zenject;

public class WeaponReload : CoroutineUser
{
    private AmmoHandler[] _inventoryAmmo;
    private int _calculatedClipAmmo;
    private WeaponHandler _weaponHandler;
    protected WeaponSlot _weaponSlot;
    private PickableItemsInventory _pickableItemsInventory;
    private ItemActionCreator _itemActionCreator;

    protected override IEnumerator Method => Reload();
    protected override IEnumerator Coroutine() { yield break; }
    public Action Reloaded { get; set; }

    [Inject]
    private void Inject(WeaponSlot weaponSlot, PickableItemsInventory pickableItemsInventory, ItemActionCreator itemActionCreator)
    {
        _weaponSlot = weaponSlot;
        _pickableItemsInventory = pickableItemsInventory;
        _itemActionCreator = itemActionCreator;
    }

    private new void Start()
    {
        base.Start();
        _weaponSlot.Changed += SetWeaponHandler;
    }

    public void ActivateReload()
    {
        if (_weaponHandler.ClipAmmo == _weaponHandler.Weapon_SO.clipMaxAmmo
            || _weaponHandler.Ammo == 0
            || _itemActionCreator.IsGoing) { return; }

        StartWithoutInterrupt();
    }

    public IEnumerator Reload()
    {
        _itemActionCreator.StartInterruptingItemAction(this, _weaponHandler.Weapon_SO.reloadSound);

        AddClipAmmoToInventoryAmmo();
        _weaponHandler.ClipAmmo = 0;

        Reloaded?.Invoke();

        yield return _weaponHandler.Weapon_SO.reloadTimeout;

        CalculateCurrentClipAmmo();

        _weaponHandler.ClipAmmo = _calculatedClipAmmo;
        IsCoroutineGoing = false;
    }

    private void CalculateCurrentClipAmmo()
    {
        int clipAmmo = _weaponHandler.Weapon_SO.clipMaxAmmo;

        for (int i = 0; i < _inventoryAmmo.Length; i++)
        {
            var currentAmmo = _inventoryAmmo[i].Ammo;
            _inventoryAmmo[i].Ammo -= clipAmmo;

            if (currentAmmo > clipAmmo) { clipAmmo = 0; break; }

            clipAmmo -= currentAmmo;

            if (clipAmmo < 0) { break; }
        }
        _calculatedClipAmmo = _weaponHandler.Weapon_SO.clipMaxAmmo - clipAmmo;
    }

    private void AddClipAmmoToInventoryAmmo()
    {
        _inventoryAmmo = (AmmoHandler[])Array.FindAll(_pickableItemsInventory.Inventory, item =>
        {
            var ammo = item as AmmoHandler;
            return ammo != null && ammo.Ammo != 0;
        });

        AmmoHandler freeAmmoHandler = Array.Find(_inventoryAmmo, ammo => ammo.Ammo + _weaponHandler.ClipAmmo <= AmmoHandler.MAX_SLOT_AMMO);
        freeAmmoHandler.Ammo += _weaponHandler.ClipAmmo;
    }

    protected void SetWeaponHandler(WeaponHandler weaponHandler)
    {
        _weaponHandler = weaponHandler;
    }
}