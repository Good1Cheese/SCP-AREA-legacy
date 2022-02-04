using System;
using UnityEngine;
using Zenject;

public abstract class MoveController : MonoBehaviour
{
    [SerializeField] protected KeyCode _moveKey;
    [SerializeField] private float _maxMoveTime;
    [SerializeField] private float _valueForFov;
    [SerializeField] private float _targetStepTime;

    protected DynamicFov _dynamicFov;
    protected MovementController _movementController;
    protected bool _leftIsLastStep;

    public float MaxMoveTime => _maxMoveTime;
    public bool IsMoving { get; set; }

    public Action Stepped { get; set; }
    public Action OnLeftStep { get; set; }
    public Action OnRightStep { get; set; }

    public Action Using { get; set; }
    public Action NotUsing { get; set; }
    public Action UseStarted { get; set; }
    public Action UseStopped { get; set; }

    [Inject]
    private void Construct(DynamicFov dynamicFov, MovementController movementController)
    {
        _dynamicFov = dynamicFov;
        _movementController = movementController;
    }

    protected float Move()
    {
        Using?.Invoke();

        if (_movementController.MoveTime < MaxMoveTime)
        {
            _movementController.MoveTime += Time.deltaTime;
        }

        return _movementController.MovementSpeed.Evaluate(_movementController.MoveTime);
    }

    public void InvokeStepInvoke()
    {
        _movementController.StepTime += Time.deltaTime;

        if (_movementController.StepTime < _targetStepTime) { return; }

        Stepped?.Invoke();

        _movementController.StepTime = 0;

        if (_leftIsLastStep)
        {
            OnLeftStep?.Invoke();
            _leftIsLastStep = false;
            return;
        }

        _leftIsLastStep = true;
        OnRightStep?.Invoke();
    }

    public void UpdateFov() => _dynamicFov.SetFov(_valueForFov);

    public virtual float GetMove()
    {
        if (Input.GetKeyDown(_moveKey))
        {
            UseStarted?.Invoke();
        }

        if (Input.GetKey(_moveKey))
        {
            IsMoving = true;
            return Move();
        }

        NotUsing?.Invoke();
        return 0;
    }

    public virtual void StopMove()
    {
        if (Input.GetKeyUp(_moveKey))
        {
            IsMoving = false;
            UseStopped?.Invoke();
        }
    }

    public virtual float GetSpeed()
    {
        StopMove();
        return GetMove();
    }
}