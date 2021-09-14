﻿public abstract class WearableItem_SO : Item_SO
{
    protected WearableItemsInventory Inventory;

    public override void GetDependencies(PlayerInstaller playerInstaller)
    {
        Inventory = playerInstaller.EquipmentInventory;
    }
}

