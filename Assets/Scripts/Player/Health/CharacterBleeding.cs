using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class CharacterBleeding : MonoBehaviour
{
    [SerializeField] float m_timeToStopBleeding;
    [SerializeField] float m_bleedDelay;
    [SerializeField] float m_bleedDamage;
    [Inject] EventManager m_eventManager;
    [Inject] PlayerHealth m_playerHealth;

    bool m_isBleeding;
    float m_pressingDuration;
    WaitForSeconds m_bleedTimeout;
    IEnumerator m_bleedingCoroutine;
    public Action OnPlayerStartBleeding { get; set; }
    public Action OnPlayerBleeding { get; set; }

    void Awake()
    {
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
        OnPlayerStartBleeding.Invoke();
    }

    IEnumerator BleedCoroutine()
    {
        m_isBleeding = true;
        while (m_playerHealth.Health > 0)
        {
            yield return m_bleedTimeout;
            OnPlayerBleeding.Invoke();
            m_playerHealth.DamageBleeding(m_bleedDamage);
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