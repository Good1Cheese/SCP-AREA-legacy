using UnityEngine;
using Zenject;

public class WeaponSaving : ItemSaving
{
    [Inject(Id = "PropsHandler")] protected readonly Transform PropsHandler;
    private WeaponHandler _weaponHandler;

    public string silencerName;
    public int clipAmmo;

    private void Start()
    {
        _weaponHandler = GetComponent<WeaponHandler>();
    }

    public override void Save()
    {
        base.Save();
        clipAmmo = _weaponHandler.ClipAmmo;

        if (_weaponHandler.SilencerHandler == null) { return; }

        silencerName = _weaponHandler.SilencerHandler.GameObject.name;
    }

    public override void LoadData()
    {
        base.LoadData();

        _weaponHandler.ClipAmmo = clipAmmo;

        if (string.IsNullOrEmpty(silencerName)) { return; }

        GameObject itemGameObject = PropsHandler.Find(silencerName).gameObject;
        SilencerHandler silencerHandler = itemGameObject.GetComponent<ItemHandler>() as SilencerHandler;

        silencerHandler.EquipOnWeapon(_weaponHandler);
    }
}