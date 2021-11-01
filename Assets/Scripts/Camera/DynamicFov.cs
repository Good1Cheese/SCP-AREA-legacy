using UnityEngine;
using Zenject;

public class DynamicFov : MonoBehaviour
{
    [SerializeField] AnimationCurve m_fovForRun;

    [SerializeField] float m_startFov;
    [SerializeField] float m_fovRestoreSpeed;
    [SerializeField] float m_delayDuringFovChange;

    [Inject] readonly MovementSpeed m_movementSpeed;

    Camera m_mainCamera;

    void Awake()
    {
        m_mainCamera = Camera.main;
        m_mainCamera.fieldOfView = m_startFov;
    }

    void Start()
    {
        m_movementSpeed.OnPlayerRunning += ChangeFovByTime;
        m_movementSpeed.OnPlayerNotRunning += ChangeFovByTime;
    }

    void ChangeFovByTime()
    {
        m_mainCamera.fieldOfView = m_fovForRun.Evaluate(m_movementSpeed.RunTime);
    }
    
    void OnDestroy()
    {
        m_movementSpeed.OnPlayerRunning -= ChangeFovByTime;
        m_movementSpeed.OnPlayerStoppedRun -= ChangeFovByTime;
    }
}

