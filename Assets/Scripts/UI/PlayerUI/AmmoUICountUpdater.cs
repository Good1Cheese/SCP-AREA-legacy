using TMPro;
using UnityEngine;
using Zenject;

public class AmmoUICountUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
    [Inject] private readonly WearableItemsInventory _wearableItemsInventory;
    [Inject] private readonly WeaponFire _weaponFire;
    [Inject] private readonly WeaponReload _weaponReload;
    private WeaponHandler _weaponHandler;

    public TextMeshProUGUI TextMeshProUGUI => _textMeshProUGUI;

    private void Awake()
    {
        _wearableItemsInventory.WeaponSlot.OnWeaponChanged += SetWeapon;
        _wearableItemsInventory.WeaponSlot.OnAmmoAdded += UpdateUIProperly;
        _weaponFire.OnPlayerFired += UpdateUI;
        _weaponReload.OnWeaponAmmoChanged += UpdateUI;
    }

    private void SetWeapon(WeaponHandler weaponHandler)
    {
        _weaponHandler = weaponHandler;
    }

    public void UpdateUI()
    {
        TextMeshProUGUI.text = string.Format($"{_weaponHandler.ClipAmmo}/{_weaponHandler.AmmoCount}");
    }

    public void UpdateUIProperly(WeaponHandler weaponHandler)
    {
        TextMeshProUGUI.text = string.Format($"{weaponHandler.ClipAmmo}/{weaponHandler.AmmoCount}");
    }

    private void OnDestroy()
    {
        _wearableItemsInventory.WeaponSlot.OnWeaponChanged -= SetWeapon;
        _wearableItemsInventory.WeaponSlot.OnAmmoAdded -= UpdateUIProperly;
        _weaponFire.OnPlayerFired -= UpdateUI;
        _weaponReload.OnPlayerReloaded -= UpdateUI;
    }

}
