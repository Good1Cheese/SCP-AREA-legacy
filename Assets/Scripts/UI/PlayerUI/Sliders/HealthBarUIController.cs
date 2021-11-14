using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Slider))]
public class HealthBarUIController : StatisticsBarUIController
{
    [Inject] readonly PlayerHealth m_playerHealth;
    [Inject] readonly GameLoader m_gameLoader;

    protected override float GetValue()
    {
        return m_playerHealth.Amount;
    }

    protected override void Subscribe()
    {
        m_playerHealth.OnPlayerGetsDamage += UpdateUI;
        m_playerHealth.OnPlayerHeals += UpdateUI;
        m_gameLoader.OnGameLoaded += UpdateUI;
    }

    protected override void Unsubscribe()
    {
        m_playerHealth.OnPlayerGetsDamage -= UpdateUI;
        m_playerHealth.OnPlayerHeals -= UpdateUI;
    }
}

