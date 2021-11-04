using UnityEngine;

public class WalkController : MoveController
{
    public override float GetMove()
    {
        IsMoving = true;
        OnPlayerUsingMove.Invoke();

        MoveTime += Time.deltaTime;
        return m_moveSpeed.Evaluate(MoveTime);
    }

    public override void StopMove()
    {
        ResetMoveTime();
    }
}