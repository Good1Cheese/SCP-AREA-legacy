using UnityEngine;
using Zenject;

public class CrosshairEnablerDisabler : MonoBehaviour
{
    private WeaponAim _weaponAim;
    private WeaponSlot _weaponSlot;
    private GameLoader _gameLoader;
    private GameObject _gameObject;

    [Inject]
    private void Construct(WeaponAim weaponAim, WeaponSlot weaponSlot, GameLoader gameLoader)
    {
        _weaponAim = weaponAim;
        _weaponSlot = weaponSlot;
        _gameLoader = gameLoader;
    }

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