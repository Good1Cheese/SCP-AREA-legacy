
using UnityEngine;

public abstract class KeyCard_SO : WearableItem_SO
{
    public override void Equip()
    {
        Inventory.KeyCardHandler.SetItem(this);
    }

    public override void Use()
    {
        Debug.Log("dsa");
    }

}

