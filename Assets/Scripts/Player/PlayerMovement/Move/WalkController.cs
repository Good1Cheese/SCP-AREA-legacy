public class WalkController : MoveController
{
    public override float GetMove()
    {
        IsMoving = true;
        return Move();
    }

    public override void StopMove()
    {
        IsMoving = false;
        UseStopped?.Invoke();
    }
}