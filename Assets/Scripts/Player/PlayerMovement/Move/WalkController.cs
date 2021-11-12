using UnityEngine;

public class WalkController : MoveController
{
    public override float GetMove()
    {
        return Move();
    }

    public override void StopMove()
    {
        IsMoving = false;
        OnPlayerStoppedUseOfMove?.Invoke();
    }
}