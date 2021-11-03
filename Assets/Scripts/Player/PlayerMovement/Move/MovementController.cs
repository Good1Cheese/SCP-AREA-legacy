using System;
using UnityEngine;

[RequireComponent(typeof(WalkSound), typeof(RunSound), typeof(WalkController))]
public class MovementController : MonoBehaviour
{
    [SerializeField] MoveController[] m_moveControllers;
    [SerializeField] WalkController m_walkController;

    public Action OnPlayerWalking { get; set; }
    public float SlowDownFactor { get; set; }

    public float GetPlayerSpeed()
    {
        float m_speed = 0;

        foreach (MoveController controller in m_moveControllers)
        {
            controller.StopMove();
            float speed = controller.GetMove();

            if (speed == 0 && m_speed != 0) { continue; }

            m_speed = speed;
        }

        if (m_speed == 0)
        {
            m_speed = m_walkController.GetMove();
            OnPlayerWalking.Invoke();
        }

        return m_speed - SlowDownFactor;
    }

}
