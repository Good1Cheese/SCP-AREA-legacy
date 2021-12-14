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
        _playerStamina.Changed += UpdateUI;
    }

    protected override void Unsubscribe()
    {
        _playerStamina.Changed -= UpdateUI;
    }
}