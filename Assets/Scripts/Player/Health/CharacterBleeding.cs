using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class CharacterBleeding : MonoBehaviour
{
    [SerializeField] int m_damage;
    [SerializeField] float m_maxDelay;
    [SerializeField] float m_minDelay;

    [Inject] readonly PlayerHealth m_playerHealth;

    WaitForSeconds m_timeoutDuringBleeding;
    IEnumerator m_bleedCoroutine;

    public bool IsBleeding { get; set; }
    public Action OnPlayerBleeding { get; set; }

    void Start()
    {
        m_bleedCoroutine = BleedCoroutine();
        SetBleedingDelay();
    }

    public void Bleed()
    {
        if (IsBleeding) { return; }

        IsBleeding = true;
        StartCoroutine(m_bleedCoroutine);
    }

    public void StopBleeding()
    {
        StopBleedingWithoutNotify();
    }

    public void StopBleedingWithoutNotify()
    {
        StopCoroutine(m_bleedCoroutine);
        m_bleedCoroutine = BleedCoroutine();

        IsBleeding = false;
    }

    public void SetBleedingDelay()
    {
        float delay = UnityEngine.Random.Range(m_minDelay, m_maxDelay);
        m_timeoutDuringBleeding = new WaitForSeconds(delay);
    }

    IEnumerator BleedCoroutine()
    {
        while (m_playerHealth.Amount > 0)
        {
            yield return m_timeoutDuringBleeding;

            m_playerHealth.DamageWithOutNotify(m_damage);
            OnPlayerBleeding?.Invoke();
            SetBleedingDelay();
        }

        IsBleeding = false;
    }
}
