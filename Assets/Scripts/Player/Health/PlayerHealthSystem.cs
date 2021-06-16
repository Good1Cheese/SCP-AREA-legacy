using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] Sprite m_imageForEmptyCell;
    public static List<PlayerHealthCell> HealthCells { get; set; } = new List<PlayerHealthCell>();

    public void Damage()
    {
        {
            Die();
            return;
        }
    }

    void Die()
    {
        MainLinks.Instance.SceneChanger.ChangeScene((int)SceneTransition.Scenes.RespawnScene);
    }
}


