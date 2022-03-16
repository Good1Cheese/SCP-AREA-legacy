using Zenject;

public class HeadBobWhileWalk : MovementHeadBob
{
    [Inject]
    private void Construct(Walk walkController)
    {
        _move = walkController;
    }
}