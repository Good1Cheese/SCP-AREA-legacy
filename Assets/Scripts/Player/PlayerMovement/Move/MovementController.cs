using UnityEngine;
using Zenject;

[RequireComponent(typeof(WalkSound), typeof(RunSound), typeof(WalkController))]
public class MovementController : MonoBehaviour
{
    [SerializeField] AnimationCurve m_movementSpeed;
    [SerializeField] MoveController[] m_moveControllers;

    [Inject] readonly WalkController m_walkController;

    float m_speed;
    MoveController m_usingMoveController;

    public AnimationCurve MovementSpeed { get => m_movementSpeed; }
    public float SlowDownFactor { get; set; }
    public float MoveTime { get; set; }

    public float GetPlayerSpeed()
    {
         GetSpeedOfMoveControllers();

        if (MoveTime > m_usingMoveController.MaxMoveTime)
        {
            MoveTime -= Time.deltaTime;
        }

        return m_speed - SlowDownFactor;
    }

    void GetSpeedOfMoveControllers()
    {
        m_speed = 0;

        foreach (MoveController controller in m_moveControllers)
        {
            if (m_speed != 0) { break; }

            controller.StopMove();
            m_speed = controller.GetMove();

            m_usingMoveController = controller;
        }

        if (m_speed == 0)
        {
            m_usingMoveController = m_walkController;
            m_speed = m_walkController.GetMove();
        }
    }
}
