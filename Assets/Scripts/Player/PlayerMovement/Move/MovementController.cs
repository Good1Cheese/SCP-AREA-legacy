using UnityEngine;
using Zenject;

[RequireComponent(typeof(WalkSound), typeof(RunSound), typeof(WalkController))]
public class MovementController : MonoBehaviour
{
    [SerializeField] MoveController[] m_moveControllers;

    [Inject] readonly WalkController m_walkController;

    float m_speed;

    public float SlowDownFactor { get; set; }

    public float GetPlayerSpeed()
    {
        m_speed = 0;

        GetSpeedOfMoveControllers();

        if (m_speed == 0)
        {
            m_speed = m_walkController.GetMove();
        }

        return m_speed - SlowDownFactor;
    }

    void GetSpeedOfMoveControllers()
    {
        foreach (MoveController controller in m_moveControllers)
        {
            if (m_speed != 0) { break; }

            controller.StopMove();
            float speed = controller.GetMove();

            if (speed == 0 && m_speed != 0) { continue; }

            m_speed = speed;
        }
    }
}
