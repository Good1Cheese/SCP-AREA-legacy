using System;
using UnityEngine;
using Zenject;

public class AdrenalinEffectSaving : DataSaving
{
    [Inject] readonly StaminaUseDisabler m_staminaUseDisabler;

    public float effectTime;
    bool m_isEffectGoing;

    void Start()
    {
        m_staminaUseDisabler.OnUseDisabled += GetEffectTime;
    }

    void GetEffectTime(float effectTime)
    {
        m_isEffectGoing = true;
        this.effectTime = effectTime;
    }

    void Update()
    {
        if (m_isEffectGoing)
        {
            if (effectTime <= 0)
            {
                m_isEffectGoing = false;
                return;
            }

            effectTime -= Time.deltaTime;
        }
    }

    public override void Save()
    {
        m_isEffectGoing = false;
    }

    public override void LoadData()
    {
        if (effectTime > 0)
        {
            m_staminaUseDisabler.Disable(effectTime);
            return;
        }
        m_staminaUseDisabler.StopDisabling();
        
    }

    void OnDestroy()
    {
        m_staminaUseDisabler.OnUseDisabled -= GetEffectTime;
    }

}
