using Zenject;

public class Walk : Move
{
    private MovementInputLink _movementInputLink;

    [Inject]
    public void Construct(MovementInputLink movementInputLink)
    {
        _movementInputLink = movementInputLink;
    }

    public override void Use()
    {
        Actions.Using?.Invoke();
        UpdateMoveTime();
        Using = true;
    }

    public void StopUse()
    {
        Actions.UseStopped?.Invoke();
        Using = false;
    }

    protected override void Subscribe()
    {
        _movementInputLink.StoppedMoving += StopUse;
    }

    protected override void Unsubscribe()
    {
        _movementInputLink.StoppedMoving -= StopUse;
    }
}