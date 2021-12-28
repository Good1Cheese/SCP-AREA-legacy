using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HealthBarUIController : StatisticsBarUIController
{
    [Inject] private readonly PlayerHealth _playerHealth;

    protected override float GetValue()
    {
        return _playerHealth.Amount;
    }

    protected override void Subscribe()
    {
        _playerHealth.Damaged += UpdateUI;
        _playerHealth.Healed += UpdateUI;
        _gameLoader.Loaded += UpdateUI;
    }

    protected override void Unsubscribe()
    {
        _playerHealth.Damaged -= UpdateUI;
        _playerHealth.Healed -= UpdateUI;
        _gameLoader.Loaded -= UpdateUI;
    }
}