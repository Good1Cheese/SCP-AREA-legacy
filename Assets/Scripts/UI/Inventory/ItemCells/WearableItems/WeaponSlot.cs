using System;
using System.Collections;

public class WeaponSlot : WearableItemSlot
{
    IEnumerator weaponAction;
    public IEnumerator WeaponAction
    {
        get => weaponAction;
        set
        {
            if (weaponAction != null && value != null) { return; }
            weaponAction = value;
            if (value == null) { return; }
            StartCoroutine(weaponAction);
        }
    }

    public bool IsWeaponActionIsGoing { get; set; }

    public Action<Weapon_SO> OnWeaponChanged { get; set; }
    public Action OnWeaponDropped { get; set; }
    public Action OnWeaponActivatedOrDeactivated { get; set; }
    public Action<Weapon_SO> OnAmmoAdded { get; set; }
    public Action OnSilencerEquiped { get; set; }

    public override void OnItemSetted()
    {
        base.OnItemSetted();
        OnWeaponChanged.Invoke(Item as Weapon_SO);
    }

    public override void OnItemDeleted()
    {
        base.OnItemDeleted();
        OnWeaponDropped.Invoke();
    }

}
