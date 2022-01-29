using Zenject;

public class HeadBobWhileSlowRunWalk : MovementHeadBob
{
    [Inject]
    private void Construct(SlowWalkRunController slowWalkRunController)
    {
        _moveController = slowWalkRunController;
    }
}