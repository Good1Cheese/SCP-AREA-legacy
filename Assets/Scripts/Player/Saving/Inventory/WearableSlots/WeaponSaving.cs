using UnityEngine;
using Zenject;

public class WeaponSaving : ItemSaving
{
    [Inject(Id = "PropsHandler")] protected readonly Transform PropsHandler;

    private WeaponHandler _weaponHandler;
    public string silencerName;

    private void Start()
    {
        _weaponHandler = GetComponent<WeaponHandler>();
    }

    public override void Save()
    {
        base.Save();

        if (_weaponHandler.SilencerHandler == null) { return; }

        silencerName = _weaponHandler.SilencerHandler.GameObject.name;
    }

    public override void Load()
    {
        base.Load();

        if (string.IsNullOrEmpty(silencerName)) { return; }

        GameObject itemGameObject = PropsHandler.Find(silencerName).gameObject;
        SilencerHandler silencerHandler = itemGameObject.GetComponent<ItemHandler>() as SilencerHandler;

        silencerHandler.EquipOnWeapon(_weaponHandler);
    }
}