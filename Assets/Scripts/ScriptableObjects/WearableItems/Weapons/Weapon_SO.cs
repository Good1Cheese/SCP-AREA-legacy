using UnityEngine;

[CreateAssetMenu(fileName = "new Weapon", menuName = "ScriptableObjects/Weapon")]
public class Weapon_SO : WearableItem_SO
{
    public GameObject playerWeaponPrefab;
    public GameObject playerWeapon;

    public int damagePerShot;
    public int caliber;
    public int ammoCount;
    public int clipAmmo;
    public int clipMaxAmmo;

    public float delayAfterShot;
    public WaitForSeconds timeoutAfterShot;

    public Vector3 spawnOffset;
    public Vector3 bulletSpawnPoint;
    public GameObject bulletHolePrefab;

    public RuntimeAnimatorController weaponAnimationContoller;

    public Silencer_SO silencer_SO;

    public AudioClip currentShotSound;
    public AudioClip shotSoundPrefab;
    public AudioClip missFireSoundPrefab;
    public AudioClip shotSoundWithSilencerPrefab;
    public AudioClip reloadSoundPrefab;

    public void SetSilencer(Silencer_SO silencer_SO)
    {
        this.silencer_SO = silencer_SO;
        silencer_SO.gameObject.SetActive(playerWeaponPrefab.activeSelf);

        silencer_SO.SilencerTransform.localPosition = silencer_SO.positionForSilencer;
        silencer_SO.SilencerTransform.localRotation = silencer_SO.rotationForSilencer;
    }

    public override void Equip()
    {
        Inventory.WeaponSlot.SetItem(this);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        clipAmmo = 0;
        ammoCount = 0;
        currentShotSound = shotSoundPrefab;
        silencer_SO = null;
    }
}

