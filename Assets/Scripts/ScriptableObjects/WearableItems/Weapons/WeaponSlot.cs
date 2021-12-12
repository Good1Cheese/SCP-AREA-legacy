using System;
using UnityEngine;
using Zenject;

public class WeaponSlot : WearableSlot
{
    public Action<WeaponHandler> OnWeaponChanged { get; set; }
    public Action OnAmmoAdded { get; set; }

    public override void OnItemSet()
    {
        base.OnItemSet();

        OnWeaponChanged.Invoke(ItemHandler as WeaponHandler);
        OnItemToggled?.Invoke(false);
    }
}