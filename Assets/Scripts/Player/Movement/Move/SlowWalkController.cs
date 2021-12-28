using UnityEngine;
using Zenject;

[RequireComponent(typeof(SlowWalkEffect))]
public class SlowWalkController : MoveController
{
    [Inject] private readonly PlayerMovement _playerMovement;

    private void Start()
    {
        _playerMovement.NotMoving += MoveOnNotMoving;
    }

    private void MoveOnNotMoving() => GetSpeed();

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