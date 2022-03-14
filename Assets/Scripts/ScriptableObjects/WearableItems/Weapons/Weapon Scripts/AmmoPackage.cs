using System;
using UnityEngine;
using static Ammo_SO;

public class AmmoPackage : MonoBehaviour
{
    [SerializeField] private int _capacity;

    public ItemSlots<AmmoHandler> Clips { get; private set; }

    private void Awake()
    {
        Clips = new ItemSlots<AmmoHandler>(_capacity);
    }

    public bool Store(AmmoHandler ammoHandler)
    {
        var freeSlot = Array.Find(Clips.Slots, slot => !slot.HasItem);

        if (freeSlot == null) { return false; }

        freeSlot.Set(ammoHandler);

        return true;
    }

    public void DropAll(AmmoType ammoType)
    {
        for (int i = 0; i < Clips.Slots.Length; i++)
        {
            var item = Clips.Slots[i].Item;

            if (item == null || item.Ammo_SO.ammoType != ammoType) { continue; }

            item.Dropped();
        }
    }

    public void Remove(ItemSlot<AmmoHandler> clipSlot)
    {
        for (int i = 0; i < Clips.Slots.Length; i++)
        {
            if (Clips.Slots[i] == clipSlot)
            {
                Clips.Slots[i].Clear();
                return;
            }
        }
    }
}