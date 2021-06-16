using System;
using System.Collections;
using UnityEngine;

public class CharacterBleeding : MonoBehaviour
{
    [SerializeField] float m_timeToStopBleeding;
    [SerializeField] float m_bleedDelay;
    [SerializeField] float m_bleedDamage;

    bool m_isBleeding;
    float m_pressingDuration;
    WaitForSeconds m_bleedTimeout;
    IEnumerator m_bleedingCoroutine;

    public Action OnPlayerBleeding { get; set; }

    void Awake()
    {
        MainLinks.Instance.PlayerBleeding = this;
        m_bleedTimeout = new WaitForSeconds(m_bleedDelay);
        m_bleedingCoroutine = BleedCoroutine();
    }

    void Update()
    {
        if (!m_isBleeding) { return; }
        GetDuradurationOfPressingHealButton();

        if (m_pressingDuration >= m_timeToStopBleeding)
        {
            StopBleeding();
            m_isBleeding = false;
        }
    }

    public void Bleed()
    {
        if (m_isBleeding) { return; }
        StartCoroutine(m_bleedingCoroutine);
        MainLinks.Instance.PlayerStamina.StopRegeneration();
    }

    IEnumerator BleedCoroutine()
    {
        m_isBleeding = true;
        var playerHealthController = MainLinks.Instance.PlayerHealth;
        while (playerHealthController.Health > 0)
        {
            yield return m_bleedTimeout;
            OnPlayerBleeding?.Invoke();
            MainLinks.Instance.PlayerHealth.DamageBleeding(m_bleedDamage);
        }
        m_isBleeding = false;
    }

    void StopBleeding()
    {
        StopCoroutine(m_bleedingCoroutine);
        m_bleedingCoroutine = BleedCoroutine();
    }

    void GetDuradurationOfPressingHealButton()
    {
        if (Input.GetButton("Healing"))
        {
            m_pressingDuration += Time.deltaTime;
            return;
        }
        m_pressingDuration = 0;
    }
}