using UnityEngine;

[CreateAssetMenu(fileName = "new Silencer", menuName = "ScriptableObjects/Silencer")]
public class Silencer_SO : WearableItem_SO
{
    EquipmentInventory m_equipmentInventory;
    Weapon_SO weapon_SO;

    public override void GetDependencies(PlayerInstaller playerInstaller)
    {
        base.GetDependencies(playerInstaller);
        m_equipmentInventory = playerInstaller.EquipmentInventory;
    }

    public override void Equip()
    {
        weapon_SO = m_equipmentInventory.WeaponCell.Item as Weapon_SO;
        if (weapon_SO != null)
        {
            weapon_SO.EquipSilencer(this);
        }
    }


}

