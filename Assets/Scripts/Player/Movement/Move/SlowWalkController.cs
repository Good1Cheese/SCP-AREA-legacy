using UnityEngine;
using Zenject;

[RequireComponent(typeof(SlowWalkEffect))]
public class SlowWalkController : MoveController
{
    private MovementInputLink _playerMovement;

    private void Start()
    {
        _playerMovement.NotMoving += MoveOnNotMoving;
    }

    [Inject]
    private void Construct(MovementInputLink playerMovement)
    {
        _playerMovement = playerMovement;
    }

    public override float GetSpeed()
    {
        if (Input.GetKeyDown(_moveKey))
        {
            IsMoving = !IsMoving;
            InvokeStartOrEndEvents();
        }

        if (IsMoving)
        {
            return Move();
        }

        NotUsing?.Invoke();
        return 0;
    }
    private void MoveOnNotMoving() => GetSpeed();

    private void InvokeStartOrEndEvents()
    {
        if (IsMoving)
        {
            UseStarted?.Invoke();
            return;
        }

        UseStopped?.Invoke();
    }

    private void OnDestroy()
    {
        _playerMovement.NotMoving -= MoveOnNotMoving;
    }
}