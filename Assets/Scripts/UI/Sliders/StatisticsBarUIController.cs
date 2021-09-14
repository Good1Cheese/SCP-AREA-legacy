using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class StatisticsBarUIController : MonoBehaviour
{
    Slider m_slider;

    void Start()
    {
        m_slider = GetComponent<Slider>();
        float startBarValue = GetValue();
        m_slider.maxValue = startBarValue;
        m_slider.value = startBarValue;

        Subscribe();
    }

    protected void UpdateUI()
    {
        m_slider.value = GetValue();
    }

    protected abstract float GetValue();
    protected abstract void Subscribe();
    protected abstract void Unsubscribe();

    void OnDestroy()
    {
        Unsubscribe();
    }

}