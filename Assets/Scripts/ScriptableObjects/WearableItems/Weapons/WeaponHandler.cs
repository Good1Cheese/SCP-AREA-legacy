using System.Linq;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponSaving))]
public class WeaponHandler : WearableItemHandler
{
    [Inject] private readonly WeaponAim _weaponAim;
    [Inject] private readonly Camera _mainCamera;
    [Inject] private readonly PickableItemsInventory _pickableItemsInventory;
    [Inject] private readonly WeaponSlot _weaponSlot;

    private int _ammoCount;

    public int Ammo => _ammoCount;
    public Weapon_SO Weapon_SO { get; private set; }
    public ClippingMaker ClippingMaker { get; private set; }
    public SilencerHandler SilencerHandler { get; set; }
    public int ClipAmmo { get; set; }
    public AudioClip CurrentShotSound { get; set; }

    private new void Start()
    {
        base.Start();

        if (Weapon_SO.reloadTimeout != null) { return; }

        Weapon_SO.reloadTimeout = new WaitForSeconds(Weapon_SO.reloadDelay);
        Weapon_SO.shotTimeout = new WaitForSeconds(Weapon_SO.shotDelay);
    }

    private new void Awake()
    {
        base.Awake();

        Weapon_SO = (Weapon_SO)_wearableIte_SO;
        CurrentShotSound = Weapon_SO.shotSound;
        ClippingMaker = GameObjectForPlayer.GetComponentInChildren<ClippingMaker>();
        ClippingMaker.WeaponAim = _weaponAim;
        ClippingMaker.ParentTransform = GameObjectForPlayer.transform;
        ClippingMaker.MainCamera = _mainCamera.transform;
    }

    public override void Equip()
    {
        _weaponSlot.SetItem(this);
    }

    public void UpdateAmmo()
    {
        _ammoCount = _pickableItemsInventory.Inventory.TakeWhile(item => item != null)
            .Where(item => item as AmmoHandler != null)
            .Sum(item => ((AmmoHandler)item).Ammo);
    }
}