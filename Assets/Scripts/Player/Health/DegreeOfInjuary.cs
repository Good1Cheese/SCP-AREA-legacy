using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using Zenject;

[RequireComponent(typeof(Volume))]
public class DegreeOfInjuary : MonoBehaviour
{
    [SerializeField] float m_slowDownFactorDuringStrongBleeding;
    [SerializeField] float m_saturationDuringWeakInjuary;
    [SerializeField] float m_saturationDuringStrongInjuary;
    [Inject] PlayerSpeed m_playerSpeed;
    [Inject] PlayerStamina m_playerStamina;
    [Inject] PlayerHealth m_playerHealth;

    ColorAdjustments m_colorAdjustments;
    float m_riskyHpAmout;

    void Awake()
    {
        Volume volume = GetComponent<Volume>();
        volume.profile.TryGet(out m_colorAdjustments);
    }

    void Start()
    {
        float m_maxPlayerHealth = m_playerHealth.Health;
        m_riskyHpAmout = m_maxPlayerHealth - (m_maxPlayerHealth / 2);
        m_playerHealth.OnPlayerGetsDamage += CheckInjuaryDegree;
    }
    void CheckInjuaryDegree()
    {
        float health = m_playerHealth.Health;
        if (health >= m_riskyHpAmout)
        {
            OnWeekInjuary();
            return;
        }
        else if (health <= m_riskyHpAmout)
        {
            OnStrongInjuary();
        }
    }

    void OnWeekInjuary()
    {
        ForbideRun();
        m_colorAdjustments.saturation.value = m_saturationDuringWeakInjuary;
    }

    void OnStrongInjuary()
    {
        ForbideRun();
        m_colorAdjustments.saturation.value = m_saturationDuringStrongInjuary;
        m_playerSpeed.SlowDownFactor = m_slowDownFactorDuringStrongBleeding;
    }

    void ForbideRun()
    {
        m_playerStamina.StaminaValue = 0;
        m_playerStamina.enabled = false;
    }

    void OnDisable()
    {
        m_playerHealth.OnPlayerGetsDamage -= CheckInjuaryDegree;
    }
}
