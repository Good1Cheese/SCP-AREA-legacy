using UnityEngine;
using Zenject;

public class MoveDetector : MonoBehaviour
{
    [Inject] private readonly GameObject _playerGameObject;

    protected MoveController _moveController;
    private EnemyManager _enemyManager;

    private void Start()
    {
        _enemyManager = GetComponentInParent<EnemyManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == _playerGameObject && _moveController.IsMoving)
        {
            /*            _enemyManager.WasPlayerDetected = true*/
            ;
        }
    }
}
