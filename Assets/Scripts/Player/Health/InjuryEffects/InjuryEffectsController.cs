using System;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

public class InjuryEffectsController : MonoBehaviour
{
    protected const float MAX_EFFECT_CURVE_TIME = 1.6f;

    [Inject] protected readonly PlayerHealth m_playerHealth;

    float m_curveTargetTime;
    float m_curveCurrentTime;
    sbyte m_curveTimeMultiplayer;
    Func<bool> m_timeChangeCondition;

    public Action<float> OnEffectTimeChanging { get; set; }

    void Start()
    {
        SetCurveTimeDataAfterDamage();

        m_playerHealth.OnPlayerGetsDamage += SetCurveTimeDataAfterDamage;
        m_playerHealth.OnPlayerHeals += SetCurveTimeDataAfterBleedDamage;
        m_playerHealth.OnPlayerDies += OnDestroy;
    }

    void Update()
    {
        if (m_timeChangeCondition.Invoke())
        {
            m_curveCurrentTime += Time.deltaTime * m_curveTimeMultiplayer;
            OnEffectTimeChanging?.Invoke(m_curveCurrentTime);
        }
    }

    public void SetCurveTimeDataAfterDamage() => SetCurveTimeData(() => m_curveCurrentTime < m_curveTargetTime, 1);

    public void SetCurveTimeDataAfterBleedDamage() => SetCurveTimeData(() => m_curveCurrentTime > m_curveTargetTime, -1);

    void SetCurveTimeData(Func<bool> func, sbyte value)
    {
        m_curveTargetTime = GetEffectTargetTime();
        m_timeChangeCondition = func;
        m_curveTimeMultiplayer = value;
    }

    float GetEffectTargetTime()
    {
        return MAX_EFFECT_CURVE_TIME * (m_playerHealth.MaxAmount - m_playerHealth.Amount) / 100;
    }

    void OnDestroy()
    {
        m_playerHealth.OnPlayerGetsDamage -= SetCurveTimeDataAfterDamage;
        m_playerHealth.OnPlayerHeals -= SetCurveTimeDataAfterBleedDamage;
        m_playerHealth.OnPlayerDies -= OnDestroy;
    }
}