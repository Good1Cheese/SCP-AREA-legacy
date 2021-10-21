using System;

public class WeaponSlot : WearableItemSlot
{
    public Action<WeaponHandler> OnWeaponChanged { get; set; }
    public Action OnWeaponDropped { get; set; }

    public Action<WeaponHandler> OnAmmoAdded { get; set; }

    public Action OnSilencerEquiped { get; set; }
    public Action OnSilencerUnequiped { get; set; }

    public Action<bool> IsWeaponActived { get; set; }
    public bool IsWeaponActionIsGoing { get; set; }

    public override void OnItemReplaced()
    {
        IsWeaponActived.Invoke(false);
    }

    public override void OnItemSet()
    {
        base.OnItemSet();
        OnWeaponChanged.Invoke(ItemHandler as WeaponHandler);
    }

    public override void OnItemDeleted()
    {
        base.OnItemDeleted();
        OnWeaponDropped?.Invoke();
    }

}
