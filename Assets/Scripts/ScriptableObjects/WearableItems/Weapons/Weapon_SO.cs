
using UnityEngine;

public abstract class Weapon_SO : WearableItem_SO
{
    public override void Equip()
    {
        Inventory.WeaponHandler.SetItem(this);
    }

    public override void Use()
    {
        Debug.Log("dsa");
    }

}


