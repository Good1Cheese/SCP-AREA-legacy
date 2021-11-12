using System;
using UnityEngine;
using Zenject;

public class MoveDetector : MonoBehaviour
{
    [Inject] readonly GameObject m_playerGameObject;

    protected MoveController m_moveController;
    EnemyManager m_enemyManager;

    void Start()
    {
        m_enemyManager = GetComponentInParent<EnemyManager>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == m_playerGameObject && m_moveController.IsMoving)
        {
/*            m_enemyManager.WasPlayerDetected = true*/;
        }
    }
}
