using Zenject;

public class HeadBobWhileSlowRunWalk : MovementHeadBob
{
    [Inject]
    private void Construct(SlowWalkRun slowWalkRun)
    {
        _move = slowWalkRun;
    }
}