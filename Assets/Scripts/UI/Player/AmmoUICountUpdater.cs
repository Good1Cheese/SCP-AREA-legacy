using TMPro;
using UnityEngine;
using Zenject;

public class AmmoUICountUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    private WeaponSlot _weaponSlot;
    private WeaponHandler _weaponHandler;
    private bool _wasAmmoUpdated;

    public TextMeshProUGUI TextMeshProUGUI => _textMeshProUGUI;

    [Inject]
    private void Construct(WeaponSlot weaponSlot)
    {
        _weaponSlot = weaponSlot;
    }

    private void Awake()
    {
        _weaponSlot.Changed += SetWeaponHandler;
        _weaponSlot.AmmoAdded += UpdateAmmoAndUI;
    }

    private void SetWeaponHandler(WeaponHandler weaponHandler)
    {
        _weaponHandler = weaponHandler;

        if (_wasAmmoUpdated) { return; }

        _wasAmmoUpdated = true;
        UpdateAmmoAndUI();
    }

    private void UpdateAmmoAndUI()
    {
        if (_weaponHandler == null)
        {
            _wasAmmoUpdated = false;
            return;
        }

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