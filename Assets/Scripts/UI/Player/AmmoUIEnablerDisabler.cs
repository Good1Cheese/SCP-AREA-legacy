using UnityEngine;
using Zenject;

public class AmmoUIEnablerDisabler : MonoBehaviour
{
    [SerializeField] private AmmoUICountUpdater _ammoUICountUpdater;

    private WeaponSlot _weaponSlot;
    private GameObject _textMeshProGameObject;

    [Inject]
    private void Construct(WeaponSlot weaponSlot)
    {
        _weaponSlot = weaponSlot;
    }

    private void Start()
    {
        _weaponSlot.ItemRemoved += DeactivateWeaponUI;
        _weaponSlot.ActionStarted += DeactivateWeaponUI;

        if (_ammoUICountUpdater == null)
        {
            Debug.LogError("AmmoUICountUpdater field ist't serialized");
        }

        _textMeshProGameObject = _ammoUICountUpdater.TextMeshProUGUI.gameObject;
        DeactivateWeaponUI();
    }

    private void DeactivateWeaponUI()
    {
        _textMeshProGameObject.SetActive(false);
    }

    public void ActiveOrDisableUI(bool activeState)
    {
        _ammoUICountUpdater.UpdateUI();
        _textMeshProGameObject.SetActive(activeState);
    }

    private void OnDestroy()
    {
        _weaponSlot.ItemRemoved -= DeactivateWeaponUI;
        _weaponSlot.ActionStarted -= DeactivateWeaponUI;
    }
}