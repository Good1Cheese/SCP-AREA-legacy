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

    public Action OnPlayerUsingMove { get; set; }
    public Action OnPlayerNotUsingMove { get; set; }
    public Action OnPlayerStartedUsing { get; set; }
    public Action OnPlayerStoppedUsing { get; set; }

    public virtual float GetMove()
    {
        if (Input.GetKeyDown(_key))
        {
            OnPlayerStartedUsing?.Invoke();
        }

        if (Input.GetKey(_key))
        {
            return Move();
        }

        OnPlayerNotUsingMove?.Invoke();
        return 0;
    }

    protected float Move()
    {
        IsMoving = true;
        OnPlayerUsingMove?.Invoke();

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
            OnPlayerStoppedUsing?.Invoke();
        }
    }
}
