using System;
using Zenject;
using UnityEngine;

[RequireComponent(typeof(CharacterBleeding))]
public class PlayerHealth : MonoBehaviour
{
    [Inject] readonly CharacterBleeding m_characterBleeding;

    public HealthCells HealthCells { get; set; } = new HealthCells();

    public Action OnPlayerDies { get; set; }
    public Action OnPlayerGetsDamage { get; set; }
    public Action OnPlayerHeals { get; set; }

    public void Damage()
    {
        HealthCell healthCell = HealthCells.GetFirstFilledCell();
        healthCell.Clear();

        if (HealthCells.GetFirstFilledCell() == null)
        {
            Die();
        }

        OnPlayerGetsDamage?.Invoke();
    }

    public void Heal()
    {
        if (HealthCells.IsCurrentCellLast()
            || m_characterBleeding.IsPlayerBleeding) { return; }

        HealthCells.GetNextCell().Fill();

        OnPlayerHeals?.Invoke();
    }

    public void Die()
    {
        OnPlayerDies?.Invoke();
        gameObject.SetActive(false);
    }

}