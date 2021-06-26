using System;
using System.Collections.Generic;
using Zenject;
using UnityEngine;


public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] Sprite m_imageForEmptyCell;
    [SerializeField] Sprite m_imageForFullCell;

    [Inject] SceneTransition m_sceneTransition;
    int m_currentHealthCellIndex;

    public static List<PlayerHealthCell> HealthCells { get; set; } = new List<PlayerHealthCell>();

    public Action OnPlayerDie { get; set; }
    public Action OnPlayerGetsDamage { get; set; }

    void Start()
    {
        m_currentHealthCellIndex = HealthCells.Count - 1;
    }

    public void Damage()
    {
        OnPlayerGetsDamage?.Invoke();
        HealthCells[m_currentHealthCellIndex].SetSprite(m_imageForEmptyCell);
        m_currentHealthCellIndex--;
        if (m_currentHealthCellIndex < 0)
        {
            Die();
        }
    }

    public void Bleed()
    {

    }

    public void Heal()
    {
        HealthCells[m_currentHealthCellIndex].SetSprite(m_imageForFullCell);
        m_currentHealthCellIndex++;
    }

    public int GetCurrentHealthPercent() => m_currentHealthCellIndex * 100 / HealthCells.Count;

    void Die()
    {
        OnPlayerDie?.Invoke();
        m_sceneTransition.LoadScene((int)SceneTransition.Scenes.RespawnScene);
    }
}


