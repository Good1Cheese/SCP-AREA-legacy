using UnityEngine;

public class SilencerHandler : WearableItemHandler
{
    GameObject m_silencerForWorldWeapon;

    new void Awake()
    {
        base.Awake();

        m_silencerForWorldWeapon = Instantiate(m_wearableItem_SO.playerGameObjectPrefab);
        m_silencerForWorldWeapon.SetActive(false);
    }

    public override void Equip()
    {
        EquipOnWeapon(m_wearableItemsInventory.WeaponSlot.ItemHandler as WeaponHandler);
        m_wearableItemsInventory.WeaponSlot.OnSilencerEquiped?.Invoke();
    }

    public void EquipOnWeapon(WeaponHandler weaponForEquiping)
    {
        if (weaponForEquiping == null || weaponForEquiping.SilencerHandler != null)
        {
            GameObject.SetActive(true);
            return;
        }

        SpawnSilencer(GameObjectForPlayer, weaponForEquiping.GameObjectForPlayer);
        SpawnSilencer(m_silencerForWorldWeapon, weaponForEquiping.GameObject);

        weaponForEquiping.SilencerHandler = this;
    }

    void SpawnSilencer(GameObject silencer, GameObject spawnObject)
    {
        silencer.transform.SetParent(spawnObject.transform, false);
        silencer.transform.localPosition = m_wearableItem_SO.playerGameObjectspawnOffset;
        silencer.SetActive(true);
    }
}