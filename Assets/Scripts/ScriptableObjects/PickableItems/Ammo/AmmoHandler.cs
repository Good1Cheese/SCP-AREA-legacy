using UnityEngine;
using Zenject;

[RequireComponent(typeof(AmmoSaving))]
public class AmmoHandler : PickableItemHandler
{
    [SerializeField] private int _ammoCount;

    [Inject] private readonly AmmoMixup _ammoMixup;

    public Ammo_SO Ammo_SO => (Ammo_SO)_pickableIte_SO;
    public int AmmoCount { get => _ammoCount; set => _ammoCount = value; }

    public override void Equip()
    {
        _ammoMixup.MixUpAmmo(this);
        base.Equip();
    }
}
