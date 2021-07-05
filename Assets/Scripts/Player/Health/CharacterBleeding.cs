using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class CharacterBleeding : MonoBehaviour
{
    [SerializeField] float m_delayDuringBleeding;
    [SerializeField] float m_increasingTimeForBleeding;
    [SerializeField] Sprite m_imageForBleedingCell;
    [Inject] readonly PlayerHealth m_playerHealthSystem;

    WaitForSeconds m_timeoutDuringBleeding;
    public bool IsPlayerBleeding { get; set; }

    public Action OnPlayerBleeding { get; set; }

    public void Bleed()
    {
        if (IsPlayerBleeding) { return; }

        IsPlayerBleeding = true;
        StartCoroutine(BleedCoroutine());
    }

    IEnumerator BleedCoroutine()
    {
        while (m_playerHealthSystem.GetCurrentHealthPercent() > 0)
        {
            m_timeoutDuringBleeding = new WaitForSeconds(m_delayDuringBleeding);
            m_playerHealthSystem.GetCurrentHealthCell().SetSprite(m_imageForBleedingCell);

            yield return m_timeoutDuringBleeding;

            OnPlayerBleeding.Invoke();
            m_playerHealthSystem.Damage();
            m_delayDuringBleeding -= m_increasingTimeForBleeding;
        }
        IsPlayerBleeding = false;
    }
}
