using System;
using UnityEngine;
using Zenject;

public class DynamicFov : MonoBehaviour
{
    [SerializeField] private AnimationCurve _fov;

    [SerializeField] private float _startFov;

    [Inject] private readonly Camera _mainCamera;
    [Inject] private readonly PlayerMovement _playerMovement;
    [Inject] private readonly PlayerStamina _playerStamina;
    [Inject] private readonly RunController _runController;
    [Inject] private readonly SlowWalkController _slowWalkController;

    public float MoveTime { get; set; }

    private void Awake()
    {
        _mainCamera.fieldOfView = _startFov;
    }

    private void Start()
    {
        _runController.OnPlayerUsingMove += SetFovForRun;
        _slowWalkController.OnPlayerUsingMove += SetFovForSlowWalk;
        _slowWalkController.OnPlayerNotUsingMove += ResetFovAfterSlowWalk;
        _runController.OnPlayerNotUsingMove += ResetFovAfterMoving;
        _playerStamina.OnStaminaRunningOut += ResetFovAfterMoving;
        _playerMovement.OnPlayerNotMoving += ResetFovAfterMoving;
    }

    private void SetFovForRun()
    {
        SetFov(() => MoveTime >= 0.5f, 1);
    }

    private void SetFovForSlowWalk()
    {
        SetFov(() => MoveTime <= -0.5f, -1);
    }

    private void ResetFovAfterSlowWalk()
    {
        SetFov(() => MoveTime >= 0, 1);
    }

    private void ResetFovAfterMoving()
    {
        SetFov(() => MoveTime <= 0, -1);
    }

    private void SetFov(Func<bool> func, in sbyte deltaTimeMultipliyer)
    {
        if (func.Invoke()) { return; }

        MoveTime += Time.deltaTime * deltaTimeMultipliyer;
        _mainCamera.fieldOfView = _fov.Evaluate(MoveTime);
    }

    private void OnDestroy()
    {
        _runController.OnPlayerUsingMove -= SetFovForRun;
        _slowWalkController.OnPlayerUsingMove -= SetFovForSlowWalk;
        _slowWalkController.OnPlayerNotUsingMove -= ResetFovAfterSlowWalk;
        _runController.OnPlayerNotUsingMove -= ResetFovAfterMoving;
        _playerStamina.OnStaminaRunningOut -= ResetFovAfterMoving;
        _playerMovement.OnPlayerNotMoving -= ResetFovAfterMoving;
    }
}