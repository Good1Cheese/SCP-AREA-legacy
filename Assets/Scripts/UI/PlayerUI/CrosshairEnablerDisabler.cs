using UnityEngine;
using Zenject;

public class CrosshairEnablerDisabler : MonoBehaviour
{
    [Inject] private readonly WeaponAim _weaponAim;
    [Inject] private readonly WeaponSlot _weaponSlot;
    [Inject] private readonly GameLoader _gameLoader;
    private GameObject _gameObject;

    private void Awake()
    {
        _gameObject = gameObject;

        _weaponAim.Aimed += Deactivate;
        _weaponAim.Unaimed += Activate;
        _weaponSlot.ItemRemoved += Activate;
        _gameLoader.UILoading += _gameObject.SetActive;
    }

    private void Activate()
    {
        _gameObject.SetActive(true);
    }

    private void Deactivate()
    {
        _gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _weaponAim.Aimed -= Deactivate;
        _weaponAim.Unaimed -= Activate;
        _weaponSlot.ItemRemoved -= Activate;
        _gameLoader.UILoading -= _gameObject.SetActive;
    }
}
