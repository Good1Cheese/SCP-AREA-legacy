using Zenject;

public class HeadBobWhileSlowWalk : MovementHeadBob
{
    [Inject]
    private void Construct(SlowWalkController slowWalkController)
    {
        _moveController = slowWalkController;
    }
}