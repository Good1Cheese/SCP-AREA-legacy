using System;
using UnityEngine;
using Zenject;

public abstract class MoveController : MonoBehaviour
{
    [SerializeField] protected KeyCode m_key;
    [SerializeField] float m_maxMoveTime;

    [Inject] protected readonly MovementController m_movementController;

    public bool IsMoving { get; set; }
    public float MaxMoveTime { get => m_maxMoveTime; }

    public Action OnPlayerUsingMove { get; set; }
    public Action OnPlayerNotUsingMove { get; set; }
    public Action OnPlayerStartedUseOfMove { get; set; }
    public Action OnPlayerStoppedUseOfMove { get; set; }

    public virtual float GetMove()
    {
        if (Input.GetKeyDown(m_key))
        {
            OnPlayerStartedUseOfMove?.Invoke();
        }

        if (Input.GetKey(m_key))
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

        if (m_movementController.MoveTime < MaxMoveTime)
        {
            m_movementController.MoveTime += Time.deltaTime;
        }

        return m_movementController.MovementSpeed.Evaluate(m_movementController.MoveTime);
    }

    public virtual void StopMove()
    {
        if (Input.GetKeyUp(m_key))
        {
            IsMoving = false;
            OnPlayerStoppedUseOfMove?.Invoke();
        }
    }
}
