using UnityEngine;
using Zenject;

[RequireComponent(typeof(AmmoSaving))]
public class AmmoHandler : PickableItemHandler
{
    public const int MAX_SLOT_AMMO = 50;

    [SerializeField] private int _ammoCount;

    private WeaponSlot _weaponSlot;
    private AmmoMixup _ammoMixup;

    [Inject]
    private void Inject(WeaponSlot weaponSlot, AmmoMixup ammoMixup)
    {
        _weaponSlot = weaponSlot;
        _ammoMixup = ammoMixup;
    }

    public Ammo_SO Ammo_SO => (Ammo_SO)_pickableItem_SO;

    public int Ammo
    {
        get => _ammoCount;
        set
        {
            value = Mathf.Clamp(value, 0, int.MaxValue);
            _ammoCount = value;
        }
    }

    public override void Equip()
    {
        _ammoMixup.MixUpAmmo(this);
        base.Equip();

        _weaponSlot.AmmoAdded?.Invoke();
    }
}