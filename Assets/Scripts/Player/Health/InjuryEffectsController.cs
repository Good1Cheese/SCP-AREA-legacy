using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using Zenject;

[RequireComponent(typeof(Volume))]
public class InjuryEffectsController : MonoBehaviour
{
    [SerializeField] int m_lowHealthPercent;

    [SerializeField] float[] m_slowDownValues;
    [SerializeField] float[] m_lensDistortionValues;
    [SerializeField] float[] m_saturationValues;

    [Inject] readonly MovementSpeed m_playerSpeed;
    [Inject] readonly PlayerHealth m_playerHealth;

    ColorAdjustments m_colorAdjustments;
    LensDistortion m_lensDistortion;

    int m_currentCellIndex;

    [Inject]
    void Construct(Volume volume)
    {
        volume.profile.TryGet(out m_colorAdjustments);
        volume.profile.TryGet(out m_lensDistortion);
    }

    void Start()
    {
        m_playerHealth.OnPlayerGetsDamage += ActivateEffects;
        m_playerHealth.OnPlayerHeals += ReduceEffects;
        m_playerHealth.OnPlayerDies += OnDestroy;
    }

    public void ActivateEffects()
    {
        m_currentCellIndex = m_playerHealth.HealthCells.GetCurrentCellIndex();
        SetEffectsValues();
    }

    void ReduceEffects()
    {
        m_currentCellIndex = m_playerHealth.HealthCells.GetCurrentCellIndex();

        if (m_playerHealth.HealthCells.IsCurrentCellLast())
        {
            DisableEffects();
            return;
        }

        SetEffectsValues();
    }

    void SetEffectsValues()
    {
        m_colorAdjustments.saturation.value = m_saturationValues[m_currentCellIndex];
        m_lensDistortion.intensity.value = m_lensDistortionValues[m_currentCellIndex];
        m_playerSpeed.SlowDownSpeed(m_slowDownValues[m_currentCellIndex]);
    }

    void DisableEffects()
    {
        m_colorAdjustments.saturation.value = 0;
        m_lensDistortion.intensity.value = 0;
        m_playerSpeed.SlowDownSpeed(0);
    }

    void OnDestroy()
    {
        m_playerHealth.OnPlayerGetsDamage -= ActivateEffects;
        m_playerHealth.OnPlayerHeals -= ReduceEffects;
        m_playerHealth.OnPlayerDies -= OnDestroy;
    }
}
