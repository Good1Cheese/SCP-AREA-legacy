
using UnityEngine;

[CreateAssetMenu(fileName = "new Mask", menuName = "ScriptableObjects/Mask")]
public class Mask_SO : WearableItem_SO
{
    public override void Equip()
    {
        Inventory.MaskHandler.SetItem(this);
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}


