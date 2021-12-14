using System;
using UnityEngine;
using Zenject;

public abstract class MoveController : MonoBehaviour
{
    [SerializeField] protected KeyCode _key;
    [SerializeField] private float _maxMoveTime;

    [Inject] protected readonly MovementController _movementController;

    public bool IsMoving { get; set; }
    public float MaxMoveTime => _maxMoveTime;

    public Action Using { get; set; }
    public Action NotUsing { get; set; }
    public Action UseStarted { get; set; }
    public Action UseStopped { get; set; }

    public virtual float GetMove()
    {
        if (Input.GetKeyDown(_key))
        {
            UseStarted?.Invoke();
        }

        if (Input.GetKey(_key))
        {
            return Move();
        }

        NotUsing?.Invoke();
        return 0;
    }

    protected float Move()
    {
        IsMoving = true;
        Using?.Invoke();

        if (_movementController.MoveTime < MaxMoveTime)
        {
            _movementController.MoveTime += Time.deltaTime;
        }

        return _movementController.MovementSpeed.Evaluate(_movementController.MoveTime);
    }

    public virtual void StopMove()
    {
        if (Input.GetKeyUp(_key))
        {
            IsMoving = false;
            UseStopped?.Invoke();
        }
    }
}
