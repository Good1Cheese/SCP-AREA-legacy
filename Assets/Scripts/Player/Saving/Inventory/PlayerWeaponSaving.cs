using Zenject;

public class PlayerWeaponSaving : WearableItemSaving
{
    [Inject] private readonly WeaponSlot _weaponSlot;

    protected override WearableSlot SlotToSave => _weaponSlot;
}