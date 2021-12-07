using TMPro;
using UnityEngine;
using Zenject;

public class AmmoUICountUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    [Inject] private readonly WeaponSlot _weaponSlot;

    private WeaponHandler _weaponHandler;

    public TextMeshProUGUI TextMeshProUGUI => _textMeshProUGUI;

    private void Awake()
    {
        _weaponSlot.OnWeaponChanged += SetWeapon;
        _weaponSlot.OnAmmoAdded += UpdateAmmoAndUI;
    }

    private void SetWeapon(WeaponHandler weaponHandler)
    {
        _weaponHandler = weaponHandler;
    }

    private void UpdateAmmoAndUI()
    {
        _weaponHandler.UpdateAmmo();
        UpdateUI();
    }

    public void UpdateUI()
    {
        TextMeshProUGUI.text = string.Format($"{_weaponHandler.ClipAmmo}/{_weaponHandler.Ammo}");
    }

    private void OnDestroy()
    {
        _weaponSlot.OnWeaponChanged -= SetWeapon;
        _weaponSlot.OnAmmoAdded -= UpdateAmmoAndUI;
    }
}