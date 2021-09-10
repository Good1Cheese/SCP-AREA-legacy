using UnityEngine;

[CreateAssetMenu(fileName = "new Silencer", menuName = "ScriptableObjects/Silencer")]
public class Silencer_SO : WearableItem_SO
{
    public GameObject silencerForPlayer;

    public Vector3 positionForSilencer;
    public Quaternion rotationForSilencer;

    public Transform SilencerTransform { get; set; }

    public override void Equip()
    {
        Weapon_SO weapon = (Weapon_SO)Inventory.WeaponSlot.Item;
        if (weapon == null) { gameObject.SetActive(true); return; }

        Destroy(gameObject);
        gameObject = Instantiate(silencerForPlayer, weapon.playerWeapon.transform);
        SilencerTransform = gameObject.transform;

        Inventory.WeaponSlot.OnSilencerEquiped.Invoke();
        weapon.SetSilencer(this);
    }

}

