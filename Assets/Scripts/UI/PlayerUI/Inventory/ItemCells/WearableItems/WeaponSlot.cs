using System;

public class WeaponSlot : WearableItemSlot
{
    public Action<Weapon_SO> OnWeaponChanged { get; set; }
    public Action<Weapon_SO> OnAmmoAdded { get; set; }

    public Action OnWeaponDropped { get; set; }
    public Action OnSilencerEquiped { get; set; }

    public Action<bool> IsWeaponActived { get; set; }
    public bool IsWeaponActionIsGoing { get; set; }

    public override void OnItemSet()
    {
        print("Weapon set");
        base.OnItemSet();
        OnWeaponChanged.Invoke(Item as Weapon_SO);
    }

    public override void OnItemDeleted()
    {
        print("Weapon deleted");
        base.OnItemDeleted();
        OnWeaponDropped.Invoke();
    }

}
