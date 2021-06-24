using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Slider))]
public class StaminaBarUIController : StatisticsBarUIController
{
    [Inject] PlayerStamina m_playerStamina;

    protected override float GetBarValue()
    {
        return m_playerStamina.StaminaValue;
    }

    protected override void Subscribe()
    {
        m_playerStamina.OnStaminaValueChanged += UpdateUI;
    }

    protected override void Unsubscribe()
    {
        m_playerStamina.OnStaminaValueChanged -= UpdateUI;
    }
}

