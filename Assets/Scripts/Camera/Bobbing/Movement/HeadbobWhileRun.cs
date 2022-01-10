using Zenject;

public class HeadbobWhileRun : MovementHeadbob
{
    [Inject] private readonly RunController _runController;

    protected override MoveController MoveController => _runController;
}