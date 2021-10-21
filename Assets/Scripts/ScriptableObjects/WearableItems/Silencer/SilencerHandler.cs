using UnityEngine;
using Zenject;

public class SilencerHandler : ItemHandler
{
    [SerializeField] Silencer_SO m_silencer_SO;

    [Inject] readonly WearableItemsInventory m_wearableItemsInventory;

    GameObject m_silencerForPlayerWeapon;
    GameObject m_silencerForWorldWeapon;

    void Awake()
    {
        m_silencerForPlayerWeapon = Instantiate(m_silencer_SO.silencerForPlayerPrefab);
        m_silencerForPlayerWeapon.SetActive(false);

        m_silencerForWorldWeapon = Instantiate(m_silencerForPlayerWeapon);
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
            print("Bug moment");
            GameObject.SetActive(true);
            return;
        }

        SpawnSilencer(m_silencerForPlayerWeapon, weaponForEquiping.WearableItemForPlayer);
        SpawnSilencer(m_silencerForWorldWeapon, weaponForEquiping.GameObject);

        weaponForEquiping.SilencerHandler = this;
    }

    void SpawnSilencer(GameObject silencer, GameObject spawnObject)
    {
        silencer.transform.SetParent(spawnObject.transform, false);
        silencer.transform.localPosition = m_silencer_SO.positionForSilencer;
        silencer.SetActive(true);
    }

    public override Item_SO GetItem() => m_silencer_SO;
}