using TMPro;
using UnityEngine;

public class AmmoUICountUpdater : WeaponUser
{
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    public TextMeshProUGUI TextMeshProUGUI => _textMeshProUGUI;

    public void UpdateUI()
    {
        TextMeshProUGUI.text = string.Format($"{_weaponHandler.CurrentClipAmmo}/{_weaponHandler.Weapon_SO.clipMaxAmmo}");
    }
}