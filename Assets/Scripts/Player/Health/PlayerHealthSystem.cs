using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] Sprite m_imageForEmptyCell;
    [SerializeField] Sprite m_imageForFullCell;
    int m_currentHealthCellIndex = 0;
    int m_healthCellsLength;
    public static List<PlayerHealthCell> HealthCells { get; set; } = new List<PlayerHealthCell>();

    void Start()
    {
        m_healthCellsLength = HealthCells.Count;
    }

    public void Damage()
    {
        HealthCells[m_currentHealthCellIndex].Image.sprite = m_imageForEmptyCell;
        m_currentHealthCellIndex++;
        if (m_currentHealthCellIndex >= m_healthCellsLength)
        {
            Die();
        }
    }

    public void Heal()
    {
        HealthCells[m_currentHealthCellIndex].Image.sprite = m_imageForFullCell;
        m_currentHealthCellIndex++;
    }

    void Die()
    {
      // TODO: Replace with Zenject
      //  MainLinks.Instance.SceneChanger.ChangeScene((int)SceneTransition.Scenes.RespawnScene);
    }
}


