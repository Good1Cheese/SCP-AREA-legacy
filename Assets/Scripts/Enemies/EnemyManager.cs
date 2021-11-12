using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyManager : MonoBehaviour
{
    [Inject(Id = "Player")] readonly Transform m_playerTransform;

    NavMeshAgent m_navMeshAgent;

    public bool WasPlayerDetected 
    { 
        set 
        {
            enabled = value;
        }
    }

    void Start()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        enabled = false;
    }

    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        m_navMeshAgent.SetDestination(m_playerTransform.position);
    }
}