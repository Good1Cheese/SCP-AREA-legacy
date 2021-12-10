using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class StaminaDisabler : MonoBehaviour
{
    [Inject] private readonly PlayerStamina _playerStamina;

    private int _startBurnSpeedMultipliyer;
    private IEnumerator _disableCoroutine;

    public Action<float> OnDisabled { get; set; }

    private void Start()
    {
        _disableCoroutine = DisableCoroutine(0);
        _startBurnSpeedMultipliyer = _playerStamina.BurnSpeedMultipliyer;
    }

    public void Disable(float effectTime)
    {
        _disableCoroutine = DisableCoroutine(effectTime);
        StartCoroutine(_disableCoroutine);
    }

    public void StopDisabling()
    {
        StopCoroutine(_disableCoroutine);
    }

    public IEnumerator DisableCoroutine(float effectTime)
    {
        _playerStamina.StaminaTime = _playerStamina.MaxStaminaTime;
        _playerStamina.BurnSpeedMultipliyer = 0;

        OnDisabled?.Invoke(effectTime);

        yield return new WaitForSeconds(effectTime);

        _playerStamina.BurnSpeedMultipliyer = _startBurnSpeedMultipliyer;
    }
}