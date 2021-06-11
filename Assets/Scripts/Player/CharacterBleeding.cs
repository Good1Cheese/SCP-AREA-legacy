using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(DegreeOfInjuary))]
public class CharacterBleeding : MonoBehaviour
{
    [SerializeField] float m_timeToStopBleeding;
    [SerializeField] float m_bleedDelay;
    [SerializeField] float m_bleedDamage;

    bool m_isBleeding;
    float m_pressingDuration;
    WaitForSeconds m_bleedTimeout;

    public Action OnPlayerBleeding { get; set; }

    void Start()
    {
        MainLinks.Instance.PlayerBleeding = this;
        m_bleedTimeout = new WaitForSeconds(m_bleedDelay);
    }

    void Update()
    {
        if (!m_isBleeding) { return; }
        OnPlayerBleeding?.Invoke();
        GetDuradurationOfPressingHealButton();

        if (m_pressingDuration >= m_timeToStopBleeding)
        {
            print("Bleeding Stopped");
            StopAllCoroutines();
            m_isBleeding = false;
        }
    }

    public void Bleed() => StartCoroutine(BleedCoroutine());

    IEnumerator BleedCoroutine()
    {
        m_isBleeding = true;
        var playerHealthController = MainLinks.Instance.PlayerHealth;
        while (playerHealthController.Health > 0)
        {
            MainLinks.Instance.PlayerHealth.OnPlayerGetsDamage?.Invoke();
            MainLinks.Instance.PlayerHealth.Damage(m_bleedDamage);
            yield return m_bleedTimeout;
        }
        m_isBleeding = false;
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