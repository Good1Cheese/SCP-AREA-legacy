using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using Zenject;

[RequireComponent(typeof(Volume))]
public class DegreeOfInjury : MonoBehaviour
{
    [SerializeField] float m_slowDownFactorWhileInjured;
    [SerializeField] float m_startLensDistortionIntensity;
    [SerializeField] float m_startSaturation;
    [Inject] PlayerMovementSpeed m_playerSpeed;
    [Inject] PlayerStamina m_playerStamina;
    [Inject] PlayerHealthSystem m_playerHealth;

    float m_effectMultiplier = 1;
    ColorAdjustments m_colorAdjustments;
    LensDistortion m_lensDistortion;

    void Awake()
    {
        Volume volume = GetComponent<Volume>();
        volume.profile.TryGet(out m_colorAdjustments);
        volume.profile.TryGet(out m_lensDistortion);
    }

    void Start()
    {
        m_playerHealth.OnPlayerGetsDamage += CheckInjuaryDegree;
    }

    void CheckInjuaryDegree()
    {
        SetEffects();
        m_playerSpeed.SlowDownSpeed(m_slowDownFactorWhileInjured);

        if (m_playerHealth.GetCurrentHealthPercent() <= 25)
        {
            m_playerStamina.DisableRunAbility();
        }
        m_effectMultiplier++;
    }


    void SetEffects()
    {
        m_colorAdjustments.saturation.value = m_startSaturation * m_effectMultiplier;
        m_lensDistortion.intensity.value = m_startLensDistortionIntensity * m_effectMultiplier;
    }

    void OnDisable()
    {
        m_playerHealth.OnPlayerGetsDamage -= CheckInjuaryDegree;
    }
}
