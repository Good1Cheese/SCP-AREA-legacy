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

    public Vector3 spawnOffset;
    public Vector3 bulletSpawnPoint;
    public GameObject bulletHolePrefab;

    public RuntimeAnimatorController weaponAnimationContoller;

    public AudioClip shotSoundPrefab;
    public AudioClip missFireSoundPrefab;
    public AudioClip shotSoundWithSilencerPrefab;
    public AudioClip reloadSoundPrefab;
}

