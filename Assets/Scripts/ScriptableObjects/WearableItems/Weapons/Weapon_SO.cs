using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new Weapon", menuName = "ScriptableObjects/Weapon")]
public class Weapon_SO : WearableItem_SO
{
    public GameObject weaponForPlayerPrefab;
    public GameObject playerWeapon;

    public int damagePerShot;
    public int cartridgeСlipAmmo;
    public int ammoCount;
    public int cartidgeClipMaxAmmo;
    public int caliber;

    public Vector3 spawnOffset;
    public Vector3 bulletSpawnPoint;
    public GameObject bulletHolePrefab;

    public RuntimeAnimatorController weaponAnimationContoller;

    public Silencer_SO silencer;

    public AudioClip shotSound;
    public AudioClip reloadSound;
    public AudioClip shotSoundWithSilencer;
    public AudioClip missFireSound;

    public void SetSilencer(Silencer_SO silencer)
    {
        this.silencer = silencer;
        silencer.gameObject.SetActive(weaponForPlayerPrefab.activeSelf);

        silencer.SilencerTransform.localPosition = silencer.positionForSilencer;
        silencer.SilencerTransform.localRotation = silencer.rotationForSilencer;
    }

    public override void Equip()
    {
        Inventory.WeaponSlot.SetItem(this);
    }
}

