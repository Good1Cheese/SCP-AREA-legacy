using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

[RequireComponent(typeof(Volume))]
public class DegreeOfInjuary : MonoBehaviour
{
    [SerializeField] float m_slowDownFactorDuringStrongBleeding;
    [SerializeField] float m_saturationDuringWeakInjuary;
    [SerializeField] float m_saturationDuringStrongInjuary;

    ColorAdjustments m_colorAdjustments;
    float m_riskyHpAmout;

    void Awake()
    {
        Volume volume = GetComponent<Volume>();
        volume.profile.TryGet(out m_colorAdjustments);
    }

    void Start()
    {
        float m_maxPlayerHealth = MainLinks.Instance.PlayerHealth.Health;
        m_riskyHpAmout = m_maxPlayerHealth - (m_maxPlayerHealth / 4);
        MainLinks.Instance.PlayerHealth.OnPlayerGetsDamage += CheckInjuaryDegree;
    }
    void CheckInjuaryDegree()
    {
        float health = MainLinks.Instance.PlayerHealth.Health;
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
        MainLinks.Instance.PlayerSpeed.SlowDownFactor = m_slowDownFactorDuringStrongBleeding;
        m_colorAdjustments.saturation.value = m_saturationDuringStrongInjuary;
    }

    static void ForbideRun()
    {
        MainLinks.Instance.PlayerStamina.StaminaValue = 0;
        MainLinks.Instance.PlayerStamina.enabled = false;
        MainLinks.Instance.PlayerStamina.StopRegeneration();
    }

    void OnDisable()
    {
        MainLinks.Instance.PlayerHealth.OnPlayerGetsDamage -= CheckInjuaryDegree;
    }
}
