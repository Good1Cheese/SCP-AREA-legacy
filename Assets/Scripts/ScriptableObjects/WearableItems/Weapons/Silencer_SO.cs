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
        Weapon_SO weapon = Inventory.WeaponSlot.Item as Weapon_SO;
        if (weapon == null) { gameObject.SetActive(true); return; }

        gameObject = Instantiate(silencerForPlayer, weapon.playerWeapon.transform);
        SilencerTransform = gameObject.transform;

        GameObject worldSilencer = Instantiate(silencerForPlayer, weapon.gameObject.transform);
        worldSilencer.transform.position = Vector3.zero;
        worldSilencer.transform.localPosition = positionForSilencer;


        Inventory.WeaponSlot.OnSilencerEquiped.Invoke();
        weapon.SetSilencer(this);
    }

}

