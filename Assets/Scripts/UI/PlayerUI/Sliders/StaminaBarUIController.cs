using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Slider))]
public class StaminaBarUIController : StatisticsBarUIController
{
    [Inject] private readonly PlayerStamina _playerStamina;

    protected override float GetValue()
    {
        return _playerStamina.Stamina;
    }

    protected override void Subscribe()
    {
        _playerStamina.OnStaminaValueChanged += UpdateUI;
    }

    protected override void Unsubscribe()
    {
        _playerStamina.OnStaminaValueChanged -= UpdateUI;
    }
}

