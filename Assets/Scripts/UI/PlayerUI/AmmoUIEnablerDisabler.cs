using UnityEngine;
using Zenject;

[RequireComponent(typeof(AmmoUICountUpdater))]
public class AmmoUIEnablerDisabler : MonoBehaviour
{
    [Inject] private readonly WearableItemsInventory _wearableItemsInventory;
    private AmmoUICountUpdater _ammoUICountUpdater;
    private GameObject _textMeshProGameObject;

    private void Awake()
    {
        _wearableItemsInventory.WeaponSlot.OnWeaponDropped += DeactivateWeaponUI;
        _wearableItemsInventory.WeaponSlot.IsWeaponActived += ActiveOrDisableUI;
    }

    private void Start()
    {
        _ammoUICountUpdater = GetComponent<AmmoUICountUpdater>();
        _textMeshProGameObject = _ammoUICountUpdater.TextMeshProUGUI.gameObject;
        DeactivateWeaponUI();
    }

    private void DeactivateWeaponUI()
    {
        _textMeshProGameObject.SetActive(false);
    }

    private void ActiveOrDisableUI(bool activeState)
    {
        _ammoUICountUpdater.UpdateUI();
        _textMeshProGameObject.SetActive(activeState);
    }

    private void OnDestroy()
    {
        _wearableItemsInventory.WeaponSlot.OnWeaponDropped -= DeactivateWeaponUI;
        _wearableItemsInventory.WeaponSlot.IsWeaponActived -= ActiveOrDisableUI;
    }
}