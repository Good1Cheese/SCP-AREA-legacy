using UnityEngine;

public class SilencerHandler : WearableItemHandler
{
    private GameObject _silencerForWorldWeapon;

    private new void Awake()
    {
        base.Awake();

        _silencerForWorldWeapon = Instantiate(_wearableIte_SO.playerGameObjectPrefab);
        _silencerForWorldWeapon.SetActive(false);
    }

    public override void Equip()
    {
        EquipOnWeapon(_wearableItemsInventory.WeaponSlot.ItemHandler as WeaponHandler);
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
    }

    private void SpawnSilencer(GameObject silencer, GameObject spawnObject)
    {
        silencer.transform.SetParent(spawnObject.transform, false);
        silencer.transform.localPosition = _wearableIte_SO.playerGameObjectspawnOffset;
        silencer.SetActive(true);
    }
}