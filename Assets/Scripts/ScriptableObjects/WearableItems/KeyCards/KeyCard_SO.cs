
using UnityEngine;

[CreateAssetMenu(fileName = "new KeyCard", menuName = "ScriptableObjects/KeyCard")]
public class KeyCard_SO : WearableItem_SO
{
    public int accsesLevel;

    public override void Equip()
    {
        Inventory.KeyCardHandler.SetItem(this);
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}

