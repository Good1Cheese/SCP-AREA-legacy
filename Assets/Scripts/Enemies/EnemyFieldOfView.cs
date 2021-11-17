using UnityEngine;
using Zenject;

public class EnemyFieldOfView : MonoBehaviour
{
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _obstructionMask;
    [SerializeField] private float _radius;
    [SerializeField] [Range(0, 360)] private float _angle;

    [Inject] private readonly GameObject _playerGameObject;

    private void Update()
    {
        FieldOfViewCheck();
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, _radius, _targetMask);

        if (rangeChecks.Length == 0) { return; }

        Transform target = rangeChecks[0].transform;
        Vector3 directionToTarget = (target.position - transform.position).normalized;

        if (Vector3.Angle(transform.forward, directionToTarget) > _angle / 2) { return; }

        Physics.Raycast(transform.position, directionToTarget, out RaycastHit raycastHit, _radius * 2);

        if (_playerGameObject == raycastHit.collider.gameObject)
        {
            print("das");
        }
    }
}
