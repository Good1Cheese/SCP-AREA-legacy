using Zenject;

public class PlayerWeaponSaving : WearableItemSaving
{
    [Inject] readonly m_wearableItemsInventory m_wearableItemsInventory;

    public int ammoCount;
    public int clipAmmo;

    public WeaponHandler WeaponHandler { get => ItemHandler as WeaponHandler; }

    protected override WearableItemSlot SlotToSave => m_wearableItemsInventory.WeaponSlot;

    protected override void SaveWearableItem()
    {
        ammoCount = WeaponHandler.AmmoCount;
        clipAmmo = WeaponHandler.ClipAmmo;
    }

    public override void Load(string json)
    {
        base.Load(json);

        if (ItemHandler == null) { return; }

        LoadWeaponData();
    }

    public void LoadWeaponData()
    {
        WeaponHandler.AmmoCount = ammoCount;
        WeaponHandler.ClipAmmo = clipAmmo;
    }
}
