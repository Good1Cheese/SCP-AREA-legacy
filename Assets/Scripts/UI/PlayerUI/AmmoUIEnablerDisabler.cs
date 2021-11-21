using UnityEngine;
using Zenject;

public class AmmoUIEnablerDisabler : MonoBehaviour
{
    [SerializeField] AmmoUICountUpdater _ammoUICountUpdater;
    [Inject] private readonly WearableItemsInventory _wearableItemsInventory;

    private GameObject _textMeshProGameObject;

    private void Awake()
    {
        _wearableItemsInventory.WeaponSlot.OnWeaponDropped += DeactivateWeaponUI;
    }

    private void Start()
    {
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
        _wearableItemsInventory.WeaponSlot.OnWeaponDropped -= DeactivateWeaponUI;
    }
}