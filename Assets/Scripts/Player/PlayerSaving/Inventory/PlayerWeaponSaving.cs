using Zenject;

public class PlayerWeaponSaving : WearableItemSaving
{
    [Inject] private readonly WearableItemsInventory _wearableItemsInventory;

    protected override WearableItemSlot SlotToSave => _wearableItemsInventory.WeaponSlot;
}
