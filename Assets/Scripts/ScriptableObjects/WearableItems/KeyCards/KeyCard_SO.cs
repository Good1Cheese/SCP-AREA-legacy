
using UnityEngine;

[CreateAssetMenu(fileName = "new KeyCard", menuName = "ScriptableObjects/KeyCard")]
public class KeyCard_SO : WearableItem_SO
{
    public int accsesLevel;

    public override void Equip()
    {
        Inventory.KeyCardCell.SetItem(this);
    }
}

