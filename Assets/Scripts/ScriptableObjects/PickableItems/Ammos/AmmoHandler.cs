using UnityEngine;
using Zenject;

[RequireComponent(typeof(AmmoSaving))]
public class AmmoHandler : PickableItemHandler
{
    public const int MAX_SLOT_AMMO = 50;

    [SerializeField] private int _ammoCount;

    private AmmoPackage _ammoPackage;

    [Inject]
    private void Inject(AmmoPackage ammoStorage)
    {
        _ammoPackage = ammoStorage;
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
        if (!_ammoPackage.Store(this)) return;

        Equiped();
    }
}