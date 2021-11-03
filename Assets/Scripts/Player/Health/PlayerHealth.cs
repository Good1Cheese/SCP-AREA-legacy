using System;
using Zenject;
using UnityEngine;

[RequireComponent(typeof(CharacterBleeding), typeof(PlayerDamageSound))]
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
        if (m_characterBleeding.IsBleeding) { return; }

        if (HealthCells.IsCurrentCellLast())
        {
            HealthCells[HealthCells.GetCurrentCellIndex()].Fill();
            OnPlayerHeals?.Invoke();

            return;
        }
        HealthCells.GetNextCell().Fill();

        OnPlayerHeals?.Invoke();
    }

    public void Die()
    {
        OnPlayerDies?.Invoke();
        //Destroy(gameObject);
    }

}