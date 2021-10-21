using System;
using UnityEngine;
using static Ammo_SO;

[CreateAssetMenu(fileName = "new Weapon", menuName = "ScriptableObjects/WearableItems/Weapon")]
public class Weapon_SO : Item_SO
{
    public GameObject playerWeaponPrefab;

    public int damagePerShot;
    public AmmoType ammoType;
    public int clipMaxAmmo;

    public float delayAfterShot;
    public WaitForSeconds timeoutAfterShot;

    public Vector3 bulletSpawnPoint;
    public GameObject bulletHolePrefab;

    public RuntimeAnimatorController weaponAnimationContoller;

    public AudioClip shotSound;
    public AudioClip missFireSound;
    public AudioClip shotSoundWithSilencer;
    public AudioClip reloadSound;

    public static implicit operator AudioClip(Weapon_SO v)
    {
        throw new NotImplementedException();
    }
}

