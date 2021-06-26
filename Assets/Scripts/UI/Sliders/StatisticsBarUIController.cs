using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class StatisticsBarUIController : MonoBehaviour
{
    Slider m_slider;

    protected abstract float GetBarValue();
    protected abstract void Subscribe();
    protected abstract void Unsubscribe();

    void Start()
    {
        m_slider = GetComponent<Slider>();
        float startBarValue = GetBarValue();
        m_slider.maxValue = startBarValue;
        m_slider.value = startBarValue;

        Subscribe();
    }

    protected void UpdateUI()
    {
        m_slider.value = GetBarValue();
    }

    void OnDestroy()
    {
        Unsubscribe();
    }
}