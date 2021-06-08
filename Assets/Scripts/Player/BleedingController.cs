using System;
using System.Collections;
using UnityEngine;

public class BleedingController : MonoBehaviour
{
    [SerializeField] float m_timeToStopBleeding;
    [SerializeField] float m_bleedDelay;
    [SerializeField] float m_bleedDamage;

    bool m_isBleeding;
    float m_pressingDuration;
    WaitForSeconds m_bleedTimeout;

    void Start()
    {
        m_bleedTimeout = new WaitForSeconds(m_bleedDelay);
    }

    void Update()
    {
        if (!m_isBleeding) { return; }
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
        var playerHealthController = MainLinks.Instance.PlayerHealthController;
        while (playerHealthController.Health > 0)
        {
            MainLinks.Instance.PlayerHealthController.Damage(m_bleedDamage);
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