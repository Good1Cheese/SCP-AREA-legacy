using System.Linq;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponSaving))]
public class WeaponHandler : WearableItemHandler
{
    private WeaponAim _weaponAim;
    private Transform _mainCamera;
    private AmmoPackage _ammoPackage;

    private static bool _isVariablesInitialized;

    public Weapon_SO Weapon_SO { get; private set; }
    public ClippingMaker ClippingMaker { get; private set; }
    public SilencerHandler SilencerHandler { get; set; }
    public AudioClip CurrentShotSound { get; set; }
    public ItemSlot<AmmoHandler> CurrentClipSlot { get; set; } = new ItemSlot<AmmoHandler>();

    public int CurrentClipAmmo
    {
        get => CurrentClipSlot.Item == null ? 0 : CurrentClipSlot.Item.Ammo;
        set => CurrentClipSlot.Item.Ammo = value;
    }

    [Inject]
    private void Construct(WeaponSlot weaponSlot,
                           WeaponAim weaponAim,
                           [Inject(Id = "Camera")] Transform mainCamera,
                           AmmoPackage ammoPackage)
    {
        _wearableSlot = weaponSlot;
        _weaponAim = weaponAim;
        _mainCamera = mainCamera;
        _ammoPackage = ammoPackage;
    }

    private new void Start()
    {
        base.Start();

        if (_isVariablesInitialized) { return; }

        _isVariablesInitialized = true;

        Weapon_SO.shotTimeout = new WaitForSeconds(Weapon_SO.shotDelay);
        Weapon_SO.firstReloadStageTimeout = new WaitForSeconds(Weapon_SO.firstReloadStageDelay);
        Weapon_SO.secondReloadStageTimeout = new WaitForSeconds(Weapon_SO.secondReloadStageDelay);
        Weapon_SO.thirdReloadStageTimeout = new WaitForSeconds(Weapon_SO.thirdReloadStageDelay);
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

        if (CurrentClipSlot.Item == null) { return; }

        _ammoPackage.Store(CurrentClipSlot.Item);
    }

    public override void Dropped()
    {
        _ammoPackage.Remove(CurrentClipSlot);
        base.Dropped();
    }
}