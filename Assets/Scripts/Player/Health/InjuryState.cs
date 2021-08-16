using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using Zenject;

[RequireComponent(typeof(Volume))]
public class InjuryState : MonoBehaviour
{
    [SerializeField] int m_lowHealthPercent;

    [SerializeField] float m_slowDownFactorWhileInjured;
    [SerializeField] float m_slowDownFactorIncreasingPerStep;

    [SerializeField] float m_startLensDistortionIntensity;
    [SerializeField] float m_startSaturation;

    [SerializeField] float m_effectsMultiplierStartValue;
    [SerializeField] float m_effectsMultiplier;
    [SerializeField] float m_effectsMultiplierIncreasingPerStep;

    [Inject] readonly MovementSpeed m_playerSpeed;
    [Inject] readonly PlayerStamina m_playerStamina;
    [Inject] readonly PlayerHealth m_playerHealth;
    [Inject] readonly GameLoading m_gameLoading;

    ColorAdjustments m_colorAdjustments;
    LensDistortion m_lensDistortion;

    [Inject]
    void Construct(Volume volume)
    {
        volume.profile.TryGet(out m_colorAdjustments);
        volume.profile.TryGet(out m_lensDistortion);
    }

    void Start()
    {
        m_playerHealth.OnPlayerGetsDamage += ActiveteEffects;
        m_playerHealth.OnPlayerHeals += DeactivateEffects;
        m_playerHealth.OnPlayerHeals += DeactivateEffects;
        m_gameLoading.OnGameLoaded += ResetEffects;
    }

    void ActiveteEffects()
    {
        IntensifyEffects();
        m_playerSpeed.SlowDownSpeed(m_slowDownFactorWhileInjured);

        if (m_playerHealth.GetCurrentHealthPercent() <= m_lowHealthPercent)
        {
            m_playerStamina.DisableRunAbility();
        }

        m_effectsMultiplier += m_effectsMultiplierIncreasingPerStep;
        m_slowDownFactorWhileInjured += m_slowDownFactorIncreasingPerStep;
    }

    void DeactivateEffects()
    {
        m_slowDownFactorWhileInjured -= m_slowDownFactorIncreasingPerStep;
        m_effectsMultiplier -= m_effectsMultiplierIncreasingPerStep;

        ReduceEffects();
        m_playerSpeed.SlowDownSpeed(m_slowDownFactorWhileInjured);

    }

    void ResetEffects()
    {
        int healthCellsCount = m_playerHealth.HealthCells.Count - m_playerHealth.CurrentHealthCellIndex - 1;
        m_effectsMultiplier = m_effectsMultiplierStartValue + m_effectsMultiplierIncreasingPerStep * healthCellsCount;
        m_colorAdjustments.saturation.value = m_startSaturation * m_effectsMultiplier * healthCellsCount;
        m_lensDistortion.intensity.value = m_startLensDistortionIntensity * m_effectsMultiplier * healthCellsCount;
    }

    void ReduceEffects()
    {
        m_colorAdjustments.saturation.value -= m_startSaturation;
        m_lensDistortion.intensity.value -= m_startLensDistortionIntensity;
    }

    void IntensifyEffects()
    {
        m_colorAdjustments.saturation.value = m_startSaturation * m_effectsMultiplier;
        m_lensDistortion.intensity.value = m_startLensDistortionIntensity * m_effectsMultiplier;
    }

    void OnDestroy()
    {
        m_playerHealth.OnPlayerGetsDamage -= ActiveteEffects;
        m_playerHealth.OnPlayerHeals -= DeactivateEffects;
    }
}
