using UnityEngine;
using Zenject;

public class MoveDetector : MonoBehaviour
{
    [Inject] private readonly GameObject _playerGameObject;

    protected Move _move;
    private EnemyManager _enemyManager;

    private void Start()
    {
        _enemyManager = GetComponentInParent<EnemyManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == _playerGameObject && _move.Using)
        {
            /*            _enemyManager.WasPlayerDetected = true*/
            ;
        }
    }
}
