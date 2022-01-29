using Zenject;

public class HeadBobWhileWalk : MovementHeadBob
{
    [Inject]
    private void Construct(WalkController walkController)
    {
        _moveController = walkController;
    }
}