using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Slider))]
public class HealthBarUIController : StatisticsBarUIController
{
    [Inject] private readonly PlayerHealth _playerHealth;
    [Inject] private readonly GameLoader _gameLoader;

    protected override float GetValue()
    {
        return _playerHealth.Amount;
    }

    protected override void Subscribe()
    {
        _playerHealth.OnPlayerGetsDamage += UpdateUI;
        _playerHealth.OnPlayerHeals += UpdateUI;
        _gameLoader.OnGameLoaded += UpdateUI;
    }

    protected override void Unsubscribe()
    {
        _playerHealth.OnPlayerGetsDamage -= UpdateUI;
        _playerHealth.OnPlayerHeals -= UpdateUI;
        _gameLoader.OnGameLoaded -= UpdateUI;
    }
}

