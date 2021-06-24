using UnityEngine.UI;
using UnityEngine;
using System;
using Zenject;

[RequireComponent(typeof(Slider))]
public class HealthBarUIController : StatisticsBarUIController
{
    [Inject] PlayerHealth m_playerHealth;

    protected override float GetBarValue()
    {
        return m_playerHealth.Health;
    }

    protected override void Subscribe()
    {
        m_playerHealth.OnHealthValueChanged += UpdateUI;
    }

    protected override void Unsubscribe()
    {
        m_playerHealth.OnHealthValueChanged -= UpdateUI;
    }
}

