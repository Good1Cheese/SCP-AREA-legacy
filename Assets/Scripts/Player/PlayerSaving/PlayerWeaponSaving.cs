using Zenject;

public class PlayerWeaponSaving : WearableItemSaving
{
    [Inject] readonly WearableItemsInventory m_wearableItemsInventory;
    [Inject] readonly WeaponActivator m_weaponActivator;

    public bool isActive;
    public int ammoCount;
    public int clipAmmo;

    public WeaponHandler WeaponHandler { get => ItemHandler as WeaponHandler; }

    public override void Save()
    {
        ItemHandler = m_wearableItemsInventory.WeaponSlot.ItemHandler;

        if (ItemHandler == null) { return; }

        ammoCount = WeaponHandler.AmmoCount;
        isActive = WeaponHandler.GameObjectForPlayer.activeSelf;
        clipAmmo = WeaponHandler.ClipAmmo;

        itemName = ItemHandler.name;
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
        m_weaponActivator.SetItemActiveState(isActive);
    }
}
