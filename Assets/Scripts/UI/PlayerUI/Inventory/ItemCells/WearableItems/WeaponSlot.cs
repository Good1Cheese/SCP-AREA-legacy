using System;
using Zenject;

public class WeaponSlot : WearableItemSlot
{
    [Inject] readonly WeaponSpawnerAndDestroyer m_weaponSpawnerAndDestroyer;

    public Action<WeaponHandler> OnWeaponChanged { get; set; }
    public Action OnWeaponDropped { get; set; }

    public Action<WeaponHandler> OnAmmoAdded { get; set; }

    public Action OnSilencerEquiped { get; set; }
    public Action OnSilencerUnequiped { get; set; }

    public Action<bool> IsWeaponActived { get; set; }
    public bool IsWeaponActionIsGoing { get; set; }

    public override void OnItemSet()
    {
        base.OnItemSet();
        OnWeaponChanged.Invoke(ItemHandler as WeaponHandler);
    }

    public override void OnItemDeleted()
    {
        base.OnItemDeleted();
        m_weaponSpawnerAndDestroyer.DespawnWeapon();
        OnWeaponDropped?.Invoke();
    }

    public override void ClearSlot()
    {
        base.ClearSlot();
        OnWeaponDropped?.Invoke();
    }

}
