using UnityEngine.UI;
using UnityEngine;
using System;

[RequireComponent(typeof(Slider))]
public class HealthBarUIController : StatisticsBarUIController
{
    protected override float GetBarValue()
    {
        return MainLinks.Instance.PlayerHealth.Health;
    }

    protected override void Subscribe()
    {
        MainLinks.Instance.PlayerHealth.OnHealthValueChanged += UpdateUI;
    }

    protected override void Unsubscribe()
    {
        MainLinks.Instance.PlayerHealth.OnHealthValueChanged -= UpdateUI;
    }
}

