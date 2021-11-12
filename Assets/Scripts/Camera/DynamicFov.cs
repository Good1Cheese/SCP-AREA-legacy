using System;
using UnityEngine;
using Zenject;

public class DynamicFov : MonoBehaviour
{
    [SerializeField] AnimationCurve m_fovForMovement;

    [SerializeField] float m_startFov;

    [Inject] readonly PlayerMovement m_playerMovement;
    [Inject] readonly MovementController m_movementController;
    [Inject] readonly WalkController m_walkController;
    [Inject] readonly RunController m_runController;
    [Inject] readonly SlowWalkController m_slowWalkController;

    Camera m_mainCamera;
    public float MoveTimeAfterStop { get; set; }

    void Awake()
    {
        m_mainCamera = Camera.main;
        m_mainCamera.fieldOfView = m_startFov;
    }

    void Start()
    {
        m_playerMovement.OnPlayerNotMoving += ResetFovAfterMoving;
        m_walkController.OnPlayerUsingMove += SetFovForMove;
        m_runController.OnPlayerUsingMove += SetFovForMove;
        m_slowWalkController.OnPlayerUsingMove += SetFovForMove;
    }

    void SetFovForMove()
    {
        m_mainCamera.fieldOfView = m_fovForMovement.Evaluate(m_movementController.MoveTime);
    }
    void ResetFovAfterMoving()
    {
        if (m_movementController.MoveTime != 0)
        {
            MoveTimeAfterStop = m_movementController.MoveTime;
        }

        MoveTimeAfterStop -= Time.deltaTime;
        m_mainCamera.fieldOfView = m_fovForMovement.Evaluate(MoveTimeAfterStop);
    }

    void OnDestroy()
    {
        m_playerMovement.OnPlayerNotMoving -= ResetFovAfterMoving;
        m_walkController.OnPlayerUsingMove -= SetFovForMove;
        m_runController.OnPlayerUsingMove -= SetFovForMove;
        m_slowWalkController.OnPlayerUsingMove -= SetFovForMove;
    }
}