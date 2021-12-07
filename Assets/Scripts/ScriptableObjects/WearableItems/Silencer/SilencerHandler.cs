using UnityEngine;
using Zenject;

public class SilencerHandler : WearableItemHandler
{
    [Inject] private readonly WeaponSlot _weaponSlot;

    private GameObject _silencerForWorldWeapon;

    private new void Awake()
    {
        base.Awake();

        _silencerForWorldWeapon = Instantiate(_wearableIte_SO.playerGameObjectPrefab);
        _silencerForWorldWeapon.SetActive(false);
    }

    public override void Equip()
    {
        EquipOnWeapon((WeaponHandler)_weaponSlot.ItemHandler);
    }

    public void EquipOnWeapon(WeaponHandler weaponForEquiping)
    {
        if (weaponForEquiping == null || weaponForEquiping.SilencerHandler != null)
        {
            GameObject.SetActive(true);
            return;
        }

        SpawnSilencer(GameObjectForPlayer, weaponForEquiping.GameObjectForPlayer);
        SpawnSilencer(_silencerForWorldWeapon, weaponForEquiping.GameObject);

        weaponForEquiping.SilencerHandler = this;
        weaponForEquiping.CurrentShotSound = weaponForEquiping.Weapon_SO.shotSoundWithSilencer;
    }

    private void SpawnSilencer(GameObject silencer, GameObject spawnObject)
    {
        silencer.transform.SetParent(spawnObject.transform, false);
        silencer.transform.localPosition = _wearableIte_SO.playerGameObjectSpawnOffset;
        silencer.SetActive(true);
    }
}