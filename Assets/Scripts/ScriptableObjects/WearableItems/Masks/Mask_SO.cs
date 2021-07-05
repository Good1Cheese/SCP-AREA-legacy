
using UnityEngine;

public abstract class Mask_SO : WearableItem_SO
{
    public override void Equip()
    {
        Inventory.MaskHandler.SetItem(this);
    }

    public override void Use()
    {
        Debug.Log("dsa");
    }

}


