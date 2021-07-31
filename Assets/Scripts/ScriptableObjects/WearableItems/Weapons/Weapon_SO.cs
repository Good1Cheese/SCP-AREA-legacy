using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new Weapon", menuName = "ScriptableObjects/Weapon")]
public class Weapon_SO : WearableItem_SO
{
    public GameObject weaponForPlayer;

    public int damagePerShot;
    public int cartridgeСlipAmmo;
    public int ammoCount;
    public int cartidgeClipMaxAmmo;
    public int caliber;

    public Vector3 spawnOffset;
    public Vector3 bulletSpawnPoint;

    public RuntimeAnimatorController weaponAnimationContoller;

    public AudioClip shotSound;
    public AudioClip reloadSound;
    public AudioClip shotSoundWithSilencer;
    public AudioClip missFireSound;

    public Silencer_SO silencer_SO;

    public static Action<Weapon_SO> OnSilencerEquiped;

    public override void Equip()
    {
        Inventory.WeaponCell.SetItem(this);
    }

    public void EquipSilencer(Silencer_SO silencer)
    {
        silencer_SO = silencer;
        OnSilencerEquiped.Invoke(this);
    }

}

