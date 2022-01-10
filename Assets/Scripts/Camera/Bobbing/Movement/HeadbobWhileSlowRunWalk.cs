using Zenject;

public class HeadbobWhileSlowRunWalk : MovementHeadbob
{
    [Inject] private readonly SlowWalkRunController _slowWalkRunController;

    protected override MoveController MoveController => _slowWalkRunController;
}