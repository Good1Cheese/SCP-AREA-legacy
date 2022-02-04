using System;
using UnityEngine;
using Zenject;
using static Ammo_SO;

public class AmmoPackage : MonoBehaviour
{
    [SerializeField] private int _capacity;

    private WeaponSlot _weaponSlot;

    public ItemSlots<AmmoHandler> Сlips { get; private set; }

    [Inject]
    private void Construct(WeaponSlot weaponSlot)
    {
        _weaponSlot = weaponSlot;
    }

    private void Awake()
    {
        Сlips = new ItemSlots<AmmoHandler>(_capacity);
    }

    public bool Store(AmmoHandler ammoHandler)
    {
        var freeSlot = Array.Find(Сlips.Slots, slot => !slot.HasItem);

        if (freeSlot == null) return false;

        freeSlot.Set(ammoHandler);

        return true;
    }

    public void DropAll(AmmoType ammoType)
    {
        for (int i = 0; i < Сlips.Slots.Length; i++)
        {
            var item = Сlips.Slots[i].Item;

            if (item == null || item.Ammo_SO.ammoType != ammoType) { continue; }

            item.Dropped();
        }
    }
}