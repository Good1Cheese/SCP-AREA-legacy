using System.Linq;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponSaving))]
public class WeaponHandler : WearableItemHandler
{
    private WeaponAim _weaponAim;
    private Transform _mainCamera;
    private PickableItemsInventory _pickableItemsInventory;
    private int _ammoCount;

    public int Ammo => _ammoCount;
    public Weapon_SO Weapon_SO { get; private set; }
    public ClippingMaker ClippingMaker { get; private set; }
    public SilencerHandler SilencerHandler { get; set; }
    public int ClipAmmo { get; set; }
    public AudioClip CurrentShotSound { get; set; }

    [Inject]
    private void Construct(WeaponSlot weaponSlot,
                           WeaponAim weaponAim,
                           [Inject(Id = "Camera")] Transform mainCamera,
                           PickableItemsInventory pickableItemsInventory)
    {
        _wearableSlot = weaponSlot;
        _weaponAim = weaponAim;
        _mainCamera = mainCamera;
        _pickableItemsInventory = pickableItemsInventory;
    }

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
        ClippingMaker.MainCamera = _mainCamera;
    }

    public override void Equip()
    {
        _wearableSlot.SetItem(this);
    }
}