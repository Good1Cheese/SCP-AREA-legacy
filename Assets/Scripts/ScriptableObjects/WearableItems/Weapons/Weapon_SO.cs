using UnityEngine;
using static Ammo_SO;

[CreateAssetMenu(fileName = "new Weapon", menuName = "ScriptableObjects/WearableItems/Weapons/HK-USP")]
public class Weapon_SO : WearableIte_SO
{
    public int damagePerShot;
    public AmmoType ammoType;
    public int clipMaxAmmo;

    public Vector3 recoil;
    public Vector3 aimRecoil;

    public float recoilReturnSpeed;
    public float snappiness;

    public float shotDelay;
    public WaitForSeconds shotTimeout;

    public float reloadDelay;
    public WaitForSeconds reloadTimeout;

    public Vector3 bulletSpawnPoint;
    public GameObject bulletHolePrefab;

    public RuntimeAnimatorController weaponAnimationContoller;

    public AudioClip shotSound;
    public AudioClip missFireSound;
    public AudioClip shotSoundWithSilencer;
    public AudioClip reloadSound;
}

