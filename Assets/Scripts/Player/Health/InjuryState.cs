using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using Zenject;

[RequireComponent(typeof(Volume))]
public class InjuryState : MonoBehaviour
{
    [SerializeField] int m_lowHealthPercent;
    [SerializeField] float[] m_slowDownValues;
    [SerializeField] float[] m_lensDistortionValues;
    [SerializeField] float[] m_saturationValues;

    [Inject] readonly MovementSpeed m_playerSpeed;
    [Inject] readonly PlayerStamina m_playerStamina;
    [Inject] readonly PlayerHealth m_playerHealth;
    [Inject] readonly GameLoading m_gameLoading;

    ColorAdjustments m_colorAdjustments;
    LensDistortion m_lensDistortion;

    int m_currentCellIndex = - 1;

    [Inject]
    void Construct(Volume volume)
    {
        volume.profile.TryGet(out m_colorAdjustments);
        volume.profile.TryGet(out m_lensDistortion);
    }

    void Start()
    {
        m_playerHealth.OnPlayerGetsDamage += IntensifyEffects;
        m_playerHealth.OnPlayerHeals += ReduceEffects;
        m_gameLoading.OnGameLoaded += ResetEffects;
    }

    void IntensifyEffects()
    {
        m_currentCellIndex++;

        SetEffectsValues();

        if (m_playerHealth.GetCurrentHealthPercent() <= m_lowHealthPercent)
        {
            m_playerStamina.DisableRunAbility();
        }
    }

    void ReduceEffects()
    {
        m_currentCellIndex--;
        SetEffectsValues();
    }

    void SetEffectsValues()
    {
        m_colorAdjustments.saturation.value = m_saturationValues[m_currentCellIndex];
        m_lensDistortion.intensity.value = m_lensDistortionValues[m_currentCellIndex];
        m_playerSpeed.SlowDownSpeed(m_slowDownValues[m_currentCellIndex]);
    }

    void ResetEffects()
    {
        // Сделать сохранение эффектов
    }

    void OnDestroy()
    {
        m_playerHealth.OnPlayerGetsDamage -= IntensifyEffects;
        m_playerHealth.OnPlayerHeals -= ReduceEffects;
    }
}
