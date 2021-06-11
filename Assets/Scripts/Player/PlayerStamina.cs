using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField] float m_regenerationSpeed;
    [SerializeField] float m_spendingSpeed;
    [SerializeField] float m_delayDuringRegeneration;
    [SerializeField] float m_delayBeforeRegenerationStart;
    [SerializeField] float m_staminaValue;

    WaitForSeconds m_timeoutBeforeRegeneration;
    WaitForSeconds m_timeoutDuringRegeneration;

    float m_maxStaminaAmount;
    public float StaminaValue
    {
        get => m_staminaValue;
        set
        {
            m_staminaValue = value;
            OnStaminaValueChanged?.Invoke();
        }
    }

    public Action OnStaminaValueChanged { get; set; }
    public bool IsPlayerRunning { get; set; }
    public bool HasPlayer≈noughStaminaToRun { get { return StaminaValue != 0; } }

    void Awake()
    {
        m_maxStaminaAmount = m_staminaValue;
        MainLinks.Instance.PlayerStamina = this;
        MainLinks.Instance.PlayerSpeed.OnPlayerRun += BurnStamina;
        MainLinks.Instance.PlayerSpeed.OnPlayerStoppedRun += RegenerateStamina;
    }

    void Start()
    {
        m_timeoutBeforeRegeneration = new WaitForSeconds(m_delayBeforeRegenerationStart);
        m_timeoutDuringRegeneration = new WaitForSeconds(m_delayDuringRegeneration);
    }

    void RegenerateStamina()
    {
        IsPlayerRunning = false;
        StartCoroutine(RegenerateStaminaCoroutine());
    }

    public void BurnStamina()
    {
        IsPlayerRunning = true;
        StaminaValue -= m_spendingSpeed;
    }

    IEnumerator RegenerateStaminaCoroutine()
    {
        yield return m_timeoutBeforeRegeneration;

        while (m_staminaValue < m_maxStaminaAmount && !IsPlayerRunning)
        {
            StaminaValue += m_regenerationSpeed;
            yield return m_timeoutDuringRegeneration;
        }
    }

    public void StopRegeneration()
    {
        StopAllCoroutines();
    }

    void OnDisable()
    {
        MainLinks.Instance.PlayerSpeed.OnPlayerRun -= BurnStamina;
        MainLinks.Instance.PlayerSpeed.OnPlayerStoppedRun -= RegenerateStamina;
    }
}


