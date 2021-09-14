using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class CharacterBleeding : MonoBehaviour
{
    [SerializeField] float m_delayDuringBleeding;
    [SerializeField] float m_increasingTimeForBleeding;

    [SerializeField] Sprite m_imageForBleedingCell;
    [SerializeField] Sprite m_imageForFullCell;

    [Inject] readonly PlayerHealth m_playerHealth;

    WaitForSeconds m_timeoutDuringBleeding;
    IEnumerator m_bleedCoroutine;

    public bool IsPlayerBleeding { get; set; }
    public Action OnPlayerBleeding { get; set; }
    public Action OnPlayerBleedingStarted { get; set; }
    public Action OnPlayerBleedingEnded { get; set; }
    public float DelayDuringBleeding { get => m_delayDuringBleeding; set => m_delayDuringBleeding = value; }

    void Start()
    {
        m_bleedCoroutine = BleedCoroutine();
        CreateBleedingTimeout(DelayDuringBleeding);
        m_playerHealth.OnPlayerGetsDamage += MoveBleedingCellAway;
    }

    void MoveBleedingCellAway()
    {
        if (!IsPlayerBleeding) { return; }
        m_playerHealth.GetCurrentHealthCell().SetSprite(m_imageForBleedingCell);
    }

    public void Bleed()
    {
        if (IsPlayerBleeding) { return; }

        IsPlayerBleeding = true;
        StartCoroutine(m_bleedCoroutine);
    }

    public void StopBleeding()
    {
        IsPlayerBleeding = false;

        StopCoroutine(m_bleedCoroutine);
        m_bleedCoroutine = BleedCoroutine();
        m_playerHealth.GetCurrentHealthCell().SetSprite(m_imageForFullCell);
    }

    public void CreateBleedingTimeout(float time)
    {
        m_timeoutDuringBleeding = new WaitForSeconds(time);
    }

    IEnumerator BleedCoroutine()
    {
        OnPlayerBleedingStarted?.Invoke();

        while (m_playerHealth.GetCurrentHealthPercent() > 0)
        {
            print(m_playerHealth.GetCurrentHealthPercent());
            m_playerHealth.GetCurrentHealthCell().SetSprite(m_imageForBleedingCell);

            yield return m_timeoutDuringBleeding;

            OnPlayerBleeding?.Invoke();
            m_playerHealth.Damage();
            m_delayDuringBleeding -= m_increasingTimeForBleeding;
        }
        IsPlayerBleeding = false;
        CreateBleedingTimeout(DelayDuringBleeding);

        OnPlayerBleedingEnded?.Invoke();
    }
}
