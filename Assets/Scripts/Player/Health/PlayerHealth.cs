using System;
using System.Collections.Generic;
using Zenject;
using UnityEngine;

[RequireComponent(typeof(CharacterBleeding))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] Sprite m_imageForEmptyCell;
    [Inject] readonly CharacterBleeding m_characterBleeding;

    public int CurrentHealthCellIndex { get; set; }
    public int LastHealthCellIndex { get; set; } = 0;
    public List<HealthCell> HealthCells { get; set; } = new List<HealthCell>();
    public Action OnPlayerDie { get; set; }
    public Action OnPlayerGetsDamage { get; set; }
    public Action OnPlayerHeals { get; set; }

    void Start()
    {
        CurrentHealthCellIndex = HealthCells.Count - 1;
    }

    public void Damage()
    {
        GetCurrentHealthCell().MakeCellEmpty();
        CurrentHealthCellIndex--;

        OnPlayerGetsDamage?.Invoke();
        if (CurrentHealthCellIndex < LastHealthCellIndex)
        {
            Die();
        }
    }

    public void Heal()
    {
        if (CurrentHealthCellIndex == HealthCells.Count - 1
            || m_characterBleeding.IsPlayerBleeding) {   return; }

        CurrentHealthCellIndex++;
        GetCurrentHealthCell().MakeCellFull();
        OnPlayerHeals.Invoke();
    }

    public HealthCell GetCurrentHealthCell()
    {
        return HealthCells[CurrentHealthCellIndex];
    }

    public HealthCell GetFirstHealthCell()
    {
        return HealthCells[HealthCells.Count - 1];
    }

    public int GetCurrentHealthPercent() => (CurrentHealthCellIndex * 100 / HealthCells.Count) + 25;

    public void Die()
    {
        OnPlayerDie?.Invoke();
    }

}
