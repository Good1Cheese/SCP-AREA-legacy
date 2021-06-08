using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    [SerializeField] float m_regenerationSpeed;
    [SerializeField] float m_spendingSpeed;
    [SerializeField] float m_delayBeforeRegenerationStart;
    [SerializeField] float m_maxStaminaAmount;

    WaitForSeconds m_timeOutBeforeRegeneration;
    WaitForSeconds m_timeOutDuringRegeneration;

    public float StaminaValue { get; set; }
    public bool IsPlayerRunning { get; set; }
    public bool HasPlayer≈noughStaminaToRun { get { return StaminaValue != 0; } }

    void Awake()
    {
        StaminaValue = m_maxStaminaAmount;
        MainLinks.Instance.OnPlayerRun += BurnStamina;
        MainLinks.Instance.OnPlayerStoppedRun += RegenerateStamina;
        MainLinks.Instance.PlayerStamina = this;
    }

    void Start()
    {
        m_timeOutBeforeRegeneration = new WaitForSeconds(m_delayBeforeRegenerationStart);
        m_timeOutDuringRegeneration = new WaitForSeconds(0.05f);
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
        yield return m_timeOutBeforeRegeneration;

        while (StaminaValue < m_maxStaminaAmount && !IsPlayerRunning)
        {
            StaminaValue += m_regenerationSpeed;
            yield return m_timeOutDuringRegeneration;
        }
    }

    void OnDisable()
    {
        MainLinks.Instance.OnPlayerRun -= BurnStamina;
        MainLinks.Instance.OnPlayerStoppedRun -= RegenerateStamina;
    }
}


