
using UnityEngine;

[CreateAssetMenu(fileName = "new Weapon", menuName = "ScriptableObjects/Weapon")]
public class Weapon_SO : WearableItem_SO
{
    public override void Equip()
    {
        Inventory.WeaponHandler.SetItem(this);
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}


