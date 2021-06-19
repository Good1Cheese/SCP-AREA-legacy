using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerStamina : MonoBehaviour
{
    [Inject] PlayerSpeed m_playerSpeed;
    [Inject] CharacterBleeding m_playerBleeding;

    [SerializeField] float m_regenerationSpeed;
    [SerializeField] float m_spendingSpeed;
    [SerializeField] float m_delayDuringRegeneration;
    [SerializeField] float m_delayBeforeRegenerationStart;
    [SerializeField] float m_staminaValue;

    WaitForSeconds m_timeoutBeforeRegeneration;
    WaitForSeconds m_timeoutDuringRegeneration;
    IEnumerator m_regenerationCoroutine;

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

    void Awake()
    {
        m_regenerationCoroutine = RegenerateStaminaCoroutine();
        m_timeoutBeforeRegeneration = new WaitForSeconds(m_delayBeforeRegenerationStart);
        m_timeoutDuringRegeneration = new WaitForSeconds(m_delayDuringRegeneration);
    }

    void Start()
    {
        m_maxStaminaAmount = m_staminaValue;
        m_playerSpeed.OnPlayerRun += BurnStamina;
        m_playerSpeed.OnPlayerStoppedRun += RegenerateStamina;
        m_playerBleeding.OnPlayerStartBleeding += StopRegeneration;
    }

    void RegenerateStamina()
    {
        StartCoroutine(m_regenerationCoroutine);
    }

    void BurnStamina()
    {
        StaminaValue -= m_spendingSpeed;
        StopRegeneration();
    }

    IEnumerator RegenerateStaminaCoroutine()
    {
        yield return m_timeoutBeforeRegeneration;

        while (m_staminaValue < m_maxStaminaAmount)
        {
            StaminaValue += m_regenerationSpeed;
            yield return m_timeoutDuringRegeneration;
        }
    }

    public void StopRegeneration()
    {
        StopCoroutine(m_regenerationCoroutine);
        m_regenerationCoroutine = RegenerateStaminaCoroutine();
    }

    void OnDisable()
    {
        m_playerSpeed.OnPlayerRun -= BurnStamina;
        m_playerSpeed.OnPlayerStoppedRun -= RegenerateStamina;
    }
}


