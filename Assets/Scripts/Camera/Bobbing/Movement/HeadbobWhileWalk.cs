using Zenject;

public class HeadbobWhileWalk : MovementHeadbob
{
    [Inject] private readonly WalkController _walkController;

    protected override MoveController MoveController => _walkController;
}