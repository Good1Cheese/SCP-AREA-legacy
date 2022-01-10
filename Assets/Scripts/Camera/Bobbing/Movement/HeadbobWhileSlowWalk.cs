using Zenject;

public class HeadbobWhileSlowWalk : MovementHeadbob
{
    [Inject] private readonly SlowWalkController _slowWalkController;

    protected override MoveController MoveController => _slowWalkController;
}