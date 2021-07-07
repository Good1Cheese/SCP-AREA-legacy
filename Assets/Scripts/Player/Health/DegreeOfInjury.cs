using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using Zenject;

[RequireComponent(typeof(Volume))]
public class DegreeOfInjury : MonoBehaviour
{
    [SerializeField] int m_lowHealthPercent;

    [SerializeField] float m_slowDownFactorWhileInjured;
    [SerializeField] float m_slowDownFactorIncreasingPerStep;

    [SerializeField] float m_startLensDistortionIntensity;
    [SerializeField] float m_startSaturation;

    [SerializeField] float m_effectsMultiplier;
    [SerializeField] float m_effectsMultiplierIncreasingPerStep;

    [Inject] readonly MovementSpeed m_playerSpeed;
    [Inject] readonly PlayerStamina m_playerStamina;
    [Inject] readonly PlayerHealth m_playerHealth;

    ColorAdjustments m_colorAdjustments;
    LensDistortion m_lensDistortion;

    void Awake()
    {
        Volume volume = GetComponent<Volume>();
        volume.profile.TryGet(out m_colorAdjustments);
        volume.profile.TryGet(out m_lensDistortion);
        m_playerHealth.OnPlayerGetsDamage += ActiveteEffects;
        m_playerHealth.OnPlayerHeals += DeactivateEffects;
    }

    void ActiveteEffects()
    {
        SetEffects();
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

        ResetEffects();
        m_playerSpeed.SlowDownSpeed(m_slowDownFactorWhileInjured);

    }

    void ResetEffects()
    {
        m_colorAdjustments.saturation.value -= m_startSaturation;
        m_lensDistortion.intensity.value -= m_startLensDistortionIntensity;
    }

    void SetEffects()
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
