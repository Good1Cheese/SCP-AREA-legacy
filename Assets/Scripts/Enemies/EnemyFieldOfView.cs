using System.Collections;
using UnityEngine;
using Zenject;

public class EnemyFieldOfView : MonoBehaviour
{
    [SerializeField] LayerMask m_targetMask;
    [SerializeField] LayerMask m_obstructionMask;
    [SerializeField] float m_radius;
    [SerializeField] [Range(0, 360)] float m_angle;

    [Inject] readonly GameObject m_playerGameObject;

    void Update()
    {
        FieldOfViewCheck();
    }

    void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, m_radius, m_targetMask);

        if (rangeChecks.Length == 0) { return; }

        Transform target = rangeChecks[0].transform;
        Vector3 directionToTarget = (target.position - transform.position).normalized;

        if (Vector3.Angle(transform.forward, directionToTarget) > m_angle / 2) { return; }

        Physics.Raycast(transform.position, directionToTarget, out RaycastHit raycastHit, m_radius * 2);

        if (m_playerGameObject == raycastHit.collider.gameObject)
        {
            print("das");
        }
    }
}
