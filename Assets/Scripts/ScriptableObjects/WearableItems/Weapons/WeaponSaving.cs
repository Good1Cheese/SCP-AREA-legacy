using UnityEngine;
using Zenject;

public class WeaponSaving : ItemSaving
{
    [Inject(Id = "PropsHandler")] readonly protected Transform PropsHandler;

    WeaponHandler m_weaponHandler;

    public string silencerName;
    public int ammoCount;
    public int clipAmmo;
    public bool isAmmoAdded;

    new void Start()
    {
        base.Start();
        m_weaponHandler = GetComponent<WeaponHandler>();
    }

    public override void Save()
    {
        ammoCount = m_weaponHandler.AmmoCount;
        clipAmmo = m_weaponHandler.ClipAmmo;
        base.Save();

        if (m_weaponHandler.SilencerHandler == null) { return; }

        silencerName = m_weaponHandler.SilencerHandler.GameObject.name;
    }

    public override void LoadData()
    {
        base.LoadData();

        m_weaponHandler.AmmoCount = ammoCount;
        m_weaponHandler.ClipAmmo = clipAmmo;

        if (string.IsNullOrEmpty(silencerName)) { return; }

        GameObject itemGameObject = PropsHandler.Find(silencerName).gameObject;
        SilencerHandler silencerHandler = itemGameObject.GetComponent<ItemHandler>() as SilencerHandler;

        silencerHandler.EquipOnWeapon(m_weaponHandler);
    }
}