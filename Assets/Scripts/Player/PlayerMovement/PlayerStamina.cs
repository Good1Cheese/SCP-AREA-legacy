using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerStamina : MonoBehaviour
{
  //  [Inject] CharacterBleeding m_playerBleeding;

    [SerializeField] float m_regenerationSpeed;
    [SerializeField] float m_spendingSpeed;
    [SerializeField] float m_delayDuringRegeneration;
    [SerializeField] float m_delayBeforeRegenerationStart;
    [SerializeField] float m_staminaValue;
    [Inject] readonly MovementSpeed m_playerSpeed;
    [Inject] readonly PlayerMovement m_playerMovement;
    [Inject] readonly PlayerHealth m_playerHealth;

    WaitForSeconds m_timeoutBeforeRegeneration;
    WaitForSeconds m_timeoutDuringRegeneration;
    IEnumerator m_regenerationCoroutine;

    bool hadPlayerStamina;
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
    public bool HasPlayerStamina
    {
        get 
        {
            bool hasPlayerStamina = StaminaValue > 0;
            if (!hasPlayerStamina && hadPlayerStamina)
            {
                OnStaminaRanOut.Invoke();
            }
            hadPlayerStamina = hasPlayerStamina;
            return hasPlayerStamina;
        }
    }

    public float MaxStaminaAmount { get; set; }
    public Action OnStaminaRanOut { get; set; }

    void Awake()
    {
        m_regenerationCoroutine = RegenerateStaminaCoroutine();
        m_timeoutBeforeRegeneration = new WaitForSeconds(m_delayBeforeRegenerationStart);
        m_timeoutDuringRegeneration = new WaitForSeconds(m_delayDuringRegeneration); 
        MaxStaminaAmount = m_staminaValue;
    }

    void Start()
    {
        m_playerSpeed.OnPlayerRun += BurnStamina;
        m_playerSpeed.OnPlayerStoppedRun += RegenerateStamina;
        m_playerMovement.OnPlayerStoppedMoving += GetRegenerateStamina;
        m_playerHealth.OnPlayerHeals += RegenerateStamina;
        m_playerHealth.OnPlayerGetsDamage += StopRegeneration;
    }

    void GetRegenerateStamina()
    {
        if (StaminaValue == MaxStaminaAmount) { return; }
        RegenerateStamina();
    }

    void BurnStamina()
    {
        StaminaValue -= m_spendingSpeed;
        StopRegeneration();
    }

    public void RegenerateStamina()
    {
        StopRegeneration();
        StartCoroutine(m_regenerationCoroutine);
    }

    IEnumerator RegenerateStaminaCoroutine()
    {
        print("Started Regeneration");
        yield return m_timeoutBeforeRegeneration;
        print("Delay out");

        while (m_staminaValue < MaxStaminaAmount)
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

    public void DisableRunAbility()
    {
        StaminaValue = 0;
        enabled = false;
    }

    void OnDestroy()
    {
        m_playerSpeed.OnPlayerRun -= BurnStamina;
        m_playerSpeed.OnPlayerStoppedRun -= RegenerateStamina;
        m_playerMovement.OnPlayerStoppedMoving -= GetRegenerateStamina;
        m_playerHealth.OnPlayerHeals -= RegenerateStamina;
        m_playerHealth.OnPlayerGetsDamage -= StopRegeneration;
    }
}


