using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class StaminaUseDisabler : MonoBehaviour
{
    [Inject] private readonly PlayerStamina _playerStamina;
    private float _startSpendingValue;
    private IEnumerator _disableCoroutine;

    public Action<float> OnUseDisabled { get; set; }

    private void Start()
    {
        //_startSpendingValue = _playerStamina.SpendingSpeed;
        _disableCoroutine = DisableCoroutine(0);
    }

    public void Disable(float effectTime)
    {
        _disableCoroutine = DisableCoroutine(effectTime);
        StartCoroutine(_disableCoroutine);
    }

    public void StopDisabling()
    {
        StopCoroutine(_disableCoroutine);
        //_playerStamina.SpendingSpeed = _startSpendingValue;
    }

    public IEnumerator DisableCoroutine(float effectTime)
    {
        //OnUseDisabled?.Invoke(effectTime);
        //_playerStamina.SpendingSpeed = 0;
        //_playerStamina.StaminaValue = _playerStamina.MaxStaminaAmount;

        yield return new WaitForSeconds(effectTime);

        //_playerStamina.SpendingSpeed = _startSpendingValue;
    }
}
