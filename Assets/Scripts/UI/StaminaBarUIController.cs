using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class StaminaBarUIController : StatisticsBarUIController
{
    protected override float GetBarValue()
    {
        return MainLinks.Instance.PlayerStamina.StaminaValue;
    }

    protected override void Subscribe()
    {
        MainLinks.Instance.PlayerStamina.OnStaminaValueChanged += UpdateUI;
    }

    protected override void Unsubscribe()
    {
        MainLinks.Instance.PlayerStamina.OnStaminaValueChanged -= UpdateUI;
    }
}

