using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class StaminaDisabler : MonoBehaviour
{
    private PlayerStamina _playerStamina;
    private int _startBurnSpeedMultipliyer;
    private IEnumerator _disableCoroutine;

    public Action<float> Disabled { get; set; }

    [Inject]
    private void Construct(PlayerStamina playerStamina)
    {
        _playerStamina = playerStamina;
    }

    private void Awake()
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
        _playerStamina.CurveTime = _playerStamina.MaxCurveTime;
        _playerStamina.BurnSpeedMultipliyer = 0;

        Disabled?.Invoke(effectTime);

        yield return new WaitForSeconds(effectTime);

        _playerStamina.BurnSpeedMultipliyer = _startBurnSpeedMultipliyer;
    }
}