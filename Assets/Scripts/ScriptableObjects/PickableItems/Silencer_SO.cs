using UnityEngine;

[CreateAssetMenu(fileName = "new Silencer", menuName = "ScriptableObjects/Silencer")]
public class Silencer_SO : WearableItem_SO
{
    public override void Equip()
    {
        Inventory.WeaponSlot.OnSilencerEquiped.Invoke();
        Weapon_SO weapon = (Weapon_SO)Inventory.WeaponSlot.Item;
        weapon.silencer = this;
    }

    public override bool HasPlayerThisItem()
    {
        Weapon_SO weapon = (Weapon_SO)Inventory.WeaponSlot.Item;
        return weapon.silencer != null;
    }

}

