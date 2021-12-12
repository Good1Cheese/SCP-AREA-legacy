using UnityEngine;
using Zenject;

public class WeaponActivator : WearableItemActivator
{
    [SerializeField] private Transform _weaponParent;

    [Inject] private readonly WeaponSlot _weaponSlot;

    protected override WearableSlot WearableItemSlot => _weaponSlot;

    private new void Start()
    {
        base.Start();
        _itemParent = _weaponParent;
    }
}