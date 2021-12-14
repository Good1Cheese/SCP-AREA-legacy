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
        _weaponSlot.Changed += SetWeaponHandler;
        _weaponSlot.AmmoAdded += UpdateAmmoAndUI;
    }

    private void SetWeaponHandler(WeaponHandler weaponHandler)
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
        _weaponSlot.Changed -= SetWeaponHandler;
        _weaponSlot.AmmoAdded -= UpdateAmmoAndUI;
    }
}