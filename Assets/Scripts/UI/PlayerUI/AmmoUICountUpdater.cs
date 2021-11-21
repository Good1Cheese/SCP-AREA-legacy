using TMPro;
using UnityEngine;
using Zenject;

public class AmmoUICountUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    [Inject] private readonly WearableItemsInventory _wearableItemsInventory;

    private WeaponHandler _weaponHandler;

    public TextMeshProUGUI TextMeshProUGUI => _textMeshProUGUI;

    private void Awake()
    {
        _wearableItemsInventory.WeaponSlot.OnWeaponChanged += SetWeapon;
    }

    private void SetWeapon(WeaponHandler weaponHandler)
    {
        _weaponHandler = weaponHandler;
    }

    public void UpdateUI()
    {
        TextMeshProUGUI.text = string.Format($"{_weaponHandler.ClipAmmo}/{_weaponHandler.AmmoCount}");
    }

    private void OnDestroy()
    {
        _wearableItemsInventory.WeaponSlot.OnWeaponChanged -= SetWeapon;
    }
}
