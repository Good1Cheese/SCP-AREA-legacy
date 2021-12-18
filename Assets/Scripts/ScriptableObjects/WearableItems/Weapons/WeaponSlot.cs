using System;

public class WeaponSlot : WearableSlot
{
    public Action<WeaponHandler> Changed { get; set; }
    public Action AmmoAdded { get; set; }

    public override void Setted()
    {
        base.Setted();

        Changed.Invoke(ItemHandler as WeaponHandler);
        Toggled?.Invoke(false);
    }
}