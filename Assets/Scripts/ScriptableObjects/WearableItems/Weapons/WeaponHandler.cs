using UnityEngine;
using Zenject;

[RequireComponent(typeof(WeaponSaving))]
public class WeaponHandler : WearableItemHandler
{
    private int _ammoCount;
    private SilencerHandler _silencerHandler;
    [Inject(Id = "Player")] private readonly Transform _playerTransform;

    public int AmmoCount
    {
        get => _ammoCount;
        set => _ammoCount = value;
    }

    public int ClipAmmo { get; set; }
    public AudioClip CurrentShotSound { get; set; }
    public SilencerHandler SilencerHandler
    {
        get => _silencerHandler;
        set
        {
            CurrentShotSound = Weapon_SO.shotSoundWithSilencer;
            _silencerHandler = value;
        }
    }

    public Weapon_SO Weapon_SO => (Weapon_SO)_wearableIte_SO;

    private new void Awake()
    {
        base.Awake();

        GameObjectForPlayer.GetComponentInChildren<ClippingMaker>().PlayerTransform = _playerTransform;
        CurrentShotSound = Weapon_SO.shotSound;

        if (Weapon_SO.reloadTimeout != null) { return; }

        Weapon_SO.reloadTimeout = new WaitForSeconds(Weapon_SO.reloadDelay);
        Weapon_SO.shotTimeout = new WaitForSeconds(Weapon_SO.shotDelay);
    }

    public override void Equip()
    {
        _wearableItemsInventory.WeaponSlot.SetItem(this);
    }
}