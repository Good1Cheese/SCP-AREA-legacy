using TMPro;
using UnityEngine;
using Zenject;

public class AmmoUICountUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    private WeaponSlot _weaponSlot;
    private WeaponHandler _weaponHandler;
    private WeaponReload _weaponReload;

    public TextMeshProUGUI TextMeshProUGUI => _textMeshProUGUI;

    [Inject]
    private void Construct(WeaponSlot weaponSlot, WeaponReload weaponReload)
    {
        _weaponSlot = weaponSlot;
        _weaponReload = weaponReload;
    }

    private void Awake()
    {
        _weaponSlot.Changed += SetWeaponHandler;
    }

    private void SetWeaponHandler(WeaponHandler weaponHandler)
    {
        _weaponHandler = weaponHandler;
    }

    public void UpdateUI()
    {
        TextMeshProUGUI.text = string.Format($"{_weaponReload.CurrentClipAmmo}/{_weaponHandler.Weapon_SO.clipMaxAmmo}");
    }

    private void OnDestroy()
    {
        _weaponSlot.Changed -= SetWeaponHandler;
    }
}