using UnityEngine;
using Zenject;

public class DynamicFov : MonoBehaviour
{
    [SerializeField] AnimationCurve m_fovForRun;
    [SerializeField] AnimationCurve m_fovForSlowWalk;

    [SerializeField] float m_startFov;
    [SerializeField] float m_fovRestoreSpeed;
    [SerializeField] float m_delayDuringFovChange;
        
    [Inject] readonly MovementController m_movementController;
    [Inject] readonly RunController m_runController;
    [Inject] readonly SlowWalkController m_slowWalkController;

    Camera m_mainCamera;
    bool m_wasRunLastMove;

    void Awake()
    {
        m_mainCamera = Camera.main;
        m_mainCamera.fieldOfView = m_startFov;
    }

    void Start()
    {
        m_movementController.OnPlayerWalking += ChangeFovWhileWalk;
        m_runController.OnPlayerUsingMove += ChangeFovWhileRun;
        m_slowWalkController.OnPlayerUsingMove += ChangeFovWhileSlowWalk;
    }

    public void ChangeFovWhileWalk()
    {
        if (m_wasRunLastMove)
        {
            ChangeFovWhileRun();
            return;
        }
        ChangeFovWhileSlowWalk();
    }

    public void ChangeFovWhileRun()
    {
        m_wasRunLastMove = true;
        m_mainCamera.fieldOfView = m_fovForRun.Evaluate(m_runController.MoveTime);
    }

    public void ChangeFovWhileSlowWalk()
    {
        m_wasRunLastMove = false;
        m_mainCamera.fieldOfView = m_fovForSlowWalk.Evaluate(m_slowWalkController.MoveTime);
    }

    void OnDestroy()
    {
        m_movementController.OnPlayerWalking -= ChangeFovWhileWalk;
        m_runController.OnPlayerUsingMove -= ChangeFovWhileRun;
        m_slowWalkController.OnPlayerUsingMove -= ChangeFovWhileSlowWalk;
        
    }
}