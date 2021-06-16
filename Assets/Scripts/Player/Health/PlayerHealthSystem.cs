using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] Sprite m_imageForEmptyCell;
    int m_activeCellIndex = 0;
    public static List<PlayerHealthCell> HealthCells { get; set; } = new List<PlayerHealthCell>();

    public void Damage()
    {
        if (m_activeCellIndex == HealthCells.Count - 1)
        {
            Die();
            return;
        }
        HealthCells[m_activeCellIndex].Image.sprite = m_imageForEmptyCell;
        m_activeCellIndex++;
    }

    void Die()
    {
        MainLinks.Instance.SceneChanger.ChangeScene((int)SceneTransition.Scenes.RespawnScene);
    }
}


