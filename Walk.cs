public class Walk : Move
{
    public override float CheckIfRanOrRunnigAndReturnSpeed()
    {
        IsMoving = true;
        return GetSpeed();
    }

    public override void StopMoveIfNeeded()
    {
        IsMoving = false;
        Actions.UseStopped?.Invoke();
    }
}