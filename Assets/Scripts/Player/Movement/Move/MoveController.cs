using System;
using UnityEngine;
using Zenject;

public abstract class MoveController : MonoBehaviour
{
    [SerializeField] protected KeyCode _moveKey;
    [SerializeField] private float _maxMoveTime;
    [SerializeField] private float _valueForFov;

    [Inject] protected readonly DynamicFov _dynamicFov;
    [Inject] protected readonly MovementController _movementController;

    public bool IsMoving { get; set; }
    public float MaxMoveTime => _maxMoveTime;

    public int MoveIndex { get; set; }
    public Action Using { get; set; }
    public Action NotUsing { get; set; }
    public Action UseStarted { get; set; }
    public Action UseStopped { get; set; }

    protected float Move()
    {
        Using?.Invoke();

        if (_movementController.MoveTime < MaxMoveTime)
        {
            _movementController.MoveTime += Time.deltaTime;
        }

        return _movementController.MovementSpeed.Evaluate(_movementController.MoveTime);
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