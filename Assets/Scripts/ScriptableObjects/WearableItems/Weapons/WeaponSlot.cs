using System;

public class WeaponSlot : WearableSlot
{
    public Action<WeaponHandler> OnWeaponChanged { get; set; }
    public Action OnWeaponDropped { get; set; }
    public Action OnAmmoAdded { get; set; }
    public Action<bool> IsWeaponActived { get; set; }

    public override void OnItemSet()
    {
        base.OnItemSet();

        OnWeaponChanged.Invoke(ItemHandler as WeaponHandler);
        IsWeaponActived.Invoke(false);
    }

    public override void OnItemDeleted()
    {
        base.OnItemDeleted();
        OnWeaponDropped?.Invoke();
    }
}