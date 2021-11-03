using UnityEngine;

public class WalkController : MoveController
{
    public override float GetMove()
    {
        MoveTime += Time.deltaTime;
        return m_moveSpeed.Evaluate(MoveTime);
    }

    public override void StopMove()
    {
    }
}