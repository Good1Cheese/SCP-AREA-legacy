using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class CharacterBleeding : MonoBehaviour
{
    [SerializeField] float[] m_bleedTimeValues;

    [SerializeField] float m_delayDuringBleeding;
    [SerializeField] float m_increasingTimeForBleeding;

    [SerializeField] Sprite m_imageForBleedingCell;
    [SerializeField] Sprite m_imageForFullCell;

    [Inject] readonly PlayerHealth m_playerHealth;

    WaitForSeconds m_timeoutDuringBleeding;
    IEnumerator m_bleedCoroutine;

    public bool IsBleeding { get; set; }
    public Action OnPlayerBleeding { get; set; }
    public Action OnPlayerBleedingStarted { get; set; }
    public Action OnPlayerBleedingStopped { get; set; }
    public Action OnPlayerBleedingEnded { get; set; }
    public float DelayDuringBleeding { get => m_delayDuringBleeding; }

    void Start()
    {
        m_bleedCoroutine = BleedCoroutine();
        CreateBleedingTimeout(m_bleedTimeValues[m_playerHealth.HealthCells.Cells.Count - 1]);
        m_playerHealth.OnPlayerGetsDamage += MoveBleedingCellAway;
    }

    void MoveBleedingCellAway()
    {
        if (!IsBleeding) { return; }

        HealthCell healthCell = m_playerHealth.HealthCells.GetFirstFilledCell();

        if (healthCell == null) { return; }

        healthCell.SetSprite(m_imageForBleedingCell);
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
        OnPlayerBleedingStopped?.Invoke();
    }

    public void StopBleedingWithoutNotify()
    {
        StopCoroutine(m_bleedCoroutine);
        m_bleedCoroutine = BleedCoroutine();

        for (int i = 0; i < m_playerHealth.HealthCells.Cells.Count; i++)
        {
            m_playerHealth.HealthCells[i].SetSprite(m_imageForFullCell);
        }

        IsBleeding = false;
    }

    public void CreateBleedingTimeout(float time)
    {
        m_timeoutDuringBleeding = new WaitForSeconds(time);
    }

    IEnumerator BleedCoroutine()
    {
        OnPlayerBleedingStarted?.Invoke();

        HealthCell currentCell = m_playerHealth.HealthCells.GetFirstFilledCell();
        currentCell.SetSprite(m_imageForBleedingCell);


        while (m_playerHealth.HealthCells.GetFirstFilledCell() != null)
        {
            yield return m_timeoutDuringBleeding;

            m_playerHealth.Damage();
            OnPlayerBleeding?.Invoke();

            if (m_playerHealth.HealthCells.GetFirstFilledCell() == null) { yield break; }
            CreateBleedingTimeout(m_bleedTimeValues[m_playerHealth.HealthCells.GetCurrentCellIndex()]);
        }
        IsBleeding = false;

        OnPlayerBleedingEnded?.Invoke();
    }

    void OnDestroy()
    {
        m_playerHealth.OnPlayerGetsDamage -= MoveBleedingCellAway;
    }
}
