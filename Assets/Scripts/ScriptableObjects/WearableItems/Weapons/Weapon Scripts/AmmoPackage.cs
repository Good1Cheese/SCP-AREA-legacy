using System;
using UnityEngine;

public class AmmoPackage : MonoBehaviour
{
    [SerializeField] private int _capacity;

    public ItemSlots<AmmoHandler> Сlips { get; set; }

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
}