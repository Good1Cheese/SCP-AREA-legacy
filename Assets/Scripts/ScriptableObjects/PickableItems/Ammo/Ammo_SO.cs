using UnityEngine;

[CreateAssetMenu(fileName = "new HK-USP Ammo", menuName = "ScriptableObjects/Ammo/HK-USP")]
public class Ammo_SO : PickableItem_SO
{
    public enum AmmoType
    {
        Pistol,
        Smg,
        Rifle,
    }

    public AmmoType ammoType;

    public override void Use()
    {
        
    }
}
