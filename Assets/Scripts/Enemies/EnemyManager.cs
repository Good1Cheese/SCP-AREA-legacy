using UnityEngine;
using UnityEngine.AI;
using Zenject;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyManager : MonoBehaviour
{
    [Inject(Id = "Player")] private readonly Transform _playerTransform;
    private NavMeshAgent _navMeshAgent;

    public bool WasPlayerDetected
    {
        set => enabled = value;
    }

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        enabled = false;
    }

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        _navMeshAgent.SetDestination(_playerTransform.position);
    }
}