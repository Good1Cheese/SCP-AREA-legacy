
using UnityEngine;

[CreateAssetMenu(fileName = "new Mask", menuName = "ScriptableObjects/Mask")]
public class Mask_SO : WearableItem_SO
{
    public override void Equip()
    {
        Inventory.MaskSlot.SetItem(this);
    }
}


