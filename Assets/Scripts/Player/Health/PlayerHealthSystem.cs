using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] Sprite m_imageForEmptyCell;
    int m_activeCell = 0;
    public static List<PlayerHealthCell> HealthCells { get; set; } = new List<PlayerHealthCell>();

    public void Damage()
    {
        if (m_activeCell == HealthCells.Count - 1)
        {
            Die();
            return;
        }
        HealthCells[m_activeCell].Image.sprite = m_imageForEmptyCell;
        m_activeCell++;
    }

    void Die()
    {
        MainLinks.Instance.SceneChanger.ChangeScene((int)SceneTransition.Scenes.RespawnScene);
    }
}


