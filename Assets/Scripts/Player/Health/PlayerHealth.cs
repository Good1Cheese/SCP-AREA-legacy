using System;
using System.Collections.Generic;
using Zenject;
using UnityEngine;

[RequireComponent(typeof(CharacterBleeding))]
public class PlayerHealth : MonoBehaviour
{
    [Inject] readonly CharacterBleeding m_characterBleeding;

    public const int LastCellIndex = 0;

    public int CurrentCellIndex { get; set; }
    public int HealableCellIndex { get; private set; }

    public List<HealthCell> Cells { get; set; } = new List<HealthCell>();

    public Action OnPlayerDie { get; set; }
    public Action OnPlayerGetsDamage { get; set; }
    public Action OnPlayerHeals { get; set; }

    void Start()
    {
        CurrentCellIndex = Cells.Count - 1;
        HealableCellIndex = CurrentCellIndex;
    }

    public void Damage()
    {
        GetCurrentHealthCell().MakeCellEmpty();
        CurrentCellIndex--;

        OnPlayerGetsDamage?.Invoke();
        if (CurrentCellIndex < LastCellIndex)
        {
            Die();
        }
    }

    public void Heal()
    {
        if (CurrentCellIndex == Cells.Count - 1
            || m_characterBleeding.IsPlayerBleeding) { return; }

        CurrentCellIndex++;
        GetCurrentHealthCell().MakeCellFull();
        OnPlayerHeals?.Invoke();
    }

    public HealthCell GetCurrentHealthCell()
    {
        return Cells[CurrentCellIndex];
    }

    public int GetCurrentHealthPercent()
    {
        return (CurrentCellIndex + 1) * 100 / Cells.Count;
    }

    public void Die()
    {
        OnPlayerDie?.Invoke();
        gameObject.SetActive(false);
    }

}
