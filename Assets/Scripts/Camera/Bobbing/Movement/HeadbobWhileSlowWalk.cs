using Zenject;

public class HeadBobWhileSlowWalk : MovementHeadBob
{
    [Inject]
    private void Construct(SlowWalk slowWalk)
    {
        _move = slowWalk;
    }
}